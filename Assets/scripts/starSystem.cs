using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using Debug = UnityEngine.Debug;


public class starSystem
{
    public Star[] s; 
    int maxStarNum = 20;
    bool[] validMap; // Check if objects in s array are valid
    private int validCount = 0; // Valid star number
    private double G = 6.67408E-11;
    private double minDistanceToDistroy = 7E-1;
    private bool systemRunning = true;

    
    
    public int numOfForwardMethodInstead = 10; // Number of Forward Method used instead when other updata methods cannot perform
    public int gearsMethodOrderNum = 2; // Order of Gear's Method
    private int updateNumBeforeGearsMethod = 0; // When using n'th order Gear's Method,
                                                // the first n-1 times of update need to apply TrapezoidalMethod
    private VEC alpha; // the coefficients of Gear's Method
    private VEC[,] old_velAll;
    private VEC[,] old_posAll;

    private int printCount = 100;
    private int printNum = 100;
    private int nowPrintCount = 0;

    private System.IO.StreamWriter file;


    public starSystem()
    {
        validMap = new bool[maxStarNum];
        s = new Star[maxStarNum];
        for(int i = 0; i < maxStarNum; i++)
        {
            s[i] = new Star();
        }
        alpha = new VEC(gearsMethodOrderNum + 1);
    }

    /*
     * Add star to system
     */
    public void AddStar(Star a)
    {
        int index = -1;
        bool flag = false;
        for (index = 0; index < maxStarNum; index++)
        {
            if (validMap[index] == false)
            {
                flag = true;
                break;
            }
            alpha = GetAlphaOfGearsMethod(gearsMethodOrderNum);
        }
        if(flag == false)
        {
            Debug.Log("Star system full!");
        }
        else
        {
            s[index] = a;
            validMap[index] = true;
            validCount++;
            updateNumBeforeGearsMethod = 0;
            old_velAll = new VEC[gearsMethodOrderNum-1, validCount];
            old_posAll = new VEC[gearsMethodOrderNum-1, validCount];
        }
    }

    /*
     * Destroy star from the system
     * If input param validCondition is set to -1, the orbit track will be removed; if set to 0, the orbit track will remain.
     */
    public void DestroyStar(Star a, int validCondition)
    {
        int index = -1;
        bool flag = false;
        for (index = 0; index < maxStarNum; index++)
        {
            if (validMap[index] == true)
            {
                if(s[index] == a)
                {
                    flag = true;
                    s[index].valid = validCondition;
                    s[index] = new Star();
                    validMap[index] = false;
                    validCount--;
                    updateNumBeforeGearsMethod = 0;
                    old_velAll = new VEC[gearsMethodOrderNum - 1, validCount];
                    old_posAll = new VEC[gearsMethodOrderNum - 1, validCount];
                    return;
                }
            }
        }
        if (flag == false)
        {
            Debug.Log("ERROR! Cannot find star to destroy!");
        }
    }

    public void StartSystem()
    {
        systemRunning = true;
    }

    public void PauseSystem()
    {
        systemRunning = false;
    }

    /*
     * Destroy all stars from the system
     * If input param validCondition is set to -1, the orbit track will be removed; if set to 0, the orbit track will remain.
     */
    public void ClearAllStars(int validCondition)
    {
        for (int i = 0; i < maxStarNum; i++)
        {
            if(validMap[i] == true)
            {
                DestroyStar(s[i], validCondition);
            }
        }
    }

    /*
     * Update system with time interval h
     */
    public void UpdateSystem(double h)
    {
        if (systemRunning == true)
        {
            //if (printNum < 0)
            //{
            //    printNum = printCount;
            //    nowPrintCount++;
            //    Debug.Log(nowPrintCount.ToString());
            //    string lines = distance(s[0].pos, new VEC(3)).ToString();
            //    using (StreamWriter sw = File.AppendText("./test.txt"))
            //    {
            //        sw.WriteLine(lines);
            //    }
            //}
            //printNum--;

            //UpdateForwardMethod(h);
            //UpdateBackwardMethod(h);
            UpdateTrapezoidalMethod(h);
            //UpdateGearsMethod(h);
        }
    }

    /*
     * void UpdateGearsMethod(double h)
     * 
     * Gear's method on velocity 
     * Trapezoidal method on position
     */
    public void UpdateGearsMethod(double h)
    {
        
        if (validCount <= 0)
        {
            return;
        }
        
        int[] validIndex = new int[validCount];
        int indexTmp = 0;

        for (int i = 0; i < maxStarNum; i++)
        {
            if (validMap[i] == true)
            {
                validIndex[indexTmp] = i;
                indexTmp++;
            }
        }

        if (updateNumBeforeGearsMethod < gearsMethodOrderNum - 1)
        {
            for (int i = 0; i < validCount; i++)
            {
                old_velAll[updateNumBeforeGearsMethod, i] = new VEC(s[validIndex[i]].vel);
                old_posAll[updateNumBeforeGearsMethod, i] = new VEC(s[validIndex[i]].pos);
            }
            UpdateTrapezoidalMethod(h);
            updateNumBeforeGearsMethod++; 
            return;
        }

        VEC[] velAll = new VEC[validCount];
        VEC[] new_velAll = new VEC[validCount];

        VEC[] posAll = new VEC[validCount];
        VEC[] new_posAll = new VEC[validCount];

        VEC massAll = new VEC(validCount);

        for (int i = 0; i < validCount; i++)
        {
            velAll[i] = new VEC(s[validIndex[i]].vel);
            new_velAll[i] = new VEC(s[validIndex[i]].vel);

            posAll[i] = new VEC(s[validIndex[i]].pos);
            new_posAll[i] = new VEC(s[validIndex[i]].pos);

            massAll[i] = s[validIndex[i]].mass;
        }

        double e_threshold = 1E-9;
        double err = 1.0 + e_threshold;
        int maxIterNum = 100;

        while (err > e_threshold & maxIterNum > 0)
        {
            VECV3 F = new VECV3(2 * validCount);
            for (int i = 0; i < validCount; i++)
            {
                VEC a = new VEC(3);
                for (int j = 0; j < validCount; j++)
                {
                    if (i != j)
                    {
                        a += alpha[gearsMethodOrderNum] * VEC.Normalize(new_posAll[j] - new_posAll[i]) * massAll[j] / DistancePow2(new_posAll[i], new_posAll[j]);
                    }
                }
                F[i] = G * a;

                a = alpha[0] * velAll[i] - new_velAll[i];
                for (int j = 0; j < gearsMethodOrderNum-1; j++)
                {
                    a = a + old_velAll[j, i] * alpha[j+1];
                }
                F[i] = F[i] + a / h;
            }

            for (int i = validCount; i < 2 * validCount; i++)
            {
                F[i] = (new_posAll[i - validCount] - posAll[i - validCount]) / h - 0.5 * (new_velAll[i - validCount] + velAll[i - validCount]);
            }

            MATM3 JF = new MATM3(2 * validCount);
            MAT tmp;
            for (int i = 0; i < validCount; i++)
            {
                tmp = new MAT(3);
                for (int k = 0; k < 3; k++)
                {
                    tmp[k][k] = -1.0 / h;
                }
                JF[i][i] = tmp;
            }
            for (int i = 0; i < validCount; i++)
            {
                for (int j_inJF = validCount; j_inJF < 2 * validCount; j_inJF++)
                {
                    int j = j_inJF - validCount;
                    tmp = new MAT(3);
                    if (i != j)
                    {
                        for (int a = 0; a < 3; a++)
                        {
                            for (int b = 0; b < 3; b++)
                            {
                                if (a == b)
                                {
                                    tmp[a][b] = massAll[j] * G *
                                        (-3 * Math.Pow(DistancePow2(new_posAll[j], new_posAll[i]), -2.5) * (new_posAll[j][a] - new_posAll[i][a]) * new_posAll[j][a]
                                        + Math.Pow(DistancePow2(new_posAll[j], new_posAll[i]), -1.5));
                                }
                                else
                                {
                                    tmp[a][b] = massAll[j] * G * -3 * Math.Pow(DistancePow2(new_posAll[j], new_posAll[i]), -2.5) * (new_posAll[j][a] - new_posAll[i][a]) * (new_posAll[j][b] - new_posAll[i][b]);
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int a = 0; a < 3; a++)
                        {
                            for (int b = 0; b < 3; b++)
                            {
                                if (a == b)
                                {
                                    double sInTmp = 0;
                                    for (int k = 0; k < validCount; k++)
                                    {
                                        if (k != j)
                                        {
                                            sInTmp += massAll[k] *
                                                (3 * Math.Pow(DistancePow2(new_posAll[k], new_posAll[j]), -2.5) * (new_posAll[k][a] - new_posAll[j][a]) * (new_posAll[k][a] - new_posAll[i][a])
                                                - Math.Pow(DistancePow2(new_posAll[k], new_posAll[j]), -1.5));
                                        }
                                    }
                                    tmp[a][a] = G * sInTmp;
                                }
                                else
                                {
                                    double sInTmp = 0;
                                    for (int k = 0; k < validCount; k++)
                                    {
                                        if (k != j)
                                        {
                                            sInTmp += massAll[k] *
                                                (3 * Math.Pow(DistancePow2(new_posAll[k], new_posAll[j]), -2.5) * (new_posAll[k][a] - new_posAll[j][a]) * (new_posAll[k][b] * new_posAll[i][b]));
                                        }
                                    }
                                    tmp[a][a] = G * sInTmp;
                                }
                            }
                        }
                    }
                    JF[i][j_inJF] = alpha[gearsMethodOrderNum] * tmp;
                }
            }

            for (int i = validCount; i < 2 * validCount; i++)
            {
                for (int j = 0; j < validCount; j++)
                {
                    if (i - validCount == j)
                    {
                        tmp = new MAT(3);
                        for (int k = 0; k < 3; k++)
                        {
                            tmp[k][k] = -0.5;
                        }
                        JF[i][j] = tmp;
                    }
                }
            }

            for (int i = validCount; i < 2 * validCount; i++)
            {
                for (int j = validCount; j < 2 * validCount; j++)
                {
                    if (i == j)
                    {
                        tmp = new MAT(3);
                        for (int k = 0; k < 3; k++)
                        {
                            tmp[k][k] = 1.0 / h;
                        }
                        JF[i][j] = tmp;
                    }
                }
            }
            VECV3 delta = numericalFunctions.LU_Solve(JF, -F);
            for (int i = 0; i < validCount; i++)
            {
                new_velAll[i] += delta[i];
                new_posAll[i] += delta[i + validCount];
            }
            err = VECV3.L2_norm_special(F);

            maxIterNum--;
            if (maxIterNum == 0)
            {
                Debug.Log("Max iter number reached. Err: " + err + "  Using Forward method instead in this period.");
                int prev_validCount = validCount;
                for (int i = 0; i < numOfForwardMethodInstead; i++)
                {
                    UpdateForwardMethod(h / numOfForwardMethodInstead);
                }
                
                if(prev_validCount == validCount)
                {
                    for (int i = 0; i < validCount; i++)
                    {
                        for (int j = gearsMethodOrderNum - 2; j > 0; j--)
                        {
                            old_velAll[j, i] = new VEC(old_velAll[j - 1, i]);
                            old_posAll[j, i] = new VEC(old_posAll[j - 1, i]);
                        }
                        old_velAll[0, i] = new VEC(velAll[i]);
                        old_posAll[0, i] = new VEC(posAll[i]);
                    }
                }
                return;
            }
        }
        if (maxIterNum != 0)
        {
            for (int i = 0; i < validCount; i++)
            {
                for(int j = gearsMethodOrderNum - 2; j > 0; j--)
                {
                    old_velAll[j, i] = new VEC(old_velAll[j - 1, i]);
                    old_posAll[j, i] = new VEC(old_posAll[j - 1, i]);
                }
                old_velAll[0, i] = new VEC(velAll[i]);
                old_posAll[0, i] = new VEC(posAll[i]);

                s[validIndex[i]].vel = new_velAll[i];
                s[validIndex[i]].pos = new_posAll[i];
            }
        }

        for (int i = 0; i < validCount; i++)
        {
            for (int j = i + 1; j < validCount; j++)
            {
                if (DistanceBetweenStarsPow2(s[validIndex[i]], s[validIndex[j]]) < minDistanceToDistroy)
                {
                    s[validIndex[j]].pos += (s[validIndex[i]].pos - s[validIndex[j]].pos) * s[validIndex[i]].mass / (s[validIndex[j]].mass + s[validIndex[i]].mass);
                    s[validIndex[j]].vel = (s[validIndex[j]].vel * s[validIndex[j]].mass + s[validIndex[i]].vel * s[validIndex[i]].mass) / (s[validIndex[j]].mass + s[validIndex[i]].mass);
                    s[validIndex[j]].mass += s[validIndex[i]].mass;
                    DestroyStar(s[validIndex[i]], 0);
                    continue;
                }
            }
        }
    }

    /*
     * void UpdateTrapezoidalMethod(double h)
     * 
     * Trapezoidal method on velocity 
     * Trapezoidal method on position
     *
     * Is the most accurate method 
     */
    public void UpdateTrapezoidalMethod(double h)
    {
        if (validCount <= 0)
        {
            return;
        }

        // Map valid star index into validIndex
        int[] validIndex = new int[validCount];
        int indexTmp = 0;
        for (int i = 0; i < maxStarNum; i++)
        {
            if (validMap[i] == true)
            {
                validIndex[indexTmp] = i;
                indexTmp++;
            }
        }

        // copy velocity and position of all stars into arrays
        VEC[] velAll = new VEC[validCount];
        VEC[] new_velAll = new VEC[validCount];

        VEC[] posAll = new VEC[validCount];
        VEC[] new_posAll = new VEC[validCount];

        VEC massAll = new VEC(validCount);
        for (int i = 0; i < validCount; i++)
        {
            velAll[i] = new VEC(s[validIndex[i]].vel);
            new_velAll[i] = new VEC(s[validIndex[i]].vel);

            posAll[i] = new VEC(s[validIndex[i]].pos);
            new_posAll[i] = new VEC(s[validIndex[i]].pos);

            massAll[i] = s[validIndex[i]].mass;
        }

        double e_threshold = 1E-9; // the error threshold of performing Newton’s Method
        double err = 1.0 + e_threshold; 
        int maxIterNum = 100; // Newton’s Method might not converge, we need a max iteration number to terminate.

        while (err > e_threshold & maxIterNum > 0)
        {
            VECV3 F = new VECV3(2 * validCount);

            for (int i = 0; i < validCount; i++)
            {
                VEC a = new VEC(3);
                for (int j = 0; j < validCount; j++)
                {
                    if (i != j)
                    {
                        a += 0.5 * VEC.Normalize(new_posAll[j] - new_posAll[i]) * massAll[j] / DistancePow2(new_posAll[i], new_posAll[j])
                            + 0.5 * VEC.Normalize(posAll[j] - posAll[i]) * massAll[j] / DistancePow2(posAll[i], posAll[j]);
                    }
                }
                F[i] = G * a - (new_velAll[i] - velAll[i]) / h;
            }

            for (int i = validCount; i < 2 * validCount; i++)
            {
                F[i] = (new_posAll[i - validCount] - posAll[i - validCount]) / h - 0.5 * (new_velAll[i - validCount] + velAll[i - validCount]);
            }

            MATM3 JF = new MATM3(2 * validCount);
            MAT tmp;
            for (int i = 0; i < validCount; i++)
            {
                tmp = new MAT(3);
                for (int k = 0; k < 3; k++)
                {
                    tmp[k][k] = -1.0 / h;
                }
                JF[i][i] = tmp;
            }
            for (int i = 0; i < validCount; i++)
            {
                for (int j_inJF = validCount; j_inJF < 2 * validCount; j_inJF++)
                {
                    int j = j_inJF - validCount;
                    tmp = new MAT(3);
                    if (i != j)
                    {
                        for (int a = 0; a < 3; a++)
                        {
                            for (int b = 0; b < 3; b++)
                            {
                                if (a == b)
                                {
                                    tmp[a][b] = massAll[j] * G *
                                        (-3 * Math.Pow(DistancePow2(new_posAll[j], new_posAll[i]), -2.5) * (new_posAll[j][a] - new_posAll[i][a]) * new_posAll[j][a]
                                        + Math.Pow(DistancePow2(new_posAll[j], new_posAll[i]), -1.5));
                                }
                                else
                                {
                                    tmp[a][b] = massAll[j] * G * -3 * Math.Pow(DistancePow2(new_posAll[j], new_posAll[i]), -2.5) * (new_posAll[j][a] - new_posAll[i][a]) * (new_posAll[j][b] - new_posAll[i][b]);
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int a = 0; a < 3; a++)
                        {
                            for (int b = 0; b < 3; b++)
                            {
                                if (a == b)
                                {
                                    double sInTmp = 0;
                                    for (int k = 0; k < validCount; k++)
                                    {
                                        if (k != j)
                                        {
                                            sInTmp += massAll[k] *
                                                (3 * Math.Pow(DistancePow2(new_posAll[k], new_posAll[j]), -2.5) * (new_posAll[k][a] - new_posAll[j][a]) * (new_posAll[k][a] - new_posAll[i][a])
                                                - Math.Pow(DistancePow2(new_posAll[k], new_posAll[j]), -1.5));
                                        }
                                    }
                                    tmp[a][a] = G * sInTmp;
                                }
                                else
                                {
                                    double sInTmp = 0;
                                    for (int k = 0; k < validCount; k++)
                                    {
                                        if (k != j)
                                        {
                                            sInTmp += massAll[k] *
                                                (3 * Math.Pow(DistancePow2(new_posAll[k], new_posAll[j]), -2.5) * (new_posAll[k][a] - new_posAll[j][a]) * (new_posAll[k][b] * new_posAll[i][b]));
                                        }
                                    }
                                    tmp[a][a] = G * sInTmp;
                                }
                            }
                        }
                    }
                    JF[i][j_inJF] = 0.5 * tmp;
                }
            }

            for (int i = validCount; i < 2 * validCount; i++)
            {
                for (int j = 0; j < validCount; j++)
                {
                    if (i - validCount == j)
                    {
                        tmp = new MAT(3);
                        for (int k = 0; k < 3; k++)
                        {
                            tmp[k][k] = -0.5;
                        }
                        JF[i][j] = tmp;
                    }
                }
            }

            for (int i = validCount; i < 2 * validCount; i++)
            {
                for (int j = validCount; j < 2 * validCount; j++)
                {
                    if (i == j)
                    {
                        tmp = new MAT(3);
                        for (int k = 0; k < 3; k++)
                        {
                            tmp[k][k] = 1.0 / h;
                        }
                        JF[i][j] = tmp;
                    }
                }
            }

            VECV3 delta = numericalFunctions.LU_Solve(JF, -F);
            
            for (int i = 0; i < validCount; i++)
            {
                new_velAll[i] += delta[i];
                new_posAll[i] += delta[i + validCount];
            }
            err = VECV3.L2_norm_special(F);

            maxIterNum--;
            if (maxIterNum == 0)
            {
                Debug.Log("Max iter number reached. Err: " + err + "  Using Forward method instead in this period.");
                for(int i = 0; i < numOfForwardMethodInstead; i++)
                {
                    UpdateForwardMethod(h / numOfForwardMethodInstead);
                }
                return;
            }
        }

        // Apply new position and velocity to Star objects
        for (int i = 0; i < validCount; i++)
        {
            if (maxIterNum != 0)
            {
                s[validIndex[i]].vel = new_velAll[i];
                s[validIndex[i]].pos = new_posAll[i];
            }
        }

        // Check if any pair of stars have distance smaller than minDistanceToDistroy.
        // If so, merge two stars.
        for (int i = 0; i < validCount; i++)
        {
            for (int j = i + 1; j < validCount; j++)
            {
                if (DistanceBetweenStarsPow2(s[validIndex[i]], s[validIndex[j]]) < minDistanceToDistroy)
                {
                    s[validIndex[j]].pos += (s[validIndex[i]].pos - s[validIndex[j]].pos) * s[validIndex[i]].mass / (s[validIndex[j]].mass + s[validIndex[i]].mass);
                    s[validIndex[j]].vel = (s[validIndex[j]].vel * s[validIndex[j]].mass + s[validIndex[i]].vel * s[validIndex[i]].mass) / (s[validIndex[j]].mass + s[validIndex[i]].mass);
                    s[validIndex[j]].mass += s[validIndex[i]].mass;
                    DestroyStar(s[validIndex[i]], 0);
                    continue;
                }
            }
        }
    }

    /*
     * void UpdateBackwardMethod(double h)
     * 
     * Backward method on velocity 
     * Trapezoidal method on position
     */
    public void UpdateBackwardMethod(double h)
    {
        if (validCount <= 0)
        {
            return;
        }
        int[] validIndex = new int[validCount];
        int indexTmp = 0;
        
        for (int i = 0; i < maxStarNum; i++)
        {
            if (validMap[i] == true)
            {
                validIndex[indexTmp] = i;
                indexTmp++;
            }
        }

        VEC[] velAll = new VEC[validCount];
        VEC[] new_velAll = new VEC[validCount];

        VEC[] posAll = new VEC[validCount];
        VEC[] new_posAll = new VEC[validCount];

        VEC massAll = new VEC(validCount);
        for (int i = 0; i < validCount; i++)
        {
            velAll[i] = new VEC(s[validIndex[i]].vel);
            new_velAll[i] = new VEC(s[validIndex[i]].vel);

            posAll[i] = new VEC(s[validIndex[i]].pos);
            new_posAll[i] = new VEC(s[validIndex[i]].pos);

            massAll[i] = s[validIndex[i]].mass;
        }

        double e_threshold = 1E-7;
        double err = 1.0 + e_threshold;
        int maxIterNum = 100;

        while(err > e_threshold & maxIterNum > 0)
        {
            VECV3 F = new VECV3(2 * validCount);
            for (int i = 0; i < validCount; i++)
            {
                VEC a = new VEC(3);
                for (int j = 0; j < validCount; j++)
                {
                    if (i != j)
                    {
                        a += VEC.Normalize(new_posAll[j] - new_posAll[i]) * massAll[j] / DistancePow2(new_posAll[i], new_posAll[j]);
                    }
                }
                F[i] = G * a - (new_velAll[i] - velAll[i]) / h;
            }
            for (int i = validCount; i < 2 * validCount; i++)
            {
                F[i] = (new_posAll[i - validCount] - posAll[i - validCount]) / h - 0.5 * (new_velAll[i - validCount] + velAll[i - validCount]);
            }

            MATM3 JF = new MATM3(2 * validCount);
            MAT tmp;
            for (int i = 0; i < validCount; i++)
            {
                tmp = new MAT(3);
                for (int k = 0; k < 3; k++)
                {
                    tmp[k][k] = -1.0 / h;
                }
                JF[i][i] = tmp;
            }
            for (int i = 0; i < validCount; i++)
            {
                for (int j_inJF = validCount; j_inJF < 2 * validCount; j_inJF++)
                {
                    int j = j_inJF - validCount;
                    tmp = new MAT(3);
                    if (i != j)
                    {
                        for (int a = 0; a < 3; a++)
                        {
                            for (int b = 0; b < 3; b++)
                            {
                                if (a == b)
                                {
                                    tmp[a][b] = massAll[j] * G *
                                        (-3 * Math.Pow(DistancePow2(new_posAll[j], new_posAll[i]), -2.5) * (new_posAll[j][a] - new_posAll[i][a]) * new_posAll[j][a]
                                        + Math.Pow(DistancePow2(new_posAll[j], new_posAll[i]), -1.5));
                                }
                                else
                                {
                                    tmp[a][b] = massAll[j] * G * -3 * Math.Pow(DistancePow2(new_posAll[j], new_posAll[i]), -2.5) * (new_posAll[j][a] - new_posAll[i][a]) * (new_posAll[j][b] - new_posAll[i][b]);
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int a = 0; a < 3; a++)
                        {
                            for (int b = 0; b < 3; b++)
                            {
                                if (a == b)
                                {
                                    double sInTmp = 0;
                                    for (int k = 0; k < validCount; k++)
                                    {
                                        if (k != j)
                                        {
                                            sInTmp += massAll[k] *
                                                (3 * Math.Pow(DistancePow2(new_posAll[k], new_posAll[j]), -2.5) * (new_posAll[k][a] - new_posAll[j][a]) * (new_posAll[k][a] - new_posAll[i][a])
                                                - Math.Pow(DistancePow2(new_posAll[k], new_posAll[j]), -1.5));
                                        }
                                    }
                                    tmp[a][a] = G * sInTmp;
                                }
                                else
                                {
                                    double sInTmp = 0;
                                    for (int k = 0; k < validCount; k++)
                                    {
                                        if (k != j)
                                        {
                                            sInTmp += massAll[k] *
                                                (3 * Math.Pow(DistancePow2(new_posAll[k], new_posAll[j]), -2.5) * (new_posAll[k][a] - new_posAll[j][a]) * (new_posAll[k][b] * new_posAll[i][b]));
                                        }
                                    }
                                    tmp[a][a] = G * sInTmp;
                                }
                            }
                        }
                    }
                    JF[i][j_inJF] = tmp;
                }
            }

            for (int i = validCount; i < 2 * validCount; i++)
            {
                for (int j = 0; j < validCount; j++)
                {
                    if (i - validCount == j)
                    {
                        tmp = new MAT(3);
                        for (int k = 0; k < 3; k++)
                        {
                            tmp[k][k] = -0.5;
                        }
                        JF[i][j] = tmp;
                    }
                }
            }

            for (int i = validCount; i < 2 * validCount; i++)
            {
                for (int j = validCount; j < 2 * validCount; j++)
                {
                    if (i == j)
                    {
                        tmp = new MAT(3);
                        for (int k = 0; k < 3; k++)
                        {
                            tmp[k][k] = 1.0 / h;
                        }
                        JF[i][j] = tmp;
                    }
                }
            }
            VECV3 delta = numericalFunctions.LU_Solve(JF, -F);
            for (int i = 0; i < validCount; i++)
            {
                new_velAll[i] += delta[i];
                new_posAll[i] += delta[i + validCount];
            }
            err = VECV3.L2_norm_special(F);             

            maxIterNum--;
            if (maxIterNum == 0)
            {
                Debug.Log("Max iter number reached. Err: " + err + "  Using Forward method instead in this period.");
                UpdateForwardMethod(h / 2.0);
                UpdateForwardMethod(h / 2.0);
                return;
            }
        }
        for (int i = 0; i < validCount; i++)
        {
            if (maxIterNum != 0)
            {
                s[validIndex[i]].vel = new_velAll[i];
                s[validIndex[i]].pos = new_posAll[i];
            }
        }
        
        for (int i = 0; i < validCount; i++)
        {
            for (int j = i + 1; j < validCount; j++)
            {
                if (DistanceBetweenStarsPow2(s[validIndex[i]], s[validIndex[j]]) < minDistanceToDistroy)
                {
                    s[validIndex[j]].pos += (s[validIndex[i]].pos - s[validIndex[j]].pos) * s[validIndex[i]].mass / (s[validIndex[j]].mass + s[validIndex[i]].mass);
                    s[validIndex[j]].vel = (s[validIndex[j]].vel * s[validIndex[j]].mass + s[validIndex[i]].vel * s[validIndex[i]].mass) / (s[validIndex[j]].mass + s[validIndex[i]].mass);
                    s[validIndex[j]].mass += s[validIndex[i]].mass;
                    DestroyStar(s[validIndex[i]], 0);
                    continue;
                }
            }
        }
    }

    /*
     * Simplest way to update the system but have the most error
     */
    public void UpdateForwardMethod(double h)
    {
        int[] validIndex = new int[validCount];
        int indexTmp = 0;
        VEC[] newvels = new VEC[validCount];
        for (int i = 0; i < validCount; i++)
        {
            newvels[i] = new VEC(3);
        }
        for (int i = 0; i < maxStarNum; i++)
        {
            if (validMap[i] == true)
            {
                validIndex[indexTmp] = i;
                indexTmp++;
            }
        }

        double[] arr = new double[2] { -1, -1 };
        VEC toDestroy = new VEC(2, arr);
        for (int i = 0; i < validCount; i++)
        {
            VEC updateV;
            updateV = new VEC(3);
            for (int j = 0; j < validCount; j++)
            {
                if (j != i)
                {
                    updateV += VEC.Normalize(s[validIndex[j]].pos - s[validIndex[i]].pos) * s[validIndex[j]].mass / DistanceBetweenStarsPow2(s[validIndex[i]], s[validIndex[j]]);
                }
            }
            newvels[i] = s[validIndex[i]].vel + h * G * updateV;
        }

        for (int i = 0; i < validCount; i++)
        {
            s[validIndex[i]].pos += h * s[validIndex[i]].vel;
        }

        for (int i = 0; i < validCount; i++)
        {
            s[validIndex[i]].vel = newvels[i];
        }

        for (int i = 0; i < validCount; i++)
        {
            for(int j = i + 1; j < validCount; j++)
            {
                if (DistanceBetweenStarsPow2(s[validIndex[i]], s[validIndex[j]]) < minDistanceToDistroy)
                {
                    s[validIndex[j]].pos += (s[validIndex[i]].pos - s[validIndex[j]].pos) * s[validIndex[i]].mass / (s[validIndex[j]].mass + s[validIndex[i]].mass);
                    s[validIndex[j]].vel = (s[validIndex[j]].vel * s[validIndex[j]].mass + s[validIndex[i]].vel * s[validIndex[i]].mass) / (s[validIndex[j]].mass + s[validIndex[i]].mass);
                    s[validIndex[j]].mass += s[validIndex[i]].mass;
                    DestroyStar(s[validIndex[i]], 0);
                    continue;
                }
            }
        }
    }

    /*
     * Return distance pow 2 of two input Star objects
     */
    private double DistanceBetweenStarsPow2(Star a, Star b)
    {
        double dist = 0;
        for(int i = 0; i < 3; i++)
        {
            dist += Math.Pow(a.pos[i] - b.pos[i], 2);
        }
        return dist;
    }

    /*
     * Return distance pow 2 between two input vectors
     */
    private double DistancePow2(VEC a, VEC b)
    {
        double dist = 0;
        for (int i = 0; i < 3; i++)
        {
            dist += Math.Pow(a[i] - b[i], 2);
        }
        return dist;
    }

    /*
     * Return distance of two input Star objects
     */
    private double DistanceBetweenStars(Star a, Star b)
    {
        double dist = 0;
        for (int i = 0; i < 3; i++)
        {
            dist += Math.Pow(a.pos[i] - b.pos[i], 2);
        }
        return Math.Sqrt(dist);
    }

    /*
     * Return distance between two input vectors
     */
    private double Distance(VEC a, VEC b)
    {
        double dist = 0;
        for (int i = 0; i < 3; i++)
        {
            dist += Math.Pow(a[i] - b[i], 2);
        }
        return Math.Sqrt(dist);
    }

    public double centralMass()
    {
        return 0.0;
    }

    /*
     * Return vector of coefficients of Gear's Method with certain order number
     */
    private VEC GetAlphaOfGearsMethod(int order)
    {
        MAT A = new MAT(order + 1);
        VEC b = new VEC(order + 1);

        for(int i = 0; i < order + 1; i++)
        {
            b[i] = 1;
            A[i][order] = i;
        }
        
        for(int k = 0; k <= order; k++)
        {
            for(int j = 0; j < order; j++)
            {
                A[k][j] = Math.Pow(-j, k);
            }
        }
        VEC v = numericalFunctions.LU_Solve(A, b);
        return v;
    }

    public static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
    {
        using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
        {
            var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            binaryFormatter.Serialize(stream, objectToWrite);
        }
    }
}

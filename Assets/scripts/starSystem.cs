using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class starSystem
{
    public Star[] s;
    int maxStarNum = 10;
    bool[] validMap;
    private int validCount = 0;
    private double G = 6.67430E-11;

    public starSystem()
    {
        validMap = new bool[maxStarNum];
        s = new Star[maxStarNum];
        for(int i = 0; i < maxStarNum; i++)
        {
            s[i] = new Star();
        }

        
    }

    public void addStar(Star a)
    {
        int index = -1;
        for (index = 0; index < maxStarNum; index++)
        {
            if (validMap[index] == false)
            {
                break;
            }
        }
        if(index == -1)
        {
            Debug.Log("Star system full!");
        }
        else
        {
            s[index] = a;
            validMap[index] = true;
            validCount++;
        }
    }

    public void destroyStar(Star a)
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
                    s[index].valid = 0;
                    s[index] = new Star();
                    validMap[index] = false;
                    validCount--;
                    break;
                }
            }
        }
        if (flag == false)
        {
            Debug.Log("ERROR! Cannot fond star to destroy!");
        }
    }

    public void update(double h)
    {

        int[] validIndex = new int[validCount];
        int indexTmp = 0;
        VEC[] newVols = new VEC[validCount];
        for(int i = 0; i < validCount; i++)
        {
            newVols[i] = new VEC(3);
        }
        for(int i = 0; i < maxStarNum; i++)
        {
            if(validMap[i] == true)
            {
                validIndex[indexTmp] = i;
                indexTmp++;
            }
        }

        double[] arr = new double[2] { -1, -1 };
        VEC toDestroy = new VEC(2, arr);
        double DistanceBetweenStarsPow2Tmp;
        for (int i = 0; i < validCount; i++)
        {
            VEC updateV;
            updateV = new VEC(3);
            for (int j = 0; j < validCount; j++)
            {
                if (j != i)
                {
                    DistanceBetweenStarsPow2Tmp = DistanceBetweenStarsPow2(s[validIndex[i]], s[validIndex[j]]);
                    if(DistanceBetweenStarsPow2Tmp < 7E-1)
                    {
                        toDestroy[0] = i;
                        toDestroy[1] = j;
                    }
                    updateV += VEC.Normalize(s[validIndex[j]].pos - s[validIndex[i]].pos) * s[validIndex[j]].mass / DistanceBetweenStarsPow2Tmp;
                }
            }
            newVols[i] = s[validIndex[i]].vol + h * G * updateV;
        }

        for(int i = 0; i < validCount; i++)
        {
            s[validIndex[i]].pos += h * s[validIndex[i]].vol;
        }

        for(int i = 0; i < validCount; i++)
        {
            s[validIndex[i]].vol = newVols[i];
        }
        
        if(toDestroy[0] != -1 & toDestroy[1] != -1)
        {
            s[validIndex[0]].pos += (s[validIndex[1]].pos - s[validIndex[0]].pos) * s[validIndex[0]].mass / (s[validIndex[0]].mass + s[validIndex[1]].mass);
            s[validIndex[0]].vol = (s[validIndex[0]].vol * (s[validIndex[0]].mass / 1E9) + s[validIndex[1]].vol * (s[validIndex[1]].mass / 1E9)) / ((s[validIndex[0]].mass / 1E9) + (s[validIndex[1]].mass / 1E9));
            s[validIndex[0]].mass += s[validIndex[1]].mass;
            destroyStar(s[validIndex[1]]);
        }
    }

    private double DistanceBetweenStarsPow2(Star a, Star b)
    {
        double dist = 0;
        for(int i = 0; i < 3; i++)
        {
            dist += Math.Pow(a.pos[i] - b.pos[i], 2);
        }
        return dist;
    }
    
}

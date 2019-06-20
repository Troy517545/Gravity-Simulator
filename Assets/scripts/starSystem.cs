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

        for(int i = 0; i < validCount; i++)
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

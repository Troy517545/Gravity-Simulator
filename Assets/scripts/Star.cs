using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star
{
    public int valid = 0;
    public VEC pos = new VEC(3);
    public VEC vel = new VEC(3);

    public double mass = 100000;

    public Star()
    {
        valid = 0;

        pos[0] = 0;
        pos[1] = 0;
        pos[2] = 0;
        vel[0] = 0;
        vel[1] = 0;
        vel[2] = 0;
        mass = 0;
    }

    public Star(double X, double Y, double Z, double VX, double VY, double VZ, double MASS)
    {
        valid = 1;
        pos[0] = X;
        pos[1] = Y;
        pos[2] = Z;
        vel[0] = VX;
        vel[1] = VY;
        vel[2] = VZ;
        mass = MASS;
    }
    public void print()
    {
       
    }

    public string Info()
    {
        if (valid == 0)
        {
            return "Star not exists";
        }
        else
        {
            string s = "x: " + pos[0] + ",   y: " + pos[1] + ",   z: " + pos[2] + ",   vx: " + vel[0] + ",   vy: " + vel[1] + ",   vz: " + vel[2];
            return s;
        }
    }
}

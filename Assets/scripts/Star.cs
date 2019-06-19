using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star
{
    public double x = 0, y = 0, z = 0, vx = 0, vy = 0, vz = 0;

    public Star() { }

    public Star(double X, double Y, double Z, double VX, double VY, double VZ)
    {
        x = X;
        y = Y;
        z = Z;
        vx = VX;
        vy = VY;
        vz = VZ;
    }
    public void print()
    {
       
    }

    public string Info()
    {
        string s = "x: " + x + ",   y: " + y + ",   z: " + z + ",   vx: " + vx + ",   vy: " + vy + ",   vz: " + vz;
        return s;
    }
}

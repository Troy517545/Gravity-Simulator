using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star
{
    public double x=0, y=0, z=0;
    public Star(double X, double Y, double Z)
    {
        x = X;
        y = Y;
        z = Z;
    }
    public void print()
    {
        Debug.Log(x + " " + y + " " + z);
    }
}

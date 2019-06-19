using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using UnityEngine;

public class VEC {

    public int dim;     // vector length
    public double[] val;



    public VEC(int n)
    {
        dim = n;
        val = new double[dim];
    }

    public VEC(int n, double[] a)
    {
        dim = n;
        val = new double[dim];
        for (int i = 0; i < dim; i++)
        {
            val[i] = a[i];
        }
    }

    public VEC(VEC v1)
    {
        dim = v1.len();
        val = new double[dim];
        for (int i = 0; i < dim; i++)
        {
            val[i] = v1[i];
        }
    }

    public int len()
    {
        return dim;
    }

    public double this[int i]
    {
        get
        {
            return val[i];
        }
        set
        {
            val[i] = value;
        }
    }

    public static VEC operator -(VEC v1)
    {
        int n = v1.len();
        for(int i = 0; i < n; i++)
        {
            v1[i] = -v1[i];
        }
        return v1;
    }

    public static VEC operator +(VEC v1, VEC v2)
    {
        int n = v1.len();
        VEC v = new VEC(n);
        for (int i = 0; i < n; i++)
        {
            v[i] = v1[i] + v2[i];
        }
        return v;
    }

    public static VEC operator -(VEC v1, VEC v2)
    {
        int n = v1.len();
        VEC v = new VEC(n);
        for (int i = 0; i < n; i++)
        {
            v[i] = v1[i] - v2[i];
        }
        return v;
    }

    public static double operator *(VEC v1, VEC v2)
    {
        double s = 0;
        int n = v1.len();
        for(int i = 0; i < n; i++)
        {
            s += v1[i] * v2[i];
        }
        return s;
    }

    public static VEC operator *(VEC v1, double a)
    {
        int n = v1.len();
        VEC v = new VEC(n);
        for (int i = 0; i < n; i++)
        {
            v[i] = v1[i] * a;
        }
        return v;
    }

    public static VEC operator *(double a, VEC v1)
    {
        int n = v1.len();
        VEC v = new VEC(n);
        for (int i = 0; i < n; i++)
        {
            v[i] = v1[i] * a;
        }
        return v;
    }

    public static VEC operator /(VEC v1, double a)
    {
        int n = v1.len();
        VEC v = new VEC(n);
        for (int i = 0; i < n; i++)
        {
            v[i] = v1[i] / a;
        }
        return v;
    }





    public void print()
    {
        var tmp = "\n" + val[0] + " ";
        for(int i = 1; i < dim; i++)
        {
            tmp = tmp + val[i] + " ";
            
        }
        Debug.Log(tmp);
    }
}

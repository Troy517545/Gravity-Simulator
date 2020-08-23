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

    public double abs()
    {
        double s = 0;
        for(int i = 0; i < dim; i++)
        {
            s += val[i] * val[i];
        }
        return Math.Sqrt(s);
    }

    public static VEC Normalize(VEC v)
    {
        int n = v.len();
        VEC X = new VEC(n);
        double abs = v.abs();
        if (abs < 1E-12)
        {
            return X;
        }
        else
        {
            X = v / abs;
            return X;
        }
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

    public string Info()
    {
        string str = val[0].ToString();
        for (int i = 1; i < dim; i++)
        {
            str = str + " " + val[i].ToString();

        }
        return str;
    }

    public static double Lp_norm(VEC v, double p)
    {
        double s = 0;
        if (p == -1)
        {
            for(int i = 0; i < v.len(); i++)
            {
                if (s < Math.Abs(v[i]))
                {
                    s = Math.Abs(v[i]);
                }
            }
        }
        else
        {
            for(int i = 0; i < v.len(); i++)
            {
                s += Math.Pow(v[i], p);
            }
            s = Math.Pow(s, 1.0 / p);
        }
        return s;
    }
}

public class VECV3
{

    public int dim;     // vector length
    public VEC[] val;

    public VECV3(int n)
    {
        dim = n;
        val = new VEC[dim];
        for(int i = 0; i < dim; i++)
        {
            val[i] = new VEC(3);
        }
    }

    public VECV3(int n, VEC[] a)
    {
        dim = n;
        val = new VEC[dim];
        for (int i = 0; i < dim; i++)
        {
            val[i] = new VEC(a[i]);
        }
    }

    public VECV3(VECV3 v1)
    {
        dim = v1.len();
        val = new VEC[dim];
        for (int i = 0; i < dim; i++)
        {
            val[i] = new VEC(v1[i]);
        }
    }

    public int len()
    {
        return dim;
    }

    public VEC this[int i]
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

    public static VECV3 operator -(VECV3 v1)
    {
        int n = v1.len();
        for (int i = 0; i < n; i++)
        {
            v1[i] = new VEC(-v1[i]);
        }
        return v1;
    }

    public static VECV3 operator +(VECV3 v1, VECV3 v2)
    {
        int n = v1.len();
        VECV3 v = new VECV3(n);
        for (int i = 0; i < n; i++)
        {
            v[i] = v1[i] + v2[i];
        }
        return v;
    }

    public static VECV3 operator -(VECV3 v1, VECV3 v2)
    {
        int n = v1.len();
        VECV3 v = new VECV3(n);
        for (int i = 0; i < n; i++)
        {
            v[i] = v1[i] - v2[i];
        }
        return v;
    }

    public static double operator *(VECV3 v1, VECV3 v2)
    {
        double s = 0;
        int n = v1.len();
        for (int i = 0; i < n; i++)
        {
            s += v1[i] * v2[i];
        }
        return s;
    }

    public static VECV3 operator *(VECV3 v1, double a)
    {
        int n = v1.len();
        VECV3 v = new VECV3(n);
        for (int i = 0; i < n; i++)
        {
            v[i] = v1[i] * a;
        }
        return v;
    }

    

    public static VECV3 operator *(double a, VECV3 v1)
    {
        int n = v1.len();
        VECV3 v = new VECV3(n);
        for (int i = 0; i < n; i++)
        {
            v[i] = v1[i] * a;
        }
        return v;
    }

    public static VECV3 operator /(VECV3 v1, double a)
    {
        int n = v1.len();
        VECV3 v = new VECV3(n);
        for (int i = 0; i < n; i++)
        {
            v[i] = v1[i] / a;
        }
        return v;
    }

    public VEC abs()
    {
        VEC s = new VEC(dim);
        for (int i = 0; i < dim; i++)
        {
            s[i] = val[i].abs();
        }
        return s;
    }

    public void print()
    {
        Debug.Log(" ");
    }

    public string Info()
    {
        string str = "(" + val[0].Info() + ")";
        for(int i = 1; i < dim; i++)
        {
            str = str + "|(" + val[i].Info() + ")";
        }
        return str;
    }

    public static double L2_norm_special(VECV3 v)
    {
        double s = 0;
        for(int i = 0; i < v.len(); i++)
        {
            for(int k = 0; k < 3; k++)
            {
                s += Math.Pow(v[i][k], 2);
            }
        }
        return Math.Sqrt(s);
    }
}

public class VECM3
{

    public int dim;     // vector length
    public MAT[] val;

    public VECM3(int n)
    {
        dim = n;
        val = new MAT[dim];
        for (int i = 0; i < dim; i++)
        {
            val[i] = new MAT(3);
        }
    }

    public VECM3(int n, MAT[] a)
    {
        dim = n;
        val = new MAT[dim];
        for (int i = 0; i < dim; i++)
        {
            val[i] = new MAT(a[i]);
        }
    }

    public VECM3(VECM3 v1)
    {
        dim = v1.len();
        val = new MAT[dim];
        for (int i = 0; i < dim; i++)
        {
            val[i] = new MAT(v1[i]);
        }
    }

    public int len()
    {
        return dim;
    }

    public MAT this[int i]
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

    public static VECM3 operator -(VECM3 v1)
    {
        int n = v1.len();
        for (int i = 0; i < n; i++)
        {
            v1[i] = -v1[i];
        }
        return v1;
    }

    public static VECM3 operator +(VECM3 v1, VECM3 v2)
    {
        int n = v1.len();
        VECM3 v = new VECM3(n);
        for (int i = 0; i < n; i++)
        {
            v[i] = v1[i] + v2[i];
        }
        return v;
    }

    public static VECM3 operator -(VECM3 v1, VECM3 v2)
    {
        int n = v1.len();
        VECM3 v = new VECM3(n);
        for (int i = 0; i < n; i++)
        {
            v[i] = v1[i] - v2[i];
        }
        return v;
    }

    public static MAT operator *(VECM3 v1, VECM3 v2)
    {
        MAT s = new MAT(3);
        int n = v1.len();
        for (int i = 0; i < n; i++)
        {
            s += v1[i] * v2[i];
        }
        return s;
    }

    public static VECM3 operator *(VECM3 v1, double a)
    {
        int n = v1.len();
        VECM3 v = new VECM3(n);
        for (int i = 0; i < n; i++)
        {
            v[i] = v1[i] * a;
        }
        return v;
    }

    public static VECM3 operator *(double a, VECM3 v1)
    {
        int n = v1.len();
        VECM3 v = new VECM3(n);
        for (int i = 0; i < n; i++)
        {
            v[i] = v1[i] * a;
        }
        return v;
    }

    public static VECM3 operator /(VECM3 v1, double a)
    {
        int n = v1.len();
        VECM3 v = new VECM3(n);
        for (int i = 0; i < n; i++)
        {
            v[i] = v1[i] / a;
        }
        return v;
    }

    public void print()
    {
        Debug.Log(" ");
    }

    public string Info()
    {
        string str = "(" + val[0].Info() + ")";
        for (int i = 1; i < dim; i++)
        {
            str = str + "|(" + val[i].Info() + ")";
        }
        return str;
    }
}
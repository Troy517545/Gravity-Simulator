using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MAT
{
    public int n;     // vector length
    public VEC[] va;

    public MAT(int dim)
    {
        n = dim;
        va = new VEC[n];
        for(int i = 0; i < n; i++)
        {
            va[i] = new VEC(n);
        }
    }

    public MAT(MAT m1)
    {
        n = m1.dim();
        va = new VEC[n];
        for (int i = 0; i < n; i++)
        {
            va[i] = new VEC(m1[i]);
        }
    }

    public MAT(int dim, double[] a)
    {
        n = dim;
        va = new VEC[n];
        for (int i = 0; i < n; i++)
        {
            va[i] = new VEC(n);
        }
        for(int i = 0; i < n; i++)
        {
            for(int j = 0; j < n; j++)
            {
                this[i][j] = a[i * n + j];
            }
        }
    }

    public int dim()
    {
        return n;
    }

    public MAT tpose()
    {
        int dim = this.dim();
        MAT A = new MAT(dim);
        for(int i = 0; i < dim; i++)
        {
            for(int j = 0; j < dim; j++)
            {
                A[i][j] = this[j][i];
            }
        }
        return A;
    }

    public VEC this[int i]
    {
        get
        {
            return va[i];
        }
        set
        {
            va[i] = value;
        }
    }

    public static MAT operator -(MAT m1)
    {
        int n = m1.dim();
        for (int i = 0; i < n; i++)
        {
            m1[i] = -m1[i];
        }
        return m1;
    }

    public static MAT operator +(MAT m1, MAT m2)
    {
        int n = m1.dim();
        MAT m = new MAT(n);
        for (int i = 0; i < n; i++)
        {
            m[i] = m1[i] + m2[i];
        }
        return m;
    }

    public static MAT operator -(MAT m1, MAT m2)
    {
        int n = m1.dim();
        MAT m = new MAT(n);
        for (int i = 0; i < n; i++)
        {
            m[i] = m1[i] - m2[i];
        }
        return m;
    }

    public static MAT operator *(MAT m1, MAT m2)
    {
        int n = m1.dim();
        MAT m = new MAT(n);
        double s;
        for (int i = 0; i < n; i++)
        {
            for(int j = 0; j < n; j++)
            {
                s = 0;
                for(int k = 0; k < n; k++)
                {
                    s += m1[i][k] * m2[k][j];
                }
                m[i][j] = s;
            }
        }
        return m;
    }

    public static MAT operator /(MAT m1, MAT m2)
    {
        MAT m2Inverse = InverseM3(m2);
        return m1 * m2Inverse;
    }

    public static VEC operator *(MAT m1, VEC v1)
    {
        int n = m1.dim();
        VEC v = new VEC(n);
        double s;
        for (int i = 0; i < n; i++)
        {
            s = 0;
            for (int j = 0; j < n; j++)
            {
                s += m1[i][j] * v1[j];
            }
            v[i] = s;
        }
        return v;
    }

    public static MAT operator *(MAT m1, double a)
    {
        int n = m1.dim();
        MAT m = new MAT(n);
        for(int i = 0; i < n; i++)
        {
            m[i] = m1[i] * a;
        }
        return m;
    }

    public static MAT operator *(double a, MAT m1)
    {
        int n = m1.dim();
        MAT m = new MAT(n);
        for (int i = 0; i < n; i++)
        {
            m[i] = m1[i] * a;
        }
        return m;
    }

    public static MAT operator /(MAT m1, double a)
    {
        int n = m1.dim();
        MAT m = new MAT(n);
        for (int i = 0; i < n; i++)
        {
            m[i] = m1[i] / a;
        }
        return m;
    }

    public static VEC operator *(VEC v1, MAT m1)
    {
        int n = m1.dim();
        VEC v = new VEC(n);
        double s;
        for (int i = 0; i < n; i++)
        {
            s = 0;
            for(int j = 0; j < n; j++)
            {
                s += v1[j] * m1[j][i];
            }
            v[i] = s;
        }
        return v;
    }

    public static VEC operator /(VEC v1, MAT m1)
    {
        MAT m2Inverse = InverseM3(m1);
        return v1 * m2Inverse;
    }

    public static MAT InverseM3(MAT A)
    {
        MAT I = unit(3);
        int n = A.dim();
        A = numericalFunctions.luFact(A);
        MAT L = new MAT(n);
        MAT U = new MAT(n);
        for (int i = 1; i < n; i++)
        {
            for (int j = 0; j < i; j++)
            {
                L[i][j] = A[i][j];
            }
        }
        for (int i = 0; i < n; i++)
        {
            L[i][i] = 1;
        }
        for (int i = 0; i < n; i++)
        {
            for (int j = i; j < n; j++)
            {
                U[i][j] = A[i][j];
            }
        }

        MAT yT = new MAT(3);

        for(int i = 0; i < 3; i++)
        {
            yT[i] = numericalFunctions.LU_Solve(L, I[i]);
        }
        MAT y = yT.tpose();
        MAT xT = new MAT(3);
        for(int i = 0; i < 3; i++)
        {
            xT[i] = numericalFunctions.LU_Solve(U, yT[i]);
        }
        return xT.tpose();
    }

    public static MAT unit(int n)
    {
        MAT A = new MAT(n);
        for(int i = 0; i < n; i++)
        {
            A[i][i] = 1;
        }
        return A;
    }

    public void print()
    {
        var tmp = "\n | " + this[0][0] + " ";
        for(int i = 0; i < n; i++)
        {
            for(int j = 0; j < n; j++)
            {
                if(i == 0 & j == 0) { }
                else
                {
                    tmp = tmp + this[i][j] + " ";
                }
            }
            tmp = tmp + " | ";
        }
        Debug.Log(tmp);
    }

    public string Info()
    {
        var tmp = "\n | " + this[0][0] + " ";
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (i == 0 & j == 0)
                {

                }
                else
                {
                    tmp = tmp + this[i][j] + " ";
                }
            }
            tmp = tmp + " | ";
        }
        Debug.Log(tmp);
        return tmp;
    }
}



class numericalFunctions
{
    public static MAT luFact(MAT m1)
    {
        int n = m1.dim();
        for(int i = 0; i < n; i++)
        {
            for(int j = i + 1; j < n; j++){
                m1[j][i] /= m1[i][i];
            }
            for(int j = i + 1; j < n; j++)
            {
                for(int k = i + 1; k < n; k++)
                {
                    m1[j][k] -= m1[j][i] * m1[i][k];
                }
            }
        }
        return m1;
    }

    public static VEC LU_Solve(MAT A1, VEC b1)
    {
        MAT A = new MAT(A1);
        VEC b = new VEC(b1);
        int n = A.dim();
        A = luFact(A);
        MAT L = new MAT(n);
        MAT U = new MAT(n);
        for (int i = 1; i < n; i++)
        {
            for (int j = 0; j < i; j++)
            {
                L[i][j] = A[i][j];
            }
        }
        for (int i = 0; i < n; i++)
        {
            L[i][i] = 1;
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = i; j < n; j++)
            {
                U[i][j] = A[i][j];
            }
        }
        VEC y = fwdSubs(L, b);
        VEC x = bckSubs(U, y);
        return x;
    }

    public static VEC fwdSubs(MAT m1, VEC b)
    {
        int len = b.len();
        VEC v = new VEC(len);
        for (int i = 0; i < len; i++)
        {
            double s = 0;
            for (int j = 0; j < i; j++)
            {
                s += m1[i][j] * v[j];
            } 
            v[i] = (b[i] - s) / m1[i][i];
        }
        return v;
    }

    public static VEC bckSubs(MAT m1, VEC b)
    {
        int len = b.len();
        VEC v = new VEC(len);
        for (int i = 0; i < len; i++)
        {
            v[i] = b[i];
        }
        for (int i = len - 1; i >= 0; i--)
        {
            v[i] = v[i] / m1[i][i];
            for (int j = i - 1; j >= 0; j--)
            {
                v[j] = v[j] - (m1[j][i] * v[i]);
            }
        }
        return v;
    }

    public static MATM3 luFact(MATM3 m1)
    {
        int n = m1.dim();
        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                m1[j][i] /= m1[i][i];
            }
            for (int j = i + 1; j < n; j++)
            {
                for (int k = i + 1; k < n; k++)
                {
                    m1[j][k] -= m1[j][i] * m1[i][k];
                }
            }
        }
        return m1;
    }
    public static VECV3 LU_Solve(MATM3 A1, VECV3 b1)
    {
        MATM3 A = new MATM3(A1);
        
        VECV3 b = new VECV3(b1);
        int n = A.dim();
        A = luFact(A);
        
        MATM3 L = new MATM3(n);
        MATM3 U = new MATM3(n);
        for (int i = 1; i < n; i++)
        {
            for (int j = 0; j < i; j++)
            {
                L[i][j] = A[i][j];
            }
        }
        for (int i = 0; i < n; i++)
        {
            L[i][i] = MAT.unit(3);
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = i; j < n; j++)
            {
                U[i][j] = A[i][j];
            }
        }
        VECV3 y = fwdSubs(L, b);
        VECV3 x = bckSubs(U, y);
        return x;
    }

    public static VECV3 fwdSubs(MATM3 m1, VECV3 b)
    {
        int len = b.len();
        VECV3 v = new VECV3(len);
        for (int i = 0; i < len; i++)
        {
            VEC s = new VEC(3);
            for (int j = 0; j < i; j++)
            {
                s += m1[i][j] * v[j];
            }
            v[i] = (b[i] - s) / m1[i][i];
        }
        return v;
    }

    public static VECV3 bckSubs(MATM3 m1, VECV3 b)
    {
        int len = b.len();
        VECV3 v = new VECV3(len);
        for (int i = 0; i < len; i++)
        {
            v[i] = b[i];
        }
        for (int i = len - 1; i >= 0; i--)
        {
            v[i] = v[i] / m1[i][i];
            for (int j = i - 1; j >= 0; j--)
            {
                v[j] = v[j] - (m1[j][i] * v[i]);
            }
        }
        return v;
    }
}

public class MATM3
{
    public int n;     // vector length
    public VECM3[] va;

    public MATM3(int dim)
    {
        n = dim;
        va = new VECM3[n];
        for (int i = 0; i < n; i++)
        {
            va[i] = new VECM3(dim);
        }
    }

    public MATM3(MATM3 m1)
    {
        n = m1.dim();
        va = new VECM3[n];
        for (int i = 0; i < n; i++)
        {
            va[i] = new VECM3(m1[i]);
        }
    }

    public int dim()
    {
        return n;
    }

    public MATM3 tpose()
    {
        int dim = this.dim();
        MATM3 A = new MATM3(dim);
        for (int i = 0; i < dim; i++)
        {
            for (int j = 0; j < dim; j++)
            {
                A[i][j] = new MAT(this[j][i]);
            }
        }
        return A;
    }

    public VECM3 this[int i]
    {
        get
        {
            return va[i];
        }
        set
        {
            va[i] = value;
        }
    }

    public static MATM3 operator -(MATM3 m1)
    {
        int n = m1.dim();
        MATM3 A = new MATM3(n);
        for (int i = 0; i < n; i++)
        {
            A[i] = -m1[i];
        }
        return A;
    }

    public static MATM3 operator +(MATM3 m1, MATM3 m2)
    {
        int n = m1.dim();
        MATM3 m = new MATM3(n);
        for (int i = 0; i < n; i++)
        {
            m[i] = m1[i] + m2[i];
        }
        return m;
    }

    public static MATM3 operator -(MATM3 m1, MATM3 m2)
    {
        int n = m1.dim();
        MATM3 m = new MATM3(n);
        for (int i = 0; i < n; i++)
        {
            m[i] = m1[i] - m2[i];
        }
        return m;
    }

    public static MATM3 operator *(MATM3 m1, MATM3 m2)
    {
        int n = m1.dim();
        MATM3 m = new MATM3(n);
        MAT s;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                s = new MAT(3);
                for (int k = 0; k < n; k++)
                {
                    s += m1[i][k] * m2[k][j];
                }
                m[i][j] = s;
            }
        }
        return m;
    }

    public static MATM3 operator *(MATM3 m1, double a)
    {
        int n = m1.dim();
        MATM3 m = new MATM3(n);
        for (int i = 0; i < n; i++)
        {
            m[i] = m1[i] * a;
        }
        return m;
    }

    public static MATM3 operator *(double a, MATM3 m1)
    {
        int n = m1.dim();
        MATM3 m = new MATM3(n);
        for (int i = 0; i < n; i++)
        {
            m[i] = m1[i] * a;
        }
        return m;
    }

    public static MATM3 operator /(MATM3 m1, double a)
    {
        int n = m1.dim();
        MATM3 m = new MATM3(n);
        for (int i = 0; i < n; i++)
        {
            m[i] = m1[i] / a;
        }
        return m;
    }

    public string Info()
    {
        var tmp = "\n | " + this[0][0] + " ";
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (i == 0 & j == 0) { }
                else
                {
                    tmp = tmp + this[i][j] + " ";
                }
            }
            tmp = tmp + " | ";
        }
        Debug.Log(tmp);
        return tmp;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        double[] arr = { 3, 2, -1, 2, 4, -2, 1, -2, 5};
        double[] arr1 = { 0.4, 0.4, 1.2 };
        MAT A = new MAT(3, arr);
        VEC b = new VEC(3, arr1);

        //for (int i = 0; i < 5; i++)
        //{
        //    for(int j = 0; j < 5; j++)
        //    {
        //        A[i][j] = i;
        //    }
        //    b[i] = i;
        //}
        A.print();
        b.print();

        VEC x = numericalFunctions.LU_Solve(A, b);

        x.print();
        A.print();
        (A * x).print();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

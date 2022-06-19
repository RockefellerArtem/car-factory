using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test3 : MonoBehaviour
{
    private int[] _num = new int[4];

    private List<string> list = new List<string>();
    private void Update()
    {
        Test("Hello world");
        Debug.Log(Test2(5, 5));
        Tetat();
    }

    private void Test(string message)
    {
        Debug.Log(message);
    }

    private int Test2(int a, int b)
    {
        return a + b;
    }

    private int Pum(params int[] parametrs)
    {
        int result = 0;

        return result;
    }

    private void Tetat()
    {
        Debug.Log(_num[0] = 1);
        Debug.Log(_num[1] = 2);
        Debug.Log(_num[2] = 3);
        Debug.Log(_num[3] = 4);
    }
}

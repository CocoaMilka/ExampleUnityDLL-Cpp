using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Test : MonoBehaviour
{
    [DllImport("ExampleUnityDll")]
    public static extern int addOne(int number);

    int i = 10;

    void Start()
    {
        Debug.Log(addOne(i));
    }
}

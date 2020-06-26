using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class JobTest : MonoBehaviour
{
   
    // Update is called once per frame
    void Update()
    {
        float startTime = Time.realtimeSinceStartup;
        ReallyToughTask();
        float endTime = Time.realtimeSinceStartup;
        Debug.Log($"Cost Time:{(endTime-startTime)*1000} ms");
    }

    private void ReallyToughTask()
    {
        float value = 0f;
        for (int i = 0; i < 50000; i++)
        {
            value = math.exp10(math.sqrt(value));
        }
    }
}

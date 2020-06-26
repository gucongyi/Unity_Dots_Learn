using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

public class JobTest : MonoBehaviour
{
    [SerializeField]
    bool useJobs;
    // Update is called once per frame
    void Update()
    {
        float startTime = Time.realtimeSinceStartup;
        if (!useJobs)
        {
            Helper.ReallyToughTask();
        }
        else
        {
            JobHandle jobHandle = ReallyTouchTaskJob();
            //等待任务执行完成
            jobHandle.Complete();
        }
        
        float endTime = Time.realtimeSinceStartup;
        Debug.Log($"Cost Time:{(endTime-startTime)*1000} ms");
    }

    JobHandle ReallyTouchTaskJob()
    {
        ReallyTouchJob job = new ReallyTouchJob();
        //启动线程执行任务
        JobHandle jobHandle=job.Schedule();
        return jobHandle;
    }
   
}
public static class Helper 
{
    public static void ReallyToughTask()
    {
        float value = 0f;
        for (int i = 0; i < 50000; i++)
        {
            value = math.exp10(math.sqrt(value));
        }
    }
}

[BurstCompile]
public struct ReallyTouchJob : IJob
{
    public void Execute()
    {
        Helper.ReallyToughTask();
    }
}

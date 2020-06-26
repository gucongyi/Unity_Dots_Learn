using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

public class JobMutiThreadTest : MonoBehaviour
{
    [SerializeField]
    bool useJobs;
    // Update is called once per frame
    void Update()
    {
        float startTime = Time.realtimeSinceStartup;
        if (!useJobs)
        {
            for (int i = 0; i < 10; i++)
            {
                Helper.ReallyToughTask();
            }
        }
        else
        {
            NativeList<JobHandle> jobHandleList = new NativeList<JobHandle>(Allocator.Temp);
            for (int i = 0; i < 10; i++)
            {
                JobHandle jobHandle = ReallyTouchTaskJob();
                jobHandleList.Add(jobHandle);
            }
            //等待所有线程完成
            JobHandle.CompleteAll(jobHandleList);
            jobHandleList.Dispose();
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

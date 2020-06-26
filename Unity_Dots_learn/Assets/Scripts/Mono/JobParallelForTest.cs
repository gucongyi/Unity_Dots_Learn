using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

public class JobParallelForTest : MonoBehaviour
{
    public Transform pfZombine;
    List<Zombie> zombieList;
    public class Zombie
    {
        public Transform transform;
        public float moveY;
    }
    private void Start()
    {
        zombieList = new List<Zombie>();
        for (int i = 0; i < 1000; i++)
        {
            Transform zombineTransform = Instantiate(pfZombine,new Vector3(UnityEngine.Random.Range(-8f,8f), UnityEngine.Random.Range(-5f, 5f),0f),Quaternion.identity);
            zombineTransform.gameObject.SetActive(true);
            zombieList.Add(new Zombie() 
            {
                transform=zombineTransform,
                moveY= UnityEngine.Random.Range(1f,2f)
            });
        }
    }
    // Update is called once per frame
    void Update()
    {
        float startTime = Time.realtimeSinceStartup;
        NativeArray<float3> positionArray = new NativeArray<float3>(zombieList.Count,Allocator.TempJob);
        NativeArray<float> moveYArray=new NativeArray<float>(zombieList.Count, Allocator.TempJob);

        //赋值
        for (int i = 0; i < zombieList.Count; i++)
        {
            positionArray[i] = zombieList[i].transform.position;
            moveYArray[i] = zombieList[i].moveY;
        }
        ReallyTouchParallelJob reallyTouchParallelJob = new ReallyTouchParallelJob()
        {
            deltaTime = Time.deltaTime,
            positionArray = positionArray,
            moveYArray = moveYArray
        };
        //执行,100个循环
        JobHandle jobHandle=reallyTouchParallelJob.Schedule(zombieList.Count, 100);
        //等待完成
        jobHandle.Complete();

        //每帧执行完后更新位置，用来下一帧传入
        for (int i = 0; i < zombieList.Count; i++)
        {
            zombieList[i].transform.position= positionArray[i];
            zombieList[i].moveY= moveYArray[i];
        }

        positionArray.Dispose();
        moveYArray.Dispose();
        float endTime = Time.realtimeSinceStartup;
        Debug.Log($"Cost Time:{(endTime-startTime)*1000} ms");
    }

    
}

[BurstCompile]
public struct ReallyTouchParallelJob : IJobParallelFor
{
    public NativeArray<float3> positionArray;
    public NativeArray<float> moveYArray;
    //多线程，不能用Time.deltaTime，必须传过来
    public float deltaTime;
    public void Execute(int index)
    {
        positionArray[index] += new float3(0, moveYArray[index] * deltaTime, 0f);
        if (positionArray[index].y>5f)
        {
            moveYArray[index] = -math.abs(moveYArray[index]);
        }
        if (positionArray[index].y < -5f)
        {
            moveYArray[index] = +math.abs(moveYArray[index]);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

public class PlayerMovementSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;
        JobHandle jobHandle=Entities.ForEach((ref Translation translation,in MoveDirectionComponent moveDirection) => 
        {
            translation.Value.x += moveDirection.Value * deltaTime;
        }).Schedule(inputDeps);
        return jobHandle;
    }
}

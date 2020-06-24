using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

//系统脚本不用挂载，每帧会自动执行OnUpdate
public class MoveSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Translation translation, ref MoveSpeedComponent moveSpeedComponent) => {
            translation.Value.y += moveSpeedComponent.speed* Time.DeltaTime;
            if (translation.Value.y>5)
            {
                moveSpeedComponent.speed = -math.abs(moveSpeedComponent.speed);
            }

            if (translation.Value.y <-5)
            {
                moveSpeedComponent.speed = +math.abs(moveSpeedComponent.speed);
            }

        });
    }
}

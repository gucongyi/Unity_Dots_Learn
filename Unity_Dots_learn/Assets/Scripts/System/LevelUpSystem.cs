using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

//系统脚本不用挂载，每帧会自动执行OnUpdate
public class LevelUpSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref LevelComponent levelComponent) => {
            levelComponent.level += 1f * Time.DeltaTime;
        });
    }
}

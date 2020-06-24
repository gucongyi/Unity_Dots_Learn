using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

public class TestECSMono : MonoBehaviour
{
    void Start()
    {
        //创建Entity
        EntityManager entityManager= World.DefaultGameObjectInjectionWorld.EntityManager;
        ////1.组件方式创建Entity
        ////Entity entity=entityManager.CreateEntity(typeof(LevelComponent));
        //Entity entity = entityManager.CreateEntity();
        ////添加对应组件
        //entityManager.AddComponent<LevelComponent>(entity);

        //2.原型方式创建Entity
        EntityArchetype entityArchetype = entityManager.CreateArchetype(typeof(LevelComponent), typeof(Translation));
        Entity entity = entityManager.CreateEntity(entityArchetype);
        entityManager.SetName(entity, "levelEntity");

        //给entity的对应组件赋值
        entityManager.SetComponentData(entity,new LevelComponent() { level=10});
        entityManager.SetComponentData(entity, new Translation() { Value = new Unity.Mathematics.float3(1f,2f,3f)});
    }
}

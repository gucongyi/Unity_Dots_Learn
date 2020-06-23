using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class TestECSMono : MonoBehaviour
{
    void Start()
    {
        //创建Entity
        EntityManager entityManager= World.DefaultGameObjectInjectionWorld.EntityManager;
        //Entity entity=entityManager.CreateEntity(typeof(LevelComponent));
        Entity entity = entityManager.CreateEntity();
        //添加对应组件
        entityManager.AddComponent<LevelComponent>(entity);
        //给entity的对应组件赋值
        entityManager.SetComponentData(entity,new LevelComponent() { level=10});
    }
}

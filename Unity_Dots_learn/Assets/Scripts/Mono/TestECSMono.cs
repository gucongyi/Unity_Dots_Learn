using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Rendering;

public class TestECSMono : MonoBehaviour
{
    [SerializeField]
    private Mesh mesh;
    [SerializeField]
    private Material material;
    void Start()
    {
        //创建Entity
        EntityManager entityManager= World.DefaultGameObjectInjectionWorld.EntityManager;
        ////1.组件方式创建Entity
        ////Entity entity=entityManager.CreateEntity(typeof(LevelComponent));
        //Entity entity = entityManager.CreateEntity();
        ////添加对应组件
        //entityManager.AddComponent<LevelComponent>(entity);

        ////2.原型方式创建Entity
        //EntityArchetype entityArchetype = entityManager.CreateArchetype(typeof(LevelComponent), typeof(Translation));
        //Entity entity = entityManager.CreateEntity(entityArchetype);
        //entityManager.SetName(entity, "levelEntity");



        ////给entity的对应组件赋值
        //entityManager.SetComponentData(entity,new LevelComponent() { level=10});
        //entityManager.SetComponentData(entity, new Translation() { Value = new Unity.Mathematics.float3(1f,2f,3f)});

        //3.NativeArray方式创建EntityArray
        EntityArchetype entityArchetype = entityManager.CreateArchetype(
            typeof(LevelComponent), 
            typeof(Translation),
            //以下5个为渲染组件
            typeof(RenderMesh),
            typeof(LocalToWorld),
            typeof(RenderBounds),
            typeof(WorldRenderBounds),
            typeof(ChunkWorldRenderBounds),
            //以上5个为渲染组件
            typeof(MoveSpeedComponent)
            );
        NativeArray<Entity> entityArray = new NativeArray<Entity>(1000, Allocator.Temp);
        //根据原型填充entityArray
        entityManager.CreateEntity(entityArchetype,entityArray);
        for (int i = 0; i < entityArray.Length; i++)
        {
            Entity entity = entityArray[i];
            entityManager.SetComponentData(entity, new LevelComponent{ level = Random.Range(10,20) });
            entityManager.SetComponentData(entity, new MoveSpeedComponent { speed = Random.Range(1, 2) });
            entityManager.SetComponentData(entity, new Translation {
                Value = new Unity.Mathematics.float3(Random.Range(-8f,8f),Random.Range(-5f,5f),0f)
            });
            entityManager.SetSharedComponentData(entity, new RenderMesh{
                mesh = mesh,
                material = material
            });
        }
        //要自己释放
        entityArray.Dispose();
    }
}

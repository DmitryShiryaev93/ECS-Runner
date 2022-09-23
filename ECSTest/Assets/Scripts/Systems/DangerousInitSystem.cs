using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.EcsLite.Di;

namespace Runer
{
    public class DangerousInitSystem : IEcsInitSystem
    {
        readonly EcsWorldInject _ecsWorld = default;
        readonly EcsPoolInject<DangerousComponent> _dangerousPool = default;

        public void Init(IEcsSystems ecsSystems)
        {
            foreach (var i in GameObject.FindGameObjectsWithTag(Constants.Tags.DangerousTag))
            {
                var dangerousEntity = _ecsWorld.Value.NewEntity();

                _dangerousPool.Value.Add(dangerousEntity);
                ref var dangerousComponent = ref _dangerousPool.Value.Get(dangerousEntity);

                dangerousComponent.obstacleTransform = i.transform;
                dangerousComponent.pointA = i.transform.Find("A").position;
                dangerousComponent.pointB = i.transform.Find("B").position;
            }
        }
    }
}

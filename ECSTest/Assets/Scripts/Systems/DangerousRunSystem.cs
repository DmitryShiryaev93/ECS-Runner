using Leopotam.EcsLite;
using UnityEngine;
using Leopotam.EcsLite.Di;

namespace Runer
{
    public class DangerousRunSystem : IEcsRunSystem
    {
        readonly EcsPoolInject<DangerousComponent> _dangerousPool = default;
        readonly EcsFilterInject<Inc<DangerousComponent>> _filter = default;

        public void Run(IEcsSystems ecsSystems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var dangerousComponent = ref _dangerousPool.Value.Get(entity);
                Vector3 pos1 = dangerousComponent.pointA;
                Vector3 pos2 = dangerousComponent.pointB;

                dangerousComponent.obstacleTransform.localPosition = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time, 1.0f));
            }
        }
    }
}
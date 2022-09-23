using Leopotam.EcsLite;
using UnityEngine;
using Leopotam.EcsLite.Di;


namespace Runer
{
    public class CameraFollowSystem : IEcsInitSystem, IEcsRunSystem
    {
        private int cameraEntity;
        readonly EcsWorldInject _world = default;

        readonly EcsPoolInject<CameraComponent> _cameraPool = default;
        readonly EcsPoolInject<PlayerComponent> _playerPool = default;

        readonly EcsFilterInject<Inc<PlayerComponent>> _filter = default;
        

        public void Init(IEcsSystems ecsSystems)
        {
            var gameData = ecsSystems.GetShared<GameData>();

            var cameraEntity = _world.Value.NewEntity();

            _cameraPool.Value.Add(cameraEntity);
            ref var cameraComponent = ref _cameraPool.Value.Get(cameraEntity);

            cameraComponent.cameraTransform = Camera.main.transform;
            cameraComponent.cameraSmoothness = gameData.configuration.cameraFollowSmoothness;
            cameraComponent.curVelocity = Vector3.zero;
            cameraComponent.offset = new Vector3(0f, 2f, -9f);

            this.cameraEntity = cameraEntity;
        }

        public void Run(IEcsSystems ecsSystems)
        {
            /*var filter = ecsSystems.GetWorld().Filter<PlayerComponent>().End();
            var playerPool = ecsSystems.GetWorld().GetPool<PlayerComponent>();
            var cameraPool = ecsSystems.GetWorld().GetPool<CameraComponent>();*/

            ref var cameraComponent = ref _cameraPool.Value.Get(cameraEntity);

            foreach(var entity in _filter.Value)
            {
                ref var playerComponent = ref _playerPool.Value.Get(entity);

                Vector3 currentPosition = cameraComponent.cameraTransform.position;
                Vector3 targetPoint = playerComponent.playerTransform.position + cameraComponent.offset;

                cameraComponent.cameraTransform.position = Vector3.SmoothDamp(currentPosition, targetPoint, ref cameraComponent.curVelocity, cameraComponent.cameraSmoothness);
            }    
        }
    }
}

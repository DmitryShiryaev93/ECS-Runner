using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Runer
{
    public class DangerousHitSystem : IEcsRunSystem
    {
        readonly EcsPoolInject<HitComponent> _hitPool = default;
        readonly EcsFilterInject<Inc<HitComponent>> _hitFilter = default;

        readonly EcsPoolInject<PlayerComponent> _playerPool = default;
        readonly EcsFilterInject<Inc<PlayerComponent>> _playerFilter = default;

        public void Run(IEcsSystems ecsSystems)
        {
            var gameData = ecsSystems.GetShared<GameData>();

            foreach (var hitEntity in _hitFilter.Value)
            {
                ref var hitComponent = ref _hitPool.Value.Get(hitEntity);

                foreach (var playerEntity in _playerFilter.Value)
                {
                    ref var playerComponent = ref _playerPool.Value.Get(playerEntity);

                    if (hitComponent.other.CompareTag(Constants.Tags.DangerousTag))
                    {
                        Vector3 pos = gameData.bloodParticle.transform.position;
                        pos = playerComponent.playerTransform.position;
                        pos.y = gameData.bloodParticle.transform.position.y;
                        gameData.bloodParticle.transform.position = pos;

                        gameData.bloodParticle.SetActive(true);
                        playerComponent.playerTransform.gameObject.SetActive(false);
                        ecsSystems.GetWorld().DelEntity(playerEntity);
                        gameData.gameOverPanel.SetActive(true);
                    }
                }

            }
        }
    }
}
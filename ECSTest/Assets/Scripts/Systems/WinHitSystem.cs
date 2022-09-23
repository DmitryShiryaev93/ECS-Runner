using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Runer
{
    public class WinHitSystem : IEcsRunSystem
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

                    if (hitComponent.other.CompareTag(Constants.Tags.WinPointTag))
                    {
                        playerComponent.playerTransform.gameObject.SetActive(false);
                        ecsSystems.GetWorld().DelEntity(playerEntity);
                        gameData.playerWonPanel.SetActive(true);
                    }
                }
            }
        }
    }
}
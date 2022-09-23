using Leopotam.EcsLite;
using UnityEngine;
using Leopotam.EcsLite.Di;

namespace Runer
{
    public class PlayerJumpSystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<PlayerComponent, PlayerInputComponent, GroundedComponent>> _playerGroundedFilter = default;
        readonly EcsFilterInject<Inc<TryJump>> _tryJumpFilter = default;
        readonly EcsPoolInject<PlayerComponent> _playerPool = default;

        public void Run(IEcsSystems ecsSystems)
        {
            foreach (var tryJumpEntity in _tryJumpFilter.Value)
            {
                ecsSystems.GetWorld().DelEntity(tryJumpEntity);

                foreach (var playerEntity in _playerGroundedFilter.Value)
                {
                    ref var playerComponent = ref _playerPool.Value.Get(playerEntity);
                    playerComponent.playerRB.AddForce(Vector3.up * playerComponent.playerJumpForce, ForceMode.VelocityChange);
                    playerComponent.Animator.Play("jump-up");
                }
            }
        }
    }
}
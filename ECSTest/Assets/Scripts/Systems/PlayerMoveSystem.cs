using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.EcsLite.Di;


namespace Runer
{
    public class PlayerMoveSystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<PlayerComponent, PlayerInputComponent>> _filter = default;
        readonly EcsPoolInject<PlayerComponent> _playerPool = default;
        readonly EcsPoolInject<PlayerInputComponent> _playerInputPool = default;

        public void Run(IEcsSystems ecsSystems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var playerComponent = ref _playerPool.Value.Get(entity);
                ref var playerInputComponent = ref _playerInputPool.Value.Get(entity);

                playerComponent.playerRB.MovePosition(playerComponent.playerTransform.position + playerComponent.playerSpeedForward * Time.deltaTime * Vector3.forward);
                playerComponent.playerRB.AddForce(playerInputComponent.moveInput * playerComponent.playerSpeed, ForceMode.Acceleration);
            }
            
        }
    }
}
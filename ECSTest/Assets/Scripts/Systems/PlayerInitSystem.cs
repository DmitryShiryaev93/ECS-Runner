using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.EcsLite.Di;

namespace Runer
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        readonly EcsWorldInject _World = default;
        readonly EcsPoolInject<PlayerComponent> _playerPool = default;
        readonly EcsPoolInject<PlayerInputComponent> _playerInputPool = default;
        readonly EcsPoolInject<GroundedComponent> _groundedComp = default;

        public void Init(IEcsSystems ecsSystems)
        {
            var gameData = ecsSystems.GetShared<GameData>();

            var playerEntity = _World.Value.NewEntity();

            _playerPool.Value.Add(playerEntity);
            ref var playerComponent = ref _playerPool.Value.Get(playerEntity);

            _playerInputPool.Value.Add(playerEntity);
            ref var playerInputComponent = ref _playerInputPool.Value.Get(playerEntity);

            var playerGO = GameObject.FindGameObjectWithTag("Player");
            var groundCheckerView = playerGO.GetComponentInChildren<GroundCheckerView>();
            playerGO.GetComponent<CollisionCheckerView>().ecsWorld = _World.Value;
            groundCheckerView.groundedPool = _groundedComp.Value;
            groundCheckerView.playerEntity = playerEntity;
            groundCheckerView.PlayerComponent = _playerPool.Value;

            playerComponent.playerSpeed = gameData.configuration.playerSpeed;
            playerComponent.playerTransform = playerGO.transform;
            playerComponent.playerJumpForce = gameData.configuration.playerJumpForce;
            playerComponent.playerSpeedForward = gameData.configuration.playerSpeedForward;
            playerComponent.playerCollider = playerGO.GetComponent<CapsuleCollider>();
            playerComponent.playerRB = playerGO.GetComponent<Rigidbody>();
            playerComponent.Animator = playerGO.GetComponentInChildren<Animator>();
        }
    }
}
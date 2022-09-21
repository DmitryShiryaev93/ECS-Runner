using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runer
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        public void Init(IEcsSystems ecsSystems)
        {
            var ecsWorld = ecsSystems.GetWorld();
            var gameData = ecsSystems.GetShared<GameData>();

            var playerEntity = ecsWorld.NewEntity();

            var playerPool = ecsWorld.GetPool<PlayerComponent>();
            playerPool.Add(playerEntity);
            ref var playerComponent = ref playerPool.Get(playerEntity);

            var playerInputPool = ecsWorld.GetPool<PlayerInputComponent>();
            playerInputPool.Add(playerEntity);
            ref var playerInputComponent = ref playerInputPool.Get(playerEntity);

            var playerGO = GameObject.FindGameObjectWithTag("Player");
            var groundCheckerView = playerGO.GetComponentInChildren<GroundCheckerView>();
            groundCheckerView.groundedPool = ecsSystems.GetWorld().GetPool<GroundedComponent>();
            groundCheckerView.playerEntity = playerEntity;
            groundCheckerView.PlayerComponent = ecsSystems.GetWorld().GetPool<PlayerComponent>();
            playerGO.GetComponent<CollisionCheckerView>().ecsWorld = ecsWorld;

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
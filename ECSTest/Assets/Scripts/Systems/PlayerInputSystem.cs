using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Runer
{
    public class PlayerInputSystem : IEcsRunSystem
    {
        Vector3 posIn;
        bool goSwap;

        public void Run(IEcsSystems ecsSystems)
        {
            var filter = ecsSystems.GetWorld().Filter<PlayerInputComponent>().End();
            var playerInputPool = ecsSystems.GetWorld().GetPool<PlayerInputComponent>();
            var tryJumpPool = ecsSystems.GetWorld().GetPool<TryJump>();

            foreach (var entity in filter)
            {
                ref var playerInputComponent = ref playerInputPool.Get(entity);
                
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    posIn = Input.mousePosition;
                    goSwap = true;
                }
                if (goSwap)
                {
                    Vector2 delta = posIn - Input.mousePosition;

                    if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                    {
                        if (delta.x < 0) playerInputComponent.moveInput = new Vector3(1, 0, 0);
                        else playerInputComponent.moveInput = new Vector3(-1, 0, 0);

                    }
                    if (Input.GetKeyUp(KeyCode.Mouse0))
                    {
                        goSwap = false;
                        playerInputComponent.moveInput = Vector3.zero;

                        if (Mathf.Abs(delta.x) < Mathf.Abs(delta.y) && delta.y < 0)
                        {
                            var tryJump = ecsSystems.GetWorld().NewEntity();
                            tryJumpPool.Add(tryJump);
                        }
                    }
                }
            }
        }
    }
}
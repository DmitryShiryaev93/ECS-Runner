using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runer
{
    public class GroundCheckerView : MonoBehaviour
    {
        public EcsPool<GroundedComponent> groundedPool;
        public EcsPool<PlayerComponent> PlayerComponent;
        public int playerEntity;

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                if (!groundedPool.Has(playerEntity))
                {
                    groundedPool.Add(playerEntity);

                    ref var playerComp = ref PlayerComponent.Get(playerEntity);
                    playerComp.Animator.Play("jump-down");
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                if (groundedPool.Has(playerEntity))
                {
                    groundedPool.Del(playerEntity);
                }
            }
        }
    }
}
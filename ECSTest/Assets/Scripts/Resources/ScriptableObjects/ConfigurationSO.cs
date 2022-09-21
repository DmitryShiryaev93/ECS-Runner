using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runer
{
    [CreateAssetMenu(fileName = "Configuration")]
    public class ConfigurationSO : ScriptableObject
    {
        public float playerJumpForce;
        public float playerSpeed;
        public float playerSpeedForward;
        public float cameraFollowSmoothness;
    }
}
using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Runer
{
    public class GameData
    {
        public ConfigurationSO configuration;
        public Text[] coinCounter;
        public GameObject playerWonPanel;
        public GameObject gameOverPanel;
        public SceneService sceneService;
        public GameObject bloodParticle;
    }
}

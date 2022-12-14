using Leopotam.EcsLite;
using Leopotam.EcsLite.ExtendedSystems;
using LeopotamGroup.Globals;
using Leopotam.EcsLite.Di;
using UnityEngine;
using UnityEngine.UI;

namespace Runer
{
    sealed class EcsStartup : MonoBehaviour 
    {
        private EcsWorld ecsWorld;
        private IEcsSystems initSystems;
        private IEcsSystems updateSystems;
        private IEcsSystems fixedUpdateSystems;

        [SerializeField] private ConfigurationSO configuration;
        [SerializeField] private Text[] coinCounter;
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private GameObject playerWonPanel;
        [SerializeField] private GameObject bloodParticle;

        void Start ()
        {
            ecsWorld = new EcsWorld ();
            var gameData = new GameData();

            gameData.configuration = configuration;
            gameData.coinCounter = coinCounter;
            gameData.gameOverPanel = gameOverPanel;
            gameData.playerWonPanel = playerWonPanel;
            gameData.sceneService = Service<SceneService>.Get(true);
            gameData.bloodParticle = bloodParticle;

            initSystems = new EcsSystems(ecsWorld, gameData)
                .Add(new PlayerInitSystem())
                .Add(new DangerousInitSystem())
                .Inject();

            initSystems.Init();


            updateSystems = new EcsSystems(ecsWorld, gameData)
                .Add(new PlayerInputSystem())
                .Add(new DangerousRunSystem())
                .Add(new DangerousHitSystem())
                .Add(new CoinHitSystem())
                .Add(new WinHitSystem())
                .DelHere<HitComponent>()
                .Inject();

            updateSystems.Init();

            fixedUpdateSystems = new EcsSystems(ecsWorld, gameData)
                .Add(new PlayerMoveSystem())
                .Add(new CameraFollowSystem())
                .Add(new PlayerJumpSystem())
                .Inject();

            fixedUpdateSystems.Init();
        }

        private void Update()
        {
            updateSystems.Run();
        }

        private void FixedUpdate()
        {
            fixedUpdateSystems.Run();
        }

        private void OnDestroy()
        {
            initSystems.Destroy();
            updateSystems.Destroy();
            fixedUpdateSystems.Destroy();
            ecsWorld.Destroy();
        }
    }
}
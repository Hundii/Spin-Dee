using Common;
using UnityEngine;

namespace Core
{
    public class RoundHandler : MonoBehaviour, INonPersistentManager
    {
        [SerializeField] private float roundSeconds;
        [SerializeField] private float bossRoundSeconds;

        private JarSO jarSO;
        private AudioManager audioManager;

        private bool isPaused;
        public int CurrentRound { get; private set; } = 1;
        public bool IsCurrentRoundBossRound { get; private set; }
        public float CurrentRoundTime { get; private set; }

        private void Awake()
        {
            var sceneData = this.Inject<SharedDataService>().GetData<GameSelectionSceneData>(false);
            jarSO = sceneData == null ? SOContainerContainer.JarSOContainer.turkoiseJar : sceneData.jarSO;
        }

        private void OnEnable()
        {
            IngameEvents.RoundContinuedByPlayer += HandleRoundContinuedByPlayer;
        }

        private void Start()
        {
            audioManager = this.Inject<AudioManager>();
            PlayMusicBasedOnRoundType();
        }

        private void Update()
        {
            if (isPaused)
            {
                return;
            }
            CurrentRoundTime += Time.deltaTime;
            HandleTimeChange();
        }

        private void HandleTimeChange()
        {
            float threshold = IsCurrentRoundBossRound ? bossRoundSeconds : roundSeconds;
            if (CurrentRoundTime < threshold)
            {
                return;
            }
            CurrentRoundTime = 0f;
            IngameEvents.RoundEnded.Invoke(CurrentRound);
            CurrentRound++;
            if (CurrentRound % (jarSO.roundsBeforeBoss + 1) == 0)
            {
                IsCurrentRoundBossRound = true;
            }
            else
            {
                IsCurrentRoundBossRound = false;
            }
        }

        public float GetCurrentRoundRemainingTime()
        {
            float threshold = IsCurrentRoundBossRound ? bossRoundSeconds : roundSeconds;
            return threshold - CurrentRoundTime;
        }

        public float GetCurrentRoundRemainingTimePercentage()
        {
            float threshold = IsCurrentRoundBossRound ? bossRoundSeconds : roundSeconds;
            return GetCurrentRoundRemainingTime() / threshold * 100;
        }

        public int GetMaxRound()
        {
            return jarSO.maxRounds;
        }

        public void HandleRoundContinuedByPlayer()
        {
            PlayMusicBasedOnRoundType();
            IngameEvents.RoundStarted.Invoke(CurrentRound);
        }

        public void PlayMusicBasedOnRoundType()
        {
            if (IsCurrentRoundBossRound)
            {
                audioManager.PlayBossMusic();
                return;
            }
            audioManager.PlayNormalMusic();
        }

        public void Pause()
        {
            isPaused = true;
        }

        public void Resume()
        {
            isPaused = false;
        }

        private void OnDisable()
        {
            IngameEvents.RoundContinuedByPlayer -= HandleRoundContinuedByPlayer;
        }
    }
}

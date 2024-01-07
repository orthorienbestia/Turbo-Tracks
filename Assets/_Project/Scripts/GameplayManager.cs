using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class GameplayManager : MonoBehaviour
    {
        public static GameplayManager Instance { get; private set; }
        private int _lapCount;
        private int _currentLap;

        public event Action OnLapComplete;
        public event Action OnGameComplete;

        public GameObject midWayPoint;
        public GameObject lapFinishPoint;

        private int _coinsCollected;

        [SerializeField] TMP_Text _coinsCollectedText;
        [SerializeField] private KartMovementController kartMovementController;
        [SerializeField] private Button revGearButton;
        [SerializeField] private TMP_Text _lapText;
        [SerializeField] private GameObject _gameCompletePanel;
        [SerializeField] private TMP_Text _finalPositionText;
        [SerializeField] private TMP_Text _speedText;
        private TMP_Text _revText;

        [SerializeField] Volume _postProcessingVolume;
        private Vignette _vignette;
        private const float VignetteIntensity = 0.33f;
        private MotionBlur _motionBlur;
        private const float MotionBlurIntensity = 0.55f;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }

            _lapCount = PlayerPrefs.GetInt(AppConstants.LapCountPrefKey, 1);
            _currentLap = 1;
            _lapText.text = $"Lap: {_currentLap}/{_lapCount}";

            midWayPoint.SetActive(true);
            lapFinishPoint.SetActive(false);

            _revText = revGearButton.GetComponentInChildren<TMP_Text>();
            
            _postProcessingVolume.profile.TryGet(out _vignette);
            _vignette.intensity.value = 0;
            
            _postProcessingVolume.profile.TryGet(out _motionBlur);
            _motionBlur.intensity.value = 0;
            
            Time.timeScale = 0;
            StartCoroutine(StartGame());
        }

        IEnumerator StartGame()
        {
            yield return new WaitForSecondsRealtime(4);
            Time.timeScale = 1;
        }

        private static readonly Color revGearColor = new Color(0.9960785f, 0.6431373f, 0);
        private static readonly Color normalGearColor = new Color(0.4901961f, 0.9960784f, 0);
        private void Start()
        {
            _coinsCollectedText.text = _coinsCollected.ToString();
            revGearButton.onClick.AddListener(() =>
            {
                kartMovementController.GearChanged();
                revGearButton.image.color = kartMovementController.IsReverseGear ? revGearColor : normalGearColor;
                _revText.text = kartMovementController.IsReverseGear ? "R" : "N";
            });
            revGearButton.image.color = kartMovementController.IsReverseGear ? revGearColor : normalGearColor;
            _revText.text = kartMovementController.IsReverseGear ? "R" : "N";
        }
#if UNITY_EDITOR
        private void Update()
        {
            // Reload scene
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
#endif

        private void LateUpdate()
        {
            _speedText.text = $"{Mathf.RoundToInt(kartMovementController.CurrentSpeed).ToString()} KPH";
        }

        public void BackToHomeScene()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene((int)Scenes.HomeScene);
        }

        private void CompleteLap()
        {
            Debug.Log("Lap Complete: " + _currentLap + " of " + _lapCount + " laps");
            OnLapComplete?.Invoke();
            _currentLap++;
            if (_currentLap > _lapCount)
            {
                Debug.Log("Game Complete");
                OnGameComplete?.Invoke();
                _gameCompletePanel.SetActive(true);
                _finalPositionText.text = AIKartHandler.hasReachedTarget ? "2nd" : "1st";
                Time.timeScale = 0;
            }
            else
            {
                _lapText.text = $"Lap: {_currentLap}/{_lapCount}";
            }
        }

        public void CrossedWayPoint(GameObject wayPoint)
        {
            if (wayPoint == midWayPoint)
            {
                midWayPoint.SetActive(false);
                lapFinishPoint.SetActive(true);
            }
            else
            {
                midWayPoint.SetActive(true);
                lapFinishPoint.SetActive(false);

                CompleteLap();
            }
        }

        public void CollectCoin()
        {
            _coinsCollected++;
            _coinsCollectedText.text = _coinsCollected.ToString();
        }

        public void ToggleTurboBoostEffect(bool value)
        {
            if(_turboBoostEffectCoroutine != null)
                StopCoroutine(_turboBoostEffectCoroutine);
            _turboBoostEffectCoroutine = StartCoroutine(_TurboBoostEffect(value));
        }
        
        private Coroutine _turboBoostEffectCoroutine;
        private IEnumerator _TurboBoostEffect(bool value)
        {
            const float duration = 1f;
            if (value)
            {
                for (float t = 0; t < duration; t += Time.deltaTime)
                {
                    _vignette.intensity.value = Mathf.Lerp(0, VignetteIntensity, t / duration);
                    _motionBlur.intensity.value = Mathf.Lerp(0, MotionBlurIntensity, t / duration);
                    yield return null;
                }
            }
            else
            {
                for (float t = 0; t < duration; t += Time.deltaTime)
                {
                    _vignette.intensity.value = Mathf.Lerp(VignetteIntensity, 0, t / duration);
                    _motionBlur.intensity.value = Mathf.Lerp(MotionBlurIntensity, 0, t / duration);
                    yield return null;
                }
            }
        }
    }
}
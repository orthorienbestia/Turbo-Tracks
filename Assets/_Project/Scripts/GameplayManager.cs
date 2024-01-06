using System;
using System.Collections;
using TMPro;
using UnityEngine;
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
        private TMP_Text _revText;
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
            
            Time.timeScale = 0;
            StartCoroutine(StartGame());
        }

        IEnumerator StartGame()
        {
            yield return new WaitForSecondsRealtime(4);
            Time.timeScale = 1;
        }

        private static readonly Color revGearColor = new Color(0.9960785f,0.6431373f,0);
        private static readonly Color normalGearColor = new Color(0.4901961f,0.9960784f,0);
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

        private void Update()
        {
            // Reload scene
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    
        public void BackToHomeScene()
        {
            SceneManager.LoadScene((int)Scenes.HomeScene);
        }

        private void CompleteLap()
        {
            Debug.Log("Lap Complete: "+ _currentLap + " of " + _lapCount + " laps");
            OnLapComplete?.Invoke();
            _currentLap++;
            if (_currentLap > _lapCount)
            {
                Debug.Log("Game Complete");
                OnGameComplete?.Invoke();
                _gameCompletePanel.SetActive(true);
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
    }
}

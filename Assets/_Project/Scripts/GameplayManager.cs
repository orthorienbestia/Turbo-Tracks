using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts
{
    public class GameplayManager : MonoBehaviour
    {
        public static GameplayManager Instance { get; private set; }
        private int _lapCount;
        private int _currentLap;
    
        public event System.Action OnLapComplete;
        public event System.Action OnGameComplete;
    
        public GameObject midWayPoint;
        public GameObject lapFinishPoint;
    
        private int _coinsCollected;
        
        [SerializeField] TMP_Text _coinsCollectedText;
    
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
        
            midWayPoint.SetActive(true);
            lapFinishPoint.SetActive(false);
            Time.timeScale = 0;
            StartCoroutine(StartGame());
        }

        IEnumerator StartGame()
        {
            yield return new WaitForSecondsRealtime(4);
            Time.timeScale = 1;
        }

        private void Start()
        {
            _coinsCollectedText.text = _coinsCollected.ToString();
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

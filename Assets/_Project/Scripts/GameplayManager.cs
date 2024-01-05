using UnityEngine;
// Handle logic for laps, time, and score
public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance { get; private set; }

    private const string LapCountPrefKey = "LapCount";
    
    private int _lapCount;
    private int _currentLap;
    
    public event System.Action OnLapComplete;
    public event System.Action OnGameComplete;
    
    public GameObject midWayPoint;
    public GameObject lapFinishPoint;
    
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
        
        _lapCount = PlayerPrefs.GetInt(LapCountPrefKey, 1);
        _currentLap = 1;
        
        midWayPoint.SetActive(true);
        lapFinishPoint.SetActive(false);
    }

    private void CompleteLap()
    {
        Debug.Log("Lap Complete");
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
}

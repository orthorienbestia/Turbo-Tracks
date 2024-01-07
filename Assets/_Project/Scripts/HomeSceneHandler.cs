using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class HomeSceneHandler : MonoBehaviour
    {
        [SerializeField] private GameObject carModifyPanel;
        [SerializeField] private Button carModifyButton;

        [SerializeField] private Slider _lapSlider;
        [SerializeField] private TMP_Text _lapText;

        [SerializeField] private TMP_Text _coinsCollectedText;
        private const string LapTextPrefix = "Lap Count : ";

        private void Awake()
        {
            Application.targetFrameRate = 60;
        }

        private void Start()
        {
            carModifyPanel.SetActive(false);
            carModifyButton.onClick.AddListener(() => carModifyPanel.SetActive(true));

            _lapSlider.value = PlayerPrefs.GetInt(AppConstants.LapCountPrefKey, 1);
            _lapText.text = LapTextPrefix + _lapSlider.value.ToString(CultureInfo.InvariantCulture);
            _lapSlider.onValueChanged.AddListener(OnLapSliderValueChanged);
            _coinsCollectedText.text = PlayerPrefs.GetInt(AppConstants.CoinsPrefKey, 0).ToString();
        }

        private void OnLapSliderValueChanged(float val)
        {
            PlayerPrefs.SetInt(AppConstants.LapCountPrefKey, (int)val);
            _lapText.text = LapTextPrefix + _lapSlider.value.ToString(CultureInfo.InvariantCulture);
        }

        public void LoadGameScene()
        {
            SceneManager.LoadScene((int)Scenes.GameScene);
        }
    }
}

internal enum Scenes
{
    HomeScene = 0,
    GameScene = 1
}
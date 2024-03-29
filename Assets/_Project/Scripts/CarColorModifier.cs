using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace _Project.Scripts
{
    public class CarColorModifier : MonoBehaviour
    {
        private const float Saturation = 0.63f;
        private const float Value = 1f;

        [SerializeField] private Renderer[] renderers;
        [SerializeField] private Material material;

        [SerializeField] private Slider slider;
        [SerializeField] private Button randomColorButton;
        [SerializeField] private Button closeButton;
        private static readonly int ColorShaderProperty = Shader.PropertyToID("_BaseColor");
        private static readonly int ReceiveShadowsShaderProperty = Shader.PropertyToID("_ReceiveShadows");

        private void Awake()
        {
            slider.onValueChanged.AddListener(OnSliderValueChanged);
            randomColorButton.onClick.AddListener(OnRandomColorButtonClicked);
            closeButton.onClick.AddListener(OnCloseButtonClicked);

            var materialInstance = Instantiate(material);
            materialInstance.SetFloat(ReceiveShadowsShaderProperty, 0);
            foreach (var rendererItem in renderers)
            {
                rendererItem.material = materialInstance;
            }
        }

        private void Start()
        {
            var color = GetColorFromPlayerPrefs();
            ApplyColorToCar(color);
            Color.RGBToHSV(color, out var hue, out _, out _);
            slider.value = hue;
        }

        private void OnDestroy()
        {
            slider.onValueChanged.RemoveListener(OnSliderValueChanged);
            randomColorButton.onClick.RemoveListener(OnRandomColorButtonClicked);
            closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        }

        private void OnSliderValueChanged(float value)
        {
            var color = Color.HSVToRGB(value, Saturation, Value);
            ApplyColorToCar(color);
            SaveColorToPlayerPrefs(color);
        }

        private void OnRandomColorButtonClicked()
        {
            var color = Color.HSVToRGB(Random.Range(0f, 1f), Saturation, Value);
            ApplyColorToCar(color);
            SaveColorToPlayerPrefs(color);
            Color.RGBToHSV(color, out var hue, out _, out _);
            slider.value = hue;
        }

        private void ApplyColorToCar(Color color)
        {
            foreach (var rendererItem in renderers)
            {
                rendererItem.material.SetColor(ColorShaderProperty, color);
            }
        }

        private void SaveColorToPlayerPrefs(Color color)
        {
            PlayerPrefs.SetFloat(AppConstants.ColorPlayerPrefsKey + "R", color.r);
            PlayerPrefs.SetFloat(AppConstants.ColorPlayerPrefsKey + "G", color.g);
            PlayerPrefs.SetFloat(AppConstants.ColorPlayerPrefsKey + "B", color.b);
        }

        private Color GetColorFromPlayerPrefs()
        {
            return new Color(PlayerPrefs.GetFloat(AppConstants.ColorPlayerPrefsKey + "R", 1f),
                PlayerPrefs.GetFloat(AppConstants.ColorPlayerPrefsKey + "G", 1f),
                PlayerPrefs.GetFloat(AppConstants.ColorPlayerPrefsKey + "B", 1f));
        }

        private void OnCloseButtonClicked()
        {
            gameObject.SetActive(false);
        }
    }
}
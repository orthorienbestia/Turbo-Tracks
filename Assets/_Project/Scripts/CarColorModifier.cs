using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace _Project.Scripts
{
    public class CarColorModifier : MonoBehaviour
    {
        private const string ColorPlayerPrefsKey = "CarColor";
        
        private const float Saturation = 0.63f;
        private const float Value = 1f;
        
        [SerializeField] private Renderer[] renderers;
        [SerializeField] private Material material;
        
        [SerializeField] private Slider slider;
        [SerializeField] private Button randomColorButton;
        [SerializeField] private Button closeButton;
        private static readonly int ColorShaderProperty = Shader.PropertyToID("_BaseColor");

        private void Awake()
        {
            slider.onValueChanged.AddListener(OnSliderValueChanged);
            randomColorButton.onClick.AddListener(OnRandomColorButtonClicked);
            closeButton.onClick.AddListener(OnCloseButtonClicked);
            
            // Create new material instance to avoid changing the original material.
            var materialInstance = Instantiate(material);
            foreach (var rendererItem in renderers)
            {
                rendererItem.material = materialInstance;
            }
        }
        
        private void Start()
        {
            var color = GetColorFromPlayerPrefs();
            SetColor(color);
            slider.value = color.r;
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
            SetColor(color);
            SaveColorToPlayerPrefs(color);
        }
        
        private void OnRandomColorButtonClicked()
        {
            var color = Color.HSVToRGB(Random.Range(0f,1f), Saturation, Value);
            SetColor(color);
            SaveColorToPlayerPrefs(color);
            Color.RGBToHSV(color, out var hue, out _, out _);
            slider.value = hue;
        }
        
        private void SetColor(Color color)
        {
            foreach (var rendererItem in renderers)
            {
                rendererItem.material.SetColor(ColorShaderProperty, color);
            }
        }
        
        private void SaveColorToPlayerPrefs(Color color)
        {
            PlayerPrefs.SetFloat(ColorPlayerPrefsKey + "R", color.r);
            PlayerPrefs.SetFloat(ColorPlayerPrefsKey + "G", color.g);
            PlayerPrefs.SetFloat(ColorPlayerPrefsKey + "B", color.b);
        }
        
        private Color GetColorFromPlayerPrefs()
        {
            return new Color(PlayerPrefs.GetFloat(ColorPlayerPrefsKey + "R", 1f),
                PlayerPrefs.GetFloat(ColorPlayerPrefsKey + "G", 1f),
                PlayerPrefs.GetFloat(ColorPlayerPrefsKey + "B", 1f));
        }
        
        private void OnCloseButtonClicked()
        {
            gameObject.SetActive(false);
        }
        
    }
}

using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class CarColorSetter : MonoBehaviour
    {
        private static readonly int ColorShaderProperty = Shader.PropertyToID("_BaseColor");

        private void Start()
        {
            var skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
            if (skinnedMeshRenderer == null)
            {
                Debug.LogError("No SkinnedMeshRenderer found");
                return;
            }

            skinnedMeshRenderer.material.SetColor(ColorShaderProperty, GetColorFromPlayerPrefs());
        }

        private Color GetColorFromPlayerPrefs()
        {
            return new Color(PlayerPrefs.GetFloat(AppConstants.ColorPlayerPrefsKey + "R", 1f),
                PlayerPrefs.GetFloat(AppConstants.ColorPlayerPrefsKey + "G", 1f),
                PlayerPrefs.GetFloat(AppConstants.ColorPlayerPrefsKey + "B", 1f));
        }
    }
}
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace _Project.Scripts.Utility
{
    /// <summary>
    /// Class helps to debug by logging text from editor.
    /// Functions can be called in inspector from unity events and triggers.
    /// </summary>
    public class MonoLogger : MonoBehaviour
    {
        public void Log(string text)
        {
            Debug.Log(text);
        }

        public void LogWarning(string text)
        {
            Debug.LogWarning(text);
        }

        public void LogError(string text)
        {
            Debug.LogError(text);
        }
    }
}
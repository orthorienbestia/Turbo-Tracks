using DG.Tweening;
using UnityEngine;

// Continuous scale animation scale up and down. 
namespace _Project.Scripts.Utility
{
    public class ScaleAnimation : MonoBehaviour
    {
        public float maxScale;
        public float minScale;
        public float waveDuration = 1f;
    
        private Vector3 _currentScale;
    
        private void Awake()
        {
            _currentScale = transform.localScale;
        }
    
        private void Update()
        {
            transform.localScale = Vector3.Lerp(Vector3.one *minScale,Vector3.one* maxScale, Mathf.PingPong(Time.time, waveDuration));
            Debug.Log(transform.localScale);
        }
    }
}

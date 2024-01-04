using System;
using UnityEngine;

namespace _Project.Scripts.Collectables
{
    public abstract class Collectable : MonoBehaviour
    {
        public event Action<Collider> OnObjectCollected;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            Debug.Log("Object Picked Up: " + gameObject.name + " by " + other.gameObject.name);
            
            OnObjectCollected?.Invoke(other);
            ObjectCollected(other);
        }

        protected abstract void ObjectCollected(Collider other);
    }
}
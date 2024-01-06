using System;
using UnityEngine;

namespace _Project.Scripts.Collectables
{
    public abstract class Collectable : MonoBehaviour
    {
        public Action<Collider> OnObjectCollected;
        
        protected virtual void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            Debug.Log("Object Picked Up: " + gameObject.name + " by " + other.gameObject.name);
            
            OnObjectCollected?.Invoke(other);
            ObjectCollected(other);
            
            var kartMovementController = other.GetComponent<KartMovementController>();
            kartMovementController.GetCollectable(this);
        }

        protected abstract void ObjectCollected(Collider other);
    }
}
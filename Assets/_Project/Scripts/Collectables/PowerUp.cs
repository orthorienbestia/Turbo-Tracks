using UnityEngine;

namespace _Project.Scripts.Collectables
{
    
    public abstract class PowerUp : Collectable
    {
        // PowerUp duration in seconds.
        [SerializeField] protected float duration = 2.5f;
        protected static bool isApplied;
        
        protected override void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player") || isApplied) return;
            Debug.Log("Object Picked Up: " + gameObject.name + " by " + other.gameObject.name);
            
            OnObjectCollected?.Invoke(other);
            ObjectCollected(other);
            
            var kartMovementController = other.GetComponent<KartMovementController>();
            kartMovementController.GetCollectable(this);
        }
        
        protected override void ObjectCollected(Collider other)
        {
            if(isApplied) return;
            ApplyPowerUp(other.GetComponent<KartMovementController>());
            Destroy(gameObject,duration+5);
        }

        protected abstract void ApplyPowerUp(KartMovementController kartMovementController);
    }
}

using UnityEngine;

namespace _Project.Scripts.Collectables
{
    
    public abstract class PowerUp : Collectable
    {
        // PowerUp duration in seconds.
        [SerializeField] protected float duration = 2.5f;
        
        protected override void ObjectCollected(Collider other)
        {
            ApplyPowerUp(other.GetComponent<KartMovementController>());
            Destroy(gameObject,10);
        }

        protected abstract void ApplyPowerUp(KartMovementController kartMovementController);
    }
}

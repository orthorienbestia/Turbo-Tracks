using UnityEngine;

namespace _Project.Scripts.Collectables
{
    
    public abstract class PowerUp : Collectable
    {
        // PowerUp duration in seconds.
        [SerializeField] protected float duration = -1;
        
        protected override void ObjectCollected(Collider other)
        {
            ApplyPowerUp(other.GetComponent<KartMovementController>());
        }

        protected abstract void ApplyPowerUp(KartMovementController kartMovementController);
    }
}

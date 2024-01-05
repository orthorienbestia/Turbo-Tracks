using UnityEngine;

namespace _Project.Scripts.Collectables
{
    public class TurboBoostPowerUp : PowerUp
    {
        [SerializeField] private float topSpeedIncrease = 10f;
        [SerializeField] private float torqueIncrease = 10f;
        
        protected override void ApplyPowerUp(KartMovementController kartMovementController)
        {
            
        }
    }
}
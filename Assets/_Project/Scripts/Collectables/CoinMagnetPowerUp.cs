using UnityEngine;

namespace _Project.Scripts.Collectables
{
    public class CoinMagnetPowerUp : PowerUp
    {
        [SerializeField] private float magnetRadius = 10f;
        
        protected override void ApplyPowerUp(KartMovementController kartMovementController)
        {
            
        }
    }
}
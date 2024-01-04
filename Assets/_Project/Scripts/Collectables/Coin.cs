using UnityEngine;

namespace _Project.Scripts.Collectables
{
    public class Coin : Collectable
    {
        protected override void ObjectCollected(Collider other)
        {
            // TODO: Implement Coin
            var kartMovementController = other.GetComponent<KartMovementController>();
            kartMovementController.CollectCoin(this);
        }
    }
}
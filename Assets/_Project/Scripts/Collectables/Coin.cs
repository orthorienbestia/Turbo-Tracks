using UnityEngine;

namespace _Project.Scripts.Collectables
{
    public class Coin : Collectable
    {
        protected override void ObjectCollected(Collider other)
        {
            GameplayManager.Instance.CollectCoin();
            PlayerPrefs.SetInt(AppConstants.CoinsPrefKey, PlayerPrefs.GetInt(AppConstants.CoinsPrefKey, 0) + 1);
            Destroy(gameObject, 5);
        }
    }
}
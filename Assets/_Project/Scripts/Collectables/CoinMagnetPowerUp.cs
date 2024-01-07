using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Collectables
{
    public class CoinMagnetPowerUp : PowerUp
    {
        [FormerlySerializedAs("magnetRadius")] [SerializeField]
        private float magnetRadiusMultiplier = 2f;

        protected override void ApplyPowerUp(KartMovementController kartMovementController)
        {
            if (isApplied) return;
            kartMovementController.MagnetRadius *= magnetRadiusMultiplier;
            kartMovementController.coinMagnetEffectGameObject.SetActive(true);
            isApplied = true;

            Debug.Log("Coin Magnet Applied");
            StartCoroutine(RemovePowerUp(kartMovementController));
        }

        private IEnumerator RemovePowerUp(KartMovementController kartMovementController)
        {
            if (!isApplied) yield break;
            yield return new WaitForSeconds(duration);
            kartMovementController.MagnetRadius /= magnetRadiusMultiplier;
            kartMovementController.coinMagnetEffectGameObject.SetActive(false);
            isApplied = false;

            Debug.Log("Coin Magnet Removed");
        }
    }
}
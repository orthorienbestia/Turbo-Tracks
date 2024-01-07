using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Collectables
{
    public class CoinMagnetPowerUp : PowerUp
    {
        [FormerlySerializedAs("magnetRadius")] [SerializeField]
        private float magnetRadiusMultiplier = 2f;

        private static bool _isApplied;

        protected override void ApplyPowerUp(KartMovementController kartMovementController)
        {
            if (_isApplied) return;
            kartMovementController.MagnetRadius *= magnetRadiusMultiplier;
            kartMovementController.coinMagnetEffectGameObject.SetActive(true);
            _isApplied = true;

            Debug.Log("Coin Magnet Applied");
            StartCoroutine(RemovePowerUp(kartMovementController));
        }

        private IEnumerator RemovePowerUp(KartMovementController kartMovementController)
        {
            if (!_isApplied) yield break;
            yield return new WaitForSeconds(duration);
            kartMovementController.MagnetRadius /= magnetRadiusMultiplier;
            kartMovementController.coinMagnetEffectGameObject.SetActive(false);
            _isApplied = false;

            Debug.Log("Coin Magnet Removed");
        }
    }
}
using System.Collections;
using UnityEngine;

namespace _Project.Scripts.Collectables
{
    public class TurboBoostPowerUp : PowerUp
    {
        [SerializeField] private float topSpeedIncrease = 10f;


        protected override void ApplyPowerUp(KartMovementController kartMovementController)
        {
            if (isApplied) return;
            kartMovementController._topSpeed += topSpeedIncrease;
            GameplayManager.Instance.ToggleTurboBoostEffect(true);
            isApplied = true;

            Debug.Log("Turbo Boost Applied");
            StartCoroutine(RemovePowerUp(kartMovementController));
        }

        private IEnumerator RemovePowerUp(KartMovementController kartMovementController)
        {
            if (!isApplied) yield break;
            yield return new WaitForSeconds(duration);
            kartMovementController._topSpeed -= topSpeedIncrease;
            GameplayManager.Instance.ToggleTurboBoostEffect(false);
            isApplied = false;

            Debug.Log("Turbo Boost Removed");
        }
    }
}
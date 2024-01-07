using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class HUD : MonoBehaviour
{
    [SerializeField] private TMP_Text _speedText;

    [FormerlySerializedAs("_carController")] [SerializeField]
    private KartMovementController kartMovementController;

    private string _speedType;

    private void Start()
    {
        _speedType = " KPH";
    }

    private void LateUpdate()
    {
        _speedText.text = Mathf.RoundToInt(kartMovementController.CurrentSpeed).ToString() + _speedType;
    }
}
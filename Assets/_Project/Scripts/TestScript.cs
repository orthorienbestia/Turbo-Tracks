using _Project.Scripts;
using TMPro;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private TMP_Text _text;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
        GyroscopeHandler.OnGyroscopeUpdate += OnGyroscopeUpdate;
    }

    private void OnGyroscopeUpdate(Vector3 obj)
    {
        _text.text = obj.ToString();
    }
}

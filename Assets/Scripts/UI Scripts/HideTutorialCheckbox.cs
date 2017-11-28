using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class HideTutorialCheckbox : MonoBehaviour
{
    private Toggle _toggle;

    // Use this for initialization
    private void Start()
    {
        _toggle = GetComponent<Toggle>();
        _toggle.isOn = GameManager.Instance.SaveData.HideTutorials;
        _toggle.onValueChanged.AddListener((value) => Toggled(value));
    }

    private void Toggled(bool value)
    {
        GameManager.Instance.SaveData.HideTutorials = value;
    }
}

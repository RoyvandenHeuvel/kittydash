using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class VisibleJoystickCheckbox : MonoBehaviour
{
    private Toggle _toggle;

    // Use this for initialization
    private void Start()
    {
        _toggle = GetComponent<Toggle>();
        _toggle.isOn = GameManager.Instance.SaveData.JoystickVisible;
        _toggle.onValueChanged.AddListener((value) => Toggled(value));
    }

    private void Toggled(bool value)
    {
        GameManager.Instance.SaveData.JoystickVisible = value;
    }

    private void Update()
    {
        _toggle.enabled = GameManager.Instance.SaveData.SelectedControls.Equals(Controls.Joystick);
    }
}

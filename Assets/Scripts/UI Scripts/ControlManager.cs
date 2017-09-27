using Assets.Scripts;
using UnityEngine;

public class ControlManager : MonoBehaviour {

    public GameObject DPadControls;
    public GameObject JoystickControls;

    private void Update()
    {
        switch (GameManager.Instance.SaveData.SelectedControls)
        {
            case Controls.DPads:
                DPadControls.SetActive(true);
                JoystickControls.SetActive(false);
                break;
            case Controls.Joystick:
                JoystickControls.SetActive(true);
                DPadControls.SetActive(false);
                break;
            default:
                break;
        }
    }
}

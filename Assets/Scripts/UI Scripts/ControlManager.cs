using Assets.Scripts;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ControlManager : MonoBehaviour
{

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
                var images = JoystickControls.GetComponentsInChildren<Image>();
                if (GameManager.Instance.SaveData.JoystickVisible)
                {
                    foreach (var image in images)
                    {
                        image.color = new Color(255, 255, 255, 255);
                    }

                }
                else
                {
                    foreach (var image in images)
                    {
                        image.color = new Color(255, 255, 255, 0);
                    }
                }
                JoystickControls.GetComponent<Image>().color = new Color(0, 0, 0, 0);
                DPadControls.SetActive(false);
                break;
            default:
                break;
        }
    }
}

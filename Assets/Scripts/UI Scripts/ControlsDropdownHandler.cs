using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI_Scripts
{
    [RequireComponent(typeof(Dropdown))]
    public class ControlsDropdownHandler : MonoBehaviour
    {
        private Dropdown _dropdown;

        private void Start()
        {
            _dropdown = GetComponent<Dropdown>();

            _dropdown.value = (int)GameManager.Instance.SaveData.SelectedControls;

            _dropdown.onValueChanged.AddListener(delegate
            {
                OnChanged();
            });

        }

        public void OnChanged()
        {
            switch (_dropdown.value)
            {
                case 0:
                    GameManager.Instance.SaveData.SelectedControls = Controls.DPads;
                    break;
                case 1:
                    GameManager.Instance.SaveData.SelectedControls = Controls.Joystick;
                    break;
                default:
                    break;
            }
        }
    }
}
using GooglePlayGames;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI_Scripts
{
    [RequireComponent(typeof(Button))]
    public class LeaderboardButton : MonoBehaviour
    {
        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(GameManager.Instance.ShowLeaderboard);
        }

        private void Update()
        {
            _button.interactable = PlayGamesPlatform.Instance.IsAuthenticated();
        }
    }
}

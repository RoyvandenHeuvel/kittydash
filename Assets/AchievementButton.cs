using Assets.Scripts;
using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AchievementButton : MonoBehaviour
{

    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(GameManager.Instance.ShowAchievements);
    }

    private void Update()
    {
        _button.interactable = PlayGamesPlatform.Instance.IsAuthenticated();
    }

}

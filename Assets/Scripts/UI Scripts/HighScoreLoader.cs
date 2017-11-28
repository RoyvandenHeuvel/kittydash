using UnityEngine;
using System.Collections;
using Assets.Scripts;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class HighScoreLoader : MonoBehaviour
{
    void Start()
    {
        Social.ShowLeaderboardUI();
    }
}
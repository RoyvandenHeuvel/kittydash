using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public static class HighScoreUtilities
    {
        public static void PostScores(int score)
        {
            PlayGamesPlatform.Activate();
            Social.ReportScore(score, GPGSIds.leaderboard_leaderboard, (bool success) =>
            {
                Debug.Log(success);
            });
        }

        public static void GetScores()
        {
            GooglePlayGames.PlayGamesPlatform.Activate();
            PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_leaderboard);

        }
    }
}

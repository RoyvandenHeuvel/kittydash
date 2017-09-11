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
        private const string secretKey = "kitty_dash_highscore_key"; // Edit this value and make sure it's the same as the one stored on the server
        private const string addScoreURL = "http://localhost/addscore.php?";
        private const string highscoreURL = "http://localhost/display.php";

        public static IEnumerator PostScores(string name, int score)
        {
            // Supply it with a string representing the players name and the players score.
            string hash = MD5Utilities.Md5Sum(name + score + secretKey);

            string post_url = addScoreURL + "name=" + WWW.EscapeURL(name) + "&score=" + score + "&hash=" + hash;

            // Post the URL to the site and create a download object to get the result.
            WWW hs_post = new WWW(post_url);
            yield return hs_post; // Wait until the download is done
        }

        public static IEnumerator GetScores(Text text)
        {
            text.text = "Loading scores...";
            WWW hs_get = new WWW(highscoreURL);
            yield return hs_get;

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                text.text = "You don't have an active internet connection.";
            }

            text.text = hs_get.text; // this is a GUIText that will display the scores in game.
        }
    }
}

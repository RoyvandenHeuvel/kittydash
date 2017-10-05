using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance = null;

        public static GameManager Instance
        {
            get
            {
                return _instance;
            }
        }

        public SaveData SaveData;
        public GameData GameData;

        private string saveLocation;

        void Awake()
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
                .Build();

            GooglePlayGames.PlayGamesPlatform.InitializeInstance(config);
            GooglePlayGames.PlayGamesPlatform.Activate();

            Social.localUser.Authenticate((bool success) =>
            {
                Debug.Log(success);
                if (!success)
                {
                    Application.Quit();
                }
            });

            if (_instance == null)
            {
                saveLocation = Application.persistentDataPath + "/save.dat";
                Load();
                _instance = this;
            }
            else if (Instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

        void OnApplicationQuit()
        {
            Save();
        }

        void OnApplicationPause()
        {
            Save();
        }

        public void ShowAchievements()
        {
            PlayGamesPlatform.Instance.ShowAchievementsUI((UIStatus status) => { Debug.Log(status); });
        }

        public void ShowLeaderboard()
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_leaderboard);
        }

        public void PostHighScore()
        {
            int score = GameData.Coins;
            SaveData.Coins += score;
            GameData.Coins = 0;

            Debug.Log(string.Format("Total coins: {0}", SaveData.Coins));

            PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_hungry_kitty, SaveData.Coins * 0.1d,
                (bool success) => { Debug.Log("Achievement progress updated? " + success); }
            );

            PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_out_of_lives, 1,
                (bool success) => { Debug.Log("Achievement progress updated? " + success); }
            );

            PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_very_hungry_kitty, score,
                (bool success) => { Debug.Log("Achievement progress updated? " + success); }
            );

            PlayGamesPlatform.Instance.ReportScore(score, GPGSIds.leaderboard_leaderboard, (bool success) =>
            {
                Debug.Log(string.Format("Posting score to leaderboard was a success? {0}", success));
            });

            ShowLeaderboard();

            UnityEngine.SceneManagement.SceneManager.LoadScene("Mainmenu");
        }

        public void Save()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(saveLocation, FileMode.OpenOrCreate);

            bf.Serialize(fs, SaveData);
            fs.Close();
        }

        public void Load()
        {
            if (File.Exists(saveLocation))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = File.Open(saveLocation, FileMode.Open);

                SaveData = (SaveData)bf.Deserialize(fs);
                fs.Close();
            }
        }
    }
}

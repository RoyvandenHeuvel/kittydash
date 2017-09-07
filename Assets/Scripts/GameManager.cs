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

        public GameData GameData;

        private string saveLocation;

        void Awake()
        {
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

        public void Save()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(saveLocation, FileMode.OpenOrCreate);

            bf.Serialize(fs, GameData);
            fs.Close();
        }

        public void Load()
        {
            if (File.Exists(saveLocation))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = File.Open(saveLocation, FileMode.Open);

                GameData = (GameData)bf.Deserialize(fs);
                fs.Close();
            }
        }
    }
}

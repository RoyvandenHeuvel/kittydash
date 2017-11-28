
using Assets.Scripts;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverOverlay;
    [SerializeField] private GameObject newRecord;
    [SerializeField] private AudioClip gameOverMusic;

    private Canvas parentCanvas;

    private static GameOverManager _instance;

    public static GameOverManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Start()
    {
        _instance = this;
        parentCanvas = gameOverOverlay.GetComponentInParent<Canvas>();
    }

    public void GoToScene(string sceneName)
    {
        GameManager.Instance.PostHighScore();
        KittyDashSceneManager.LoadScene(sceneName);
    }

    public void GameOver()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        EnemyFiniteStateMachine[] stateMachines = FindObjectsOfType<EnemyFiniteStateMachine>();
        CameraMovement cameraMovement = FindObjectOfType<CameraMovement>();
        Player player = FindObjectOfType<Player>();
        CloseCallScript closeCallScript = FindObjectOfType<CloseCallScript>();

        foreach (var enemy in enemies)
        {
            enemy.GetComponentInChildren<Animator>().enabled = false;
            enemy.enabled = false;
        }

        foreach (var stateMachine in stateMachines)
        {
            stateMachine.enabled = false;
        }
        closeCallScript.enabled = false;
        cameraMovement.enabled = false;
        player.GetComponentInChildren<Animator>().enabled = false;
        player.GetComponent<AudioSource>().enabled = false;
        player.enabled = false;

        for (int i = 0; i < parentCanvas.transform.childCount; i++)
        {
            var child = parentCanvas.transform.GetChild(i).gameObject;
            if (child != null)
            {
                child.SetActive(false);
            }
        }

        gameOverOverlay.SetActive(true);
        newRecord.SetActive(false);

        Tile.MusicHandler.GetComponent<AudioSource>().clip = gameOverMusic;
        Tile.MusicHandler.GetComponent<AudioSource>().Play();

        if (GameManager.Instance.SaveData.PersonalBest < GameManager.Instance.GameData.Coins)
        {
            newRecord.SetActive(true);
        }
    }
}

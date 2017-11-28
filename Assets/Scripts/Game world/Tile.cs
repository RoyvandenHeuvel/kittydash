using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject TransitionTile;
    public GameObject NextTile;
    public List<GameObject> DecorationGroups;
    public List<GameObject> ObstacleCollectableGroups;
    public float TileHeight;
    public AudioClip Music;
    [Range(0f, 1f)]
    public float MusicVolume;

    private static GameObject _musicHandler;
    public static GameObject MusicHandler
    {
        get
        {
            return _musicHandler;
        }
    }

    private const int tileChangesRequired = 2;
    private static bool _transitioning;
    private static int _changedTileCount = 0;
    private static GameObject TileToChangeTo;
    private static int _tileChanges = 1;

    void Start()
    {
        Debug.Log("Tile instantiated. Time since game started = " + Player.TimeSinceStart);


        if (_musicHandler == null)
        {
            _musicHandler = new GameObject("Background Music Handler");
            _musicHandler.AddComponent<AudioSource>();
            _musicHandler.GetComponent<AudioSource>().volume = MusicVolume;
            _musicHandler.GetComponent<AudioSource>().loop = true;
        }

        if ((_musicHandler.GetComponent<AudioSource>().clip != null && !_musicHandler.GetComponent<AudioSource>().clip.name.Equals(Music.name)) || _musicHandler.GetComponent<AudioSource>().clip == null)
        {
            _musicHandler.GetComponent<AudioSource>().clip = Music;
            _musicHandler.GetComponent<AudioSource>().Play();
        }

        if (_transitioning)
        {
            if (_changedTileCount <= tileChangesRequired)
            {
                _changedTileCount++;
                NextTile = TileToChangeTo;
            }
            else
            {
                _transitioning = false;
                _changedTileCount = 0;
            }
        }

        if (Player.TimeSinceStart - (GameManager.Instance.GameData.TimeTileInterval * _tileChanges) >= 0)
        {
            _tileChanges++;
            NextTile = TransitionTile;
            TileToChangeTo = TransitionTile.GetComponent<Tile>().NextTile;
            _changedTileCount++;
            _transitioning = true;
        }


        gameObject.transform.localPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);

        if (DecorationGroups.Count > 0)
        {
            var decoration = DecorationGroups[Random.Range(0, DecorationGroups.Count)];

            var decorationInstantiated = Instantiate(decoration);
            if (_transitioning)
            {
                decorationInstantiated.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 3);
            }
            else
            {
                decorationInstantiated.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 2);
            }
            decorationInstantiated.name = "Decoration group (Generated)";
            decorationInstantiated.transform.SetParent(GameObject.Find(Layers.Middleground).gameObject.transform);
        }

        if (ObstacleCollectableGroups.Count > 0)
        {
            var obstacleDecoration = ObstacleCollectableGroups[Random.Range(0, ObstacleCollectableGroups.Count)];

            var obstacleCollectableInstantiated = Instantiate(obstacleDecoration);
            if (_transitioning)
            {
                obstacleCollectableInstantiated.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 3);
            }
            else
            {
                obstacleCollectableInstantiated.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 2);
            }
            obstacleCollectableInstantiated.name = "Obstacle/Decoration group (Generated)";
            obstacleCollectableInstantiated.transform.SetParent(GameObject.Find(Layers.Foreground).gameObject.transform);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.IsLowerThanCamera() && !gameObject.IsHigherThanCamera())
        {
            var nextTileGameObject = Instantiate(NextTile);
            var locationNewTile = new Vector3(gameObject.transform.position.x, (gameObject.transform.position.y + (TileHeight * 3)), 0);

            if (_transitioning)
            {
                locationNewTile.z = 1;
            }

            nextTileGameObject.transform.position = locationNewTile;



            nextTileGameObject.name = "Tile (Generated)";
            nextTileGameObject.transform.SetParent(GameObject.Find(Layers.Background).transform);

            DestroyObject(gameObject);
        }
    }
}

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

    private const int tileChangesRequired = 3;
    private static bool _transitioning;
    private static int _changedTileCount = 0;
    private static GameObject TileToChangeTo;
    private static int _tileChanges = 1;

    void Start()
    {
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

        if (GameManager.Instance.GameData.Coins - (GameManager.Instance.GameData.ScoreTileInterval * _tileChanges) >= 0)
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
            decoration.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 3);

            var decorationInstantiated = Instantiate(decoration);
            decorationInstantiated.name = "Decoration group (Generated)";
            decorationInstantiated.transform.SetParent(GameObject.Find(Layers.Middleground).gameObject.transform);
        }

        if (ObstacleCollectableGroups.Count > 0)
        {
            var obstacleDecoration = ObstacleCollectableGroups[Random.Range(0, ObstacleCollectableGroups.Count)];
            obstacleDecoration.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 3);

            var obstacleCollectableInstantiated = Instantiate(obstacleDecoration);
            obstacleCollectableInstantiated.name = "Obstacle/Decoration group (Generated)";
            obstacleCollectableInstantiated.transform.SetParent(GameObject.Find(Layers.Foreground).gameObject.transform);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.IsLowerThanCamera() && !gameObject.IsHigherThanCamera())
        {
            NextTile.transform.position = new Vector3(gameObject.transform.position.x, (gameObject.transform.position.y + (TileHeight * 3)), 0);
            var nextTileGameObject = Instantiate(NextTile);
            nextTileGameObject.name = "Tile (Generated)";
            nextTileGameObject.transform.SetParent(GameObject.Find(Layers.Background).transform);

            DestroyObject(gameObject);
        }
    }
}

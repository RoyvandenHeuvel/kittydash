using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Transform NextTile;
    public List<Transform> DecorationGroups;
    public float TileHeight;

    void Start()
    {
        gameObject.transform.localPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);

        var decoration = DecorationGroups[Random.Range(0, DecorationGroups.Count)];
        decoration.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 3);

        var decorationInstantiated = Instantiate(decoration);
        decorationInstantiated.name = "Decoration group (Generated)";
        decorationInstantiated.SetParent(GameObject.Find(Layers.Middleground).gameObject.transform);

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.IsLowerThanCamera() && !gameObject.IsHigherThanCamera())
        {
            this.NextTile.transform.position = new Vector3(gameObject.transform.position.x, (gameObject.transform.position.y + (TileHeight * 3)), 0);
            var nextTileGameObject = Instantiate(this.NextTile);
            nextTileGameObject.name = "Tile (Generated)";
            nextTileGameObject.SetParent(GameObject.Find(Layers.Background).transform);

            DestroyObject(gameObject);
        }
    }
}

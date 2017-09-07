using Assets.Scripts;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject _player;
    private float _cameraSpeed;
    public float MinX;
    public float MaxX;

    // Use this for initialization
    void Start()
    {
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        _cameraSpeed = GameManager.Instance.GameData.CameraSpeed;
        float playerX = _player.transform.position.x;
        float x = playerX > MinX && playerX < MaxX ? playerX : gameObject.transform.position.x;
        float y = gameObject.transform.position.y + _cameraSpeed;
        float z = gameObject.transform.position.z;

        this.gameObject.transform.position = new Vector3(x, y, z);
    }
}

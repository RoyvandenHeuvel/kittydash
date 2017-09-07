using UnityEngine;
using Assets.Scripts;

public class Player : MonoBehaviour
{
    public VJHandler JsMovement;

    private Vector3 direction;
    private float xMin, xMax, yMin, yMax;

    void Update()
    {
        xMax = 2.8f;
        yMax = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight)).y;

        xMin = -2.8f;
        if(yMin == default(float) || Camera.main.ScreenToWorldPoint(Vector3.zero).y > yMin)
        {
            yMin = Camera.main.ScreenToWorldPoint(Vector3.zero).y;
        }

        direction = JsMovement.InputDirection; 
        
        if (direction.magnitude != 0)
        {
            transform.position += direction * GameManager.Instance.GameData.PlayerSpeed;
        }
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, xMin, xMax), Mathf.Clamp(transform.position.y + GameManager.Instance.GameData.CameraSpeed, yMin, yMax), 0f);
    }
}
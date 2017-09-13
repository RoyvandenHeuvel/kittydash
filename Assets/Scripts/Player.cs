using UnityEngine;
using Assets.Scripts;
using CnControls;

public class Player : MonoBehaviour
{
    private float xMin, xMax, yMin, yMax;

    void Update()
    {
        Vector3 newPosition = gameObject.transform.position;

        // Setting the boundaries.
        xMax = 2.8f;
        yMax = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight)).y;

        xMin = -2.8f;
        if (yMin == default(float) || Camera.main.ScreenToWorldPoint(Vector3.zero).y > yMin)
        {
            yMin = Camera.main.ScreenToWorldPoint(Vector3.zero).y;
        }

        newPosition.x += GameManager.Instance.GameData.PlayerSpeed * CnInputManager.GetAxis("Horizontal");
        newPosition.y += (GameManager.Instance.GameData.PlayerSpeed * CnInputManager.GetAxis("Vertical")) + GameManager.Instance.GameData.CameraSpeed;
        newPosition = new Vector3(Mathf.Clamp(newPosition.x, xMin, xMax), Mathf.Clamp(newPosition.y, yMin, yMax), 0f);

        transform.position = newPosition;
    }
}
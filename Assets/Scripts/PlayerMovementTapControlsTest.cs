using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTapControlsTest : MonoBehaviour
{
    private float xMin, xMax, yMin, yMax;

    public Vector3 targetLocation;

    void Start()
    {
        targetLocation = this.gameObject.transform.position;
    }

    void Update()
    {
        xMax = 2.8f;
        yMax = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight)).y;

        xMin = -2.8f;
        if (yMin == default(float) || Camera.main.ScreenToWorldPoint(Vector3.zero).y > yMin)
        {
            yMin = Camera.main.ScreenToWorldPoint(Vector3.zero).y;
        }


        transform.position = new Vector3(Mathf.Clamp(transform.position.x, xMin, xMax), Mathf.Clamp(transform.position.y + GameManager.Instance.GameData.CameraSpeed, yMin, yMax), 0f);
        transform.position = Vector3.MoveTowards(transform.position, targetLocation, GameManager.Instance.GameData.PlayerSpeed);
    }
}

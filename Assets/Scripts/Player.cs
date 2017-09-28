using UnityEngine;
using Assets.Scripts;
using CnControls;
using System.Collections;

public class Player : MonoBehaviour
{
    private float yMin, yMax;
    public float xMin, xMax;


    public GameObject SlowAnimation;

    void Start()
    {
        xMax = 2.8f;
        xMin = -2.8f;
    }

    void Update()
    {
        Vector3 newPosition = gameObject.transform.position;

        // Setting the boundaries.
        yMax = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight)).y;
        if (yMin == default(float) || Camera.main.ScreenToWorldPoint(Vector3.zero).y > yMin)
        {
            yMin = Camera.main.ScreenToWorldPoint(Vector3.zero).y;
        }

        newPosition.x += GameManager.Instance.GameData.PlayerSpeed * CnInputManager.GetAxis("Horizontal");
        newPosition.y += (GameManager.Instance.GameData.PlayerSpeed * CnInputManager.GetAxis("Vertical")) + GameManager.Instance.GameData.CameraSpeed;
        newPosition = new Vector3(Mathf.Clamp(newPosition.x, xMin, xMax), Mathf.Clamp(newPosition.y, yMin, yMax), 0f);

        transform.position = newPosition;
    }

    private IEnumerator SlowCoroutine(int duration, float factor)
    {
        var slowGO = GameObject.Instantiate(SlowAnimation);
        slowGO.transform.SetParent(gameObject.transform);
        slowGO.transform.localPosition = new Vector3(0, 0, -1);

        GameManager.Instance.GameData.PlayerSpeed  *= factor;
        for (int f = duration; f > 0; f--)
        {
            yield return null;
        }

        GameObject.Destroy(slowGO);
        GameManager.Instance.GameData.PlayerSpeed /= factor;
    }

    public void Slow(int duration, float factor)
    {
        StartCoroutine(SlowCoroutine(duration, factor));
    }
}
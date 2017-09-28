using Assets.Scripts;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ReleaseTheHounds : MonoBehaviour
{
    public GameObject Hound;
    public float xOffsetHound;
    public float yOffsetHound;
    public int ScoreInterval;

    private int _houndCount;

    private void Start()
    {
        _houndCount = 1;
    }

    void Update()
    {
        if (GameManager.Instance.GameData.Coins - (ScoreInterval * _houndCount) >= 0)
        {
            if (GameObject.Find("Enemy (Hound)") == null)
            {
                var houndInstantiated = GameObject.Instantiate(Hound);
                houndInstantiated.transform.position = new Vector3(gameObject.transform.position.x + xOffsetHound, gameObject.transform.position.y + yOffsetHound, gameObject.transform.position.z);
                houndInstantiated.name = "Enemy (Hound)";
                ShowWarning();
                _houndCount++;
            }
        }
    }

    public string WarningText;
    public Color WarningColor;
    public int WarningFontSize;
    public Font WarningFont;
    public int WarningDuration;

    void ShowWarning()
    {
        var newGO = new GameObject("Warning");
        newGO.transform.localPosition = Vector3.zero;
        newGO.transform.SetParent(GameObject.Find("Canvas").transform);
        newGO.AddComponent<Text>();
        var label = newGO.GetComponent<Text>();
        label.alignment = TextAnchor.MiddleCenter;
        label.horizontalOverflow = HorizontalWrapMode.Overflow;
        label.verticalOverflow = VerticalWrapMode.Overflow;
        label.transform.localScale = new Vector3(1, 1, 1);
        label.font = WarningFont;
        label.fontSize = WarningFontSize;
        label.color = WarningColor;
        label.transform.localPosition = Vector3.zero;
        label.text = WarningText;

        StartCoroutine(DeleteAfter(WarningDuration, newGO));
    }

    IEnumerator DeleteAfter(int period, GameObject go)
    {
        for (int i = period; i > 0; i--)
        {
            yield return null;
        }
        Destroy(go);
    }
}
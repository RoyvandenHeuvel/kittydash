using Assets.Scripts;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    private GameObject UItextGO;

    public int Amount;
    public int RewardDuration;
    public int RewardFontSize;
    public Font RewardFont;
    public Color RewardTextColor;
    public string RewardText;

    void Update()
    {
        if (gameObject.IsLowerThanCamera())
        {
            Destroy(UItextGO);
            DestroyObject(this.gameObject);
        }
    }

    private IEnumerator RewardCoroutine()
    {
        var canvasTransform = GameObject.Find("Canvas").transform;
        var canvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();
        UItextGO = new GameObject("RewardText");
        UItextGO.transform.SetParent(canvasTransform);

        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(this.transform.position);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)));

        RectTransform trans = UItextGO.AddComponent<RectTransform>();
        trans.anchoredPosition = new Vector2(WorldObject_ScreenPosition.x, WorldObject_ScreenPosition.y);
        trans.localScale = new Vector3(1, 1, 1);


        Text text = UItextGO.AddComponent<Text>();
        text.horizontalOverflow = HorizontalWrapMode.Overflow;
        text.verticalOverflow = VerticalWrapMode.Overflow;
        text.text = RewardText;
        text.font = RewardFont;
        text.fontSize = RewardFontSize;
        text.color = RewardTextColor;


        for (int i = RewardDuration; i >= 0; i--)
        {
            yield return null;
        }

        GameObject.Destroy(UItextGO);
        GameObject.Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        GameManager.Instance.GameData.Coins += Amount;
        SoundManager.instance.PlaySound("Pickup");

        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(RewardCoroutine());

    }
}

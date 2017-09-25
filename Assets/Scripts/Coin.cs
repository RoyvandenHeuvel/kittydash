using Assets.Scripts;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{

    float PositionX = 0;

    public int Amount;
    public int RewardDuration;
    public int RewardFontSize;
    public Font RewardFont;
    public Color RewardTextColor;
    public string RewardText;

    void Update()
    {
        if (this.gameObject.IsLowerThanCamera())
        {
            Vector3 temp = new Vector3(0f, 13f);
            this.transform.position += temp;
        }
    }

    private IEnumerator RewardCoroutine()
    {
        var canvasTransform = GameObject.Find("Canvas").transform;
        var canvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();
        GameObject UItextGO = new GameObject("RewardText");
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
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //GameManager.Instance.GameData.Coins++;
        //if (GameManager.Instance.GameData.Coins % 10 == 0 && GameManager.Instance.GameData.Coins > 0)
        //{
        //    var i = new Item("Camera speed boost!", x => GameManager.Instance.GameData.CameraSpeed = GameManager.Instance.GameData.CameraSpeed + x, 0.005f);
        //    GameManager.Instance.GameData.Inventory.Add(i);
        //}
        GameManager.Instance.GameData.Coins += Amount;
        SoundManager.instance.PlaySound("Pickup");

        StartCoroutine(RewardCoroutine());

        float random = Random.value;
        if (random < 0.33)
        {
            PositionX = -1;
        }
        if (random > 0.33 && random < 0.66)
        {
            PositionX = 0;
        }
        if (random > 0.66)
        {
            PositionX = 1;
        }

        PositionX -= this.transform.position.x;
        Vector3 temp = new Vector3(PositionX, 13f);
        this.transform.position += temp;
    }


}

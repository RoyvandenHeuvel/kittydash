using Assets.Scripts;
using UnityEngine;


public class Coin : MonoBehaviour
{
    float PositionX = 0;

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.IsLowerThanCamera())
        {
            Vector3 temp = new Vector3(0f, 13f);
            this.transform.position += temp;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        GameManager.Instance.GameData.Coins++;
        if (GameManager.Instance.GameData.Coins % 10 == 0 && GameManager.Instance.GameData.Coins > 0)
        {
            var i = new Item("Camera speed boost!", x => GameManager.Instance.GameData.CameraSpeed = GameManager.Instance.GameData.CameraSpeed + x, 0.005f);
            GameManager.Instance.GameData.Inventory.Add(i);
        }

        this.GetComponent<AudioSource>().Play();

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
        Vector3 temp = new Vector3(PositionX, 20f);
        this.transform.position += temp;
    }

}

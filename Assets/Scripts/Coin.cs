using Assets.Scripts;
using UnityEngine;


public class Coin : MonoBehaviour
{

    float PositionX = 0;
    int counter = 0;
    public GameObject reward;

    void Awake()
    {
        reward = GameObject.FindGameObjectWithTag("reward");
    }
    private void Start()
    {
        reward.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.IsLowerThanCamera())
        {
            Vector3 temp = new Vector3(0f, 13f);
            this.transform.position += temp;
        }
        if(counter > 0)
        {
            counter--;
            if(counter == 0)
            {
                reward.SetActive(false);

            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //GameManager.Instance.GameData.Coins++;
        //if (GameManager.Instance.GameData.Coins % 10 == 0 && GameManager.Instance.GameData.Coins > 0)
        //{
        //    var i = new Item("Camera speed boost!", x => GameManager.Instance.GameData.CameraSpeed = GameManager.Instance.GameData.CameraSpeed + x, 0.005f);
        //    GameManager.Instance.GameData.Inventory.Add(i);
        //}
        GameManager.Instance.GameData.Coins++;
        SoundManager.instance.PlaySound("Pickup");
        reward.SetActive(true);
        counter = 20;
        

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

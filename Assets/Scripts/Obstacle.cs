using Assets.Scripts;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int Damage;

    void Update()
    {
        if (this.gameObject.IsLowerThanCamera())
            DestroyObject(this.gameObject);
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        GameManager.Instance.GameData.PlayerCurrentHealth -= Damage;

        DestroyObject(this.gameObject);
    }
}

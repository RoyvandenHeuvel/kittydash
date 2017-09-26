using Assets.Scripts;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int SlowDuration;
    public float SlowFactor;

    void Update()
    {
        if (this.gameObject.IsLowerThanCamera())
            DestroyObject(this.gameObject);
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        collider.gameObject.GetComponent<Player>().Slow(SlowDuration, SlowFactor);

        DestroyObject(this.gameObject);
    }
}

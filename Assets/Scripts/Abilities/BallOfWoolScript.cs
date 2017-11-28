using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallOfWoolScript : MonoBehaviour
{

    public int SlowDuration;
    public float SlowFactor;

    // Update is called once per frame
    void Update()
    {
        if (gameObject.IsLowerThanCamera())
        {
            GameObject.Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        collider.gameObject.GetComponent<EnemyFiniteStateMachine>().Slow(SlowDuration, SlowFactor);
        SoundManager.instance.PlaySound("SlowEffect");
        GameObject.Destroy(gameObject);
    }
}

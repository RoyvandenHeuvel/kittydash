using UnityEngine;

namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour
    {
        void OnCollisionEnter2D(Collision2D collision)
        {
            // TODO: Proper game over.
            //SoundManager.instance.PlaySound("GameOver");
            GameManager.Instance.PostHighScore(Mathf.RoundToInt(collision.transform.position.y));
            UnityEngine.SceneManagement.SceneManager.LoadScene("scene_roy");
        }
    }
}

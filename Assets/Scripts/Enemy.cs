using UnityEngine;

namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour
    {
        void OnCollisionEnter2D(Collision2D collision)
        {
            // TODO: Proper game over.
            UnityEngine.SceneManagement.SceneManager.LoadScene("scene_roy");
        }
    }
}

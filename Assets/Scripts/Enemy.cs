using UnityEngine;

namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour
    {
        void OnCollisionEnter2D(Collision2D collision)
        {
            // TODO: Proper game over.
            //SoundManager.instance.PlaySound("GameOver");
            if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                GameManager.Instance.PostHighScore(Mathf.RoundToInt(GameManager.Instance.GameData.Coins));
                GameManager.Instance.GameData.Coins = 0;
                UnityEngine.SceneManagement.SceneManager.LoadScene("Mainmenu");
            }
        }

        /// <summary>
        /// Returns a boolean indicating whether the distance between this and the target is smaller than range.
        /// </summary>
        /// <param name="range">Distance you want to know the target is in or not.</param>
        /// <param name="target">The Transform to check the range with.</param>
        /// <returns>True is target is within range, false if not.</returns>
        public bool IsInRange(float range, Transform target)
        {
            return Vector3.Distance(target.position, gameObject.transform.position) <= range;
        }
    }
}

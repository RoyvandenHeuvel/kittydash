﻿using UnityEngine;

namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour
    {
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                GameOverManager.Instance.GameOver();
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

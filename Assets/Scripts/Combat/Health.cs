using System;
using UnityEngine;

namespace Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] public float maxHealth = 10f;
        [SerializeField] private float currentHealth;

        private void Start()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            var updatedHealth = currentHealth - damage;
            currentHealth = Mathf.Clamp(updatedHealth - damage, 0, maxHealth);
            print(currentHealth);
        }
    }
}
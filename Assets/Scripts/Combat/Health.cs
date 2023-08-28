using System;
using UnityEngine;

namespace Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] public int maxHealth = 10;
        [SerializeField] private int currentHealth;

        private void Start()
        {
            currentHealth = maxHealth;
        }

        public void Damage(int damage)
        {
            var updatedHealth = currentHealth - damage;
            currentHealth = Mathf.Clamp(updatedHealth - damage, 0, maxHealth);
            print(currentHealth);
        }
    }
}
using System;
using UnityEngine;

namespace Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float maxHealth = 10f;
        [SerializeField] private float currentHealth;

        private void Start()
        {
            currentHealth = maxHealth;
        }

        public bool IsAlive() => currentHealth > 0;


        public bool IsDead() => !IsAlive();

        public void TakeDamage(float damage)
        {
            if (IsDead()) return;
            var updatedHealth = currentHealth - damage;
            currentHealth = Mathf.Clamp(updatedHealth - damage, 0, maxHealth);
            print(currentHealth);
            Die();
        }

        private void Die()
        {
            if (IsDead())
            {
                GetComponent<Animator>().SetTrigger("die");
            }
        }
    }
}
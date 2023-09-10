using System;
using UnityEngine;

namespace Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] public float maxHealth = 10f;
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
                GetComponent<ActionScheduler>().CancelCurrent();
            }
        }
    }
}
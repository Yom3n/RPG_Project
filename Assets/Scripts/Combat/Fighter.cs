using System;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        private void Update()
        {
        }

        public void Attack(GameObject obj)
        {
            print(obj.name);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using Combat;
using Core;
using Movement;
using UnityEngine;


namespace Control
{
    public class PlayerController : MonoBehaviour
    {
        private Mover mover;
        private Fighter fighter;

        // Start is called before the first frame update
        void Start()
        {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
        }

        // Update is called once per frame
        void Update()
        {
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
            print("Nothing to do");
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits;
            hits = Physics.RaycastAll(ray: GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                var target = hit.transform.GetComponent<CombatTarget>();
                if (target == null || !fighter.IsTargetValid(target.gameObject)) continue;
                if (Input.GetMouseButtonDown(0))
                {
                    fighter.Attack(target.gameObject);
                }

                return true;
            }

            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (!hasHit) return false;
            if (Input.GetMouseButton(0))
            {
                mover.StartMoveToAction(hit.point);
            }

            return true;
        }
    }
}
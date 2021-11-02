using System;
using RPG.Combat;
using RPG.Movement;
using UnityEngine;
using RPG.Attributes;
using RPG.Dialog;
using UnityEngine.EventSystems;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {

        Health health;

        bool activeDialogbox;

        private void Awake()
        {
            health = GetComponent<Health>();
        }

        private void Start()
        {

        }

        private void Update()
        {
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;

        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                if (!GetComponent<Fighter>().CanAttack(target.gameObject))
                {
                    continue;
                }

                if (Input.GetMouseButton(0))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            activeDialogbox = DialogManager.instance.dialogBox.activeInHierarchy;
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (activeDialogbox == false)
                {
                    if (hasHit && !health.IsDead())
                    {
                        if (Input.GetMouseButton(0))
                        {
                            GetComponent<Mover>().StartMoveAction(hit.point);
                        }
                        return true;
                    }
                }
            }

            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
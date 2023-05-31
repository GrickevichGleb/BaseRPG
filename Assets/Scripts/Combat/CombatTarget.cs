using RPG.Control;
using UnityEngine;
using RPG.Attributes;

namespace RPG.Combat
{
    //We can't use CombatTarget.cs without Health.cs on object
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour, IRaycastable
    {
        public bool HandleRaycast(PlayerController callingController)
        {
            if (!callingController.GetComponent<Fighter>().CanAttack(gameObject))
            {
                return false;
            }
            // If "target" was hit
            if (Input.GetMouseButton(0))
            {
                callingController.GetComponent<Fighter>().Attack(gameObject);
            }
            return true;
        }
        
        public CursorType GetCursorType()
        {
            return CursorType.Combat;
        }
    }
}
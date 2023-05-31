using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Combat 
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private UnityEvent onHit;
        
        public void OnHit()
        {
            onHit.Invoke();
        }
    }
}

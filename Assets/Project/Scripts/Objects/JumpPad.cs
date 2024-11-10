using System.Collections.Generic;
using Project.Scripts.Player;
using UnityEngine;

namespace Project.Scripts.Objects
{
    public class JumpPad : MonoBehaviour
    {
        [SerializeField] private List<Debuff> _debuffs;
    
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.GetComponent<Player.Player>()) return;
            
            foreach (var debuff in _debuffs)
            {
                other.gameObject.GetComponent<PlayerDebuffManager>()?.AddDebuff(debuff);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.GetComponent<Player.Player>()) return;
            
            foreach (var debuff in _debuffs)
            {
                other.gameObject.GetComponent<PlayerDebuffManager>()?.RemoveDebuff(debuff);
            }
        }
    }
}

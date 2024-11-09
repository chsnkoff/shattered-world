using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Scripts.Player
{
    public class PlayerDebuffManager : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb;
        
        private readonly List<Debuff> _debuffs = new();

        public void AddDebuff(Debuff debuff)
        {
            _debuffs.Add(debuff);

            if (debuff.Type == Debuff.DebuffType.Gravity)
            {
                _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
            }
        }

        public void RemoveDebuff(Debuff debuff)
        {
            _debuffs.Remove(debuff);
        }

        public float GetDebuffsValueByType(Debuff.DebuffType type)
        {
            return _debuffs.Where(debuff => debuff.Type == type).ToList().Aggregate<Debuff, float>(1, (current, debuff) => current * debuff.Multiplier);
        }
    }
}

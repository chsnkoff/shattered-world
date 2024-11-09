using System;

namespace Project.Scripts.Player
{
    [Serializable]
    public class Debuff
    {
        public enum DebuffType
        {
            Movement,
            Gravity,
            JumpForce
        }

        public DebuffType Type;
        public float Multiplier;
    }
}

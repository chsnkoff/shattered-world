using UnityEngine;

namespace Project.Scripts.Camera
{
    [CreateAssetMenu(fileName = "newCameraShakeEffectConfig", menuName = "Configurations/Camera/ShakeEffect", order = 1)]
    public class CameraShakeEffectConfig : ScriptableObject
    {
        public float Amplitude;
        public float Friquency;
        public float Duration;
    }
}
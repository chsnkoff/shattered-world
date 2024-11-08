using Cinemachine;
using UnityEngine;

namespace Project.Scripts.Camera
{
    public class CameraShake : MonoBehaviour
    {
        public static CameraShake Instance { get; private set; }
        
        [SerializeField] private CinemachineFreeLook _cinemachineFreeLook;
        private float _shakeTimer;

        private void Awake()
        {
            Instance = this;
        }

        public void ShakeCamera(CameraShakeEffectConfig config)
        {
            SetShakeValues(config.Amplitude, config.Friquency);
            _shakeTimer = config.Duration;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void SetShakeValues(float amplitude, float friquency)
        {
            for (var i = 0; i < 3; i++)
            {
                var perlin = _cinemachineFreeLook.GetRig(i).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                if (!perlin) continue;
                
                perlin.m_AmplitudeGain = amplitude;
                perlin.m_FrequencyGain = friquency;
            }
        }

        public void Update()
        {
            if (!(_shakeTimer > 0)) return;
            _shakeTimer -= Time.deltaTime;

            if (_shakeTimer <= 0)
            {
                SetShakeValues(0, 0);
            }
        }
    }
}

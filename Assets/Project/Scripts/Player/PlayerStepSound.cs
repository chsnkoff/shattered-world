using Project.Scripts.Player;
using UnityEngine;

public class PlayerStepSound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _footstepSounds;
    [SerializeField] private PlayerJumpController _jumpController;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _stepInterval = 0.25f;

    private float _stepTimer;

    private void Start()
    {
        _stepTimer = _stepInterval;
        if (!_audioSource)
        {
            _audioSource = GetComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if (_jumpController.IsGrounded && _rb.velocity.magnitude > 0.1f)
        {
            _stepTimer -= Time.deltaTime;
            if (!(_stepTimer <= 0f)) return;
            PlayFootstepSound();
        }

        _stepTimer = _stepInterval;
    }

    private void PlayFootstepSound()
    {
        if (_footstepSounds.Length <= 0) return;
        var index = Random.Range(0, _footstepSounds.Length);
        _audioSource.PlayOneShot(_footstepSounds[index]);
    }
}

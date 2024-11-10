using Project.Scripts.Objects;
using Project.Scripts.Player;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerSoundController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _footstepSounds;
    [SerializeField] private AudioClip _slurpSound;
    [SerializeField] private PlayerJumpController _jumpController;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _stepInterval = 0.25f;

    private float _stepTimer;
    private bool _isInRock;

    private void OnEnable()
    {
        Rock.OnPlayerEntersRock.AddListener(OnPlayerEntersRock);
        Rock.OnPlayerExitsRock.AddListener(OnPlayerExitsRock);
    }

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
        if (_footstepSounds.Length <= 0 || _isInRock) return;
        var index = Random.Range(0, _footstepSounds.Length);
        _audioSource.PlayOneShot(_footstepSounds[index]);
    }

    private void OnPlayerEntersRock()
    {
        _isInRock = true;
        _audioSource.clip = _slurpSound;
        _audioSource.loop = true;
        _audioSource.Play();
    }
    
    private void OnPlayerExitsRock()
    {
        _audioSource.loop = false;
        _audioSource.Stop();
        _isInRock = false;
    }
}

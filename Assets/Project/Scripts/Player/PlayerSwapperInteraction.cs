using Cinemachine;
using Project.Scripts.Player;
using UnityEngine;

public class PlayerSwapperInteraction : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _alternateObject;
    [SerializeField] private CinemachineFreeLook _cfl;

    private bool _isPlayerControlled = true;

    public void ToggleControl()
    {
        _isPlayerControlled = !_isPlayerControlled;

        if (_isPlayerControlled)
        {
            _player.SetActive(true);
            _alternateObject.SetActive(false);
            SwitchToPlayer();
        }
        else
        {
            _player.SetActive(false);
            _alternateObject.SetActive(true);
            SwitchToAlternate();
        }
    }

    private void SwitchToPlayer()
    {
        _player.GetComponent<PlayerMovement>().enabled = true;
        _player.GetComponent<PlayerJumpController>().enabled = true;
        _alternateObject.GetComponent<PlayerMovement>().enabled = false;
        _alternateObject.GetComponent<PlayerJumpController>().enabled = false;
        _cfl.Follow = _player.transform;
        _cfl.LookAt = _player.transform;
    }

    private void SwitchToAlternate()
    {
        _player.GetComponent<PlayerMovement>().enabled = false;
        _player.GetComponent<PlayerJumpController>().enabled = false;
        _alternateObject.GetComponent<PlayerMovement>().enabled = true;
        _alternateObject.GetComponent<PlayerJumpController>().enabled = true;
        _cfl.Follow = _alternateObject.transform;
        _cfl.LookAt = _alternateObject.transform;}
}

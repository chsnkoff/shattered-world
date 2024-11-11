using UnityEngine;

public class PlayerSwapperInteraction : MonoBehaviour
{
    [SerializeField] private GameObject _model;
    [SerializeField] private GameObject _player;
    
    public void SwapModels(GameObject other)
    {
        _model.gameObject.SetActive(false);
        other.transform.SetParent(_player.transform);
        other.transform.localPosition = Vector3.zero;
    }
}

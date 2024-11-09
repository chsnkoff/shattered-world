using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class ObjectInteraction : MonoBehaviour
{
    private static GameObject _targetUIElement;
    
    [SerializeField] private UnityEvent _onInteract;
    [SerializeField] private GameObject _initialUIElement;
    
    private bool _isUIActivated;
    
    private void Awake()
    {
        InitTrigger();
        InitUIElement();
        DeactivateUIElement();
    }

    private void Update()
    {
        if (_isUIActivated && Input.GetKeyDown(KeyCode.E)) Interact();
    }

    private void InitUIElement()
    {
        if (!_targetUIElement && _initialUIElement) _targetUIElement = _initialUIElement;
    }

    private void InitTrigger()
    {
        var triggerCollider = GetComponent<BoxCollider>();
        if (!triggerCollider) triggerCollider = gameObject.AddComponent<BoxCollider>();
        triggerCollider.isTrigger = true;
    }
    
    private void ActivateUIElement()
    {
        if (_targetUIElement)
        {
            _targetUIElement.SetActive(true);
            _isUIActivated = true;
        }
        else
        {
            Debug.LogWarning("UI-элемент не назначен!");
        }
    }

    private void DeactivateUIElement()
    {
        if (_targetUIElement)
        {
            _targetUIElement.SetActive(false);
            _isUIActivated = false;
        }
        else
        {
            Debug.LogWarning("UI-элемент не назначен!");
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Debug.Log("Player entered trigger");
        ActivateUIElement();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Debug.Log("Player exited trigger");
        DeactivateUIElement();
    }

    private void Interact()
    {
        _onInteract?.Invoke();
        Debug.Log("Player interacted");
    }
}

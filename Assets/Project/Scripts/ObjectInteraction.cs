using Project.Scripts.Player;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class ObjectInteraction : MonoBehaviour
{
    public static readonly UnityEvent OnEnter = new();
    public static readonly UnityEvent OnExit = new();
    
    [SerializeField] private UnityEvent _onInteract;
    [SerializeField] private bool _disableAfterInteracting;

    private bool _interactedOnce;
    
    private void Awake()
    {
        InitTrigger();
    }

    private void Update()
    {
        if (UITriggerSubscriber.IsUIActivated && Input.GetKeyDown(KeyCode.E)) Interact();
    }

    private void InitTrigger()
    {
        var triggerCollider = GetComponent<BoxCollider>();
        if (!triggerCollider) triggerCollider = gameObject.AddComponent<BoxCollider>();
        triggerCollider.isTrigger = true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (_disableAfterInteracting && _interactedOnce) return;
        if (!other.GetComponent<Player>()) return;
        OnEnter?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.GetComponent<Player>()) return;
        OnExit?.Invoke();
    }

    private void Interact()
    {
        if (_disableAfterInteracting && _interactedOnce) return;
        _onInteract?.Invoke();
        _interactedOnce = true;
        if (_disableAfterInteracting) OnExit?.Invoke();
    }
}

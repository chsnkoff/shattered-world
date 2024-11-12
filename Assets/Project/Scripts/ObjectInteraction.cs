using Project.Scripts.Player;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class ObjectInteraction : MonoBehaviour
{
    public static readonly UnityEvent OnEnter = new();
    public static readonly UnityEvent OnExit = new();
    
    [SerializeField] private UnityEvent _onInteract;
    [SerializeField] private uint _usingsAmount = 1;
    
    private uint _interactionsCounter;
    private bool _isTriggered;
    
    private void Awake()
    {
        InitTrigger();
    }

    private void Update()
    {
        if (UITriggerSubscriber.IsUIActivated && _isTriggered && Input.GetKeyDown(KeyCode.E)) Interact();
    }

    private void InitTrigger()
    {
        var triggerCollider = GetComponent<BoxCollider>();
        if (!triggerCollider) triggerCollider = gameObject.AddComponent<BoxCollider>();
        triggerCollider.isTrigger = true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (_interactionsCounter >= _usingsAmount) return;
        if (!other.GetComponent<Player>()) return;
        _isTriggered = true;
        OnEnter?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.GetComponent<Player>()) return;
        _isTriggered = false;
        OnExit?.Invoke();
    }

    private void Interact()
    {
        if (_interactionsCounter >= _usingsAmount) return;
        _onInteract?.Invoke();
        _interactionsCounter++;
        if (_interactionsCounter >= _usingsAmount) OnExit?.Invoke();
    }
}

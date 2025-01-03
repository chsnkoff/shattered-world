using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public class Activator : MonoBehaviour
{
    public UnityEvent OnEnable;
    public UnityEvent OnDisable;

    [UsedImplicitly] 
    public bool IsActive;

    private void Awake()
    {
        StateOff();
    }
    
    [UsedImplicitly]
    public void ToggleStation()
    {
        switch (IsActive)
        {
            case false:
                StateOn();
                break;
            case true:
                StateOff();
                break;
        }
    }

    private void StateOn()
    {
        gameObject.SetActive(false);
        IsActive = true;
        OnEnable?.Invoke();
    }

    private void StateOff()
    {
        gameObject.SetActive(true);
        IsActive = false;
        OnDisable?.Invoke();
    }
}

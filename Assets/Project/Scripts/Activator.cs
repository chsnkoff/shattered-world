using UnityEngine;
using UnityEngine.Events;

public class Activator : MonoBehaviour
{
    public UnityEvent OnEnable;
    public UnityEvent OnDisable;

    public static bool IsActive;

    private void Awake()
    {
        StateOFF();
    }
    
    public void ToggleStation()
    {
        if (!IsActive)
        {
            StateON();
        }
        else if(IsActive)
        {
            StateOFF();
        }
    }

    private void StateON()
    {
        gameObject.SetActive(false);
        IsActive = true;
        OnEnable.Invoke();
    }

    private void StateOFF()
    {
        gameObject.SetActive(true);
        IsActive = false;
        OnDisable.Invoke();
    }

}

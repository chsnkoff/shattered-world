using System;
using System.Collections;
using System.Collections.Generic;
using Project.Scripts.Player;
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

    public void StateON()
    {
        gameObject.SetActive(false);
        IsActive = true;
        OnEnable.Invoke();
    }

    public void StateOFF()
    {
        gameObject.SetActive(true);
        IsActive = false;
        OnDisable.Invoke();
    }

}

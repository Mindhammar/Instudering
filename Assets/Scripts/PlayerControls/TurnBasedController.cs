using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TurnBasedController : MonoBehaviour
{
    public UnityEvent onAction1;
    public UnityEvent onAction2;
    public void Action1(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            onAction1.Invoke();
            Debug.Log("Diddykong");
        }
    }
    public void Action2(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            onAction2.Invoke();
        }
    }
}

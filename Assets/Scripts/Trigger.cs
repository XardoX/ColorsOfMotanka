using UnityEngine;
using MyBox;
using System;
using UnityEngine.Events;
public class Trigger : MonoBehaviour
{
    [ReadOnly]
    public bool triggered;
    public UnityEvent onTriggered;
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(triggered) return;
        if(other.CompareTag("Player"))
        {
            triggered = true;
            onTriggered.Invoke();
            
        }
    }
}

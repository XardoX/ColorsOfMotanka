using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
using System;
public class Trigger : MonoBehaviour
{
    [ReadOnly]
    public bool triggered;
    public Action onTriggered;
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(triggered) return;
        if(other.CompareTag("Player"))
        {
            triggered = true;
            onTriggered();
        }
    }
}

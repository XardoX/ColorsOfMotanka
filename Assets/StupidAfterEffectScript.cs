using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StupidAfterEffectScript : MonoBehaviour
{
    [SerializeField]
    private GameObject ShadowAfterEffect;

    [SerializeField]
    private float interval = .05f;
    
    private void Awake()
    {
        StartCoroutine(ForeverCreating());
    }

    IEnumerator ForeverCreating()
    {
        while (true)
        {
            Instantiate(ShadowAfterEffect, transform.position, Quaternion.identity).GetComponent<Dying>().player = gameObject;
            
            yield return new WaitForSeconds(interval);
        }
    }
}

using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Dying : MonoBehaviour
{
    private float lifetime = .01f;

    public GameObject player;
    
    Tween alphaTween;

    private void Start()
    {
        StartCoroutine(DyingOnCoroutine());
        
    }

    IEnumerator DyingOnCoroutine()
    {
        yield return new WaitForSeconds(.04f);
        
        var transformDifference = transform.position - player.transform.position  ; 
        
        GetComponent<Rigidbody2D>().AddForce(transformDifference * 5, ForceMode2D.Impulse);
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}

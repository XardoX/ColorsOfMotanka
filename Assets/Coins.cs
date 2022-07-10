using DG.Tweening;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField]
    private Animator myAnimator;
    
    [SerializeField]
    private GameObject circle;

    private bool isTaken = false;
    Tween alphaTween;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.TryGetComponent(out PlayerMovement player) && !isTaken)
        {
            player.coinAmount++;
            player.UpdateCoins();
            
            alphaTween =  DOTween.To(() => circle.transform.localScale.x , x => circle.transform.localScale = new Vector3(x, circle.transform.localScale.y, circle.transform.localScale.z ), 6, 1).SetEase(Ease.OutCirc);
            alphaTween =  DOTween.To(() => circle.transform.localScale.y , x => circle.transform.localScale = new Vector3(circle.transform.localScale.x, x, circle.transform.localScale.z ), 6, 1).SetEase(Ease.OutCirc);
            
            myAnimator.Play("CoinPickUp");

            
            GetComponent<ParticleSystem>().Play();

            isTaken = true;
        }
    }
}

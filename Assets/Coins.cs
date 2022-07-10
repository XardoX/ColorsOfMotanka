using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField]
    private Animator myAnimator;
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.TryGetComponent(out PlayerMovement player))
        {
            player.coinAmount++;
            
            myAnimator.Play("CoinPickUp");
            
            GetComponent<ParticleSystem>().Play();
        }
    }
}

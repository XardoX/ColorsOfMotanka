using System.Text.RegularExpressions;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    private bool CanInteract;

    [SerializeField]
    private CanvasGroup cloudTextCanvas;
    
    [SerializeField]
    private TextAsset dialog;
        
    [SerializeField]
    private TextMeshProUGUI dialogText;
    
    private int dialogLine = -1;
    private string[] dialogLines;
    
    Tween alphaTween;

    private void Awake()
    {
        dialogLines = Regex.Split ( dialog.text, "\n|\r|\r\n" );
    }

    public void OnInteract()
    {
        if (CanInteract && dialogLine <  dialogLines.Length)
        {
            
            dialogLine++;
            dialogText.text = dialogLines[dialogLine];
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.TryGetComponent(out PlayerMovement player))
        {
            alphaTween =  DOTween.To(() => cloudTextCanvas.alpha, x => cloudTextCanvas.alpha = x, 1, 1).SetEase(Ease.OutCirc);
            CanInteract = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.TryGetComponent(out PlayerMovement player))
        {
            alphaTween =  DOTween.To(() => cloudTextCanvas.alpha, x => cloudTextCanvas.alpha = x, 0, 1);
            CanInteract = false;
        }
    }   
    
}

using System.Collections;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroCutscene : MonoBehaviour
{

    [SerializeField]
    private TextAsset introTextAsset;

    private string[] introString;
    
    [SerializeField]
    private TextMeshProUGUI introText;

    [SerializeField]
    private Image image1, image2;

    [SerializeField]
    private Animator clouds1, clouds2, dust;
    
    [SerializeField]
    private float textSpeed = .1f;
    
    Tween cutscenetween;
    
    private void Awake()
    {
        introString = Regex.Split ( introTextAsset.text, "\n" );

        StartCoroutine(Intro());

    }

    IEnumerator Intro()
    {

        StringBuilder sb = new StringBuilder();

        cutscenetween =  DOTween.To(() => introText.rectTransform.localPosition.y, x => introText.rectTransform.localPosition = new Vector3(introText.rectTransform.localPosition.x,x,introText.rectTransform.localPosition.z), 225, 3).SetEase(Ease.OutCirc);

        cutscenetween =  DOTween.To(() => introText.GetComponent<CanvasGroup>().alpha, x => introText.GetComponent<CanvasGroup>().alpha = x, 1, 60).SetEase(Ease.OutCirc);

        var typeOfEase = Ease.InOutSine;
        
        foreach (var lineOfDialog in introString)
        {
            sb.Clear();
            
            for (int i = 0; i < lineOfDialog.Length; i++)
            {
                sb.Append(lineOfDialog[i]);
                introText.text = sb.ToString();
                
                if (introString.ToList().IndexOf(lineOfDialog) == 1)
                {
                    cutscenetween =  DOTween.To(() => image1.color.a, x => image1.color = new Color(255,255,255,x) , 1, 5).SetEase(Ease.OutSine);
                    cutscenetween =  DOTween.To(() => image1.rectTransform.localPosition.x, x => image1.rectTransform.localPosition = new Vector3(x,image1.rectTransform.localPosition.y,image1.rectTransform.localPosition.z) , 0, 10).SetEase(Ease.OutCirc);
                }
                if (introString.ToList().IndexOf(lineOfDialog) == 2)
                {
                    cutscenetween =  DOTween.To(() => image1.color.a, x => image1.color = new Color(255,255,255,x) , 0, 1.5f).SetEase(Ease.OutSine);
                    cutscenetween =  DOTween.To(() => image1.rectTransform.localPosition.x, x => image1.rectTransform.localPosition = new Vector3(x,image1.rectTransform.localPosition.y,image1.rectTransform.localPosition.z) , 400, 10).SetEase(Ease.OutCirc);
                    
                    cutscenetween =  DOTween.To(() => image2.color.a, x => image2.color = new Color(255,255,255,x) , 1, 5).SetEase(Ease.OutSine);
                    cutscenetween =  DOTween.To(() => image2.rectTransform.localPosition.x, x => image2.rectTransform.localPosition = new Vector3(x,image2.rectTransform.localPosition.y,image2.rectTransform.localPosition.z) , 0, 10).SetEase(Ease.OutCirc);
                }
                
                yield return new WaitForSeconds(textSpeed);
            }
            
            if (introString.ToList().IndexOf(lineOfDialog) == 2)
            {
                clouds1.Play("CutsceneCloudAnim");
                    
                clouds2.Play("CutsceneCloudAnim2");
                    
                dust.Play("FustPoof");
            }
            yield return new WaitForSeconds(2.5f);

        }

             
      
        

        
        
       // yield return new WaitUntil(() => { return true;});
        // while (true)
        // {
        //     
        // }
    }
}

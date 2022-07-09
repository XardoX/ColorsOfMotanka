using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MyBox;
public class Mural : MonoBehaviour
{
    [SerializeField]
    private Transform _triggersParent;
    [SerializeField][ReadOnly]
    private Trigger[] _triggers;
    [SerializeField]
    private GameObject _muralCam;
    [SerializeField]
    private Transform _finishedMuralMask;
    [SerializeField]
    private float _endScale=10;
    [SerializeField]
    private float _zoomOutDuration= 5f;
    // Start is called before the first frame update
    void Start()
    {
        _triggers = GetComponentsInChildren<Trigger>(true);
        foreach(Trigger trigger in _triggers)
        {
            trigger.onTriggered += ()=> CheckCompletion();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckCompletion()
    {
        bool completed = true;
        foreach(Trigger trigger in _triggers)
        {
            if(trigger.triggered == false) completed = false;
        }
        if(completed)
        {
            CompleteMural();
        }
    }

    void CompleteMural()
    {
        _muralCam.SetActive(true);
        _finishedMuralMask.gameObject.SetActive(true);
        Vector3 scale = _finishedMuralMask.localScale;
        DOTween.To(()=> scale, x=> scale = x, new Vector3(_endScale,_endScale *2,1), _zoomOutDuration)
        .OnUpdate(()=> _finishedMuralMask.localScale = scale)
        .OnComplete(()=> _muralCam.SetActive(false));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject _startCam;
    [SerializeField]
    private CanvasGroup _menu;
    [SerializeField]
    private CanvasGroup _credits;
    public void StartGame()
    {
        Debug.Log("startedGaem");

        Time.timeScale = 1f;
        _startCam.SetActive(false);
        DOTween.To(()=> _menu.alpha, x=> _menu.alpha = x, 0, 2).SetEase(Ease.OutSine);
        _credits.interactable = false;
        _menu.interactable = false;
    }
    public void RestartGame()
    {
        Debug.Log("restart");

        SceneManager.LoadScene("Main2");
    }
    public void PlayIntro()
    {
        Debug.Log("ReplayIntro");
        SceneManager.LoadScene("Intro");
    }
    public void ExitGame()
    {
        Debug.Log("Quit");

        Application.Quit();
    }
    public void ShowCredits()
    {
        _credits.interactable = true;
        DOTween.To(()=> _credits.alpha, x=> _credits.alpha = x, 1, 2).SetEase(Ease.OutSine);
    }
}

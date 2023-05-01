using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.UI;
using UnityEngine.Video;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private VideoPlayer _videoPlayer;

    [SerializeField] private GameObject pauseUI;

    [SerializeField] private GameObject lossScreen;

    [SerializeField] private GameObject winScreen;

    [SerializeField] private GameObject dialogueWindow;
    [SerializeField] private Text dialogueText;
    [SerializeField] private float showTime;

    [SerializeField] private Slider oxySlider;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
        
        pauseUI.SetActive(false);
        lossScreen.SetActive(false);
        winScreen.SetActive(false);
        oxySlider.enabled = false;
        dialogueWindow.SetActive(false);
    }

    public void ShowDialogueLine(string text)
    {
        dialogueWindow.SetActive(true);
        dialogueText.text = text;
        StartCoroutine(DialogueTime());
    }
    private IEnumerator DialogueTime()
    {
        yield return new WaitForSeconds(showTime);
        dialogueWindow.SetActive(false);
    }

    public void EnablePauseScreen()
    {
        pauseUI.SetActive(true);
    }

    public void DisablePauseScreen()
    {
        pauseUI.SetActive(false);
    }

    public void EnableSlider()
    {
        oxySlider.enabled = true;
    }
    public void SetSliderValue(float value)
    {
        oxySlider.value = value;
    }

    public void SetSliderMax(float maxValue)
    {
        oxySlider.maxValue = maxValue;
    }

    public void EnableWinScreen()
    {
        oxySlider.gameObject.SetActive(false);
        StartCoroutine(Video());
    }

    private IEnumerator Video()
    {
        Time.timeScale = 1;
        _videoPlayer.Play();
        Time.timeScale = 1;
        yield return new WaitForSeconds(37f);
        Time.timeScale = 1;
        winScreen.SetActive(true);
        Time.timeScale = 1;
        //Debug.Log("start cor");
        //_videoPlayer.Prepare();
        //WaitForSeconds waitTime = new WaitForSeconds(1);
        //while (!_videoPlayer.isPrepared)
        //{
        //    yield return waitTime;
        //    break;
        //}
        //_videoPlayer.Play();
        //if (!_videoPlayer.isPlaying)
        //{
        //    Debug.Log("not");
        //    winScreen.SetActive(true);
        //}
    }

    public void EnableLossScreen()
    {
        lossScreen.SetActive(true);
    }

    public void MainMenu(string sceneName)
    {
        GameManager.Instance.LoadScene(sceneName);
    }

    public void ReturnToMenu(string sceneName)
    {
        GameManager.Instance.LoadScene(sceneName);
    }
}

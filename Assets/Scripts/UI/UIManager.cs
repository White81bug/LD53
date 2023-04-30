using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
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
        winScreen.SetActive(true);
    }

    public void EnableLossScreen()
    {
        lossScreen.SetActive(true);
    }
}

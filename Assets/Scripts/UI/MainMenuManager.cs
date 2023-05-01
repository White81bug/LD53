using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
  [SerializeField] private GameObject creditsScreen;
  [SerializeField] private GameObject controlsScreen;

  private void Awake()
  {
    creditsScreen.SetActive(false);
    controlsScreen.SetActive(false);
  }

  public void NewGame(string sName)
  {
    GameManager.Instance.LoadScene(sName);
  }
  public void ExitGame()
  {
    GameManager.Instance.ExitGame();
  }
  public void ShowCredits()
  {
    creditsScreen.SetActive(true);
  }
  public void HideCredits()
  {
    creditsScreen.SetActive(false);
  }
  public void ShowControls()
  {
    controlsScreen.SetActive(true);
  }
  public void HideControls()
  {
    controlsScreen.SetActive(false);
  }

  #region Sound

  public void PlayNewGameSound()
  {
        AkSoundEngine.PostEvent("UI_NewGame", gameObject);
  }

  public void PlayClickSound()
  {
        AkSoundEngine.PostEvent("UI_Click", gameObject);
  }

    public void HoverSound()
    {
        AkSoundEngine.PostEvent("UI_Hover", gameObject);
    }
  #endregion
}


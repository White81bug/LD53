using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
  [SerializeField] private GameObject creditsScreen;

  private void Awake()
  {
    creditsScreen.SetActive(false);
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

  #region Sound

  public void PlayNewGameSound()
  {
    
  }

  public void PlayClickSound()
  {
    
  }
  #endregion
}


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

  public void ShowCredits()
  {
    creditsScreen.SetActive(true);
  }

  public void HideCredits()
  {
    creditsScreen.SetActive(false);
  }
}


using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private bool isStarted;
    private State _curState = State.Pause;
    public enum State
    {
        Pause,
        Play,
        Win,
        Lose
    }
    public enum Scene
    {
        MainMenu,
        GameScene
    }
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);

        isStarted = false;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
           // Debug.Log("prokatilo");
            SetStatement(1);
        }
    }

    public void SetStatement(int value)
    {
        Debug.Log($"set {value}");
        _curState = (State)value;
        switch (_curState)
        {
            case State.Play: Play(); break;
            case State.Pause: Pause(); break;
            case State.Win: Win(); break;
            case State.Lose: Lose(); break;
        }
    }

    private void Play()
    {
        //выключить экран паузы
        if (!isStarted)
        {
            UIManager.Instance.EnableSlider();
            Oxygen.Instance.StartBreathing();
            Time.timeScale = 1;
            isStarted = true;
        }
        else{Time.timeScale = 1;}
        UIManager.Instance.DisablePauseScreen();
    }
    private void Pause()
    {
        Time.timeScale = 0;
        //включаем экран паузы.
        if(isStarted)UIManager.Instance.EnablePauseScreen();
    }
    private void Win()
    {
        Time.timeScale = 0;
        //Экран выигрыша
        UIManager.Instance.EnableWinScreen();
    }
    private void Lose()
    {
        Time.timeScale = 0;
        //Экран проигрыша
        UIManager.Instance.EnableLossScreen();
    }
    
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        var async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
            yield return null;

        async.allowSceneActivation = true;

    }
    public void ExitGame()
    {
        Application.Quit();
    }

}

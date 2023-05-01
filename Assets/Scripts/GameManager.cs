using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public sealed class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool isStarted;
    private State _curState = State.Pause;
    private InputActions _inputActions;

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

        _inputActions = new InputActions();
        _inputActions.Enable();
        _inputActions.Player.Pause.performed += Pause;
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
           // Debug.Log("prokatilo");
            SetStatement(1);
        }else if (scene.name == "MainMenu")
        {
           ResetGameState();
        }
    }

    public void SetStatement(int value)
    {
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
    private void Pause(InputAction.CallbackContext ctx)
    {
        if (_curState != State.Pause)
        {
            _curState = State.Pause;
            Time.timeScale = 0;
            if (isStarted) UIManager.Instance.EnablePauseScreen();
        }
        else
        {
            Time.timeScale = 1;
            if (isStarted) UIManager.Instance.DisablePauseScreen();
        }
        //включаем экран паузы.
    }
    private void Pause()
    {
        if (_curState != State.Pause)
        {
            Time.timeScale = 0;
            if (isStarted) UIManager.Instance.EnablePauseScreen();
        }
        else
        {
            Time.timeScale = 1;
            if (isStarted) UIManager.Instance.DisablePauseScreen();
        }
        //////Time.timeScale = 0;
        ////////включаем экран паузы.
        //////if(isStarted)UIManager.Instance.EnablePauseScreen();
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

    public void ResetGameState()
    {
        isStarted = false;
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

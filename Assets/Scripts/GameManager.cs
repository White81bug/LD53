using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private bool isStarted;
    private State _curState = State.Play;
    public enum State
    {
        Pause,
        Win,
        Play,
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
    }

    public void SetStatement(State state)
    {
        _curState = state;
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
        UIManager.Instance.DisablePauseScreen();
        if (!isStarted)
        {
            UIManager.Instance.EnableSlider();
            Oxygen.Instance.StartBreathing();
            Time.timeScale = 1;
            isStarted = true;
        }
        else{Time.timeScale = 1;}
    }
    private void Pause()
    {
        Time.timeScale = 0;
        //включаем экран паузы.
        UIManager.Instance.EnablePauseScreen();
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

    public IEnumerator LoadSceneAsync(Scene scene)
    {
        var async = SceneManager.LoadSceneAsync(scene.ToString());
        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
            yield return null;

        async.allowSceneActivation = true;

    }
    
}

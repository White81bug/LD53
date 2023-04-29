using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [Header("Scripts")]
    public TransformParentManager TransformParentManager;
    public PlayerCollect PlayerCollect;
    public InputManager InputManager;
    
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
        
    }
    private void Pause()
    {
        
    }
    private void Win()
    {
        
    }
    private void Lose()
    {
        
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

using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameManager : SerializedMonoBehaviour
{
    public static IngameManager instance;

    public Transform mousePointer;
    public Transform player;

    public GameState gameState = GameState.Prepare;
    [SerializeField] List<IGameSignal> signals = new List<IGameSignal>();

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        player = Player.instance.transform;
        Prepare();
        StartGame();
    }

    //private void Update()
    //{
    //    ColorDebug.DebugGreen(Time.time);
    //}

    public void AddSignal(IGameSignal _newSignal)
    {
        signals.Add(_newSignal);
    }

    public void Lose()
    {
        gameState = GameState.Lose;
        foreach (var signal in signals)
        {
            signal.Lose();
        }
    }

    public void Pause()
    {
        gameState = GameState.Pause;
        Time.timeScale = 0;
        foreach (var signal in signals)
        {
            signal.Pause();
        }
    }

    public void Prepare()
    {
        gameState = GameState.Prepare;
        foreach (var signal in signals)
        {
            signal.Prepare();
        }
    }

    public void Resume()
    {
        gameState = GameState.Resume;
        Time.timeScale = 1;
        foreach (var signal in signals)
        {
            signal.Resume();
        }
    }

    public void Win()
    {
        gameState = GameState.Win;
        foreach (var signal in signals)
        {
            signal.Win();
        }
    }

    public void StartGame()
    {
        gameState = GameState.StartGame;
        foreach (var signal in signals)
        {
            signal.StartGame();
        }
    }
}

public interface IGameSignal
{
    void Prepare();
    void StartGame();
    void Pause();
    void Resume();
    void Win();
    void Lose();


}

public enum GameState
{
    Prepare, StartGame, Pause, Resume, Win, Lose
}


using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameManager : SerializedMonoBehaviour
{
    public Transform ___test;

    public static IngameManager instance;

    public Transform player;
    public MapScene mapScene;

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
        foreach (var signal in signals)
        {
            signal.Pause();
        }
    }

    public void Prepare()
    {
        gameState = GameState.Prepare;
        EnemySpawner.instance.StartSetup();
        foreach (var signal in signals)
        {
            signal.Prepare();
        }
    }

    public void Resume()
    {
        gameState = GameState.Resume;
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
        
        
        EnemySpawner.instance.StartSpawning();


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


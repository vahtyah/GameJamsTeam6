using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuPanel : MonoBehaviour
{
    [SerializeField] Button resumeButton, exitButton;

    private void Start()
    {
        resumeButton.onClick.AddListener(ResumeButtonOnClick);
        exitButton.onClick.AddListener(ExitButtonOnClick);
    }

    private void ExitButtonOnClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif 
        Application.Quit();
    }

    private void ResumeButtonOnClick()
    {
        IngameManager.instance.Resume();
    }
}
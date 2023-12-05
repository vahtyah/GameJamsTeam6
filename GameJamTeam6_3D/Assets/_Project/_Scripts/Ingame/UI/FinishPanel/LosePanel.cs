using UnityEngine;
using UnityEngine.UI;
using _Project._Scripts.Utils;
public class LosePanel : MonoBehaviour
{
    [SerializeField] private Button restartButton, exitButton;
    
    private void Start()
    {
        restartButton.onClick.AddListener(RestartButtonOnClick);
        exitButton.onClick.AddListener(ExitButtonOnClick);
    }

    private void ExitButtonOnClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    private void RestartButtonOnClick() {
        StartCoroutine(LoadingPanel.instance.IELoadScene(SceneManager.Scene.GamePlay));
    }
}
using System;
using System.Collections;
using _Project._Scripts.Utils;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class LoadingPanel : MonoBehaviour
{
    public static LoadingPanel instance;
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI progressText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        StartCoroutine(IELoadScene(SceneManager.Scene.GamePlay));
    }

    public IEnumerator IELoadScene(SceneManager.Scene scene)
    {
        gameObject.SetActive(true);
        slider.value = 0;
        var async = SceneManager.LoadSceneSync(scene);
        async.allowSceneActivation = false;
        var progress = 0f;
        while (!async.isDone)
        {
            progress = Mathf.MoveTowards(progress, async.progress, Time.deltaTime);
            slider.value = progress;
            progressText.text = (int)(progress * 100f) + "%";
            if (progress >= .9f)
            {
                slider.value = 1;
                progress = 1f;
                progressText.text = (int)(progress * 100f) + "%";
                yield return new WaitForEndOfFrame();
                async.allowSceneActivation = true;
                break;
            }
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        yield return null;
    }

    public void LoadingMidScene(Action _callBack)
    {
        gameObject.SetActive(true);
        StartCoroutine(IELoading(_callBack));
    }

    IEnumerator IELoading(Action _callBack)
    {
        slider.value = 0;
        float progress = 0f;
        while (progress < 0.9f)
        {
            progress = Mathf.MoveTowards(progress, 1, Time.deltaTime);
            slider.value = progress;
            progressText.text = (int)(progress * 100f) + "%";
            yield return null;
        }
        slider.value = 1;
        progress = 1f;
        progressText.text = (int)(progress * 100f) + "%";
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        _callBack?.Invoke();
    }

}

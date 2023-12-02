using System.Collections;
using _Project._Scripts.Utils;
using TMPro;
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

    private IEnumerator IELoadScene(SceneManager.Scene scene)
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
            if (progress >= .9f)
            {
                slider.value = 1;
                async.allowSceneActivation = true;
                yield return new WaitForEndOfFrame();
                gameObject.SetActive(false);
            }
            progressText.text = (int)(progress * 100f) + "%";
            yield return null;
        }
    }

    public IEnumerator IELoading()
    {
        gameObject.SetActive(true);
        slider.value = 0;
        float progress = 0f;
        while (progress < 0.9f)
        {
            progress = Mathf.MoveTowards(progress, 1, Time.deltaTime);
            progressText.text = (int)(progress * 100f) + "%";
        }
        slider.value = 1;
        progressText.text = (int)(progress * 100f) + "%";
        yield return new WaitForEndOfFrame();
        gameObject.SetActive(false);
    }

}

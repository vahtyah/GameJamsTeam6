using System.Collections;
using _Project._Scripts.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project._Scripts.Ingame.Manager
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private TextMeshProUGUI progressText;

        private void Start()
        {
            StartCoroutine(IELoadScene(SceneManager.Scene.GamePlay));
        }

        private IEnumerator IELoadScene(SceneManager.Scene scene)
        {
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
                }
                progressText.text = (int)(progress * 100f) + "%";

                yield return null;
            }
        }
    }
}
using UnityEngine;

namespace _Project._Scripts.Utils
{
    public class SceneManager
    {
        public enum Scene
        {
            Loading,
            GamePlay
        }
        
        public static void LoadScene(Scene scene)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene.ToString());
        }
        
        public static AsyncOperation LoadSceneSync(Scene scene)
        {
            return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene.ToString());
        }
    }
}
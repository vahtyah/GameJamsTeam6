using UnityEngine;
using System.Collections;

namespace EpicToonFX
{
    public class ETFXLightFade : MonoBehaviour
    {
        [Header("Seconds to dim the light")]
        public float life = 0.2f;
        public bool killAfterLife = true;
        private float initIntensity;

        // Use this for initialization
        void Start()
        {
            initIntensity = life;
        }

        // Update is called once per frame
        void Update()
        {
            initIntensity -= Time.deltaTime;
            if (killAfterLife && initIntensity <= 0)
                gameObject.SetActive(false);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemDeactivationAfterPlay : MonoBehaviour
{
    private ParticleSystem fx;
   

    private void Awake()
    {
        // Assuming the Particle System is attached to the same GameObject
        fx = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (fx.isStopped)
        {
            gameObject.SetActive(false);

        }


    }
}

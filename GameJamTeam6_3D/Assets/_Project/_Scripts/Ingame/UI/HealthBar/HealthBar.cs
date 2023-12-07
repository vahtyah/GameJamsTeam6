using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : SerializedMonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private IEnemy enemy;

    private CharacterHealth characterHealth;
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
        //enemy = transform.GetComponentInParent<IEnemy>();
        characterHealth = enemy.GetCharacterHealth();
        characterHealth.AddSignalHealthChange(UpdateHealthBar);
    }

    private void OnEnable()
    {
        slider.gameObject.SetActive(false);
    }

    private void UpdateHealthBar(int obj)
    {
        slider.value = characterHealth.GetHealthAmountNormalized();
        if(!slider.gameObject.activeSelf) slider.gameObject.SetActive(true);
    }

    private void LateUpdate()
    {
        if (camera != null) transform.LookAt(transform.position + camera.transform.forward);
    }
}
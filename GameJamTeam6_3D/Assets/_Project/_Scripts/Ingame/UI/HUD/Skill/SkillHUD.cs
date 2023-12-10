using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillHUD : MonoBehaviour
{
    [SerializeField] SkillID skillID;
    [SerializeField] Image skillIcon;
    [SerializeField] Image cooldownIcon;
    [SerializeField] TextMeshProUGUI cooldownText;
    [SerializeField] TextMeshProUGUI inputText;
    Skill skill;

    private void Start()
    {
        skill = SkillManager.Instance.GetSkill(skillID);
        skill.OnSkillUsed += OnSkillUsed;
        Init();
    }

    private void Init()
    {
        skillIcon.sprite = skill.GetSkillData().icon;
        cooldownIcon.sprite = skill.GetSkillData().icon;
        inputText.text = skill.GetSkillData().inputKeyString;
        cooldownIcon.fillAmount = 0;
        cooldownText.gameObject.SetActive(false);
    }

    private void OnSkillUsed()
    {
        cooldownIcon.fillAmount = 1;
        cooldownText.text = skill.GetSkillData().cooldownTime.ToString();
        cooldownText.gameObject.SetActive(true);
    }
    
    private void Update()
    {
        if (cooldownIcon.fillAmount <= 0) return;
        cooldownIcon.fillAmount -= Time.deltaTime / skill.GetSkillData().cooldownTime;
        cooldownText.text = (cooldownIcon.fillAmount * skill.GetSkillData().cooldownTime).ToString("F1", CultureInfo.InvariantCulture);
        if (cooldownIcon.fillAmount <= 0)
        {
            cooldownText.gameObject.SetActive(false);
        }
    }

    private void OnValidate()
    {
        name = skillID.ToString() + " Skill HUD";
    }
}
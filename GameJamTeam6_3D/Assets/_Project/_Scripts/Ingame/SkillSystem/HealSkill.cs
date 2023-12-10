using System.Collections;
using UnityEngine;

public class HealSkill : Skill
{
    [SerializeField] private int healAmount;
    ParticleSystem healEffect;
    protected override void UseSkill()
    {
        Player.instance.AddHealth(healAmount);
        healEffect = ProjectileImpactPooling.instance.Activate(7);
        healEffect.transform.position = Player.instance.transform.position;
        Player.instance.StartCoroutine(IEWaitForHeal(healEffect.main.duration));
    }

    private IEnumerator IEWaitForHeal(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        healEffect.gameObject.SetActive(false);
        //ProjectileImpactPooling.instance.Deactive(7, healEffect);
    }
}
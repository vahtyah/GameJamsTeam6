using UnityEngine;

[CreateAssetMenu(fileName = "Skill Data 1", menuName = "Skill Data", order = 0)]
public class SkillSO : ScriptableObject
{
    public Sprite icon;
    public float cooldownTime;
    public string inputKeyString;
}
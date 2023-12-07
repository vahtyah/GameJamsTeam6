using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace _Project._Scripts.Ingame.UI.PlayerInfo
{
    public class PlayerInfoPanel : MonoBehaviour
    {
        [SerializeField] private Transform UnderHealthBar, OverHealthBar;
        //[SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private float smoothTime = 0.5f;
        private Player player;

        private void Start()
        {
            player = Player.instance;
            Player.instance.GetCharacterHealth().onCurHealthChange += UpdateHealthBar;
            //LevelConfig.instance.onLevelChange += UpdateLevelText;
        }

        private void UpdateHealthBar(int health)
        {
            var healthAmountNormalized = player.GetHealthAmountNormalized();
            OverHealthBar.localScale = new Vector3(healthAmountNormalized, 1f, 1f);
            UnderHealthBar.DOScaleX(healthAmountNormalized, smoothTime);
        }
        
        //private void UpdateLevelText(int level)
        //{
        //    levelText.text = "Level " + (level + 1);
        //}
    }
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemInfoText : MonoBehaviour
{
    public static ItemInfoText instance;

    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI itemDesStat;

    private void Awake()
    {
        instance = this;
    }

    




}

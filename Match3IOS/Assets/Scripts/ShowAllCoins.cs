using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowAllCoins : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText;
        
    private void Start()
    {
        RefreshVision();
    }

    private void OnEnable()
    {
        EventBus.OnGoldChanged += RefreshVision;
    }

    private void OnDisable()
    {
        EventBus.OnGoldChanged -= RefreshVision;        
    }

    private void RefreshVision()
    { 
        goldText.text = CoinManager.Instance.GetCoinsAmountOverall().ToString();       
    }
}

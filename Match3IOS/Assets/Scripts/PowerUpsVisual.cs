using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerUpsVisual : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bombAmount;
    [SerializeField] private TextMeshProUGUI LightningAmount;
    [SerializeField] private TextMeshProUGUI broomAmount;
    [SerializeField] private TextMeshProUGUI CoinUpAmount;

    private void OnEnable()
    {
        EventBus.OnBoughtPowerUp += RefreshVisual;
    }

    private void OnDisable()
    {
        EventBus.OnBoughtPowerUp -= RefreshVisual;        
    }

    private void Start()
    {
        RefreshVisual();
    }

    private void RefreshVisual()
    {
        bombAmount.text = BonusesManager.Instance.GetBonusesTypeAmount("Bomb").ToString();
        LightningAmount.text = BonusesManager.Instance.GetBonusesTypeAmount("Lightning").ToString();
        broomAmount.text = BonusesManager.Instance.GetBonusesTypeAmount("Broom").ToString();
        CoinUpAmount.text = BonusesManager.Instance.GetBonusesTypeAmount("CoinUp").ToString();
    }
}

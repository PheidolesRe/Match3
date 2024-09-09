using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBuyBonuses : MonoBehaviour
{
    [SerializeField] private Button purchaseButton;
    [SerializeField] private string bonusesType;
    [SerializeField] private int amount;

    private void Awake()
    {
        purchaseButton.onClick.AddListener(() =>
        {
            BuyBonuses();
        });
    }


    private void BuyBonuses()
    {
        if (GetComponent<SpendGold>().GetCost() <= CoinManager.Instance.GetCoinsAmountOverall())
        {
            BonusesManager.Instance.AddBonuses(bonusesType, amount);
            EventBus.OnBoughtPowerUp?.Invoke();
        }
    }
}

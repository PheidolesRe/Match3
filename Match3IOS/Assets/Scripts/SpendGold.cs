using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpendGold : MonoBehaviour
{
    [SerializeField] private Button purchaseButton;
    [SerializeField] private int cost;

    private void Awake()
    {
        purchaseButton.onClick.AddListener(() =>
        {
            CoinManager.Instance.SubtractCoins(cost);
        });
    }

    public int GetCost()
    { 
        return cost;
    }


}

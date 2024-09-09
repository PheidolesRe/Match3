using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUpPowerUp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI powerUpAmmountText;
    [SerializeField] private string powerUpType;

    private Button scoreUpButton;
    private bool isPowerUpUsed = false;

    private void Start()
    {
        powerUpAmmountText.text = BonusesManager.Instance.GetBonusesTypeAmount(powerUpType).ToString();
    }

    private void Awake()
    {
        scoreUpButton = GetComponent<Button>();

        scoreUpButton.onClick.AddListener(() =>
        {
            if (BonusesManager.Instance.GetBonusesTypeAmount(powerUpType) > 0 && !isPowerUpUsed)
            {
                isPowerUpUsed = true;
                DecreaseAndRefreshVisualAmmount();
                CoinManager.Instance.SetCoinMultiplyTo2();
            }
        });
    }

    private void DecreaseAndRefreshVisualAmmount()
    {
        BonusesManager.Instance.SubtracBonusesAmount(powerUpType);
        powerUpAmmountText.text = BonusesManager.Instance.GetBonusesTypeAmount(powerUpType).ToString();
    }
}

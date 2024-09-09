using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelPassedUI : MonoBehaviour
{
    [SerializeField] private GameObject lvlPassedUI;
    [SerializeField] private TextMeshProUGUI goldEarnedText;

    private void OnEnable()
    {
        EventBus.OnGoalReached += Show;
    }

    private void OnDisable()
    {
        EventBus.OnGoalReached -= Show;        
    }

    private void Awake()
    {
        lvlPassedUI.SetActive(false);  
    }

    public void Show()
    { 
        goldEarnedText.text = "+ " + CoinManager.Instance.GetCoinAmmountAtLvl().ToString();
        lvlPassedUI.SetActive(true);
    }
}

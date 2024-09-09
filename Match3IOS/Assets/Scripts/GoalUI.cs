using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class GoalUI : MonoBehaviour
{
    [SerializeField] private Sprite[] spriteArray;
    [SerializeField] private Image firstGoalImage;
    [SerializeField] private Image secondGoalImage;
    [SerializeField] private TextMeshProUGUI firstGoalText;
    [SerializeField] private TextMeshProUGUI secondGoalText;

    private CHIPS firstGoalChipType;
    private CHIPS secondGoalChipType;
    private int randomFirstIndex;
    private int randomSecondIndex;
    private bool isWon = false;


    public enum CHIPS
    {
        Coin,
        Ruby,
        Sapphire,
        Star
    }

    private void OnEnable()
    {
        EventBus.OnChipDestroyd += CheckAndRefreshGoal;
    }

    private void OnDisable()
    {
        EventBus.OnChipDestroyd -= CheckAndRefreshGoal;        
    }

    private void Start()
    {
        SetGoalType();
        SetAmountGoal();
    }

    private void SetAmountGoal()
    {
        firstGoalText.text = UnityEngine.Random.Range(45, 66).ToString();
        secondGoalText.text = UnityEngine.Random.Range(45, 66).ToString();
    }

    private void SetGoalType()
    { 
        while (randomFirstIndex == randomSecondIndex)
        { 
            randomFirstIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(CHIPS)).Length);
            randomSecondIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(CHIPS)).Length);
        }

        firstGoalChipType = (CHIPS)Enum.GetValues(typeof(CHIPS)).GetValue(randomFirstIndex);
        secondGoalChipType = (CHIPS)Enum.GetValues(typeof(CHIPS)).GetValue(randomSecondIndex);
        SetSprite(firstGoalImage, firstGoalChipType);
        SetSprite(secondGoalImage, secondGoalChipType);
    }

    private void SetSprite(Image targetImage, CHIPS targetGoal)
    {
        switch (targetGoal)
        { 
            case CHIPS.Coin:
                targetImage.sprite = spriteArray[0];
                break;
            case CHIPS.Ruby:
                targetImage.sprite = spriteArray[1];
                break;
            case CHIPS.Sapphire:
                targetImage.sprite = spriteArray[2];
                break;
            case CHIPS.Star:
                targetImage.sprite = spriteArray[3];
                break;
        }
    }

    public void CheckAndRefreshGoal(string chipType)
    {
        if (chipType == firstGoalChipType.ToString() && firstGoalText.text != "0")
        {
            int newcount = Int32.Parse(firstGoalText.text);
            firstGoalText.text = (newcount - 1).ToString();
        }

        if (chipType == secondGoalChipType.ToString() && secondGoalText.text != "0")
        {
            int newcount = Int32.Parse(secondGoalText.text);
            secondGoalText.text = (newcount - 1).ToString();
        }

        if (firstGoalText.text == "0" && secondGoalText.text == "0" && !isWon)
        {
            //Won
            isWon = true;
            int coinForWon = UnityEngine.Random.Range(3, 7) * 50;
            CoinManager.Instance.AddCoinForWin(coinForWon);

            EventBus.OnGoalReached?.Invoke();
        }
    }
}

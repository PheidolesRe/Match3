using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseBackground : MonoBehaviour
{
    [SerializeField] private int backIndex;
    [SerializeField] private GameObject chooseObject;
    [SerializeField] private Button purchaseButton;
    [SerializeField] private Button chooseButton;
    [SerializeField] private Button chosenButton;

    private void OnEnable()
    {
        EventBus.OnChosenAnotherBackground += ShowChoosenOrNot;
    }

    private void OnDisable()
    {
        EventBus.OnChosenAnotherBackground -= ShowChoosenOrNot;        
    }


    private void Awake()
    {
        purchaseButton.onClick.AddListener(() =>
        {
            if (CoinManager.Instance.GetCoinsAmountOverall() >= gameObject.GetComponent<SpendGold>().GetCost())
            {
                BackgroundManager.Instance.SetBoughtBackground(backIndex);
                ShowIfAlreadyBought();
            }
        });

        chooseButton.onClick.AddListener(() =>
        {
            BackgroundManager.Instance.SetChosenBackground(backIndex);
            EventBus.OnChosenAnotherBackground?.Invoke();
        });
    }


    private void Start()
    {
        chooseObject.SetActive(false);
        ShowIfAlreadyBought();
    }

    private void ShowIfAlreadyBought()
    {
        if (BackgroundManager.Instance.GetBoughtBackground(backIndex))
        { 
            purchaseButton.gameObject.SetActive(false);
            chooseObject.SetActive(true);
        }
        ShowChoosenOrNot();
    }

    private void ShowChoosenOrNot()
    {
        if (chooseObject.activeSelf)
        {
            if (BackgroundManager.Instance.GetChosenBackground() == backIndex)
            {
                chosenButton.gameObject.SetActive(true);
                chooseButton.gameObject.SetActive(false);
            }
            else
            {
                chosenButton.gameObject.SetActive(false);
                chooseButton.gameObject.SetActive(true);
            }
        }
    }

}

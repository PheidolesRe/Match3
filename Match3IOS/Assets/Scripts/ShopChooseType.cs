using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopChooseType : MonoBehaviour
{
    [SerializeField] private Button backBuyButton;
    [SerializeField] private Button bonusesBuyButton;
    [SerializeField] private Sprite backSpriteShadowed;
    [SerializeField] private Sprite backSpriteChosen;
    [SerializeField] private Sprite bonusesSpriteShadowed;
    [SerializeField] private Sprite bonusesSpriteChosen;
    [SerializeField] private GameObject backGameObject;
    [SerializeField] private GameObject bonusesGameObject;
    [SerializeField] private GameObject bonusesVisualAmount;

    private void Awake()
    {
        backBuyButton.onClick.AddListener(() =>
        {
            ChooseBack();
        });

        bonusesBuyButton.onClick.AddListener(() =>
        {
            ChooseBonuses();
        });
    }

    private void Start()
    {
        ChooseBack();
    }

    private void ChooseBack()
    {
        backGameObject.SetActive(true);
        bonusesGameObject.SetActive(false);
        bonusesVisualAmount.SetActive(false);
        backBuyButton.gameObject.GetComponent<Image>().sprite = backSpriteChosen;
        bonusesBuyButton.gameObject.GetComponent<Image>().sprite = bonusesSpriteShadowed;
    }

    private void ChooseBonuses()
    {
        backGameObject.SetActive(false);
        bonusesGameObject.SetActive(true);
        bonusesVisualAmount.SetActive(true);
        backBuyButton.gameObject.GetComponent<Image>().sprite = backSpriteShadowed;
        bonusesBuyButton.gameObject.GetComponent<Image>().sprite = bonusesSpriteChosen;
    }
}

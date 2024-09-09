using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MovePowerUp : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject powerUpPrefab;
    [SerializeField] private TextMeshProUGUI powerUpAmmountText;
    [SerializeField] private string powerUpType;

    private float offsetPos = 0.5f;
    private GameObject newPowerUp;
    private bool isDown = false;

    private void Start()
    {
        powerUpAmmountText.text = BonusesManager.Instance.GetBonusesTypeAmount(powerUpType).ToString();
    }

    private void Update()
    {
        if (isDown) 
        {
            newPowerUp.GetComponent<IShowVisualPowerUp>().ShowArea(offsetPos);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Instantiate prefab
        // follow to touch
        
        if (BonusesManager.Instance.GetBonusesTypeAmount(powerUpType) > 0)
        { 
            isDown = true;
            DecreaseAndRefreshVisualAmmount();
            Vector3 spawnPos = (Vector2)Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) + new Vector2 (0, offsetPos);
            newPowerUp = Instantiate(powerUpPrefab, spawnPos, Quaternion.identity);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // use powerUp on chosen tile
        // if powerUp not above chip tile - restore powerup charge

        if (isDown) 
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(newPowerUp.transform.position, Vector2.up, 0.05f);
            Chips targetChip = hitInfo.collider?.gameObject.GetComponent<Chips>();
            if (targetChip == null)
            {
                IncreaseAndRefreshVisualAmmount();
            }

            newPowerUp.GetComponent<IUsePowerUp>().UsePowerUp();          
            Destroy(newPowerUp);
        }

        isDown = false;
    }

    private void DecreaseAndRefreshVisualAmmount()
    {
        BonusesManager.Instance.SubtracBonusesAmount(powerUpType);
        powerUpAmmountText.text = BonusesManager.Instance.GetBonusesTypeAmount(powerUpType).ToString();
    }

    private void IncreaseAndRefreshVisualAmmount()
    {
        BonusesManager.Instance.AddBonusesAmount(powerUpType);
        powerUpAmmountText.text = BonusesManager.Instance.GetBonusesTypeAmount(powerUpType).ToString();
    }
}

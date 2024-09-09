using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningPowerUp : MonoBehaviour, IShowVisualPowerUp, IUsePowerUp
{
    public void ShowArea(float offsetPos)
    {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) + new Vector2(0, offsetPos);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, 0.05f);
        Chips targetChip = hitInfo.collider?.gameObject.GetComponent<Chips>();
        if (targetChip != null)
        {
            int[] targetGridPos = targetChip.GetGridPos();

            for (int i = 0; i < MakeBoard.Instance.GetHeight(); i++)
            {
                MakeBoard.Instance.chipTiles[targetGridPos[0], i].gameObject.GetComponent<InteractIndicator>().ShowIndicator();
            }
        }
    }

    public void UsePowerUp()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, 0.05f);
        Chips targetChip = hitInfo.collider?.gameObject.GetComponent<Chips>();
        if (targetChip != null)
        {
            int[] targetGridPos = targetChip.GetGridPos();

            for (int i = 0; i < MakeBoard.Instance.GetHeight(); i++)
            {
                MakeBoard.Instance.chipTiles[targetGridPos[0], i].isMatched = true;
            }
            MakeBoard.Instance.DestroyMatches();
        }
    }

}

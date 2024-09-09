using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPowerUp : MonoBehaviour, IShowVisualPowerUp, IUsePowerUp
{
    public void ShowArea(float offsetPos)
    {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) + new Vector2(0, offsetPos);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, 0.05f);
        Chips targetChip = hitInfo.collider?.gameObject.GetComponent<Chips>();
        if (targetChip != null)
        {
            int[] midleGridPos = targetChip.GetGridPos();

            for (int i = midleGridPos[0] - 1; i <= midleGridPos[0] + 1; i++) 
            {
                if (i >= MakeBoard.Instance.GetWidth() || i < 0) continue;
                for (int j = midleGridPos[1] - 1; j <= midleGridPos[1] + 1; j++)
                {
                    if (j >= MakeBoard.Instance.GetHeight() || j < 0) continue;

                    MakeBoard.Instance.chipTiles[i, j].gameObject.GetComponent<InteractIndicator>().ShowIndicator();
                }
            }
        }
    }

    public void UsePowerUp()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, 0.05f);
        Chips targetChip = hitInfo.collider?.gameObject.GetComponent<Chips>();
        if (targetChip != null)
        {
            int[] midleGridPos = targetChip.GetGridPos();

            for (int i = midleGridPos[0] - 1; i <= midleGridPos[0] + 1; i++)
            {
                if (i >= MakeBoard.Instance.GetWidth() || i < 0) continue;
                for (int j = midleGridPos[1] - 1; j <= midleGridPos[1] + 1; j++)
                {
                    if (j >= MakeBoard.Instance.GetHeight() || j < 0) continue;

                    MakeBoard.Instance.chipTiles[i, j].isMatched = true;
                }
            }
            MakeBoard.Instance.DestroyMatches();
        }
    }

}

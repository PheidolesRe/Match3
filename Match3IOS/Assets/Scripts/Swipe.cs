using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    [SerializeField] private float swipeBoarding;
    [SerializeField] private float swipeDelay;

    private Chips currentChips;
    private Vector2 startTouchPos;
    private Vector2 endTouchPos;

    private void Awake()
    {
        currentChips = GetComponent<Chips>();
    }


    private void OnMouseDown()
    {
        startTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(startTouchPos);
    }

    private void OnMouseUp()
    {
        endTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);        
        //Debug.Log(endTouchPos);
        CalculateDirection(); 
    }

    private void CalculateDirection()
    { 
        Vector2 swipeDir = endTouchPos - startTouchPos;
        if (Mathf.Abs(swipeDir.x) < swipeBoarding && Mathf.Abs(swipeDir.y) < swipeBoarding) return;
        
        float swipeDirAngle = Mathf.Atan2(swipeDir.y, swipeDir.x) * Mathf.Rad2Deg;

        //Debug.Log(Mathf.Atan2(swipeDir.y, swipeDir.x));

        MoveChip(swipeDirAngle);
    }


    private void MoveChip(float swipeAngle)
    {
        int[] currentChipPos = currentChips.GetGridPos();

        if (swipeAngle >= -45 && swipeAngle < 45)
        {
            // Right
            if (currentChipPos[0] + 1 >= MakeBoard.Instance.GetWidth())
            {
                return;
            }

            if (MakeBoard.Instance.chipTiles[currentChipPos[0], currentChipPos[1]].isSwitching || MakeBoard.Instance.chipTiles[currentChipPos[0] + 1, currentChipPos[1]].isSwitching)
            {
                return;
            }

            MakeBoard.Instance.SwapChipsPos(currentChipPos[0], currentChipPos[1], currentChipPos[0] + 1, currentChipPos[1], swipeDelay);
            StartCoroutine(SwipeBackRoutine(currentChipPos[0], currentChipPos[1], currentChipPos[0] + 1, currentChipPos[1]));
        }
        if (swipeAngle >= 45 && swipeAngle < 135)
        {
            // Up
            if (currentChipPos[1] + 1 >= MakeBoard.Instance.GetHeight())
            {
                return;
            }

            if (MakeBoard.Instance.chipTiles[currentChipPos[0], currentChipPos[1]].isSwitching || MakeBoard.Instance.chipTiles[currentChipPos[0], currentChipPos[1] + 1].isSwitching)
            {
                return;
            }

            MakeBoard.Instance.SwapChipsPos(currentChipPos[0], currentChipPos[1], currentChipPos[0], currentChipPos[1] + 1, swipeDelay);
            StartCoroutine(SwipeBackRoutine(currentChipPos[0], currentChipPos[1], currentChipPos[0], currentChipPos[1] + 1));
        }
        if (swipeAngle >= 135 || swipeAngle < -135)
        {
            // Left
            if (currentChipPos[0] - 1 < 0)
            {
                return;
            }

            if (MakeBoard.Instance.chipTiles[currentChipPos[0], currentChipPos[1]].isSwitching || MakeBoard.Instance.chipTiles[currentChipPos[0] - 1, currentChipPos[1]].isSwitching)
            {
                return;
            }

            MakeBoard.Instance.SwapChipsPos(currentChipPos[0], currentChipPos[1], currentChipPos[0] - 1, currentChipPos[1], swipeDelay);
            StartCoroutine(SwipeBackRoutine(currentChipPos[0], currentChipPos[1], currentChipPos[0] - 1, currentChipPos[1]));
        }
        if (swipeAngle >= -135 && swipeAngle < -45)
        {
            // Down
            if (currentChipPos[1] - 1 < 0)
            {
                return;
            }

            if (MakeBoard.Instance.chipTiles[currentChipPos[0], currentChipPos[1]].isSwitching || MakeBoard.Instance.chipTiles[currentChipPos[0], currentChipPos[1] - 1].isSwitching)
            {
                return;
            }

            MakeBoard.Instance.SwapChipsPos(currentChipPos[0], currentChipPos[1], currentChipPos[0], currentChipPos[1] - 1, swipeDelay);
            StartCoroutine(SwipeBackRoutine(currentChipPos[0], currentChipPos[1], currentChipPos[0], currentChipPos[1] - 1));
        }
    }


    private IEnumerator SwipeBackRoutine(int choosenChipColumn, int choosenChipRow, int targetChipColumn, int targetChipRow)
    {
        yield return new WaitForSeconds(swipeDelay);
        if (!currentChips.isMatched && !MakeBoard.Instance.chipTiles[choosenChipColumn, choosenChipRow].isMatched)
        {
            MakeBoard.Instance.SwapChipsPos(choosenChipColumn, choosenChipRow, targetChipColumn, targetChipRow, swipeDelay);
        }
        else
        {
            MakeBoard.Instance.DestroyMatches();
        }
    }
}

using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Chips : MonoBehaviour
{
    [SerializeField] private Sprite[] chipsPrefab;
    [SerializeField] private GameObject deathVFXPrefab;

    private int column;
    private int row;
    public bool isSwitching = false;
    public bool isMatched = false;
    private CHIPS currentChipType;
    private SpriteRenderer spriteRenderer;
    private Tween tween;


    public enum CHIPS
    {
        Coin,
        Ruby,
        Sapphire,
        Star
    }

    private void OnEnable()
    {
        EventBus.OnBoardChanged += FindMatches;
    }

    private void OnDisable()
    {
        EventBus.OnBoardChanged -= FindMatches;
    }

    //private void OnDestroy()
    //{
    //    tween.Kill();
    //    Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
    //    EventBus.OnChipDestroyd?.Invoke(currentChipType.ToString());

    //    if (SettingManager.Instance.GetIsVibrationOn())
    //    {
    //        Handheld.Vibrate();
    //    }
    //}

    public void ChipDeath()
    {
        tween.Kill();
        Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
        EventBus.OnChipDestroyd?.Invoke(currentChipType.ToString());

        if (SettingManager.Instance.GetIsVibrationOn())
        {
            Handheld.Vibrate();
        }
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetType();
    }

    public void SetType()
    {
        int randomIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(CHIPS)).Length);
        currentChipType = (CHIPS)Enum.GetValues(typeof(CHIPS)).GetValue(randomIndex);
        SetTile();
    }

    private void SetTile()
    {
        switch (currentChipType)
        {
            case CHIPS.Coin:
                spriteRenderer.sprite = chipsPrefab[0];
                break;
            case CHIPS.Ruby:
                spriteRenderer.sprite = chipsPrefab[1];
                break;
            case CHIPS.Sapphire:
                spriteRenderer.sprite = chipsPrefab[2];
                break;
            case CHIPS.Star:
                spriteRenderer.sprite = chipsPrefab[3];
                break;
        }
    }

    public CHIPS GetChipType()
    {
        return currentChipType;
    }

    public void SetGridPos(int column, int row)
    {
        this.column = column;
        this.row = row;
    }

    public void SetGridAndWorldPos(int column, int row, float timeDelay)
    {
        MakeSmoothlyMove(column, row, timeDelay);
        this.column = column;
        this.row = row;
    }

    public void MakeSmoothlyMove(int column, int row, float timeDelay)
    {
        tween.Kill();
        Vector2 newPos = new Vector2(column * MakeBoard.Instance.boardCoef, row * MakeBoard.Instance.boardCoef);
        tween = transform.DOMove(newPos, timeDelay).OnComplete(() => SetGridPos(column, row));
    }


    public int[] GetGridPos()
    {
        return new int[] { column, row };
    }

    public void ChangeWorldPos(Vector2 newPos)
    {
        transform.position = newPos;
    }

    public void FindMatches()
    {
        if (column > 0 && column < MakeBoard.Instance.GetWidth() - 1)
        {
            if (MakeBoard.Instance.chipTiles[column - 1, row] != null && MakeBoard.Instance.chipTiles[column + 1, row] != null) 
            {
                if (MakeBoard.Instance.chipTiles[column - 1, row].GetChipType() == currentChipType && MakeBoard.Instance.chipTiles[column + 1, row].GetChipType() == currentChipType)
                {
                    MakeBoard.Instance.chipTiles[column - 1, row].ToogleIsMatched();
                    MakeBoard.Instance.chipTiles[column + 1, row].ToogleIsMatched();
                    ToogleIsMatched();
                }
            }
        }

        if (row > 0 && row < MakeBoard.Instance.GetHeight() - 1)
        {
            if (MakeBoard.Instance.chipTiles[column, row - 1] != null && MakeBoard.Instance.chipTiles[column, row + 1] != null) 
            {
                if (MakeBoard.Instance.chipTiles[column, row - 1].GetChipType() == currentChipType && MakeBoard.Instance.chipTiles[column, row + 1].GetChipType() == currentChipType)
                {
                    MakeBoard.Instance.chipTiles[column, row - 1].ToogleIsMatched();
                    MakeBoard.Instance.chipTiles[column, row + 1].ToogleIsMatched();
                    ToogleIsMatched();
                }
            }
        }
    }

    public void ToogleIsMatched()
    {
        isMatched = true;
    }


    //private IEnumerator DestroyRoutine()
    //{
    //    yield return new WaitForEndOfFrame();
    //    MakeBoard.Instance.chipTiles[column, row] = null;
    //    if (SettingManager.Instance.GetIsVibrationOn())
    //    {
    //        Handheld.Vibrate();
    //    } 
    //    Destroy(gameObject);
    //}

    //public void DecriseUpTileRow()
    //{

    //    ChangeWorldPos(new Vector2(column * MakeBoard.Instance.boardCoef, (row - 1) * MakeBoard.Instance.boardCoef));
    //}
}

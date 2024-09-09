using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class MakeBoard : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] public float boardCoef;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject chipPrefab;
    [SerializeField] private float setUpChipsDelay;
    [SerializeField] private float tileFallDelay;

    public Chips[,] chipTiles;

    public static MakeBoard Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        chipTiles = new Chips[width, height];
        SetUpTiles();
    }

    private void SetUpTiles()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 startPos = new Vector2(3 * boardCoef, (15) * boardCoef);
                Vector2 backTilePos = new Vector2(i * boardCoef, j * boardCoef);
                GameObject chipTile = Instantiate(chipPrefab, startPos, Quaternion.identity);
                chipTiles[i, j] = chipTile.GetComponent<Chips>();
                HaveMatchesAt(i, j);
                chipTile.GetComponent<Chips>().SetGridAndWorldPos(i, j, setUpChipsDelay);
                chipTile.name = "( " + i + ";" + j + " )";

                GameObject backTile = Instantiate(tilePrefab, backTilePos, Quaternion.identity);
                backTile.transform.parent = transform;
                backTile.name = "( " + i + ";" + j + " )";
            }
        }
    }

    public void SwapChipsPos(int choosenChipColumn, int choosenChipRow, int targetChipColumn, int targetChipRow, float swipeDelay)
    {
        if (targetChipColumn >= width || targetChipRow >= height || targetChipColumn < 0 || targetChipRow < 0)
        {
            return;
        }
        
        Chips buffChip = chipTiles[choosenChipColumn, choosenChipRow];

        chipTiles[choosenChipColumn, choosenChipRow].SetGridAndWorldPos(targetChipColumn, targetChipRow, swipeDelay);
        chipTiles[choosenChipColumn, choosenChipRow] = chipTiles[targetChipColumn, targetChipRow];

        chipTiles[targetChipColumn, targetChipRow].SetGridAndWorldPos(choosenChipColumn, choosenChipRow, swipeDelay);
        chipTiles[targetChipColumn, targetChipRow] = buffChip;
        EventBus.OnBoardChanged?.Invoke();

        StartCoroutine(InrteractDelayRoutine(choosenChipColumn, choosenChipRow, targetChipColumn, targetChipRow, swipeDelay));
    }

    private IEnumerator InrteractDelayRoutine(int choosenChipColumn, int choosenChipRow, int targetChipColumn, int targetChipRow, float swipeDelay)
    {
        chipTiles[choosenChipColumn, choosenChipRow].isSwitching = true;
        chipTiles[targetChipColumn, targetChipRow].isSwitching = true;
        yield return new WaitForSeconds(swipeDelay);
        chipTiles[choosenChipColumn, choosenChipRow].isSwitching = false;
        chipTiles[targetChipColumn, targetChipRow].isSwitching = false;
    }


    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }

    private void HaveMatchesAt(int column, int row)
    {

        bool foundMathes = false;

        if (column >= 2) 
        {
            int countSameType = 0;

            for (int i = -1; i > -3; i--) 
            {
                if (chipTiles[column + i, row].GetChipType() == chipTiles[column, row].GetChipType())
                {
                    countSameType++;
                }
                if (countSameType == 2)
                {
                    foundMathes = true;
                }
            }
        }


        if (!foundMathes && row >= 2)
        { 
            int countSameType = 0;

            for (int j = -1; j > -3; j--)
            {
                if (chipTiles[column, row + j].GetChipType() == chipTiles[column, row].GetChipType())
                {
                    countSameType++;
                }
                if (countSameType == 2)
                {
                    foundMathes = true;
                }
            }
        }

        if (foundMathes)
        {
            chipTiles[column, row].SetType();
            HaveMatchesAt(column, row);
        }
    }

    private void DestroyAt(int column, int row)
    {
        if (chipTiles[column, row].isMatched)
        {
            chipTiles[column, row].ChipDeath();
            Destroy(chipTiles[column, row].gameObject);
            chipTiles[column, row] = null;
        }
    }

    public void DestroyMatches()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (chipTiles[i, j] != null)
                { 
                    DestroyAt(i, j);
                }
            }
        }

        TileFall();
    }

    public void TileFall()
    {
        int nullCount = 0;
        bool haveBoardChanged = false;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (chipTiles[i, j] == null)
                {
                    nullCount++;
                    haveBoardChanged = true;
                }
                else if (nullCount > 0)
                {
                    chipTiles[i, j].SetGridAndWorldPos(i, j - nullCount, tileFallDelay);
                    chipTiles[i, j - nullCount] = chipTiles[i, j];
                }
            }
            while (nullCount > 0)
            {
                //chipTiles[i, height - nullCount] = new Chips();
                //chipTiles[i, height - nullCount].SetGridPos(i, height - nullCount);
                //chipTiles[i, height - nullCount].SetType();
                chipTiles[i, height - nullCount] = null;
                nullCount--;
            }

            //if (nullCount != 0) Debug.Log(nullCount);
            nullCount = 0;
        }

        if (haveBoardChanged)
        { 
            FillGaps();
        }
    }



    private void FillGaps()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (chipTiles[i, j] == null)
                {
                    Vector2 tilePos = new Vector2(i * boardCoef, 15 * boardCoef);
                    GameObject chipTile = Instantiate(chipPrefab, tilePos, Quaternion.identity);
                    chipTiles[i, j] = chipTile.GetComponent<Chips>();
                    chipTiles[i, j].SetGridAndWorldPos(i, j, tileFallDelay);
                    chipTiles[i, j].SetType();
                }
            }
        }

        StartCoroutine(FallRoutine());
    }

    private IEnumerator FallRoutine()
    {
        yield return new WaitForSeconds(tileFallDelay);
        EventBus.OnBoardChanged?.Invoke();
        DestroyMatches();
    }
}

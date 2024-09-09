using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UI;

public class BroomPowerUp : MonoBehaviour
{
    [SerializeField] private float shuffleTime;
    [SerializeField] private TextMeshProUGUI powerUpAmmountText;
    [SerializeField] private string powerUpType;

    private Button powerUpButton;

    private void Start()
    {
        powerUpAmmountText.text = BonusesManager.Instance.GetBonusesTypeAmount(powerUpType).ToString();
    }

    private void Awake()
    {
        powerUpButton = GetComponent<Button>();

        powerUpButton.onClick.AddListener(() =>
        {
            if (BonusesManager.Instance.GetBonusesTypeAmount(powerUpType) > 0)
            {
                DecreaseAndRefreshVisualAmmount();
                ShuffleChipsArray(GetShuffledArray(Get1DArrayFrom2D()));
                SetNewTilePos();
            }

        });
    }

    private Chips[] Get1DArrayFrom2D()
    {
        Chips[] oneDArray = new Chips[MakeBoard.Instance.chipTiles.Length]; 

        for (int i = 0; i < MakeBoard.Instance.GetWidth(); i++) 
        {
            for (int j = 0; j < MakeBoard.Instance.GetHeight(); j++)
            {
                oneDArray[(i * MakeBoard.Instance.GetHeight()) + j] = MakeBoard.Instance.chipTiles[i, j];
            }
        }

        return oneDArray;
    }

    private Chips[] GetShuffledArray(Chips[] arr)
    {
        for (int i = arr.Length - 1; i >= 1; i--)
        {
            int j = Random.Range(0, arr.Length);

            Chips tmp = arr[j];
            arr[j] = arr[i];
            arr[i] = tmp;
        }

        return arr;
    }

    private void ShuffleChipsArray(Chips[] shuffled1dArr )
    {
        Chips[,] shuffled2dArray = new Chips[MakeBoard.Instance.GetWidth(), MakeBoard.Instance.GetHeight()];

        for (int i = 0; i < MakeBoard.Instance.GetWidth(); i++)
        {
            for (int j = 0; j < MakeBoard.Instance.GetHeight(); j++)
            {
                shuffled2dArray[i, j] = shuffled1dArr[(i * MakeBoard.Instance.GetHeight()) + j];
            }
        }

        MakeBoard.Instance.chipTiles = shuffled2dArray;
        //return shuffled2dArray;
    }

    private void SetNewTilePos()
    {
        for (int i = 0; i < MakeBoard.Instance.GetWidth(); i++)
        {
            for (int j = 0; j < MakeBoard.Instance.GetHeight(); j++)
            {
                //MakeBoard.Instance.chipTiles[i, j].transform.position = new Vector2 (3 * MakeBoard.Instance.boardCoef, (15) * MakeBoard.Instance.boardCoef);
                MakeBoard.Instance.chipTiles[i, j].MakeSmoothlyMove(3, 15, shuffleTime / 2);
                StartCoroutine(MoveToPosRoutine(i, j));
            }
        }
    }

    private IEnumerator MoveToPosRoutine(int i, int j)
    {
        yield return new WaitForSeconds(shuffleTime / 2);
        MakeBoard.Instance.chipTiles[i, j].SetGridAndWorldPos(i, j, shuffleTime / 2);
        yield return new WaitForSeconds(shuffleTime / 2);
        EventBus.OnBoardChanged?.Invoke();
        MakeBoard.Instance.DestroyMatches();
    }

    private void DecreaseAndRefreshVisualAmmount()
    {
        BonusesManager.Instance.SubtracBonusesAmount(powerUpType);
        powerUpAmmountText.text = BonusesManager.Instance.GetBonusesTypeAmount(powerUpType).ToString();
    }

}

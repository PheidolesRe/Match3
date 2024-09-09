using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour 
{
    private int coinAmmountOverall;
    private int coinAmmountAtLvL;
    private int coinMultiply = 1;

    public static CoinManager Instance;

    private const string PLAYER_PREFS_COIN_AMOUNT = "CoinAmount";

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (PlayerPrefs.HasKey(PLAYER_PREFS_COIN_AMOUNT))
        { 
            coinAmmountOverall = PlayerPrefs.GetInt(PLAYER_PREFS_COIN_AMOUNT);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 1)
        {
            coinAmmountAtLvL = 0;
            coinMultiply = 1;
        }
    }

    private void OnEnable()
    {
        EventBus.OnChipDestroyd += AddCoin;
    }

    private void OnDisable()
    {
        EventBus.OnChipDestroyd -= AddCoin;        
    }

    private void AddCoin(string str) // string for Event
    {
        coinAmmountAtLvL += 5 * coinMultiply;
    }

    public int GetCoinAmmountAtLvl()
    {
        return coinAmmountAtLvL;
    }

    public void AddCoinForWin(int coins)
    {
        coinAmmountAtLvL += coins;
        coinAmmountOverall += coinAmmountAtLvL;
        Debug.Log("You've earned " + coinAmmountAtLvL);
    }

    public void SubtractCoins(int coinAmmount)
    { 
        if (coinAmmount > coinAmmountOverall) return;

        coinAmmountOverall -= coinAmmount;
        EventBus.OnGoldChanged?.Invoke();
    }

    public void SetCoinMultiplyTo2()
    {
        coinMultiply = 2;
    }

    public int GetCoinsAmountOverall()
    { 
        return coinAmmountOverall;
    }

    private void OnApplicationQuit()
    {
        SetPrefs();
    }

    private void SetPrefs()
    {
        PlayerPrefs.SetInt(PLAYER_PREFS_COIN_AMOUNT, coinAmmountOverall);
    }

    public bool HasPrefsKey()
    {
        if (PlayerPrefs.HasKey(PLAYER_PREFS_COIN_AMOUNT))
        {
            return true;
        }
        else
        { 
            return false;
        }
    }
}

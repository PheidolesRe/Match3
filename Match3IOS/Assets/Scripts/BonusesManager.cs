using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusesManager : MonoBehaviour
{
    [SerializeField] private string[] bonusesTypeArr;

    private Dictionary<string, int> bonusesDic = new Dictionary<string, int>();

    public static BonusesManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DicInit();
            GetPrefs();
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    

    private void DicInit()
    { 
        for (int i = 0; i < bonusesTypeArr.Length; i++) 
        {
            bonusesDic.Add(bonusesTypeArr[i], 0);
        }
    }

    public void AddBonuses(string bonusesType, int amount)
    {
        bonusesDic[bonusesType] += amount;
    }

    public int GetBonusesTypeAmount(string bonusesType)
    { 
        return bonusesDic[bonusesType];
    }

    public void SubtracBonusesAmount(string bonusesType)
    {
        bonusesDic[bonusesType]--;
    }

    public void AddBonusesAmount(string bonusesType)
    {
        bonusesDic[bonusesType]++;
    }

    private void OnApplicationQuit()
    {
        SetPrefs();
    }

    private void GetPrefs()
    {
        foreach (string bonusesType in bonusesTypeArr)
        {
            if (PlayerPrefs.HasKey(bonusesType))
            {
                bonusesDic[bonusesType] = PlayerPrefs.GetInt(bonusesType);
            }
        }

    }

    private void SetPrefs()
    {
        foreach (string bonusesType in bonusesTypeArr)
        {
            PlayerPrefs.SetInt(bonusesType, bonusesDic[bonusesType]);            
        }

    }
}

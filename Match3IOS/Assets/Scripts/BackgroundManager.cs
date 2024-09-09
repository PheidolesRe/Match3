using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{    
    [SerializeField] private Sprite[] backgroundSprites;

    private int chosenBackground = 0;

    private Dictionary<int, bool> boughtBackground = new Dictionary<int, bool>();

    public static BackgroundManager Instance;


    private const string PLAYER_PREFS_CHOSEN_BACKGROUND = "chosenBackground";

    private const string PLAYER_PREFS_FIRST_BACKGROUND = "firstBackground";
    private const string PLAYER_PREFS_SECOND_BACKGROUND = "secondBackground";
    private const string PLAYER_PREFS_THIRD_BACKGROUND = "thirdBackground";
    private const string PLAYER_PREFS_FOURTH_BACKGROUND = "fourthBackground";
    private const string PLAYER_PREFS_FIFTH_BACKGROUND = "fifthBackground";
    private const string PLAYER_PREFS_SIXTH_BACKGROUND = "sixthBackground";

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

    private void Start()
    {
    }

    private void DicInit()
    { 
        for (int i = 0; i < backgroundSprites.Length; i++) 
        {
            if (i == 0)
            { 
                boughtBackground.Add(i, true); continue;                
            }

            boughtBackground.Add(i, false);
        }
    }



    public int GetChosenBackground()
    { 
        return chosenBackground;
    }

    public bool GetBoughtBackground(int index)
    { 
        return boughtBackground[index];
    }

    public void SetBoughtBackground(int index)
    {
        boughtBackground[index] = true;
    }

    public void SetChosenBackground(int index)
    { 
        chosenBackground = index;
    }

    private void OnApplicationQuit()
    {
        SetPrefs();
    }

    private void SetPrefs()
    {
        PlayerPrefs.SetInt(PLAYER_PREFS_CHOSEN_BACKGROUND, chosenBackground);

        SavePrefsBool(PLAYER_PREFS_FIRST_BACKGROUND, boughtBackground[0]);
        SavePrefsBool(PLAYER_PREFS_SECOND_BACKGROUND, boughtBackground[1]);
        SavePrefsBool(PLAYER_PREFS_THIRD_BACKGROUND, boughtBackground[2]);
        SavePrefsBool(PLAYER_PREFS_FOURTH_BACKGROUND, boughtBackground[3]);
        SavePrefsBool(PLAYER_PREFS_FIFTH_BACKGROUND, boughtBackground[4]);
        SavePrefsBool(PLAYER_PREFS_SIXTH_BACKGROUND, boughtBackground[5]);

    }

    private void SavePrefsBool(string prefsName, bool isPossess)
    { 
        if (isPossess)
        {
            PlayerPrefs.SetInt(prefsName, 1);
        }
        else
        { 
            PlayerPrefs.SetInt(prefsName, 0);            
        }
        
    }

    private void GetPrefs()
    {
        if (PlayerPrefs.HasKey(PLAYER_PREFS_CHOSEN_BACKGROUND))
        {
            chosenBackground = PlayerPrefs.GetInt(PLAYER_PREFS_CHOSEN_BACKGROUND);
        }

        if (PlayerPrefs.HasKey(PLAYER_PREFS_FIRST_BACKGROUND))
        {
            if (PlayerPrefs.GetInt(PLAYER_PREFS_FIRST_BACKGROUND) == 0)
            {
                boughtBackground[0] = false;
            }
            else
            {
                boughtBackground[0] = true;
            }
        }

        if (PlayerPrefs.HasKey(PLAYER_PREFS_SECOND_BACKGROUND))
        {
            if (PlayerPrefs.GetInt(PLAYER_PREFS_SECOND_BACKGROUND) == 0)
            {
                boughtBackground[1] = false;
            }
            else
            {
                boughtBackground[1] = true;
            }
        }

        if (PlayerPrefs.HasKey(PLAYER_PREFS_THIRD_BACKGROUND))
        {
            if (PlayerPrefs.GetInt(PLAYER_PREFS_SECOND_BACKGROUND) == 0)
            {
                boughtBackground[2] = false;
            }
            else
            {
                boughtBackground[2] = true;
            }
        }

        if (PlayerPrefs.HasKey(PLAYER_PREFS_FOURTH_BACKGROUND))
        {
            if (PlayerPrefs.GetInt(PLAYER_PREFS_THIRD_BACKGROUND) == 0)
            {
                boughtBackground[3] = false;
            }
            else
            {
                boughtBackground[3] = true;
            }
        }

        if (PlayerPrefs.HasKey(PLAYER_PREFS_FIFTH_BACKGROUND))
        {
            if (PlayerPrefs.GetInt(PLAYER_PREFS_FIFTH_BACKGROUND) == 0)
            {
                boughtBackground[4] = false;
            }
            else
            {
                boughtBackground[4] = true;
            }
        }

        if (PlayerPrefs.HasKey(PLAYER_PREFS_SIXTH_BACKGROUND))
        {
            if (PlayerPrefs.GetInt(PLAYER_PREFS_SIXTH_BACKGROUND) == 0)
            {
                boughtBackground[5] = false;
            }
            else
            {
                boughtBackground[5] = true;
            }
        }
    }

    public Sprite GetChosenBackgroundSprite()
    { 
        return backgroundSprites[chosenBackground];
    }
}

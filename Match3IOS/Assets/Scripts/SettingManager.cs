using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    private bool isVibrationOn = true;

    public static SettingManager Instance;

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

        DontDestroyOnLoad(gameObject);
    }

    public bool GetIsVibrationOn()
    { 
        return isVibrationOn;
    }

    public void SetIsVibrationOn(bool virationTick)
    {
        isVibrationOn = virationTick;
    }
}

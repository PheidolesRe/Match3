using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    [SerializeField] private Button saveButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button tickMusic;
    [SerializeField] private Button tickVibration;

    private bool musicTickactive = true;
    private bool vibrationTickactive = true;

    private void Start()
    {
        gameObject.SetActive(false);

        ShowHideTick(tickVibration, SettingManager.Instance.GetIsVibrationOn());
    }

    private void Awake()
    {
        saveButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });

        backButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });

        tickMusic.onClick.AddListener(() =>
        {
            musicTickactive = !musicTickactive;
            ShowHideTick(tickMusic, musicTickactive);
        });

        tickVibration.onClick.AddListener(() =>
        {
            vibrationTickactive = !vibrationTickactive;
            ShowHideTick(tickVibration, vibrationTickactive);
        });
    }

    private void ShowHideTick(Button tickButton, bool isActive)
    {
        if (!isActive)
        {
            tickButton.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            SettingManager.Instance.SetIsVibrationOn(isActive);
        }
        else
        { 
            tickButton.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);       
            SettingManager.Instance.SetIsVibrationOn(isActive);
        }
          

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowSetting : MonoBehaviour
{
    [SerializeField] private GameObject settingPanel;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            SwitchShowHide();
        });
    }

    private void SwitchShowHide()
    {
        settingPanel.SetActive(!settingPanel.activeSelf);
        

    }
}

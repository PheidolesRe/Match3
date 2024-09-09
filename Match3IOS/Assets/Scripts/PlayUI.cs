using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayUI : MonoBehaviour
{
    private Button playButton;

    private void Awake()
    {
        playButton = GetComponent<Button>();

        playButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("LoadingScene");
        });
    }
}

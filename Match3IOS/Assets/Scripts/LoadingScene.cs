using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] private Slider progressSlider;

    private void Start()
    {
        StartCoroutine(LoadingRoutine());
    }


    private IEnumerator LoadingRoutine()
    { 
        progressSlider.value = 0;

        float progress = 0;
        while (progress < 1)        
        {
            progress = Mathf.MoveTowards(progress, 1, Time.deltaTime);
            progressSlider.value = progress;
            if (progress >= 0.95)
            {
                progressSlider.value = 1;
                SceneManager.LoadScene(1);
            }

            yield return null;
        }
    }


}


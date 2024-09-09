using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractIndicator : MonoBehaviour
{
    [SerializeField] private GameObject indicator;

    public void ShowIndicator()
    { 
        indicator.SetActive(true);
        StartCoroutine(HideRoutine());
    }

    private void HideIndicator()
    {
        indicator.SetActive(false);
    }

    private IEnumerator HideRoutine()
    { 
        yield return new WaitForEndOfFrame();
        HideIndicator();
    }
}

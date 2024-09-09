using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseChosenBackground : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Image>().sprite = BackgroundManager.Instance.GetChosenBackgroundSprite();
    }
}

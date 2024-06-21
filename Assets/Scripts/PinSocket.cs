using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PinSocket : MonoBehaviour
{
    private Button buttonComponent;

    private void OnEnable()
    {
        buttonComponent = GetComponent<Button>();

        if (CableManager.instance.currentPickedHead == CableHeadType.Female)
        {
            buttonComponent.interactable = true;
        }
        else
        {
            buttonComponent.interactable = false;
        }
    }
}

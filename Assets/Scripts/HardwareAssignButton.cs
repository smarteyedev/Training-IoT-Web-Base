using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HardwareAssignButton : MonoBehaviour
{
    [HideInInspector]
    public HardwareController hardwareController;

    private Button buttonComponent;

    private void Start()
    {
        buttonComponent = GetComponent<Button>();
        buttonComponent.onClick.AddListener(hardwareController.SetActivePickPin);
    }


}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PinSocket : MonoBehaviour
{
    private enum PinState
    {
        ReadyForEditing,
        ReadyForAssigning
    }

    private PinState currentState;

    private Button buttonComponent;
    public bool isAssigned;
    public CableController assignedCable;
    public SocketChecker socketChecker;

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

    private void Start()
    {
        CableManager.OnCablePicked += PinReadyForAssigning;
        CableManager.OnCableRemoved += PinReadyForEditing;

        buttonComponent.onClick.AddListener(() =>  PinMainBehavior());
    }

    private void OnDestroy()
    {
        CableManager.OnCablePicked -= PinReadyForAssigning;
        CableManager.OnCableRemoved -= PinReadyForEditing;
    }

    private void PinReadyForAssigning()
    {
        currentState = PinState.ReadyForAssigning;
    }

    private void PinReadyForEditing()
    {
        currentState = PinState.ReadyForEditing;
    }

    private void PinMainBehavior()
    {
        switch (currentState)
        {
            case PinState.ReadyForAssigning:
                AssignDataForValidation();
                break;
            case PinState.ReadyForEditing:
                EditingPinContent();
                break;
            default:
                break;
        }
    }

    private void AssignDataForValidation()
    {
        if (CablePickerButton.instantiatedCable == null) return;

        assignedCable = CablePickerButton.instantiatedCable.GetComponent<CableController>();
        var pinsContainer = assignedCable.GetPinsInContainer();
        pinsContainer.Add(this);
    }

    private void EditingPinContent()
    {
        //TO DO: Implement editing pin content
    }
}

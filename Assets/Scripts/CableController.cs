using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CableController : MonoBehaviour
{
    [SerializeField] private CableHeadType leftHeadType;
    [SerializeField] private CableHeadType rightHeadType;
    [SerializeField] private Button removeButton;
    [SerializeField] private Button leftHeadButton;
    [SerializeField] private Button rightHeadButton;

    public bool isRightHeadChosen;

    public List<PinSocket> pinsInRightContainer;
    public List<PinSocket> pinsInLeftContainer;

    private void Start()
    {
        InitCableButtons();
    }

    private void InitCableButtons()
    {
        removeButton.onClick.AddListener(DestroyCable);

        if (MainMenu.instance.GetActiveAssignButtons() == 0)
        {
            leftHeadButton.gameObject.SetActive(false);
            rightHeadButton.gameObject.SetActive(false);
        }

        if (leftHeadType == CableHeadType.Female) leftHeadButton.onClick.AddListener(() => {
            FemaleHeadType();
            AfterClickButton(leftHeadButton, rightHeadButton);
            isRightHeadChosen = false;
        });
        else leftHeadButton.onClick.AddListener(() => { 
            MaleHeadType();
            AfterClickButton(leftHeadButton, rightHeadButton);
            isRightHeadChosen = false;
        });

        if (rightHeadType == CableHeadType.Female) rightHeadButton.onClick.AddListener(() => {
            FemaleHeadType();
            AfterClickButton(rightHeadButton, leftHeadButton);
            isRightHeadChosen = true;
        });
        else rightHeadButton.onClick.AddListener(() => {
            MaleHeadType();
            AfterClickButton(rightHeadButton, leftHeadButton);
            isRightHeadChosen = true;
        });
    }

    public List<PinSocket> GetPinsInContainer()
    {
        if (isRightHeadChosen) return pinsInRightContainer;
        else return pinsInLeftContainer;
    }

    private void FemaleHeadType()
    {
        CableManager.instance.CableChosen(CableHeadType.Female);
        CableManager.instance.SetCableState(CableState.OnPicked);
    }

    private void MaleHeadType()
    {
        CableManager.instance.CableChosen(CableHeadType.Male);
        CableManager.instance.SetCableState(CableState.OnPicked);
    }

    private void AfterClickButton(Button deactivateButton, Button activateButton)
    {
        deactivateButton.interactable = false;
        activateButton.interactable = true;
    }

    /// <summary>
    /// To be attached to a remove button on instantiated cable
    /// </summary>
    public void DestroyCable()
    {
        if (CablePickerButton.instantiatedCable == null) return;

        MainMenu.instance.SetMenuLayout(MenuLayout.ComponentPicker);
        CableManager.instance.SetCableState(CableState.OnRemove);
        Destroy(CablePickerButton.instantiatedCable);
        CablePickerButton.instantiatedCable = null;
    }

}

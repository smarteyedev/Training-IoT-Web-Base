using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// This class should be attached to a cable prefab
/// This class is responsible for handling cable buttons and cable removal
/// This class should contain pins that are attached to the cable (For Validation pusposes)
/// </summary>
public class CableController : MonoBehaviour
{
    // Set this based on the cables head type, if left head is female, set it to female
    [SerializeField] private CableHeadType leftHeadType; 
    [SerializeField] private CableHeadType rightHeadType;

    [SerializeField] private Button removeButton;
    [SerializeField] private Button leftHeadButton;
    [SerializeField] private Button rightHeadButton;

    public bool isRightHeadChosen; // This is used to determine which container to put pins in

    public List<PinSocket> pinsInRightContainer; // Pins that are attached to the right head of the cable
    public List<PinSocket> pinsInLeftContainer; // Pins that are attached to the left head of the cable

    private void Start()
    {
        InitCableButtons();
    }

    /// <summary>
    /// This method responsible for initializing cable buttons and their listeners
    /// </summary>
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

    /// <summary>
    /// Use this to get container that is choosen by the user 
    /// (To be filled with pin that is attached to the cable)
    /// </summary>
    /// <returns></returns>
    public List<PinSocket> GetPinsInContainer()
    {
        if (isRightHeadChosen) return pinsInRightContainer;
        else return pinsInLeftContainer;
    }

    /// <summary>
    /// This method is responsible for setting the cable head type
    /// </summary>
    private void FemaleHeadType()
    {
        CableManager.instance.CableChosen(CableHeadType.Female);
        CableManager.instance.SetCableState(CableState.OnPicked);
    }

    /// <summary>
    /// This method is responsible for setting the cable head type
    /// </summary>
    private void MaleHeadType()
    {
        CableManager.instance.CableChosen(CableHeadType.Male);
        CableManager.instance.SetCableState(CableState.OnPicked);
    }

    /// <summary>
    /// This method is responsible for deactivating the button that 
    /// is clicked and activating the other button
    /// </summary>
    /// <param name="deactivateButton"></param>
    /// <param name="activateButton"></param>
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

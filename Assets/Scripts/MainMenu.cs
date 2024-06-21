using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour, IMainMenu
{
    public static IMainMenu instance;

    [SerializeField] private GameObject componentPickerLayout;
    [SerializeField] private GameObject componentAssignLayout;

    [Serializable]
    public struct ComponentAssignButton
    {
        [Tooltip("This variable will be used to filter which buttons are shown in " +
            "the component assign layout. MAKE SURE TEXT IS THE SAME AS HARDWARE NAME")]
        public string name;
        public GameObject componentTypeButton;
        public bool isInTheScene;
    }

    public List<ComponentAssignButton> componentAssignButtons;

    private void Awake()
    {
        instance = this;
    }

    private void OnDisable()
    {
        CableManager.OnFemaleCableChosen -= EnableComponentTypeButton;
    }

    public void SetMenuLayout(MenuLayout layout)
    {
        componentPickerLayout.SetActive(layout == MenuLayout.ComponentPicker);
        ActivateComponentAssign(layout == MenuLayout.ComponentAssign);
    }

    private void ActivateComponentAssign(bool isActive)
    {
        foreach (var button in componentAssignButtons)
            button.componentTypeButton.SetActive(false);

        if (isActive) InitComponentAssign();
        componentAssignLayout.SetActive(isActive);
    }

    private void InitComponentAssign()
    {
        foreach (var button in componentAssignButtons)
        {
            button.componentTypeButton.SetActive(button.isInTheScene);
            button.componentTypeButton.GetComponent<Button>().interactable = false;
            CableManager.OnFemaleCableChosen += EnableComponentTypeButton;
        }
    }

    public int GetActiveAssignButtons()
    {
        var activeList = componentAssignButtons.FindAll(x => x.isInTheScene == true);
        return activeList.Count;
    }

    private void EnableComponentTypeButton()
    {
        foreach (var button in componentAssignButtons)
        {
            button.componentTypeButton.GetComponent<Button>().interactable = true;
        }
    }

    /// <summary>
    /// Use this method to set the status of the assign button
    /// </summary>
    /// <param name="name">Which hardware supposed to be active</param>
    /// <param name="isInTheScene">staus of object in scene</param>
    public void SetActiveAssignButton(string name, bool isInTheScene, HardwareController hardwareController)
    {
        var component = componentAssignButtons.Find(x => x.name == name);
        var index = componentAssignButtons.IndexOf(component);
        componentAssignButtons[index] = new ComponentAssignButton
        {
            name = componentAssignButtons[index].name,
            componentTypeButton = componentAssignButtons[index].componentTypeButton,
            isInTheScene = isInTheScene
        };
        componentAssignButtons[index].componentTypeButton.GetComponent<HardwareAssignButton>().hardwareController = hardwareController;
        InitComponentAssign();
    }
}

public interface IMainMenu
{
    void SetActiveAssignButton(string name, bool status, HardwareController hardwareController);
    void SetMenuLayout(MenuLayout layout);
    int GetActiveAssignButtons();
}

public enum MenuLayout
{
    ComponentPicker,
    ComponentAssign
}

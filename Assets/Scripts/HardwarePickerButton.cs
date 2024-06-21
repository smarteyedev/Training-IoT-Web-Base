using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HardwarePickerButton : MonoBehaviour
{
    [Tooltip("Make sure each hardware has a unique name")]
    [SerializeField] private string hardwareName;
    [SerializeField] private Sprite hardwarePicture;
    [SerializeField] private GameObject hardwareObjectInScene;
    public UnityEvent OnButtonClicked;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI hardwareNameText;
    [SerializeField] private Image hardwarePictureRenderer;
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        hardwareNameText.text = hardwareName;
        hardwarePictureRenderer.sprite = hardwarePicture;

        OnButtonClicked.AddListener(ActivateHardware);

        button.onClick.AddListener(() => OnButtonClicked?.Invoke());
    }

    private void ActivateHardware()
    {
        hardwareObjectInScene?.SetActive(true);
        button.gameObject.SetActive(false);
        var hardwareController = hardwareObjectInScene.GetComponent<HardwareController>();
        MainMenu.instance.SetActiveAssignButton(hardwareName, true, hardwareController);
        hardwareController?.removeButton.onClick.AddListener(OnRemoveComponent);
    }

    private void OnRemoveComponent()
    {
        hardwareObjectInScene.SetActive(false);
        MainMenu.instance.SetActiveAssignButton(hardwareName, false, null);
        button.gameObject.SetActive(true);
    }
}

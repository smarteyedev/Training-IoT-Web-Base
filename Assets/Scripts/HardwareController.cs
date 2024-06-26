using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HardwareController : MonoBehaviour
{
    private Vector3 originalPosition;

    [SerializeField] private GameObject mainLayout;
    public Button removeButton;
    [SerializeField] private Button editButton;

    [SerializeField] private GameObject pickPinSocketLayout;
    [SerializeField] private Transform layoutPinPickerPosition;

    [SerializeField] private List<PinSocket> pinSockets;
    private List<PinSocket> assignedPinSockets;

    private void Start()
    {
        originalPosition = transform.position;
    }

    public void SetActivePickPin()
    {
        pickPinSocketLayout.SetActive(true);
        mainLayout.SetActive(false);
        transform.position = layoutPinPickerPosition.position;
        assignedPinSockets = pinSockets.FindAll(pin => pin.isAssigned);
    }

    public void SetActiveMainLayout()
    {
        pickPinSocketLayout.SetActive(false);
        mainLayout.SetActive(true);
        transform.position = originalPosition;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HardwareController : MonoBehaviour
{
    private Transform originalPosition;

    [SerializeField] private GameObject mainLayout;
    public Button removeButton;
    [SerializeField] private Button editButton;

    [SerializeField] private GameObject pickPinSocketLayout;
    [SerializeField] private Transform layoutPinPickerPosition;

    [SerializeField] private List<PinSocket> pinSockets;

    private void Start()
    {
        originalPosition = transform;
    }

    public void SetActivePickPin()
    {
        pickPinSocketLayout.SetActive(true);
        mainLayout.SetActive(false);
        transform.position = layoutPinPickerPosition.position;
       /* Camera.main.GetComponent<CameraController>().SetCameraPosition(layoutPinPickerPosition.position);*/
    }
}

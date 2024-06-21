using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CablePickerButton : MonoBehaviour
{
    [SerializeField] private string cableName;
    [SerializeField] private Sprite cablePicture;
    [SerializeField] private GameObject cablePrefab;
    public UnityEvent OnButtonClicked;
    public static GameObject instantiatedCable;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI cableNameText;
    [SerializeField] private Image cablePictureRenderer;
    private Button button;

    [Header("Instantiate Position")]
    [SerializeField] private Transform instantiatePosition;

    private void Start()
    {
        button = GetComponent<Button>();
        cableNameText.text = cableName;
        cablePictureRenderer.sprite = cablePicture;

        OnButtonClicked.AddListener(InstantiateCable);
        button.onClick.AddListener(() => OnButtonClicked?.Invoke());
    }

    private void InstantiateCable()
    {
        if (instantiatedCable != null) return;
        
        MainMenu.instance.SetMenuLayout(MenuLayout.ComponentAssign);

        instantiatedCable = Instantiate(cablePrefab, instantiatePosition.position, instantiatePosition.rotation);
    }
}

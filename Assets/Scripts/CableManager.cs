using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableManager : MonoBehaviour
{
    public static CableManager instance;

    public static event Action OnFemaleCableChosen;
    public static event Action OnMaleCableChosen;

    public static event Action OnCablePicked;
    public static event Action OnCableAssigned;
    public static event Action OnCableRemoved;

    public CableHeadType currentPickedHead;

    private void OnDisable()
    {
        OnFemaleCableChosen = null;
        OnMaleCableChosen = null;
    }

    private void Awake()
    {
        instance = this;
    }

    public void SetCableState(CableState state)
    {
        switch (state)
        {
            case CableState.OnPicked:
                OnCablePicked?.Invoke();
                break;
            case CableState.OnAssign:
                OnCableAssigned?.Invoke();
                break;
            case CableState.OnRemove:
                OnCableRemoved?.Invoke();
                currentPickedHead = CableHeadType.None;
                break;
            default:
                break;
        }
    }

    public void CableChosen(CableHeadType headType)
    {
        switch (headType)
        {
            case CableHeadType.Female:
                OnFemaleCableChosen?.Invoke();
                currentPickedHead = CableHeadType.Female;
                break;
            case CableHeadType.Male:
                OnMaleCableChosen?.Invoke();
                currentPickedHead = CableHeadType.Male;
                break;
        }


    }
}

public enum CableState
{
    OnPicked,
    OnAssign,
    OnRemove
}

public enum CableHeadType
{
    None,
    Female,
    Male
}
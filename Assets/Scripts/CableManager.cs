using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This Class responsible to Manage the all of the cables in the scene
/// </summary>
public class CableManager : MonoBehaviour
{
    public static CableManager instance;

    // Events when one of the cable heads is chosen
    public static event Action OnFemaleCableChosen;
    public static event Action OnMaleCableChosen;

    // Events when cable is picked, assigned or removed
    public static event Action OnCablePicked;
    public static event Action OnCableAssigned;
    public static event Action OnCableRemoved;

    // Current picked cable head
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

    /// <summary>
    /// Trigger this method when cable state is changed
    /// </summary>
    /// <param name="state"></param>
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

    /// <summary>
    /// Trigger this method when cable head is chosen
    /// </summary>
    /// <param name="headType"></param>
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
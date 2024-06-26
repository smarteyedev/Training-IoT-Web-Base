using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SocketChecker : MonoBehaviour
{
    [SerializeField] private UnityEvent OnQuestFinish;
    [SerializeField] private UnityEvent OnQuestFail;

    public List<PinSocket> firstPairPins;
    public List<PinSocket> secondPairPins;
    private bool isFirstPairCorrect;
    private bool isSecondPairCorrect;

    private bool iQuestFinished;

    public void CheckPinsOnCable(CableController cable)
    {
        var pinsInContainer = cable.GetPinsInContainer();

        if (!isFirstPairCorrect && pinsInContainer.Any(pin => firstPairPins.Contains(pin)))
        {
            isFirstPairCorrect = true;
        }

        if (!isSecondPairCorrect && pinsInContainer.Any(pin => secondPairPins.Contains(pin)))
        {
            isSecondPairCorrect = true;
        }

        ValidateQuest();
    }

    private void ValidateQuest()
    {
        if (isFirstPairCorrect && isSecondPairCorrect && !iQuestFinished)
        {
            iQuestFinished = true;
            OnQuestFinish.Invoke();
        }
        else if (!iQuestFinished)
        {
            OnQuestFail.Invoke();
        }
    }
}

}

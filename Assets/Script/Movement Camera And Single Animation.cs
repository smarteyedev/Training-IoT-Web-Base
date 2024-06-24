using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [System.Serializable]
    public struct NamedPosition
    {
        public string name;
        public Transform position;
    }

    public List<NamedPosition> namedPositions = new List<NamedPosition>();
    [SerializeField] private GameObject objectToMove;
    [SerializeField] private float transitionDuration = 1.0f; // Default duration

    private Dictionary<string, Transform> positionDictionary;

    void Start()
    {
        // Initialize the dictionary
        positionDictionary = new Dictionary<string, Transform>();
        foreach (var namedPosition in namedPositions)
        {
            positionDictionary[namedPosition.name] = namedPosition.position;
        }
    }

    public void OnPosChange(string positionName)
    {
        if (!positionDictionary.ContainsKey(positionName))
        {
            Debug.LogWarning("Invalid position name");
            return;
        }

        Transform targetPosition = positionDictionary[positionName];
        MoveObjectSmoothly(objectToMove.transform, targetPosition, transitionDuration);
    }

    private void MoveObjectSmoothly(Transform start, Transform target, float totalDuration)
    {
        objectToMove.transform.position = start.position;
        objectToMove.transform.rotation = start.rotation;

        float moveDuration = totalDuration * 0.8f; // 80% of the total duration for movement
        float rotateDuration = totalDuration * 0.2f; // 20% of the total duration for rotation

        // Start movement
        LeanTween.move(objectToMove, target.position, moveDuration).setEase(LeanTweenType.easeInOutSine);

        // Start rotation after movement completes
        LeanTween.value(gameObject, 0f, 1f, moveDuration).setOnComplete(() =>
        {
            LeanTween.rotate(objectToMove, target.rotation.eulerAngles, rotateDuration).setEase(LeanTweenType.easeInOutSine);
        });
    }

    // Public getter for transition duration
    public float TransitionDuration
    {
        get { return transitionDuration; }
    }
}

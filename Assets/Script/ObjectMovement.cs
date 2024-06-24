using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    [System.Serializable]
    public struct IndexPair
    {
        public int startIndex;
        public int endIndex;
        public float transitionDuration; // Duration for this specific pair
    }

    public List<Transform> movePositions = new List<Transform>();
    [SerializeField] private GameObject objectToMove;

    public List<IndexPair> indexPairs = new List<IndexPair>();

    void Start()
    {
        // Initialization code if needed
    }

    public void OnPosChange(int pairIndex)
    {
        if (pairIndex < 0 || pairIndex >= indexPairs.Count)
        {
            Debug.LogWarning("Invalid pair index");
            return;
        }

        IndexPair indexPair = indexPairs[pairIndex];
        OnPosChange(indexPair.startIndex, indexPair.endIndex, indexPair.transitionDuration);
    }

    public void OnPosChange(int startIndex, int endIndex, float transitionDuration)
    {
        if (startIndex < 0 || endIndex >= movePositions.Count || startIndex == endIndex)
        {
            Debug.LogWarning("Invalid position indices");
            return;
        }

        List<Transform> path = new List<Transform>();
        for (int i = startIndex; i <= endIndex; i++)
        {
            path.Add(movePositions[i]);
        }

        MoveObjectThroughPath(objectToMove.transform, path, transitionDuration);
    }

    private void MoveObjectThroughPath(Transform start, List<Transform> path, float totalDuration)
    {
        if (path.Count == 0) return;

        float segmentDuration = totalDuration / path.Count;
        StartCoroutine(MoveAlongPath(start, path, segmentDuration));
    }

    private IEnumerator MoveAlongPath(Transform start, List<Transform> path, float segmentDuration)
    {
        objectToMove.transform.position = start.position;
        objectToMove.transform.rotation = start.rotation;

        foreach (Transform target in path)
        {
            float moveDuration = segmentDuration * 0.8f; // 80% of the segment duration for movement
            float rotateDuration = segmentDuration * 0.2f; // 20% of the segment duration for rotation

            // Start movement
            LeanTween.move(objectToMove, target.position, moveDuration).setEase(LeanTweenType.easeInOutSine);

            yield return new WaitForSeconds(moveDuration);

            // Start rotation
            LeanTween.rotate(objectToMove, target.rotation.eulerAngles, rotateDuration).setEase(LeanTweenType.easeInOutSine);

            yield return new WaitForSeconds(rotateDuration);
        }
    }

    // Public method to set indices and call OnPosChange
    public void SetIndicesAndChange(int startIndex, int endIndex, float transitionDuration)
    {
        OnPosChange(startIndex, endIndex, transitionDuration);
    }
}

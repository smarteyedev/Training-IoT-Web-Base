using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [System.Serializable]
    public struct NamedPosition
    {
        public string name;
        public List<Transform> positions; // Ubah menjadi daftar posisi
    }

    public List<NamedPosition> namedPositions = new List<NamedPosition>();
    [SerializeField] private GameObject objectToMove;
    [SerializeField] private float transitionDuration = 1.0f; // Durasi default

    private Dictionary<string, List<Transform>> positionDictionary;
    private bool isCooldown = false; // Flag cooldown

    void Start()
    {
        // Inisialisasi kamus
        positionDictionary = new Dictionary<string, List<Transform>>();
        foreach (var namedPosition in namedPositions)
        {
            positionDictionary[namedPosition.name] = namedPosition.positions;
        }
    }

    public void OnPosChange(string positionName)
    {
        if (isCooldown)
        {
            Debug.LogWarning("Button is in cooldown");
            return;
        }

        if (!positionDictionary.ContainsKey(positionName))
        {
            Debug.LogWarning("Invalid position name");
            return;
        }

        List<Transform> targetPositions = positionDictionary[positionName];
        StartCoroutine(MoveObjectThroughPositions(objectToMove.transform, targetPositions, transitionDuration));
    }

    private IEnumerator MoveObjectThroughPositions(Transform start, List<Transform> targets, float totalDuration)
    {
        isCooldown = true; // Mulai cooldown

        float moveDuration = totalDuration * 0.8f; // 80% dari durasi total untuk gerakan
        float rotateDuration = totalDuration * 0.2f; // 20% dari durasi total untuk rotasi
        float individualMoveDuration = moveDuration / targets.Count; // Durasi gerakan untuk setiap posisi

        foreach (Transform target in targets)
        {
            objectToMove.transform.position = start.position;
            objectToMove.transform.rotation = start.rotation;

            // Mulai gerakan
            LeanTween.move(objectToMove, target.position, individualMoveDuration).setEase(LeanTweenType.easeInOutSine);

            // Tunggu sampai gerakan selesai
            yield return new WaitForSeconds(individualMoveDuration);

            // Mulai rotasi
            LeanTween.rotate(objectToMove, target.rotation.eulerAngles, rotateDuration).setEase(LeanTweenType.easeInOutSine);

            // Tunggu sampai rotasi selesai
            yield return new WaitForSeconds(rotateDuration);
        }

        isCooldown = false; // Akhiri cooldown
    }

    // Getter publik untuk durasi transisi
    public float TransitionDuration
    {
        get { return transitionDuration; }
    }
}

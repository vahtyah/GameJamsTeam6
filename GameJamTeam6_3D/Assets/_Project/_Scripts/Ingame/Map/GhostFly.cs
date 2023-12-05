using System;
using DG.Tweening;
using UnityEngine;

public class GhostFly : MonoBehaviour
{
    [SerializeField] private Transform pathParent;
    private Vector3[] paths;

    private void Start()
    {
        paths = new Vector3[pathParent.childCount + 1];
        for (int i = 0; i < pathParent.childCount; i++)
        {
            paths[i] = pathParent.GetChild(i).position;
        }

        Shuffle(paths);
        paths[pathParent.childCount] = paths[0];
        transform.DOPath(paths, 100f, PathType.CatmullRom).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart)
            .SetLookAt(.001f, Vector3.forward, Vector3.up);
    }

    private void Shuffle<T>(T[] array)
    {
        int n = array.Length;
        for (int i = 0; i < n; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, n);

            (array[i], array[randomIndex]) = (array[randomIndex], array[i]);
        }
    }
}
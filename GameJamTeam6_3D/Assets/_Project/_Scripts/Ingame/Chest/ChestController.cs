using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChestController : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Transform topTransform;
    [SerializeField] private float distanceCanOpen = 2f;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Vector3.Distance(IngameManager.instance.player.position, transform.position) > distanceCanOpen) return;
        topTransform.DOLocalRotate(new Vector3(-30, 0, 0), 2f).SetEase(Ease.OutBounce);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceCanOpen);
    }
}
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class WindowDrag : MonoBehaviour, IDragHandler
{
    [SerializeField] private RectTransform rectTransform;
    
    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (rectTransform == null)
        {
            Debug.LogError($"NRE: rectTransform is null for {gameObject.name}");
            return;
        }
        
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}

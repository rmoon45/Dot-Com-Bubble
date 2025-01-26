using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class WindowDrag : MonoBehaviour, IDragHandler
{
    [SerializeField] private RectTransform rectTransform;
    
    private Canvas canvas;

    private Vector2 canvasSize;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();

        canvasSize = canvas.GetComponent<RectTransform>().sizeDelta;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (rectTransform == null)
        {
            Debug.LogError($"NRE: rectTransform is null for {gameObject.name}");
            return;
        }
        
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

        var pos = rectTransform.anchoredPosition;
        Vector2 halfSize = rectTransform.sizeDelta / 2;
        float left = pos.x - halfSize.x;
        float right = pos.x + halfSize.x;
        float top = pos.y + halfSize.y;
        float bottom = pos.y + halfSize.y;
        
        //if (left < 0) rectTransform.anchoredPosition = new Vector2(halfSize.x, pos.y);
        // print(right);
        //if (right > 0) rectTransform.anchoredPosition = new Vector2(canvas.pixelRect.width / 2 - halfSize.x, pos.y);
        //rectTransform.anchoredPosition = new Vector2(canvasSize.x / 2 - halfSize.x, pos.y);
    }
}

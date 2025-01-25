using System;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    [SerializeField] Texture2D cursorMain;
    [SerializeField] Texture2D cursorPoint;
    [SerializeField] Texture2D cursorText;
    
    
    private void Awake()
    {
        ChangeCursorMain();
    }

    public void ChangeCursorMain()
    {
        UnityEngine.Cursor.SetCursor(cursorMain, Vector2.zero, CursorMode.Auto);
    }
    
    public void ChangeCursorPoint()
    {
        UnityEngine.Cursor.SetCursor(cursorPoint, Vector2.one * 10, CursorMode.Auto);
    }
    
    public void ChangeCursorText()
    {
        UnityEngine.Cursor.SetCursor(cursorText, Vector2.zero, CursorMode.Auto);
    }
}

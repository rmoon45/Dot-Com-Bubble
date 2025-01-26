using UnityEngine;
using UnityEngine.UI;

public class ModuleButton : MonoBehaviour
{
    public CostModule module;
    public ModuleType moduleType;
    public bool selected;
    public Sprite selectedSprite;
    public Sprite defaultSprite;
    public Image image;

    public void onClick()
    {
        module.SetType(moduleType);
    }

    public void SetSelected(bool selected)
    {
        this.selected = selected;
        image.sprite = selected ? selectedSprite : defaultSprite;
    }
}

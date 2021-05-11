using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(menuName = "Button Skin Data")]
public class ButtonSkinData :  ScriptableObject
{
    [Header("Button Skin")]
    public Sprite unselectBackground;
    public Sprite unselectIcon;
    public TMP_FontAsset unselectFont;
    public Color unselectColor;

    public Sprite selectBackground;
    public Sprite selectIcon;
    public TMP_FontAsset selectFont;
    public Color selectColor;
}

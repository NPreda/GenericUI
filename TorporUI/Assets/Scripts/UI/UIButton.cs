using Custom.UI;
using Tools.UI;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

[RequireComponent(typeof(IMouseInput))]
[RequireComponent(typeof(Image))]
public class UIButton : CustomUI
{

    //----------------------------------------------------------------------
    #region UnityElements

    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text content;
    private Image background;
    private IMouseInput mInput;

    #endregion
    //----------------------------------------------------------------------
    #region SkinVariables

    private bool _isSelected;
    private Sprite _icon;
    private Sprite _background;
    private TMP_FontAsset _font; 
    private Color _color;  
    #endregion
    //----------------------------------------------------------------------
    public override void Awake()
    {
        background = this.gameObject.GetComponent<Image>();
        mInput = this.gameObject.GetComponent<UiMouseInputProvider>();
        Subscribe();
        base.Awake();
    }

    protected override void OnSkinUI ()
    {
        
        base.OnSkinUI();
        if(_isSelected)
        {
            _background = skinData.selectBackground;
            _font = skinData.selectFont;
            _icon = skinData.selectIcon;
            _color = skinData.selectColor;
        }else{
            _background = skinData.unselectBackground;
            _font = skinData.unselectFont;
            _icon = skinData.unselectIcon;
            _color = skinData.unselectColor;
        }


        background.sprite = _background;
        if(content != null)
        {
            content.font = _font;
            content.color = _color;
        } 

        if(icon != null) icon.sprite = _icon;

    }

    void OnClick(PointerEventData eventData)
    {
        _isSelected = !_isSelected;
        _isDirty = true;
    }

    void Subscribe()
    {
        mInput.OnPointerUp += OnClick;
    }
    

    void Unsubscribe()
    {
        mInput.OnPointerUp += OnClick;
    }
}
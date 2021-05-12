using Custom.UI;
using Tools.UI;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

[RequireComponent(typeof(IMouseInput))]
[RequireComponent(typeof(Image))]
public class UIButton : CustomUI
{

    //----------------------------------------------------------------------
    #region Properties

    [SerializeField] private TMP_Text content;
    private Image background;
    private IMouseInput mInput;
    public event Action<UIButton> OnLeftClickEvent;        //event sent when an item is left-clicked

    public string id = "";  //used to as a localization independent identifier

    #endregion
    //----------------------------------------------------------------------
    #region SkinVariables

    protected bool _isSelected;
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

    public void Populate(string objectName)
    {
        this.gameObject.name = objectName;
        this.content.text = objectName;
    }

    public void Populate(string objectName, string id)
    {
        this.gameObject.name = id;
        this.id = id;
        this.content.text = objectName;
    }

    protected override void OnSkinUI ()
    {
        
        base.OnSkinUI();
        if(_isSelected)
        {
            _background = skinData.selectBackground;
            _font = skinData.selectFont;
            _color = skinData.selectColor;
        }else{
            _background = skinData.unselectBackground;
            _font = skinData.unselectFont;
            _color = skinData.unselectColor;
        }


        background.sprite = _background;
        if(background.sprite == null) background.color =new Color (0,0,0,0);    //turn off colour and alpha if no image
        if(content != null)
        {
            content.font = _font;
            content.color = _color;
        } 
    }

    void OnClick(PointerEventData eventData)
    {
        OnLeftClickEvent(this);
    }

    public void Select(){
        mInput.setActive(false);
        _isSelected = true;
        _isDirty = true;
    }

    public void Deselect(){
        mInput.setActive(true);
        _isSelected = false;
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
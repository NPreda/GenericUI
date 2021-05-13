using UnityEngine;
using UnityEngine.UI;

public class UIRoundButton : UIButton
{
    //----------------------------------------------------------------------
    #region Variables
    [SerializeField] private Image icon;

    private Sprite activeIcon;
    private Sprite unactiveIcon;

    #endregion
    //----------------------------------------------------------------------
    #region Logic

    public override void Awake()
    {
        base.Awake();
        icon.alphaHitTestMinimumThreshold = 0.9f;   //prevent clicks outside the circle from registering
    }

    public void Populate(string objectName, Sprite activeIcon, Sprite unactiveIcon)
    {
        this.gameObject.name = objectName;
        this.id = objectName;
        this.activeIcon = activeIcon;
        this.unactiveIcon = unactiveIcon;
        this._isDirty = true;
    }

    protected override void OnSkinUI ()
    {
        base.OnSkinUI();
        if(_isSelected)
        {
            icon.sprite = activeIcon;
        }else{
            icon.sprite = unactiveIcon;
        }
    }

    #endregion
    //----------------------------------------------------------------------
}
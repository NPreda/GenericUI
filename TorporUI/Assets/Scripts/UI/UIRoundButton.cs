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

    public void Populate(string objectName, Sprite activeIcon, Sprite unactiveIcon)
    {
        this.gameObject.name = objectName;
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
using UnityEngine;
using TMPro;
using Tools.UI;
using System;
using UnityEngine.EventSystems;


public class LogEntry: UIButton
{
    [SerializeField]private UIButton button;

    public event Action<LogEntry> RemoveEntry;

    public void Start()
    {
        button.OnLeftClickEvent += SendDelete;
        button.Select();
    }

    public override void Select(){
        button.Deselect();
        _isSelected = true;
        _isDirty = true;
    }

    public override void Deselect(){
        button.Select();
        _isSelected = false;
        _isDirty = true;
    } 

    public override void OnClick(PointerEventData eventData)
    {
        if(!_isSelected)   
            base.OnClick(eventData);
        else    Deselect();
    }

    public void SendDelete(UIButton button)
    {
        RemoveEntry(this);
    }
}

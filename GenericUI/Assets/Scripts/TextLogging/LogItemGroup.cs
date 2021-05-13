
using System;
using UnityEngine.EventSystems;

public class LogItemGroup : UIButtonGroup
{
    public event Action<LogItemGroup> ListChanged;

    //add and register button script to event system
    public void Add(LogEntry button)
    {
        buttons.Add(button);
        button.OnLeftClickEvent += OnButtonSelected;
        button.RemoveEntry += RemoveButton;
    }


    public override void RemoveButton(UIButton button)
    {
        buttons.Remove(button);
        button.DestroyHost();
        ListChanged(this);
    }

}
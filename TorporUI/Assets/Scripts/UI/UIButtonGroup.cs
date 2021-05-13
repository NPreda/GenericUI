using System.Collections.Generic;
using System;
using UnityEngine;


public class UIButtonGroup
{
    public List<UIButton> buttons = new List<UIButton>();

    public event Action<UIButton> OnButtonPressed;        //event sent when an item is left-clicked

    //add and register button script to event system
    public virtual void Add(UIButton button)
    {
        buttons.Add(button);
        button.OnLeftClickEvent += OnButtonSelected;
    }

    public void SelectButton(string buttonId)
    {
        UIButton selectedButton = buttons.Find((x) => x.id == buttonId);
        OnButtonSelected(selectedButton);
    }

    public void RemoveButton(UIButton button)
    {
        buttons.Remove(button);
        button.DestroyHost();
    }

    public void ClearButtons()
    {
        for(int i = buttons.Count - 1; i >= 0; i--)
        {
            buttons[i].DestroyHost();
            buttons.RemoveAt(i);
        }
    }

    //highlight selection
    protected void OnButtonSelected(UIButton selectedButton)
    {
        foreach(var button in buttons)
        {
            if(button != selectedButton) button.Deselect();
            else button.Select();
        }

        OnButtonPressed(selectedButton);
    }

}
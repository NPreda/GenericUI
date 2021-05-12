using System.Collections.Generic;
using System;


public class UIButtonGroup
{
    private List<UIButton> buttons = new List<UIButton>();

    public event Action<UIButton> OnButtonPressed;        //event sent when an item is left-clicked

    //add and register button script to event system
    public void Add(UIButton button)
    {
        buttons.Add(button);
        button.OnLeftClickEvent += OnButtonSelected;
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
    private void OnButtonSelected(UIButton selectedButton)
    {
        foreach(var button in buttons)
        {
            if(button != selectedButton) button.Deselect();
            else button.Select();
        }

        OnButtonPressed(selectedButton);
    }

}
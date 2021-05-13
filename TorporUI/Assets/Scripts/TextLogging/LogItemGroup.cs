


public class LogItemGroup : UIButtonGroup
{
    //add and register button script to event system
    public void Add(LogEntry button)
    {
        buttons.Add(button);
        button.OnLeftClickEvent += OnButtonSelected;
        button.RemoveEntry += RemoveButton;
    }

}
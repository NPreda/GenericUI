using UnityEngine;
using TMPro;
using System;



public class NotesController : MonoBehaviour
{
    [SerializeField] GameObject MainPanel;
    [SerializeField]private GameObject actBar;
    [SerializeField]private GameObject fieldPanel;
    [SerializeField]private TMP_InputField inputField;
    [SerializeField]private UIButton inputButton;
    [SerializeField]private TextLogControl logControl;

    private UIButtonGroup actButtons = new UIButtonGroup();
    private ButtonFactory buttonFactory = new ButtonFactory();
    private SettingsDB settings;

    public void Start()
    {
        settings = Registry.Instance.settings;
        actButtons.OnButtonPressed += ButtonSelected;

        string languagePrefix = settings.language.ToString();
        TextAsset localizationJson = Resources.Load<TextAsset>("Data/Localization/" + languagePrefix + "/UI/actbuttons");
        ActButtonStruct buttonStruct = JsonUtility.FromJson<ActButtonStruct>(localizationJson.text);

        //create the buttons
        CreateButtons(buttonStruct);
    }

    public void Enable()
    {
        MainPanel.SetActive(true);
        actButtons.SelectButton("act_1");       //hack to get it to not go straight for the act_1.json file, alternative would be to hide the user input till act selected
    }

    public void Disable()
    {
        MainPanel.SetActive(false);
    }


    private void CreateButtons(ActButtonStruct buttonStruct)    //create all the buttons for the act selection. Very clumsy, properly implemented would tie into the gmae logic.
    {
        UIButton newButton = buttonFactory.GetNewInstance(actBar, buttonStruct.act_1, "act_1", "Prefabs/UI/SelectorButton");
        actButtons.Add(newButton);  

        newButton = buttonFactory.GetNewInstance(actBar, buttonStruct.act_2, "act_2", "Prefabs/UI/SelectorButton");
        actButtons.Add(newButton);  
        
        newButton = buttonFactory.GetNewInstance(actBar, buttonStruct.act_3, "act_3", "Prefabs/UI/SelectorButton");
        actButtons.Add(newButton);  

        newButton = buttonFactory.GetNewInstance(actBar, buttonStruct.act_4, "act_4", "Prefabs/UI/SelectorButton");
        actButtons.Add(newButton);  

        inputButton= buttonFactory.GetNewInstance(fieldPanel, buttonStruct.add_note, "add_note", "Prefabs/UI/IndexButton");
        inputButton.OnLeftClickEvent += inputText;
    }

    private void ButtonSelected(UIButton button)
    {
        logControl.SetAct(button.id);
    }

    private void inputText(UIButton button)
    {
        if(inputField.text != "")
        {
            logControl.LogText(inputField.text);
            inputField.text = "";
        }
    }
}

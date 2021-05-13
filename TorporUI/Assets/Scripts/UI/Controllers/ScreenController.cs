using UnityEngine;
using System;

public class ScreenController : MonoBehaviour
{
    [SerializeField]private GameObject topBar;
    [SerializeField]private CodexController codexController;
    [SerializeField]private NotesController notesController;

    private UIButtonGroup buttonGroup = new UIButtonGroup();
    private ButtonFactory buttonFactory = new ButtonFactory();
    private SettingsDB settings;

    public void Start()
    {
        //ideally the settings would be extensive, in this implementation it only selects a variable for the localization path
        settings = Registry.Instance.settings;
        buttonGroup.OnButtonPressed += ButtonSelected;

        string languagePrefix = settings.language.ToString();
        TextAsset localizationJson = Resources.Load<TextAsset>("Data/Localization/" + languagePrefix + "/UI/topbar");
        TopButtonStruct buttonStruct = JsonUtility.FromJson<TopButtonStruct>(localizationJson.text);

        //create the buttons for the top section
        UIButton newButton = buttonFactory.GetNewInstance(topBar, buttonStruct.codex, "codex", "Prefabs/UI/SelectorButton");
        buttonGroup.Add(newButton);

        newButton = buttonFactory.GetNewInstance(topBar, buttonStruct.notes, "notes", "Prefabs/UI/SelectorButton");
        buttonGroup.Add(newButton);

        //hack to go straight to the codex screen
        buttonGroup.SelectButton("codex");       
    }


    private void ButtonSelected(UIButton button)
    {
        if (button.id == "codex")
        {
            codexController.Enable();
            notesController.Disable();
        }else if(button.id == "notes"){
            notesController.Enable();
            codexController.Disable();
        }else{
            throw new Exception("One of the top bar buttons has a malformed 'id' field");
        }
    }
}

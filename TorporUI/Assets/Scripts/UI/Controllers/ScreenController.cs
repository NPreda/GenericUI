using UnityEngine;
using System;

public class ScreenController : MonoBehaviour
{
    [SerializeField]private GameObject topBar;
    [SerializeField]private GameObject codexPanel;
    [SerializeField]private GameObject notesPanel;

    private UIButtonGroup buttonGroup = new UIButtonGroup();
    private ButtonFactory buttonFactory = new ButtonFactory();
    private SettingsDB settings;

    public void Start()
    {
        settings = Registry.Instance.settings;
        buttonGroup.OnButtonPressed += ButtonSelected;

        string languagePrefix = settings.language.ToString();
        TextAsset localizationJson = Resources.Load<TextAsset>("Data/Localization/" + languagePrefix + "/UI/topbar");
        TopButtonStruct buttonStruct = JsonUtility.FromJson<TopButtonStruct>(localizationJson.text);

        //create the buttons
        UIButton newButton = buttonFactory.GetNewInstance(topBar, buttonStruct.codex, "codex", "Prefabs/UI/SelectorButton");
        buttonGroup.Add(newButton);

        newButton = buttonFactory.GetNewInstance(topBar, buttonStruct.notes, "notes", "Prefabs/UI/SelectorButton");
        buttonGroup.Add(newButton);       
    }


    private void ButtonSelected(UIButton button)
    {
        if (button.id == "codex")
        {
            codexPanel.SetActive(true);
            notesPanel.gameObject.SetActive(false);
        }else if(button.id == "notes"){
            notesPanel.gameObject.SetActive(true);
            codexPanel.gameObject.SetActive(false);
        }else{
            throw new Exception("One of the top bar buttons has a malformed 'id' field");
        }
    }
}

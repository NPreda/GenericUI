using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CodexController : MonoBehaviour
{
    [SerializeField] GameObject categoryPanel;
    [SerializeField] GameObject subCategoryPanel;
    [SerializeField] GameObject entryLeafPanel;
    [SerializeField] EntryDisplay entryDisplay;

    private SettingsDB settings;

    private List<CodexEntry> codexEntries = new List<CodexEntry>();

    private RoundButtonFactory roundButtonFactory = new RoundButtonFactory(); 
    private ButtonFactory buttonFactory = new ButtonFactory();

    private UIButtonGroup categoryButtons = new UIButtonGroup();
    private UIButtonGroup subCategoryButtons = new UIButtonGroup();
    private UIButtonGroup entryButtons = new UIButtonGroup();

    public void Awake()
    {
        settings = Registry.Instance.settings;

        categoryButtons.OnButtonPressed += OnCategorySelected;
        subCategoryButtons.OnButtonPressed += OnSubcategorySelected;
        entryButtons.OnButtonPressed += OnEntrySelected;

        GetAllEntries();
        SpawnCategories();
    }

    private void GetAllEntries()
    {   
        var languagePrefix = settings.language.ToString();
        List<TextAsset> entryAssets = Resources.LoadAll("Data/Localization/" + languagePrefix + "/Entries", typeof(TextAsset)).Cast<TextAsset>().ToList();
        foreach(var entry in entryAssets)
        {
            var newEntry = JsonUtility.FromJson<CodexEntry>(entry.text);
            codexEntries.Add(newEntry);
        }
    }

    private void SpawnEntryButtons(string tag)
    {
        entryButtons.ClearButtons();
        foreach(var entry in codexEntries)
        {
            if(entry.tag == tag)
            {
                UIButton button = buttonFactory.GetNewInstance(entryLeafPanel, entry.title, "Prefabs/UI/LeafButton");
                entryButtons.Add(button);    
            }        
        }
    }

    //dynamically spawn the subcategory buttons according to selected category and language
    private void SpawnSubCategories(string category)
    {
        var languagePrefix = settings.language.ToString();
        TextAsset loadedFile = Resources.Load<TextAsset>("Data/Localization/" + languagePrefix +"/Categories/" + category);
        SubcategoryList subcategoryList = JsonUtility.FromJson<SubcategoryList>(loadedFile.text);
        foreach(string subcategory in subcategoryList.subcategories)
        {
            UIButton button = buttonFactory.GetNewInstance(subCategoryPanel, subcategory, "Prefabs/UI/IndexButton");
            subCategoryButtons.Add(button);
        }
    }

    //generate buttons for main category section
    private void SpawnCategories()
    {
        TextAsset loadedFile = Resources.Load<TextAsset>("Data/Codex/Categories");
        var codexCategories  = JsonUtility.FromJson<CodexCategoryList>(loadedFile.text).Categories;
        foreach(var category in codexCategories)
        {
            UIRoundButton button = roundButtonFactory.GetNewInstance(categoryPanel, category);
            categoryButtons.Add(button);
            button.OnLeftClickEvent += OnCategorySelected;
        }
    }

    private void OnCategorySelected(UIButton buttonScript)
    {
        //clean up existing buttons and displayed entry
        subCategoryButtons.ClearButtons();
        entryButtons.ClearButtons();
        entryDisplay.Clear();   

        //repopulate the space
        SpawnSubCategories(buttonScript.gameObject.name);
    }

    private void OnSubcategorySelected(UIButton buttonScript)
    {
        SpawnEntryButtons(buttonScript.gameObject.name);
    }

    private void OnEntrySelected(UIButton buttonScript)
    {
        //display entry
        CodexEntry item = codexEntries.Find(x=> x.title == buttonScript.gameObject.name);
        entryDisplay.Populate(item);
    }


}
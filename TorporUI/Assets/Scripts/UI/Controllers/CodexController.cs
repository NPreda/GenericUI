using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Language{
    en = 0,
}

public class CodexController : MonoBehaviour
{

    [SerializeField] Language languageSelection;

    [SerializeField] GameObject categoryPanel;
    [SerializeField] GameObject subCategoryPanel;
    [SerializeField] GameObject entryLeafPanel;
    [SerializeField] EntryDisplay entryDisplay;

    private List<CodexEntry> codexEntries = new List<CodexEntry>();

    private RoundButtonFactory roundButtonFactory = new RoundButtonFactory(); 
    private ButtonFactory buttonFactory = new ButtonFactory();

    private List<UIRoundButton> categoryButtons = new List<UIRoundButton>();
    private List<UIButton> subCategoryButtons = new List<UIButton>();
    private List<UIButton> entryButtons = new List<UIButton>();

    public void Awake()
    {
        GetAllEntries();
        SpawnCategories();
    }

    private void GetAllEntries()
    {       
        var languagePrefix = languageSelection.ToString();
        List<TextAsset> entryAssets = Resources.LoadAll("Data/Localization/" + languagePrefix + "/Entries", typeof(TextAsset)).Cast<TextAsset>().ToList();
        foreach(var entry in entryAssets)
        {
            var newEntry = JsonUtility.FromJson<CodexEntry>(entry.text);
            codexEntries.Add(newEntry);
        }
    }

    private void SpawnEntryButtons(string tag)
    {
        CleanButtons(entryButtons);
        foreach(var entry in codexEntries)
        {
            if(entry.tag == tag)
            {
                UIButton button = buttonFactory.GetNewInstance(entryLeafPanel, entry.title, "Prefabs/UI/LeafButton");
                entryButtons.Add(button);    
                button.OnLeftClickEvent += DisplayEntry;
            }        
        }
    }

    //dynamically spawn the subcategory buttons according to selected category and language
    private void SpawnSubCategories(string category)
    {
        CleanButtons(subCategoryButtons);
        var languagePrefix = languageSelection.ToString();
        TextAsset loadedFile = Resources.Load<TextAsset>("Data/Localization/" + languagePrefix +"/Categories/" + category);
        SubcategoryList subcategoryList = JsonUtility.FromJson<SubcategoryList>(loadedFile.text);
        foreach(string subcategory in subcategoryList.subcategories)
        {
            UIButton button = buttonFactory.GetNewInstance(subCategoryPanel, subcategory, "Prefabs/UI/IndexButton");
            button.OnLeftClickEvent += OnSubcategorySelected;
            subCategoryButtons.Add(button);
        }
    }

    //generic button destroyer
    private void CleanButtons(List<UIButton> buttonList)
    {
        for(int i = buttonList.Count - 1; i >= 0; i--)
        {
            buttonList[i].DestroyHost();
            buttonList.RemoveAt(i);
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
        foreach(var button in categoryButtons)
        {
            if(button != buttonScript) button.Deselect();
            else button.Select();
        }

        SpawnSubCategories(buttonScript.gameObject.name);
    }

    private void OnSubcategorySelected(UIButton buttonScript)
    {
        foreach(var button in subCategoryButtons)
        {
            if(button != buttonScript) button.Deselect();
            else button.Select();
        }

        SpawnEntryButtons(buttonScript.gameObject.name);
    }

    private void DisplayEntry(UIButton buttonScript)
    {
        CodexEntry item = codexEntries.Find(x=> x.title == buttonScript.gameObject.name);
        entryDisplay.Populate(item);
    }

}
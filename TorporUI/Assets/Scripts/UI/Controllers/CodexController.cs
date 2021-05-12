using System;
using System.Collections.Generic;
using UnityEngine;

public class CodexController : MonoBehaviour
{
    [SerializeField] GameObject categoryPanel;

    private RoundButtonFactory roundButtonFactory = new RoundButtonFactory(); 

    private List<UIRoundButton> categoryButtons = new List<UIRoundButton>();

    public void Awake()
    {
        TextAsset loadedfile = Resources.Load<TextAsset>("Data/Codex/Categories");
        string jsonstring = loadedfile.text;
        var codexCategories  = JsonUtility.FromJson<CodexCategoryList>(jsonstring).Categories;
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
    }

}
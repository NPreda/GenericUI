using Tools.Patterns;
using UnityEngine;

public class RoundButtonFactory : Factory
{

    public UIRoundButton GetNewInstance(GameObject parent, CodexCategory codexCategory)
    {
        this.prefab = Resources.Load("Prefabs/UI/RoundButton") as GameObject;
        Sprite activeIcon = Resources.Load<Sprite>(codexCategory.activeIconLocation);
        Sprite unactiveIcon = Resources.Load<Sprite>(codexCategory.iconLocation);
        GameObject categoryButton = GetNewInstance(parent);
        var buttonScript = categoryButton.GetComponent<UIRoundButton>();
        buttonScript.Populate(codexCategory.id, activeIcon, unactiveIcon);

        return buttonScript;
    }
}
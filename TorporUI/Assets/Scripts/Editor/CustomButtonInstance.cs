//using Custom.UI;
using UnityEngine;
using UnityEditor;

public class SelectorButtonInstance : Editor
{
    [MenuItem("GameObject/Custom UI/SelectorButton", priority = 0)]
    public static void AddButton()
    {
        Create("SelectorButton");
    }

    static GameObject clickedObject;

    private static GameObject Create (string objectName)
    {
        GameObject instance = Instantiate(Resources.Load<GameObject>("Prefabs/UI/" + objectName));
        instance.name = objectName;
        clickedObject = UnityEditor.Selection.activeObject as GameObject;
        if (clickedObject != null)
        {
            instance.transform.SetParent(clickedObject.transform, false);
        }
        return instance;
    }
}


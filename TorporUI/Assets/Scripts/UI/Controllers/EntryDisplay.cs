using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EntryDisplay: MonoBehaviour
{
    [SerializeField]private TMP_Text title;
    [SerializeField]private Image icon;
    [SerializeField]private TMP_Text content;

    public void Populate(CodexEntry codexEntry)
    {
        title.text = codexEntry.title;
        content.text = codexEntry.content;
        if(codexEntry.icon != null)
        {
            this.icon.gameObject.SetActive(true);
            this.icon.sprite = Resources.Load<Sprite>(codexEntry.icon);
        }else{
            this.icon.gameObject.SetActive(false);
        }
    }

    public void Clear()
    {
        this.title.text = "";
        this.content.text="";
        this.icon.gameObject.SetActive(false);
    }
}
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Environment = System.Environment;


public class TextLogControl : MonoBehaviour
{

    [SerializeField]
    private GameObject textTemplate;

    private LogItemGroup logItems = new LogItemGroup();
    private string actName;

    private bool _isModified = false;

    public void Start()
    {
        logItems.OnButtonPressed += OnItemSelected;
    }


    public void LogText(string newEntry)
    {
        //we delete older items after 10 entries as a temporary measure
        if (logItems.buttons.Count == 10) 
        {
            LogEntry tempItem = (LogEntry) logItems.buttons[0];
            Destroy(tempItem.gameObject);
            logItems.buttons.Remove(tempItem);
        }

        CreateEntry(newEntry);

        _isModified = true;
    }

    public void SetAct(string newAct)
    {
        //save previous notes
        SaveToFile();

        //move to new act notes
        actName = newAct;
        Populate();
    }

    private void Populate()
    {
        if(actName == "") throw new Exception("No act specified for the player log to update from");

        ClearNotes();    
        ValidateFile();

        //now let's read everything
        string dataFilePath= Application.persistentDataPath + "/";
        string notesJson = File.ReadAllText(dataFilePath + "TorporUI/" + actName +".json");

        StringListStruct stringStruct = JsonUtility.FromJson<StringListStruct>(notesJson);
        if (stringStruct != null)
        {
            foreach(string entry in stringStruct.strings)
            {
                CreateEntry(entry);
            }
        }
    }

    private void CreateEntry(string newEntry)
    {
        GameObject newText = Instantiate(textTemplate, this.gameObject.transform) as GameObject;
        LogEntry logEntryScript = newText.GetComponent<LogEntry>();
        logEntryScript.content.text = newEntry;
        logItems.Add(logEntryScript);
    }

    private void SaveToFile()
    {
        StringListStruct allEntries = new StringListStruct();
        foreach(var entry in logItems.buttons)
        {
            allEntries.strings.Add(entry.content.text);
        }

        string jsonObject = JsonUtility.ToJson(allEntries);
        
        ValidateFile();
        //now let's write down everything
        string dataFilePath= Application.persistentDataPath + "/";
        File.WriteAllText(dataFilePath + "TorporUI/" + actName +".json", jsonObject);

        _isModified = false;
    }   

    public void Update()
    {
        if(!_isModified) return;
        SaveToFile();

    }

    private void ClearNotes()
    {
        //delete existing notes
        logItems.ClearButtons();
    }

    private void ValidateFile()
    {
       //this will create the appropriate file and folder system if it doesn't exist
        string dataFilePath= Application.persistentDataPath + "/";
        Directory.CreateDirectory(dataFilePath + "TorporUI");
        if (!System.IO.File.Exists(dataFilePath + "TorporUI/" + actName +".json"))
        {
            var a = File.Create(dataFilePath + "TorporUI/" + actName +".json");
            a.Close();
        }
    }

    private void OnItemSelected(UIButton button)
    {
        //this currently does nothing
    }
}

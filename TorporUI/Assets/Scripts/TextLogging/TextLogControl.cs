using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Environment = System.Environment;


public class TextLogControl : MonoBehaviour
{

    [SerializeField]
    private GameObject textTemplate;

    private List<LogEntry> logItems;
    private string actName;

    private bool _isModified = false;

    void Start()
    {
        logItems = new List<LogEntry>();
    }


    public void LogText(string newEntry)
    {
        //we delete older items after 10 entries as a temporary measure
        if (logItems.Count == 10) 
        {
            LogEntry tempItem = logItems[0];
            Destroy(tempItem.gameObject);
            logItems.Remove(tempItem);
        }

        CreateEntry(newEntry);

        _isModified = true;
    }

    public void SetAct(string newAct)
    {
        actName = newAct;
        Populate();
    }

    private void Populate()
    {
        if(actName == "") throw new Exception("No act specified for the player log to update from");

        //delete existing logs
        for(int i = logItems.Count -1; i >= 0; i--)
        {
            Destroy(logItems[i].gameObject);
            logItems.RemoveAt(i);
        }
        
        //this will create the appropriate file and folder system if it doesn't exist
        string dataFilePath= Application.persistentDataPath + "/";
        Directory.CreateDirectory(dataFilePath + "TorporUI");
        var a = File.Create(dataFilePath + "TorporUI/" + actName +".json");
        a.Close();
        //now let's read everything
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

    private void SaveToFile()
    {
        StringListStruct allEntries = new StringListStruct();
        foreach(var entry in logItems)
        {
            allEntries.strings.Add(entry.entryText.text);
        }

        string jsonObject = JsonUtility.ToJson(allEntries);
        
        //this will create the appropriate file and folder system if it doesn't exist
        string dataFilePath= Application.persistentDataPath + "/";
        Directory.CreateDirectory(dataFilePath + "TorporUI");
        Debug.Log(dataFilePath + "TorporUI/" + actName +".json");
        //now let's write down everything
        File.WriteAllText(dataFilePath + "TorporUI/" + actName +".json", jsonObject);
    }   

    private void CreateEntry(string newEntry)
    {
        GameObject newText = Instantiate(textTemplate, this.gameObject.transform) as GameObject;
        LogEntry logEntryScript = newText.GetComponent<LogEntry>();
        logEntryScript.entryText.text = newEntry;
        logItems.Add(logEntryScript);
    }

    public void Update()
    {
        if(!_isModified) return;
        SaveToFile();

    }
}

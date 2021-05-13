using UnityEngine;

public class Registry : MonoBehaviour
{
    public SettingsDB settings;

    //Instance Control
    private static Registry m_Instance;
    public static  Registry Instance { get { return m_Instance; } }
  

    void Awake(){ 
        m_Instance = this;          //Get this instance
    }
    

}
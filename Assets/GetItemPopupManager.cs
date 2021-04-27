using Doozy.Engine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemPopupManager : Singleton<GetItemPopupManager>
{
    [Header("Popup Settings")]
    public string PopupName = "AchievementPopup";
    private UIPopup m_popup;
    List<string> items = new List<string>();
    List<string> levelups = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void pushItem(string name)
    {
        items.Add(name);
    }
    public void pushLevelup(string name)
    {
        levelups.Add(name);
    }
    public void showPreviousItems()
    {
        string text = "You get:";
        foreach(var t in items)
        {
            text += "\n" + t;
        }
        showString(text);
        items.Clear();
        foreach(var l in levelups)
        {
            showString(l);
        }
        levelups.Clear();
    }
    public void showString(string text)
    {
       

        //get a clone of the UIPopup, with the given PopupName, from the UIPopup Database 
        m_popup = UIPopupManager.GetPopup(PopupName);

        //make sure that a popup clone was actually created
        if (m_popup == null)
            return;

        //set the achievement icon
        //m_popup.Data.SetImagesSprites(achievement.Icon);
        //set the achievement title and message
        m_popup.Data.SetLabelsTexts("", text);

        //show the popup
        UIPopupManager.ShowPopup(m_popup, m_popup.AddToPopupQueue, false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

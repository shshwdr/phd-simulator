using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PaperInfo:ObjectInfo
{
    public string ritual;
    public string element;
    public string ritualName { get { return JsonManager.Instance.itemRitualDict[ritual].name; } }
    public string elementName { get { return JsonManager.Instance.itemElementDict[element].name; } }
    public override string name { get {
            if (element == "none")
            {
                return "a comprehensive survey of " + ritualName;
            }
            else if (ritual == "none")
            {
                return "a comprehensive survey of " + elementName + " element";
            }
            return elementName + " "+ritualName;
        } }

    public override string desc { get { return string.Format("{0} you can read them and learn from it",name); } }
    public override string id { get { return "paper_" + ritual + "_" + element; } }
    public int readProgress = 0;
}
public enum paperProposalState { created, selected, cancelled};
public class PaperProposalInfo : PaperInfo
{
    public int score;
    public paperProposalState state = paperProposalState.created;
    public float writeProgress = 0;

    public override string desc { get {
            string text = "Your proposal: {0}\nYou are currently writing this paper";
            if(state == paperProposalState.cancelled)
            {
                text = "Your proposal: {0}\nYou suspended this paper";
            }
            return string.Format(text, name); } }
}
public class PaperGeneration : Singleton<PaperGeneration>
{


    List<string> unlockedElement = new List<string>();
    List<string> unlockedRitual = new List<string>();
    //public Dictionary<string, PaperInfo> paperDict = new Dictionary<string, PaperInfo>();
    public float chanceToGeneratePaper()
    {
        //if never has element, chance is 1, generate element paper
        if (!AchievementSystem.Instance.isAchieved("getFirstElementPaper") || !AchievementSystem.Instance.isAchieved("getFirstRitualPaper"))
        {
            return 1;
        }

        var countOfElement = unlockedElement.Count;
        var countOfRitual = unlockedRitual.Count;

        var paperGenerationRate = 5;

        return ((countOfElement+ countOfRitual)* paperGenerationRate)/100.0f;
    }
    public PaperInfo generatePaper()
    {
        PaperInfo paperInfo = new PaperInfo();
        if (!AchievementSystem.Instance.isAchieved("getFirstElementPaper"))
        {
            //create fire paper

            paperInfo.element = "fire";
            paperInfo.ritual = "none";
            AchievementSystem.Instance.achieve("getFirstElementPaper");
            unlockedElement.Remove("fire");

        }
        else if (!AchievementSystem.Instance.isAchieved("getFirstRitualPaper"))
        {
            //create bolt paper

            paperInfo.element = "none";
            paperInfo.ritual = "bolt";
            AchievementSystem.Instance.achieve("getFirstRitualPaper");
            unlockedRitual.Remove("bolt");
        }
        else
        {
            //random element or ritual
            var rand= Random.Range(0,unlockedElement.Count + unlockedRitual.Count);
            if(rand< unlockedElement.Count)
            {

                paperInfo.element = unlockedElement[rand];
                paperInfo.ritual = "none";
            }
            else
            {

                paperInfo.element = "none";
                paperInfo.ritual = unlockedRitual[rand-unlockedElement.Count];
            }
        }

        return paperInfo;
    }

    public void generatePaperProposal(ItemElementInfo element, ItemRitualInfo ritual)
    {
        PaperProposalInfo paperInfo = new PaperProposalInfo();
        paperInfo.element = element.id;
        paperInfo.ritual = ritual.id;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach(var pair in JsonManager.Instance. itemElementDict)
        {
            if (!pair.Value.isLocked)
            {
                unlockedElement.Add(pair.Key);
            }
        }
        foreach (var pair in JsonManager.Instance.itemRitualDict)
        {
            if (!pair.Value.isLocked)
            {
                unlockedRitual.Add(pair.Key);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

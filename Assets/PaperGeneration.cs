using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PaperInfo
{
    public string ritual;
    public string element;
    public string title { get {
            if (element == "none")
            {
                return "a comprehensive survey of " + ritual;
            }
            else if (ritual == "none")
            {
                return "a comprehensive survey of " + element+" element";
            }
            return "";
        } }
    public string id { get { return "paper_" + ritual + "_" + element; } }
    public int readProgress = 0;
}
public enum paperProposalState { created, selected, cancelled};
public class PaperProposalInfo : PaperInfo
{
    public int score;
    public paperProposalState state = paperProposalState.created;
    public float writeProgress = 0;
}
public class PaperGeneration : Singleton<PaperGeneration>
{
    //public Dictionary<string, PaperInfo> paperDict = new Dictionary<string, PaperInfo>();
    public float chanceToGeneratePaper()
    {
        //if never has element, chance is 1, generate element paper
        if (AchievementSystem.Instance.isAchieved("getFirstElementPaper"))
        {

        }

        return 1;
    }
    public PaperInfo generatePaper()
    {
        //test
        PaperInfo paperInfo = new PaperInfo();
        paperInfo.element = "fire";
        paperInfo.ritual = "none";

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

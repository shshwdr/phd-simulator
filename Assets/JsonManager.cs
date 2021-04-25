using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[Serializable]
public class ActionResult
{
    public string resultDesc;
    public List<string> resultItem;
    public List<int> count;
    public float prob;
}

[Serializable]
public class ActionInfo
{
    public string input;
    public string actionName;
    public string _desc;
    public string doActionDesc;
    public List<ActionResult> _result;
    public float timeCost;

    public virtual List<ActionResult> result { get { return _result; } }
    public virtual string desc { get { return _desc; } }

}
[Serializable]
public class StudyActionInfo : ActionInfo
{

}
[Serializable]
public class PaperStudyActionInfo : StudyActionInfo
{
    public string paperId;
    public override string desc { get { return string.Format(_desc, PaperGeneration.Instance.paperDict[paperId].title); } }
    public override List<ActionResult> result { get { return _result; } }
}

[Serializable]
public class AllActionInfo
{
    public List<StudyActionInfo> studyActions;
    //public List<StudyActionInfo> studyActions;
}
public class JsonManager : Singleton<JsonManager>
{
    public Dictionary<string, StudyActionInfo> studyActionDict;
    
    void Awake()
    {
        string text = Resources.Load<TextAsset>("json/actions").text;
        AllActionInfo allActionInfoList = JsonUtility.FromJson<AllActionInfo>(text);

        studyActionDict = allActionInfoList.studyActions.ToDictionary(x => x.input, x => x);
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

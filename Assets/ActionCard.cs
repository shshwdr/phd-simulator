using Doozy.Engine.Progress;
using Doozy.Engine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public enum ActionCardState { wait, doing, done}
public class ActionCard : MonoBehaviour
{
    //protected string selectedAction;
    protected ActionInfo selectedActionInfo;
    protected ActionCardState cardState;
    protected float currentTime = 0;
    [SerializeField] protected GameObject doItButton;
    [SerializeField] protected GameObject collectButton;
    [SerializeField] protected GameObject selectMenuButton;
    [SerializeField] protected Progressor progressor;
    [SerializeField] protected ActionMenu selectMenu;

    // Start is called before the first frame update

    private void Start()
    {
        changeSelectedAction(JsonManager.Instance.studyActionDict["none"]);
        //selectedActionInfo = JsonManager.Instance.studyActionDict["none"];

        doItButton.GetComponent<UIButton>().OnClick.OnTrigger.Event.AddListener(delegate { doIt(); });
        collectButton.GetComponent<UIButton>().OnClick.OnTrigger.Event.AddListener(delegate { collect(); });
        selectMenuButton.GetComponent<UIButton>().OnClick.OnTrigger.Event.AddListener(delegate { openSelectMenu(); });
        collectButton.gameObject.SetActive(false);
        selectMenu.card = this;
    }

    public void changeSelectedAction(ActionInfo newInfo)
    {
        selectedActionInfo = newInfo;
        selectMenuButton.GetComponentInChildren<TMP_Text>().text = newInfo.name;
    }

    // Update is called once per frame
    protected virtual void Update()
    {

        if (cardState == ActionCardState.doing)
        {
            currentTime += Time.deltaTime;

            progressor.SetProgress(currentTime / selectedActionInfo.timeCost);
            if (currentTime >= selectedActionInfo.timeCost)
            {
                cardState = ActionCardState.done;
                collectButton.SetActive(true);

            }
            else
            {
            }
        }
    }

    public virtual void doIt()
    {
        cardState = ActionCardState.doing;
        currentTime = 0;
        progressor.gameObject.SetActive(true);
        doItButton.SetActive(false);

    }
    public virtual void openSelectMenu() {
        if (cardState != ActionCardState.wait)
        {
            return;
        }
        selectMenu.GetComponent<UIView>().Show();
    }
    public  virtual void collect()
    {
        if (cardState != ActionCardState.done)
        {
            Debug.LogError("collect when not done!");
        }
        cardState = ActionCardState.wait;
        progressor.gameObject.SetActive(false);

        collectButton.SetActive(false);

        doItButton.SetActive(true);
        //show result view?

        ActionResult highPriorityResult = null;
        List<ActionResult> results = new List<ActionResult>();
        List<float> probs = new List<float>();
        ActionResult selectedResult = null;
        foreach (var r in selectedActionInfo.result)
        {
            if (r.prob < 0)
            {
                highPriorityResult = r;
                continue;
            }
            else
            {
                results.Add(r);
                probs.Add(r.prob);
            }
        }
        if (highPriorityResult != null)
        {
            if(highPriorityResult.resultItem[0] == "paper")
            {
                if(Random.value< PaperGeneration.Instance.chanceToGeneratePaper())
                {
                    selectedResult = highPriorityResult;
                }
            }
            else
            {
                selectedResult = highPriorityResult;
            }
        }

        if (selectedResult == null)
        {
            int selectedId = Utils.getRandomInArrayDistribution(probs);
            selectedResult = results[selectedId];
        }

        GetItemPopupManager.Instance.pushItem(selectedResult.resultDesc);
        for(int i = 0; i < selectedResult.resultItem.Count; i++)
        {
            //Debug.Log("get "+ selectedResult.count[i]+" " + selectedResult.resultItem[i]);
            Inventory.Instance.addItem(selectedResult.resultItem[i], selectedResult.count[i],selectedActionInfo);
        }
        GetItemPopupManager.Instance.showPreviousItems();
    }
}

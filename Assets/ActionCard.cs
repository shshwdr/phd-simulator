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
        selectMenuButton.GetComponentInChildren<TMP_Text>().text = newInfo.actionName;
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
        //show result view?

        //calculate probability to get each result

        ActionResult result = selectedActionInfo.result[0];
        for(int i = 0; i < result.resultItem.Count; i++)
        {
            Debug.Log("get "+result.count[i]+" " + result.resultItem[i]);
            Inventory.Instance.addItem(result.resultItem[i], result.count[i]);
        }
    }
}

using Doozy.Engine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineActionCard : ActionCard
{
    [SerializeField] CombineProposalMenu collectMenu;
    [SerializeField] float combineTime = 0.5f;
    protected override void Update()
    {

        if (cardState == ActionCardState.doing)
        {
            currentTime += Time.deltaTime;

            progressor.SetProgress(currentTime / combineTime);
            if (currentTime >= combineTime)
            {
                cardState = ActionCardState.done;
                collectButton.SetActive(true);

            }
            else
            {
            }
        }
    }
    //public override void doIt()
    //{
    //    cardState = ActionCardState.doing;
    //    currentTime = 0;
    //    progressor.gameObject.SetActive(true);
    //    doItButton.SetActive(false);

    //}
    public override void collect()
    {
        if (cardState != ActionCardState.done)
        {
            Debug.LogError("collect when not done!");
        }
        cardState = ActionCardState.wait;
        progressor.gameObject.SetActive(false);

        collectButton.SetActive(false);
        collectMenu.GetComponent<UIView>().Show();
        collectMenu.init(((CombineActionMenu)selectMenu).elementInfo.id, ((CombineActionMenu)selectMenu).ritualInfo.id);
    }
}
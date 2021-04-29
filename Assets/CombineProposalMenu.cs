using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Doozy.Engine.UI;

public class CombineProposalMenu : MonoBehaviour
{
    [SerializeField] TMP_Text desc;
    [SerializeField] UIButton doitButton;
    [SerializeField] UIButton suspendButton;
    [SerializeField] float delayTime = 3f;
    string element;
    string ritual;
    public void init(string e, string r)
    {
        element = e;
        ritual = r;
        StartCoroutine(showResult());
    }

    IEnumerator showResult()
    {
        yield return new WaitForSeconds(3);
        desc.text = "They think this paper is feasible! It might not be perfect but can be a good start!";
        doitButton.gameObject.SetActive(true);
        suspendButton.gameObject.SetActive(true);

        doitButton.GetComponent<UIButton>().OnClick.OnTrigger.Event.AddListener(delegate { onDoitButton(); });
        suspendButton.GetComponent<UIButton>().OnClick.OnTrigger.Event.AddListener(delegate { onSuspenditButton(); });

    }

    public void onDoitButton()
    {
        generateProposal(true);

    }
    public void onSuspenditButton()
    {
        generateProposal(false);
    }

    void generateProposal(bool doit)
    {
        //create paper proposal
        PaperProposalInfo info = new PaperProposalInfo();
        info.element = element;
        info.ritual = ritual;
        if (doit)
        {
            info.state = paperProposalState.selected;
        }
        else
        {
            info.state = paperProposalState.cancelled;
        }
        Inventory.Instance.addPaperProposal(info);
        GetComponent<UIView>().Hide();

    }

    // Start is called before the first frame update
    void Start()
    {

        doitButton.gameObject.SetActive(false);
        suspendButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class TextColorChange : MonoBehaviour
{
    public GameObject eventSystem;

    //dictionary of <string, bool> for each task
    public Dictionary<string, bool> bools = new Dictionary<string, bool>();
    public Dictionary<string, string> texts = new Dictionary<string, string>();

    // Start is called before the first frame update
    void Start()
    {
        bools.Add("cabs_filed", false);          
        bools.Add("paper_jam", false);
        bools.Add("answered_call", false);       
        bools.Add("approving_papers", false);
        bools.Add("got_coffee", false);          
        bools.Add("bring_to_boss", false);

        texts.Add("cabs_filed", "File approved papers");
        texts.Add("paper_jam", "Fix paper jam");
        texts.Add("answered_call", "Answer the phone call!");
        texts.Add("approving_papers", "Approve paperwork at your desk");
        texts.Add("got_coffee", "Get coffee in the breakroom for the boss");
        texts.Add("bring_to_boss", "Bring the boss his coffee");
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < bools.Count; i++)
        {
            if(bools.ElementAt(i).Value)
            {
                //cabs filed
                if(bools.ElementAt(i).Key == "cabs_filed")
                {
                    bools[bools.ElementAt(i).Key] = false;
                    StartCoroutine(colorSwitch(bools.ElementAt(i).Key, 31));
                }
                //paper jam
                else if(bools.ElementAt(i).Key == "paper_jam")
                {
                    bools[bools.ElementAt(i).Key] = false;
                    StartCoroutine(colorSwitch(bools.ElementAt(i).Key, 61));
                }
                //answered call
                else if(bools.ElementAt(i).Key == "answered_call")
                {
                    bools[bools.ElementAt(i).Key] = false;
                    StartCoroutine(colorSwitch(bools.ElementAt(i).Key, 21));
                }
                //approving papers
                else if(bools.ElementAt(i).Key == "approving_papers")
                {
                    bools[bools.ElementAt(i).Key] = false;
                    StartCoroutine(colorSwitch(bools.ElementAt(i).Key, 31));
                }
                //got coffee
                else if(bools.ElementAt(i).Key == "got_coffee")
                {
                    bools[bools.ElementAt(i).Key] = false;
                    StartCoroutine(colorSwitch(bools.ElementAt(i).Key, 31));
                }
                //bring to boss
                else
                {
                    bools[bools.ElementAt(i).Key] = false;
                    StartCoroutine(colorSwitch(bools.ElementAt(i).Key, 41));
                }
            }
        }
    }

    public void beginColorTrans(string taskName)
    {
        if(bools.ContainsKey(taskName))
        {
            bools[taskName] = true;
        }
    }

    IEnumerator colorSwitch(string taskN, float taskTime)
    {
        string temp = eventSystem.GetComponent<TasksManager>().texts[taskN];

        //25%

        //Check if task was completed
        if (eventSystem.GetComponent<TasksManager>().bools[taskN])
        {
            eventSystem.GetComponent<TasksManager>().texts[taskN] = temp;
            //eventSystem.GetComponent<TasksManager>().updateText();
            yield break;
        }

        yield return new WaitForSeconds(taskTime / 4);
        eventSystem.GetComponent<TasksManager>().texts[taskN] = "<color=yellow>" + temp + "</color>";
        eventSystem.GetComponent<TasksManager>().updateText();
        //50%

        //Check if task was completed
        if (eventSystem.GetComponent<TasksManager>().bools[taskN])
        {
            eventSystem.GetComponent<TasksManager>().texts[taskN] = temp;
            //eventSystem.GetComponent<TasksManager>().updateText();
            yield break;
        }

        yield return new WaitForSeconds(taskTime / 4);
        eventSystem.GetComponent<TasksManager>().texts[taskN] = "<color=orange>" + temp + "</color>";
        eventSystem.GetComponent<TasksManager>().updateText();
        //75%

        //Check if task was completed
        if (eventSystem.GetComponent<TasksManager>().bools[taskN])
        {
            eventSystem.GetComponent<TasksManager>().texts[taskN] = temp;
            //eventSystem.GetComponent<TasksManager>().updateText();
            yield break;
        }

        yield return new WaitForSeconds(taskTime / 4);
        eventSystem.GetComponent<TasksManager>().texts[taskN] = "<color=red>" + temp + "</color>";
        eventSystem.GetComponent<TasksManager>().updateText();

        //Check if task was completed
        if (eventSystem.GetComponent<TasksManager>().bools[taskN])
        {
            eventSystem.GetComponent<TasksManager>().texts[taskN] = temp;
            //eventSystem.GetComponent<TasksManager>().updateText();
            yield break;
        }
    }
}

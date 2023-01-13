using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class TasksManager : MonoBehaviour
{
    public GameObject eventSystem;

    public TextMeshProUGUI tasksText;

    public Sprite openCabinet;
    public Sprite closedCabinet;

    public Dictionary<string, bool> bools = new Dictionary<string, bool>();
    public Dictionary<string, string> texts = new Dictionary<string, string>();

    public float coffeeActivationTime;
    public float phoneActivationTime;
    public float jamActivationTime;
    public float paperActivationTime;
    public float timeOverallCoffee;
    public float timeOverallPhone;
    public float timeOverallJam;
    public float timeOverallPaper;

    public bool paperhandOn = false;
    public bool coffeehandOn = false;

    public AudioSource phoneSound;
    public AudioSource beep;

    // Start is called before the first frame update
    void Start()
    {

        texts.Add("cabs_filed", "File approved papers");
        texts.Add("paper_jam", "Fix paper jam");
        texts.Add("answered_call", "Answer the phone call!");
        texts.Add("approving_papers", "Approve paperwork at your desk");
        texts.Add("got_coffee", "Get coffee in the breakroom for the boss");
        texts.Add("bring_to_boss", "Bring the boss his coffee");

        bools.Add("cabs_filed", true);          //true by default bc initial task must be completed first
        bools.Add("paper_jam", false);
        bools.Add("answered_call", true);       //true by default b/c will be a random event!
        bools.Add("approving_papers", false);
        bools.Add("got_coffee", true);          //true by default b/c will be a random event!
        bools.Add("bring_to_boss", true);       //true by default b/c will follow up a random event!

        updateText();

        coffeeActivationTime = Random.Range(30, 61);
        phoneActivationTime = Random.Range(60, 121);
        jamActivationTime = Random.Range(20, 31);
        paperActivationTime = Random.Range(20, 31);
        timeOverallCoffee = Time.realtimeSinceStartup;
        timeOverallPhone = Time.realtimeSinceStartup;
        timeOverallJam = Time.realtimeSinceStartup;
        timeOverallPaper = Time.realtimeSinceStartup;

        //make calls to task timer
        StartCoroutine(WaitOnStart("paper_jam", 61));
        StartCoroutine(WaitOnStart("approving_papers", 31));
    }

    //called to update task list
    public void taskUpdate(int val)
    {
        //val 1 = approve paperwork complete
        if(val == 1)
        {
            updateText();
            GameObject.Find("PaperworkHand").GetComponent<Image>().enabled = true;
            paperhandOn = true;
            GameObject[] drawers = GameObject.FindGameObjectsWithTag("opens");
            
            foreach(GameObject cab in drawers)
            {
                cab.GetComponent<SpriteRenderer>().sprite = openCabinet;
            }

            eventSystem.GetComponent<TaskExpireTracker>().taskTime["approving_papers"] = -10;
            eventSystem.GetComponent<TaskExpireTracker>().timeToCompleteTask["approving_papers"] = -10;
        }

        //val 2 = filing cabinet complete
        else if(val == 2)
        {
            updateText();
            GameObject.Find("PaperworkHand").GetComponent<Image>().enabled = false;
            paperhandOn = false;
            GameObject[] drawers = GameObject.FindGameObjectsWithTag("opens");

            foreach (GameObject cab in drawers)
            {
                cab.GetComponent<SpriteRenderer>().sprite = closedCabinet;
            }

            eventSystem.GetComponent<TaskExpireTracker>().taskTime["cabs_filed"] = -10;
            eventSystem.GetComponent<TaskExpireTracker>().timeToCompleteTask["cabs_filed"] = -10;
        }

        //val 3 = answer phone complete
        else if(val == 3)
        {
            updateText();
            eventSystem.GetComponent<TaskExpireTracker>().taskTime["answered_call"] = -10;
            eventSystem.GetComponent<TaskExpireTracker>().timeToCompleteTask["answered_call"] = -10;
        }

        //val 4 = paper jam complete
        else if(val == 4)
        {
            updateText();
            eventSystem.GetComponent<TaskExpireTracker>().taskTime["paper_jam"] = -10;
            eventSystem.GetComponent<TaskExpireTracker>().timeToCompleteTask["paper_jam"] = -10;
        }

        //val 5 = get coffee complete
        else if(val == 5)
        {
            updateText();
            eventSystem.GetComponent<TaskExpireTracker>().taskTime["got_coffee"] = -10;
            eventSystem.GetComponent<TaskExpireTracker>().timeToCompleteTask["got_coffee"] = -10;
        }

        //val 6 = bring to boss complete
        else
        {
            updateText();
            eventSystem.GetComponent<TaskExpireTracker>().taskTime["bring_to_boss"] = -10;
            eventSystem.GetComponent<TaskExpireTracker>().timeToCompleteTask["bring_to_boss"] = -10;
        }
    }

    //called to update task text based on whats complete and what isn't
    public void updateText()
    {
        string temp = "";

        for(int i = 0; i < 6; i++)
        {
            if (bools.ElementAt(i).Value == false)
            {
                for(int j = 0; j < 6; j++)
                {
                    if(texts.ElementAt(j).Key == bools.ElementAt(i).Key)
                    {
                        temp = temp + texts.ElementAt(j).Value + "\n";
                    }
                }
            }
        }
        beep.Play();
        tasksText.text = temp;
    }

    private void Update()
    {
        //used to reset tasks after they have been completed


        //random call for coffee
        if (bools["got_coffee"] == true && bools["bring_to_boss"] == true)
        {
            if(Time.realtimeSinceStartup - timeOverallCoffee >= coffeeActivationTime)
            {
                Debug.Log("Activated!");
                bools["got_coffee"] = false;
                eventSystem.GetComponent<TaskExpireTracker>().updateTaskTime("got_coffee", 31);
                timeOverallCoffee = Time.realtimeSinceStartup;
                updateText();
            }
        }

        //random call for phone
        if (bools["answered_call"] == true)
        {
            if (Time.realtimeSinceStartup - timeOverallPhone >= phoneActivationTime)
            {
                Debug.Log("Activated!");
                bools["answered_call"] = false;
                eventSystem.GetComponent<TaskExpireTracker>().updateTaskTime("answered_call", 21);
                timeOverallPhone = Time.realtimeSinceStartup;
                updateText();
                phoneSound.Play();
            }
        }

        //random call for paper jam
        if (bools["paper_jam"] == true)
        {
            if (Time.realtimeSinceStartup - timeOverallJam >= jamActivationTime)
            {
                Debug.Log("Activated!");
                bools["paper_jam"] = false;
                eventSystem.GetComponent<TaskExpireTracker>().updateTaskTime("paper_jam", 61);
                timeOverallJam = Time.realtimeSinceStartup;
                updateText();
            }
        }

        //random call for approving papers
        if (bools["approving_papers"] == true && bools["cabs_filed"] == true)
        {
            if(Time.realtimeSinceStartup - timeOverallPaper >= paperActivationTime)
            {
                Debug.Log("Activated!");
                bools["approving_papers"] = false;
                eventSystem.GetComponent<TaskExpireTracker>().updateTaskTime("approving_papers", 31);
                timeOverallPaper = Time.realtimeSinceStartup;
                updateText();
            }
        }

    }

    public IEnumerator WaitOnStart(string name, float time)
    {
        yield return new WaitForSeconds(2);
        eventSystem.GetComponent<TaskExpireTracker>().updateTaskTime(name, time);
    }
}

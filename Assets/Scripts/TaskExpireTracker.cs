using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TaskExpireTracker : MonoBehaviour
{
    public GameObject eventSystem;

    public Dictionary<string, float> taskTime = new Dictionary<string, float>();
    public Dictionary<string, float> timeToCompleteTask = new Dictionary<string, float>();

    private void Start()
    {
        taskTime.Add("cabs_filed", -10);
        taskTime.Add("paper_jam", -10);
        taskTime.Add("answered_call", -10);
        taskTime.Add("approving_papers", -10);
        taskTime.Add("got_coffee", -10);
        taskTime.Add("bring_to_boss", -10);

        timeToCompleteTask.Add("cabs_filed", -10);
        timeToCompleteTask.Add("paper_jam", -10);
        timeToCompleteTask.Add("answered_call", -10);
        timeToCompleteTask.Add("approving_papers", -10);
        timeToCompleteTask.Add("got_coffee", -10);
        timeToCompleteTask.Add("bring_to_boss", -10);
    }

    void Update()
    {
        //once a task is active, there is a set time limit in which to complete it - if not completed in time,
        //task removed and will be randomly readded later (also, the boss's anger will increase!)
        
        for(int i = 0; i < taskTime.Count; i++)
        {
            if(timeToCompleteTask.ElementAt(i).Value != -10 && Time.realtimeSinceStartup - taskTime.ElementAt(i).Value >= timeToCompleteTask.ElementAt(i).Value)
            {
                eventSystem.GetComponent<TasksManager>().bools[taskTime.ElementAt(i).Key] = true;
                eventSystem.GetComponent<TasksManager>().updateText();
                taskTime[taskTime.ElementAt(i).Key] = -10;
                timeToCompleteTask[timeToCompleteTask.ElementAt(i).Key] = -10;
                Debug.Log(taskTime.ElementAt(i).Key + " expired!");
                //reset task timers in task manager
                //coffee
                if(taskTime.ElementAt(i).Key == "got_coffee")
                {
                    taskTime["got_coffee"] = -10;
                    timeToCompleteTask["got_coffee"] = -10;
                    eventSystem.GetComponent<TasksManager>().timeOverallCoffee = Time.realtimeSinceStartup;
                    eventSystem.GetComponent<TasksManager>().coffeeActivationTime = Random.Range(30, 61);
                    eventSystem.GetComponent<TasksManager>().texts["got_coffee"] = "Get coffee in the breakroom for the boss";
                    eventSystem.GetComponent<BossAngerManager>().boss_anger += 10;
                }
                //phone
                if (taskTime.ElementAt(i).Key == "answered_call")
                {
                    eventSystem.GetComponent<TasksManager>().phoneSound.Stop();
                    taskTime["answered_call"] = -10;
                    timeToCompleteTask["answered_call"] = -10;
                    eventSystem.GetComponent<TasksManager>().timeOverallPhone = Time.realtimeSinceStartup;
                    eventSystem.GetComponent<TasksManager>().phoneActivationTime = Random.Range(60, 121);
                    eventSystem.GetComponent<TasksManager>().texts["answered_call"] = "Answer the phone call!";
                    eventSystem.GetComponent<BossAngerManager>().boss_anger += 15;
                }
                //jam
                if (taskTime.ElementAt(i).Key == "paper_jam")
                {
                    taskTime["paper_jam"] = -10;
                    timeToCompleteTask["paper_jam"] = -10;
                    eventSystem.GetComponent<TasksManager>().timeOverallJam = Time.realtimeSinceStartup;
                    eventSystem.GetComponent<TasksManager>().jamActivationTime = Random.Range(20, 31);
                    eventSystem.GetComponent<TasksManager>().texts["paper_jam"] = "Fix paper jam";
                    eventSystem.GetComponent<BossAngerManager>().boss_anger += 10;
                }
                //papers
                if (taskTime.ElementAt(i).Key == "approving_papers")
                {
                    taskTime["approving_papers"] = -10;
                    timeToCompleteTask["approving_papers"] = -10;
                    eventSystem.GetComponent<TasksManager>().timeOverallPaper = Time.realtimeSinceStartup;
                    eventSystem.GetComponent<TasksManager>().paperActivationTime = Random.Range(20, 31);
                    eventSystem.GetComponent<TasksManager>().texts["approving_papers"] = "Approve paperwork at your desk";
                    eventSystem.GetComponent<BossAngerManager>().boss_anger += 10;
                }

                //reset text color for other tasks
                //cabs filed
                if (taskTime.ElementAt(i).Key == "cabs_filed")
                {
                    taskTime["cabs_filed"] = -10;
                    timeToCompleteTask["cabs_filed"] = -10;
                    eventSystem.GetComponent<TasksManager>().texts["cabs_filed"] = "File approved papers";
                    GameObject.Find("PaperworkHand").GetComponent<Image>().enabled = false;
                    eventSystem.GetComponent<TasksManager>().paperhandOn = false;
                    eventSystem.GetComponent<BossAngerManager>().boss_anger += 15;
                }
                //bring to boss
                if (taskTime.ElementAt(i).Key == "bring_to_boss")
                {
                    taskTime["bring_to_boss"] = -10;
                    timeToCompleteTask["bring_to_boss"] = -10;
                    eventSystem.GetComponent<TasksManager>().texts["bring_to_boss"] = "Bring the boss his coffee";
                    GameObject.Find("CoffeeCup").GetComponent<Image>().enabled = false;
                    eventSystem.GetComponent<TasksManager>().coffeehandOn = false;
                    eventSystem.GetComponent<BossAngerManager>().boss_anger += 20;
                }
            }
        }
    }

    public void updateTaskTime(string taskName, float taskLength)
    {

        if(taskTime.ContainsKey(taskName))
        {
            Debug.Log(taskName + " added with time " + taskLength);
            taskTime[taskName] = Time.realtimeSinceStartup;
            timeToCompleteTask[taskName] = taskLength;
            eventSystem.GetComponent<TextColorChange>().beginColorTrans(taskName);
        }
    }
}

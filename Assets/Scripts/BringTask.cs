using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BringTask : MonoBehaviour
{
    public GameObject eventSystem;
    public TextMeshProUGUI dialogueText;

    bool in_area = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player" && eventSystem.GetComponent<TasksManager>().bools["bring_to_boss"] == false)
        {
            dialogueText.text = "Press E to give the boss his coffee";
            in_area = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            dialogueText.text = "";
            in_area = false;
        }
    }

    private void Update()
    {
        if (in_area)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                dialogueText.text = "";
                eventSystem.GetComponent<TasksManager>().bools["bring_to_boss"] = true;

                //reset timer and text
                eventSystem.GetComponent<TasksManager>().timeOverallCoffee = Time.realtimeSinceStartup;
                eventSystem.GetComponent<TasksManager>().coffeeActivationTime = Random.Range(30, 61);
                eventSystem.GetComponent<TasksManager>().texts["got_coffee"] = "Get coffee in the breakroom for the boss";


                eventSystem.GetComponent<TaskExpireTracker>().updateTaskTime("cabs_filed", 31);
                eventSystem.GetComponent<TasksManager>().taskUpdate(6);

                GameObject.Find("CoffeeCup").GetComponent<Image>().enabled = false;
                eventSystem.GetComponent<TasksManager>().coffeehandOn = false;
            }
        }
    }
}

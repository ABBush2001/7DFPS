using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ComputerTask : MonoBehaviour
{
    public GameObject eventSystem;
    public TextMeshProUGUI dialogueText;

    bool in_area = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player" && eventSystem.GetComponent<TasksManager>().bools["approving_papers"] == false && eventSystem.GetComponent<TasksManager>().coffeehandOn == false)
        {
            dialogueText.text = "Press E to file approved papers";
            in_area = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            dialogueText.text = "";
            in_area = false;
        }
    }

    private void Update()
    {
        if(in_area)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                dialogueText.text = "";
                eventSystem.GetComponent<TasksManager>().bools["approving_papers"] = true;
                eventSystem.GetComponent<TasksManager>().bools["cabs_filed"] = false;
                

                eventSystem.GetComponent<TaskExpireTracker>().updateTaskTime("cabs_filed", 31);
                eventSystem.GetComponent<TasksManager>().taskUpdate(1);
            }
        }
    }
}

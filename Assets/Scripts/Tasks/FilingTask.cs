using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FilingTask : MonoBehaviour
{
    public GameObject eventSystem;
    public TextMeshProUGUI dialogueText;

    bool in_area = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player" && eventSystem.GetComponent<TasksManager>().bools["cabs_filed"] == false)
        {
            dialogueText.text = "Press E to file papers";
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

                //reset timer and text for approve papers
                eventSystem.GetComponent<TasksManager>().timeOverallPaper = Time.realtimeSinceStartup;
                eventSystem.GetComponent<TasksManager>().paperActivationTime = Random.Range(20, 31);
                eventSystem.GetComponent<TasksManager>().texts["approving_papers"] = "Approve paperwork at your desk";

                //reset text
                eventSystem.GetComponent<TasksManager>().texts["cabs_filed"] = "File approved papers";

                eventSystem.GetComponent<TasksManager>().bools["cabs_filed"] = true;
                eventSystem.GetComponent<TasksManager>().taskUpdate(2);
            }
        }
    }
}

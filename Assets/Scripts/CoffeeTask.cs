using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoffeeTask : MonoBehaviour
{
    public GameObject eventSystem;
    public GameObject player;
    public TextMeshProUGUI dialogueText;
    public Image loadingBar;
    public Image fillBar;

    public AudioSource coffeeSound;

    bool in_area = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player" && eventSystem.GetComponent<TasksManager>().bools["got_coffee"] == false && eventSystem.GetComponent<TasksManager>().paperhandOn == false)
        {
            dialogueText.text = "Press E to make coffee";
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
                coffeeSound.Play();
                eventSystem.GetComponent<TasksManager>().bools["got_coffee"] = true;
                eventSystem.GetComponent<TasksManager>().bools["bring_to_boss"] = false;
                eventSystem.GetComponent<TaskExpireTracker>().updateTaskTime("bring_to_boss", 41);

               

                eventSystem.GetComponent<TasksManager>().taskUpdate(5);

                //fill loading bar
                bar();
            }
        }
    }

    void bar()
    {
        Debug.Log("bar called");

        StartCoroutine(UpdateBar());

    }

    IEnumerator UpdateBar()
    {
        var tempColor = loadingBar.color;
        tempColor.a = 255f;
        loadingBar.color = tempColor;
        player.GetComponent<PlayerController>().theRB.velocity = new Vector3(0, 0, 0);
        player.GetComponent<PlayerController>().enabled = false;

        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(1);
            fillBar.fillAmount = fillBar.fillAmount + 0.1f;
        }

        fillBar.fillAmount = 0;
        tempColor = loadingBar.color;
        tempColor.a = 0f;
        loadingBar.color = tempColor;
        player.GetComponent<PlayerController>().enabled = true;

        GameObject.Find("CoffeeCup").GetComponent<Image>().enabled = true;
        eventSystem.GetComponent<TasksManager>().coffeehandOn = true;
    }
}

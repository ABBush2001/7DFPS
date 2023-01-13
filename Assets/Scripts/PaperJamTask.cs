using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PaperJamTask : MonoBehaviour
{
    public GameObject eventSystem;
    public GameObject player;
    public TextMeshProUGUI dialogueText;
    public Image loadingBar;
    public Image fillBar;

    public AudioSource printerSound;

    bool in_area = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player" && eventSystem.GetComponent<TasksManager>().bools["paper_jam"] == false)
        {
            dialogueText.text = "Press E to fix the paper jam";
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

                printerSound.Play();

                eventSystem.GetComponent<TasksManager>().bools["paper_jam"] = true;

                //reset timer and text
                eventSystem.GetComponent<TasksManager>().timeOverallJam = Time.realtimeSinceStartup;
                eventSystem.GetComponent<TasksManager>().jamActivationTime = Random.Range(20, 31);
                eventSystem.GetComponent<TasksManager>().texts["paper_jam"] = "Fix paper jam";

                eventSystem.GetComponent<TasksManager>().taskUpdate(4);

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

    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhoneTask : MonoBehaviour
{
    public GameObject eventSystem;
    public GameObject player;
    public TextMeshProUGUI dialogueText;
    public Image loadingBar;
    public Image fillBar;

    bool in_area = false;

    public AudioSource talkSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player" && eventSystem.GetComponent<TasksManager>().bools["answered_call"] == false)
        {
            dialogueText.text = "Press E to answer the phone";
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
                eventSystem.GetComponent<TasksManager>().phoneSound.Stop();
                talkSound.Play();
                eventSystem.GetComponent<TasksManager>().bools["answered_call"] = true;

                //reset timer and text
                eventSystem.GetComponent<TasksManager>().timeOverallPhone = Time.realtimeSinceStartup;
                eventSystem.GetComponent<TasksManager>().phoneActivationTime = Random.Range(60, 121);
                eventSystem.GetComponent<TasksManager>().texts["answered_call"] = "Answer the phone call!";

                eventSystem.GetComponent<TasksManager>().taskUpdate(3);

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
        talkSound.Stop();
    }

}

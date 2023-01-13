using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject tutorial;
    public GameObject startbutton;
    public GameObject tutButton;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None; Cursor.visible = true;
    }

    public void showTutorial()
    {
        tutorial.SetActive(true);
        startbutton.SetActive(false);
        tutButton.SetActive(false);
    }

    public void closeTutorial()
    {
        tutorial.SetActive(false);
        startbutton?.SetActive(true);
        tutButton.SetActive(true);
    }

    public void startGame()
    {
        StartCoroutine(Begin());
    }

    IEnumerator Begin()
    {
        GameObject.Find("Main Camera").GetComponent<CameraFadeOut>().fadeOut = true;
        yield return new WaitForSeconds(5);
        GameObject.Find("EventSystem").GetComponent<LoadNextScene>().LoadScene(1);
    }
}

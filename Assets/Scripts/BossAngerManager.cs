using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossAngerManager : MonoBehaviour
{
    public GameObject eventSystem;

    public int boss_anger = 0;      //once boss anger hits 100, game over

    public Sprite happyBoss;
    public Sprite annoyedBoss;
    public Sprite upsetBoss;
    public Sprite angryBoss;

    public GameObject BossFace;
    public GameObject GameOver;

    public AudioSource gameOver;

    bool complete = false;

    private void Update()
    {
        //happy boss
        if(boss_anger < 25)
        {
            BossFace.GetComponent<Image>().sprite = happyBoss;
        }
        //mildly annoyed
        else if(boss_anger >= 25 && boss_anger <= 50)
        {
            BossFace.GetComponent<Image>().sprite = annoyedBoss;
        }
        //upset
        else if(boss_anger > 50 && boss_anger <= 75)
        {
            BossFace.GetComponent<Image>().sprite = upsetBoss;
        }
        //mad
        else if(boss_anger > 75 && boss_anger < 100)
        {
            BossFace.GetComponent<Image>().sprite = angryBoss;
        }
        //game over
        else
        {
            if (complete == false)
            {
                GameObject.Find("Player").GetComponent<PlayerController>().theRB.velocity = new Vector3(0, 0, 0);
                GameObject.Find("Player").GetComponent<PlayerController>().enabled = false;
                StartCoroutine(EndGame());
                complete = true;
            }
        }
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(3);
        GameOver.GetComponent<Image>().enabled = true;
        GameOver.GetComponent<Animator>().enabled = true;
        gameOver.Play();
        yield return new WaitForSeconds(1);
        GameObject.Find("Main Camera").GetComponent<CameraFadeOut>().fadeOut = true;
        yield return new WaitForSeconds(5);
        eventSystem.GetComponent<LoadNextScene>().LoadScene(2);
    }
}

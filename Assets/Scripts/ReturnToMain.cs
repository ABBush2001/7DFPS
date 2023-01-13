using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToMain : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        yield return new WaitForSeconds(5);
        GameObject.Find("Main Camera").GetComponent<CameraFadeOut>().fadeOut = true;
        yield return new WaitForSeconds(5);
        GameObject.Find("EventSystem").GetComponent<LoadNextScene>().LoadScene(0);
    }
}

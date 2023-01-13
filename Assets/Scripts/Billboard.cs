using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private SpriteRenderer theSR;

    // Start is called before the first frame update
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(PlayerController.instance.transform.position.x, PlayerController.instance.transform.position.y, transform.position.z), -Vector3.forward);
    }
}

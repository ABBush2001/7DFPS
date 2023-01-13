using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardNoFollow : MonoBehaviour
{
    private SpriteRenderer theSR;

    // Start is called before the first frame update
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
        transform.LookAt(PlayerController.instance.transform.position, -Vector3.forward);
    }
}

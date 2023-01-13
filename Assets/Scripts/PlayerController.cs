using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public Rigidbody2D theRB;

    public float moveSpeed;

    private Vector2 moveInput;
    private Vector2 mouseInput;

    public float mouseSensitivity = 1f;

    public Transform viewCam;

    private void Awake()
    {
        instance = this;
        Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Player Movement
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        Vector3 moveHorizontal = transform.up * -moveInput.x;
        Vector3 moveVertical = transform.right * moveInput.y;

        theRB.velocity = (moveHorizontal + moveVertical) * moveSpeed;

        //Player Mouse
        mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - mouseInput.x);

        viewCam.localRotation = Quaternion.Euler(viewCam.localRotation.eulerAngles + new Vector3(0f, mouseInput.y, 0f));
    }
}

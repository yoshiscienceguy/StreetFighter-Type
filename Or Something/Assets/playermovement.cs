using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    private bool ready = true;
    public Animator anim;
    CharacterController controller;
    public float h = 0;
    bool canJump = true;
    bool locked = false;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        h = Input.GetAxis("Horizontal");

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(h, 0, 0);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump") && canJump)
            {
                StartCoroutine(jumpWait());
            }


        }
        else
        {

            float oldY = moveDirection.y;
            moveDirection = new Vector3(h, 0, 0);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            moveDirection.y = oldY;

        }
        if (locked)
        {
            moveDirection = Vector3.zero;
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        anim.SetFloat("Speed", Mathf.Abs(moveDirection.x));

        if (Input.GetMouseButton(0) && ready)
        {
            StartCoroutine(animweight());
        }
    }
    IEnumerator jumpWait()
    {
        locked = true;
        canJump = false;
        anim.SetTrigger("jump");
        yield return new WaitForSeconds(.5f);
        locked = false;
        moveDirection = new Vector3(h, 0, 0);
        moveDirection.y = jumpSpeed;
        controller.Move(moveDirection * Time.deltaTime);
        yield return new WaitForSeconds(1f);
        canJump = true;
    }

    IEnumerator animweight()
    {
        locked = true;
        anim.SetTrigger("punch");
        ready = false;
        yield return new WaitForSeconds(1.8f);
        ready = true;
        locked = false;
    }
}

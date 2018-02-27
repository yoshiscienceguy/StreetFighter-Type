using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerrotate : MonoBehaviour {
    public float rotationspeed = .5f;
    private playermovement pm;
    Vector3 faceForward;
    Vector3 faceBackward;
    private Animator anim;
	// Use this for initialization
	void Start () {
        pm = GetComponentInParent<playermovement>();
        faceForward = new Vector3(0, 120f, 0 );
        faceBackward = new Vector3(0, -120f , 0);
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        float h = pm.h;
        if (h > 0) {
            if (Quaternion.Angle(transform.rotation, Quaternion.Euler(faceForward)) > 1){
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(faceForward), rotationspeed * Time.deltaTime);
                anim.SetBool("Mirror", false);
            }

        }
        if (h < 0)
        {

            if (Quaternion.Angle(transform.rotation, Quaternion.Euler(faceBackward)) > 1)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(faceBackward), rotationspeed * Time.deltaTime);
                anim.SetBool("Mirror", true);
            }
        }
    }
}

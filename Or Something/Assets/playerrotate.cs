using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerrotate : MonoBehaviour {
    public float rotationspeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        if (h > 0) {
            transform.rotation = Quaternion.Euler(0, 0, 102.547f);
        }
	}
}

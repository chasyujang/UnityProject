﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            /*Rigidybody ballRigid;
            ballRigid = gameObject.GetComponent<Rigidbody > ();
            ballRigid.AddForce(Vector3.up * 300);
            */
            GetComponent<Rigidbody>().AddForce(Vector3.up * 300);
        }
    }
}

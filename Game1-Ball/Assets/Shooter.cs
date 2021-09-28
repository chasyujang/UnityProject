using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    float move = 0.05f;
    float timeCount = 0;
    public GameObject Stone;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        timeCount += Time.deltaTime;

        if (timeCount > 3)
        {
            Instantiate(Stone, transform.position,Quaternion.identity);
            timeCount = 0;
        }
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + move);

        if (transform.localPosition.z < -4.5)
        {
            move = -move;
        }

        if (transform.localPosition.z > 4.5)
        {
            move = -move;
        }

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Ball")
        {
            GameObject.Find("GameManager").SendMessage("Restart");
        }
    }
}

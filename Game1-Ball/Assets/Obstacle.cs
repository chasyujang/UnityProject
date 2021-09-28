using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float move = 0.05f;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            Vector3 direction = transform.position - collision.gameObject.transform.position;
            direction = direction.normalized * 1000;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(direction);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //float NextPositionz = transform.position.z + move;
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
}

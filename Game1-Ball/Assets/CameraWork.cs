using System.Collections;
using UnityEngine;

public class CameraWork : MonoBehaviour
{

    GameObject ball;

    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.Find("Ball");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(ball.transform.position.x+20, ball.transform.position.y+15, ball.transform.position.z);
    }
}

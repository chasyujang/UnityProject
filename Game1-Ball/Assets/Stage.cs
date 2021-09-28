using System.Collections;
using UnityEngine;

public class Stage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*float zRotation = transform.localEulerAngles.z;
        zRotation = zRotation - Input.GetAxis("Horizontal");*/
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x + Input.GetAxis("Horizontal"), transform.localEulerAngles.y, transform.localEulerAngles.z); // 좌우
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x , transform.localEulerAngles.y, transform.localEulerAngles.z + Input.GetAxis("Vertical")); // 상하

        if (Input.touchCount > 0 || Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x < Screen.width / 2)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x - 1, transform.localEulerAngles.y, transform.localEulerAngles.z);
            }
            if (Input.mousePosition.x > Screen.width / 2)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x + 1, transform.localEulerAngles.y, transform.localEulerAngles.z);
            }
        }
        
    }
}

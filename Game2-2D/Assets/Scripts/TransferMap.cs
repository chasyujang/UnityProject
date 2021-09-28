using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{

    public string transferMapName;

    private MovingObject thePlayer;//MovingObject의 CurrentMapName변수를 참조

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<MovingObject>(); // FindObjectOfType : 하이어라키에 있는 모든 객체의 컴포넌트 검색해서 리턴 ( GetComponent는 스크립트 적용 객체만)
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            thePlayer.currentMapName = transferMapName;
            SceneManager.LoadScene(transferMapName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

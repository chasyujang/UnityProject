using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int coinCount = 0;
    public Text coinText;

    void GetCoin()
    {
        coinCount++;
        coinText.text = "Coin : " + coinCount;
    }

    void Restart()
    {
        Application.LoadLevel("Game1-Ball");
    }

    void DestroyObstacle()
    {
        GameObject[] Obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        for (int i = 0; i < Obstacles.Length; i++)
        {
            Destroy(Obstacles[i]);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

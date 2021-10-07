using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float posY = 0;

    private GameObject[] players;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
    }

    public void MoveCamera()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            if(players[i].transform.position.y <= posY)
            {
                posY = players[i].transform.position.y;
                Vector3 cameraPosition = new Vector3(transform.position.x, Mathf.SmoothStep(transform.position.y, posY, 0.3f));
                transform.position = cameraPosition + Vector3.forward * -10;
            }
        }
    }
}

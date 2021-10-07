using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public int bombLevel;
    public Sprite bomb;

    public SpriteRenderer render;
    public GameObject[] visualFeedback;

    private float timer;
    public float timeExplosion;

    public GameObject[] players;

    public GameObject[] blocks;

    public GameObject[] chests;

    public GameObject[] itens;

    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        players = GameObject.FindGameObjectsWithTag("Player");
        blocks = GameObject.FindGameObjectsWithTag("Blocks");
        render.sprite = bomb;
        Visual();
    }
   
    void Update()
    {
        Explosion();
    }

    private void Explosion()
    {
        timer += (1 * Time.deltaTime);
        if(timer >= timeExplosion) {
            Damage();            
            Destroy(gameObject);
        }
    }

    private void Damage()
    {
        //Deal Damage on players
        for(int i = 0; i < players.Length; i++)
        {
            if (players[i].transform.position.y == transform.position.y)
            {
                if (players[i].transform.position.x >= (transform.position.x - bombLevel))
                {
                    if (players[i].transform.position.x <= (transform.position.x + bombLevel))
                    {
                        players[i].GetComponent<Player>().life -= 1;                        
                    }
                }                
            }            
            else if(players[i].transform.position.x == transform.position.x)
            {
                if (players[i].transform.position.y <= (transform.position.y + bombLevel))
                {
                    if (players[i].transform.position.y >= (transform.position.y - bombLevel))
                    {
                        players[i].GetComponent<Player>().life -= 1;
                    }
                }
            }
        }
        blocks = GameObject.FindGameObjectsWithTag("Blocks");
        //Destroy Blocks
        for (int i = 0; i < blocks.Length; i++)
        {
            if (blocks[i].transform.position.y == transform.position.y)
            {
                if (blocks[i].transform.position.x >= (transform.position.x - bombLevel))
                {
                    if (blocks[i].transform.position.x <= (transform.position.x + bombLevel))
                    {
                        Destroy(blocks[i]);
                    }
                }
            }
            if(blocks[i].transform.position.x == transform.position.x)
            {
                if(blocks[i].transform.position.y <= (transform.position.y + bombLevel))
                {
                    if(blocks[i].transform.position.y >= (transform.position.y - bombLevel))
                    {                        
                        Destroy(blocks[i]);
                    }
                }
            }
        }
        //Destroy Chest
        chests = GameObject.FindGameObjectsWithTag("Chest");
        for (int i = 0; i < chests.Length; i++)
        {
            if (chests[i].transform.position.y == transform.position.y)
            {
                if (chests[i].transform.position.x >= (transform.position.x - bombLevel))
                {
                    if (chests[i].transform.position.x <= (transform.position.x + bombLevel))
                    {                        
                        Destroy(chests[i]);
                    }
                }
            }
            if (chests[i].transform.position.x == transform.position.x)
            {
                if (chests[i].transform.position.y <= (transform.position.y + bombLevel))
                {
                    if (chests[i].transform.position.y >= (transform.position.y - bombLevel))
                    {                        
                        Destroy(chests[i]);
                    }
                }
            }
        }



    }

    private void Visual()
    {
        //Set Range feedback
        for(int i = 1; i <= visualFeedback.Length; i++)
        {
            if(i == bombLevel)
            {
                visualFeedback[i - 1].SetActive(true);
            }
            else
            {
                visualFeedback[i - 1].SetActive(false);
            }
        }
    }
}

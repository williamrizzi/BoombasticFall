using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject gameControl;

    public bool playerOne;
    public bool playerTwo;

    public Sprite p1Blue;
    public Sprite p2Red;
        
    public int bombCount = 0;

    public int life = 3;
    public int bombLevel;

    public float stunTime = 2.0f;

    public GameObject[] players;

    public GameObject[] blocks;
    public GameObject[] irons;
    public GameObject[] chests;
    public GameObject[] bombs;

    private SpriteRenderer render;

    public bool blockMove = true;
    private GameObject alvo;

    private int modNegative = -1;
    private int modPositive = 1;

    IEnumerator StunCoroutine()
    {
        alvo.GetComponent<Player>().blockMove = true;
        alvo.GetComponent<SpriteRenderer>().flipY = true;
        yield return new WaitForSeconds(3.0f);
        alvo.GetComponent<Player>().blockMove = false;
        alvo.GetComponent<SpriteRenderer>().flipY = false;
    }

    IEnumerator TwistCoroutine()
    {
        alvo.GetComponent<Player>().modNegative = 1;
        alvo.GetComponent<Player>().modPositive = -1;
        yield return new WaitForSeconds(4.0f);
        alvo.GetComponent<Player>().modNegative = -1;
        alvo.GetComponent<Player>().modPositive = 1;
    }

    IEnumerator ResetBomb()
    {
        yield return new WaitForSeconds(2.0f);
        bombCount += 1;
    }


    // Start is called before the first frame update
    void Start()
    {
        gameControl = GameObject.FindGameObjectWithTag("GameController");

        blockMove = true;
        bombCount = 1;
        render = GetComponent<SpriteRenderer>();
        bombLevel = 1;
        if (playerOne)
        {
            render.sprite = p1Blue;   
        }
        else if (playerTwo)
        {
            render.sprite = p2Red;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Bomb();
        CheckLife();
    }

    //Player Movement
    private void Move()
    {
        if (playerOne)
        {            
            if (blockMove == false)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    blocks = GameObject.FindGameObjectsWithTag("Blocks");
                    Vector3 tempVec = new Vector3(transform.position.x + modNegative, transform.position.y, transform.position.z);
                    bool tempWalk = false;
                    for (int i = 0; i < blocks.Length; i++)
                    {
                        if (blocks[i].transform.position == tempVec)
                        {
                            tempWalk = true;
                        }
                    }
                    irons = GameObject.FindGameObjectsWithTag("Iron");
                    for (int i = 0; i < irons.Length; i++)
                    {
                        if (irons[i].transform.position == tempVec)
                        {
                            tempWalk = true;
                        }
                    }
                    chests = GameObject.FindGameObjectsWithTag("Chest");
                    for (int i = 0; i < chests.Length; i++)
                    {
                        if (chests[i].transform.position == tempVec)
                        {
                            tempWalk = true;
                        }
                    }
                    if (tempWalk == false)
                    {
                        render.flipX = true;
                        transform.position = new Vector3(transform.position.x + modNegative, transform.position.y, transform.position.z);
                        if (transform.position.x < 0.5f)
                        {
                            Vector3 temp = new Vector3(0.5f, transform.position.y, transform.position.z);
                            transform.position = temp;
                        }
                    }
                }

                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    blocks = GameObject.FindGameObjectsWithTag("Blocks");
                    Vector3 tempVec = new Vector3(transform.position.x + modPositive, transform.position.y, transform.position.z);
                    bool tempWalk = false;
                    for (int i = 0; i < blocks.Length; i++)
                    {
                        if (blocks[i].transform.position == tempVec)
                        {
                            tempWalk = true;
                        }
                    }
                    irons = GameObject.FindGameObjectsWithTag("Iron");
                    for (int i = 0; i < irons.Length; i++)
                    {
                        if (irons[i].transform.position == tempVec)
                        {
                            tempWalk = true;
                        }
                    }
                    chests = GameObject.FindGameObjectsWithTag("Chest");
                    for (int i = 0; i < chests.Length; i++)
                    {
                        if (chests[i].transform.position == tempVec)
                        {
                            tempWalk = true;
                        }
                    }
                    if (tempWalk == false)
                    {
                        render.flipX = false;
                        transform.position = new Vector3(transform.position.x + modPositive, transform.position.y, transform.position.z);
                        if (transform.position.x > 8.5f)
                        {
                            Vector3 temp = new Vector3(8.5f, transform.position.y, transform.position.z);
                            transform.position = temp;
                        }
                    }
                }
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    blocks = GameObject.FindGameObjectsWithTag("Blocks");
                    Vector3 tempVec = new Vector3(transform.position.x, transform.position.y + modPositive, transform.position.z);
                    bool tempWalk = false;
                    for (int i = 0; i < blocks.Length; i++)
                    {
                        if (blocks[i].transform.position == tempVec)
                        {
                            tempWalk = true;
                        }
                    }
                    irons = GameObject.FindGameObjectsWithTag("Iron");
                    for (int i = 0; i < irons.Length; i++)
                    {
                        if (irons[i].transform.position == tempVec)
                        {
                            tempWalk = true;
                        }
                    }
                    chests = GameObject.FindGameObjectsWithTag("Chest");
                    for (int i = 0; i < chests.Length; i++)
                    {
                        if (chests[i].transform.position == tempVec)
                        {
                            tempWalk = true;
                        }
                    }
                    if(tempVec.y >= 1)
                    {
                        tempWalk = true;
                    }
                    if (tempWalk == false)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y + modPositive, transform.position.z);
                    }

                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    blocks = GameObject.FindGameObjectsWithTag("Blocks");
                    Vector3 tempVec = new Vector3(transform.position.x, transform.position.y + modNegative, transform.position.z);
                    bool tempWalk = false;
                    for (int i = 0; i < blocks.Length; i++)
                    {
                        if (blocks[i].transform.position == tempVec)
                        {
                            tempWalk = true;
                        }
                    }
                    irons = GameObject.FindGameObjectsWithTag("Iron");
                    for (int i = 0; i < irons.Length; i++)
                    {
                        if (irons[i].transform.position == tempVec)
                        {
                            tempWalk = true;
                        }
                    }
                    chests = GameObject.FindGameObjectsWithTag("Chest");
                    for (int i = 0; i < chests.Length; i++)
                    {
                        if (chests[i].transform.position == tempVec)
                        {
                            tempWalk = true;
                        }
                    }
                    if (tempWalk == false)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y + modNegative, transform.position.z);
                    }
                }
            }
        }
        if (playerTwo)
        {
            if (blockMove == false)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    blocks = GameObject.FindGameObjectsWithTag("Blocks");
                    Vector3 tempVec = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                    bool tempWalk = false;
                    for (int i = 0; i < blocks.Length; i++)
                    {
                        if (blocks[i].transform.position == tempVec)
                        {
                            tempWalk = true;
                        }
                    }
                    irons = GameObject.FindGameObjectsWithTag("Iron");
                    for (int i = 0; i < irons.Length; i++)
                    {
                        if (irons[i].transform.position == tempVec)
                        {
                            tempWalk = true;
                        }
                    }
                    chests = GameObject.FindGameObjectsWithTag("Chest");
                    for (int i = 0; i < chests.Length; i++)
                    {
                        if (chests[i].transform.position == tempVec)
                        {
                            tempWalk = true;
                        }
                    }
                    if (tempWalk == false)
                    {
                        render.flipX = true;
                        transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                        if (transform.position.x < -8.5f)
                        {
                            Vector3 temp = new Vector3(-8.5f, transform.position.y, transform.position.z);
                            transform.position = temp;
                        }
                    }
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    blocks = GameObject.FindGameObjectsWithTag("Blocks");
                    Vector3 tempVec = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                    bool tempWalk = false;
                    for (int i = 0; i < blocks.Length; i++)
                    {
                        if (blocks[i].transform.position == tempVec)
                        {
                            tempWalk = true;
                        }
                    }
                    irons = GameObject.FindGameObjectsWithTag("Iron");
                    for (int i = 0; i < irons.Length; i++)
                    {
                        if (irons[i].transform.position == tempVec)
                        {
                            tempWalk = true;
                        }
                    }
                    chests = GameObject.FindGameObjectsWithTag("Chest");
                    for (int i = 0; i < chests.Length; i++)
                    {
                        if (chests[i].transform.position == tempVec)
                        {
                            tempWalk = true;
                        }
                    }
                    if (tempWalk == false)
                    {
                        render.flipX = false;
                        transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                        if (transform.position.x > -0.5f)
                        {
                            Vector3 temp = new Vector3(-0.5f, transform.position.y, transform.position.z);
                            transform.position = temp;
                        }
                    }
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    blocks = GameObject.FindGameObjectsWithTag("Blocks");
                    Vector3 tempVec = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                    bool tempWalk = false;
                    for (int i = 0; i < blocks.Length; i++)
                    {
                        if (blocks[i].transform.position == tempVec)
                        {
                            tempWalk = true;
                        }
                    }
                    irons = GameObject.FindGameObjectsWithTag("Iron");
                    for (int i = 0; i < irons.Length; i++)
                    {
                        if (irons[i].transform.position == tempVec)
                        {
                            tempWalk = true;
                        }
                    }
                    chests = GameObject.FindGameObjectsWithTag("Chest");
                    for (int i = 0; i < chests.Length; i++)
                    {
                        if (chests[i].transform.position == tempVec)
                        {
                            tempWalk = true;
                        }
                    }
                    if (tempVec.y >= 1)
                    {
                        tempWalk = true;
                    }
                    if (tempWalk == false)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                    }
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    blocks = GameObject.FindGameObjectsWithTag("Blocks");
                    Vector3 tempVec = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
                    bool tempWalk = false;
                    for (int i = 0; i < blocks.Length; i++)
                    {
                        if (blocks[i].transform.position == tempVec)
                        {
                            tempWalk = true;
                        }
                    }
                    irons = GameObject.FindGameObjectsWithTag("Iron");
                    for (int i = 0; i < irons.Length; i++)
                    {
                        if (irons[i].transform.position == tempVec)
                        {
                            tempWalk = true;
                        }
                    }
                    chests = GameObject.FindGameObjectsWithTag("Chest");
                    for (int i = 0; i < chests.Length; i++)
                    {
                        if (chests[i].transform.position == tempVec)
                        {
                            tempWalk = true;
                        }
                    }
                    if (tempWalk == false)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
                    }
                }
            }
        }
    }

    //Generate Bombs
    private void Bomb()
    {
        if (playerOne)
        {
            if (blockMove == false)
            {
                if (Input.GetKeyDown(KeyCode.Keypad0))
                {
                    if (bombCount > 0)
                    {
                        
                        switch (bombLevel)
                        {
                            case 1:
                                Instantiate(bombs[0], transform.position, transform.rotation);
                                break;
                            case 2:
                                Instantiate(bombs[1], transform.position, transform.rotation);
                                break;
                            case 3:
                                Instantiate(bombs[2], transform.position, transform.rotation);
                                break;
                        }
                        bombCount -= 1;
                        StartCoroutine("ResetBomb");
                    }
                }
            }
        }
        if (playerTwo)
        {
            if (blockMove == false)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (bombCount > 0)
                    {
                        switch (bombLevel)
                        {
                            case 1:
                                Instantiate(bombs[0], transform.position, transform.rotation);
                                break;
                            case 2:
                                Instantiate(bombs[1], transform.position, transform.rotation);
                                break;
                            case 3:
                                Instantiate(bombs[2], transform.position, transform.rotation);
                                break;
                        }
                        bombCount -= 1;
                        StartCoroutine("ResetBomb");
                    }
                }
            }
        }
    }
    //Apply buff and nerfs
    private void OnTriggerEnter2D(Collider2D collision)
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        if(collision.transform.tag == "Power")
        {
            //Upgrade bomb strength
            if(bombLevel < 3)
            {
                bombLevel += 1;
            }            
            Destroy(collision.gameObject);            
        }
        if (collision.transform.tag == "Stun")
        {
            //Stun enemy
            for(int i = 0; i < players.Length; i++)
            {
                if(players[i].GetComponent<Player>().playerOne != playerOne)
                {
                    alvo = players[i];
                    Stun();
                }
            }
            Destroy(collision.gameObject);
        }
        if (collision.transform.tag == "Twist")
        {
            //Inverts commands
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].GetComponent<Player>().playerOne != playerOne)
                {
                    alvo = players[i];
                    Twist();
                }
            }
            Destroy(collision.gameObject);
        }

        if(collision.transform.tag == "WinTrigger")
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].GetComponent<Player>().playerOne != playerOne)
                {
                    alvo = players[i];
                    blockMove = true;
                    alvo.GetComponent<Player>().blockMove = true;
                    CallWinner(transform.gameObject);
                }
            }
        }

        if (collision.transform.tag == "LoseTrigger")
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].GetComponent<Player>().playerOne != playerOne)
                {
                    alvo = players[i];
                    blockMove = true;
                    alvo.GetComponent<Player>().blockMove = true;
                    CallLoser(transform.gameObject);
                }
            }
        }
    }

    private void CheckLife()
    {
        if(life <= 0)
        {   
            players = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].GetComponent<Player>().playerOne != playerOne)
                {
                    alvo = players[i];
                    blockMove = true;
                    alvo.GetComponent<Player>().blockMove = true;
                    CallLoser(transform.gameObject);
                    Destroy(transform.gameObject);
                }
            }
        }
    }


    public void CallWinner(GameObject winner)
    {
        gameControl.GetComponent<GameController>().RoundWinner(transform.gameObject);
    }
    public void CallLoser(GameObject loser)
    {
        gameControl.GetComponent<GameController>().RoundLoser(transform.gameObject);
    }


    void Stun()
    {
        StartCoroutine("StunCoroutine");
    }

    void Twist()
    {
        StartCoroutine("TwistCoroutine");
    }

}

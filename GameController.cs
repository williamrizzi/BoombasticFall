using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // player spawn esquerdo: -4.5 , 0
    // player spawn direito:   4.5 , 0

    public int round;

    public int scoreLeft;
    public int scoreRight;

    public GameObject cameraObj;

    public GameObject text;
    public GameObject textGo;
    public GameObject textLeft;
    public GameObject textRight;

    public GameObject[] blocks;

    public GameObject gridProp;

    public GameObject[] chests;
    public GameObject[] itens;
    public GameObject[] players;

    public GameObject wallDiv;

    public int rand;

    public GameObject scenario;

    public int ironDiv;

    private int randX;
    private int randY;

    IEnumerator StartRound()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Player");        
        switch (round)
        {
            case 1:
                text.GetComponent<Text>().text = "Round 1";
                break;
            case 2:
                text.GetComponent<Text>().text = "Round 2";
                break;
            case 3:
                text.GetComponent<Text>().text = "Round 3";
                break;
        }
        
        
        text.GetComponent<Animator>().Play("RoundTextAnimation");        
        yield return new WaitForSeconds(2.5f);
        textGo.SetActive(true);
        temp[0].GetComponent<Player>().blockMove = false;        
        temp[1].GetComponent<Player>().blockMove = false;
        yield return new WaitForSeconds(1.0f);
        textGo.SetActive(false);
    }

    IEnumerator NextRoundCoroutine()
    {
        yield return new WaitForSeconds(2.0f);
        textRight.GetComponent<Text>().text = "";
        textLeft.GetComponent<Text>().text = "";
        DestroyAll();
        yield return new WaitForSeconds(0.1f);
        cameraObj.transform.position = new Vector3(0, 0, -10);
        cameraObj.GetComponent<CameraFollow>().posY = 0;
        DrawPlayers();        
        DrawScenario(); 
        DrawMineBack();        
        GenerateMap();
        DrawGrid();        
        SpawnPowerUps();
        DrawWallDiv();        
        EndOfMap();
        yield return new WaitForSeconds(0.1f);
        switch (round)
        {
            case 1:
                text.GetComponent<Text>().text = "Round 1";
                break;
            case 2:
                text.GetComponent<Text>().text = "Round 2";
                break;
            case 3:
                text.GetComponent<Text>().text = "Round 3";
                break;
        }       
        text.GetComponent<Animator>().Play("RoundTextAnimation");
        yield return new WaitForSeconds(2.5f);
        textGo.SetActive(true);
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Player");
        temp[0].GetComponent<Player>().blockMove = false;        
        temp[1].GetComponent<Player>().blockMove = false;
        yield return new WaitForSeconds(1.0f);
        textGo.SetActive(false);
    }

    IEnumerator ResetTheGame()
    {
        yield return new WaitForSeconds(4.0f);
        SceneManager.LoadScene("Game");
    }


    private Vector3 textRoundReset;


    // Start is called before the first frame update
    void Start()
    {
        textRoundReset = text.transform.position;

        round = 1;

        scoreLeft = 0;
        scoreRight = 0;

        FirstRound();
        StartCoroutine("StartRound");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            DestroyAll();
        }        
    }

    private void UnblockPlayer()
    {
        players[0].GetComponent<Player>().blockMove = false;
        players[1].GetComponent<Player>().blockMove = false;
    }

    private void DrawScenario()
    {
        Vector3 tempVec = new Vector3(0, 0, 0);
        Instantiate(scenario, tempVec, transform.rotation);
    }

    private void DrawMineBack()
    {
        for (float i = 0.5f; i <= 8.5; i += 1)
        {
            for (int j = -1; j > -65; j -= 1)
            {
                Vector3 temp = new Vector3(i, j, 0);
                Instantiate(blocks[6], temp, transform.rotation);
            }
        }
        for (float i = -0.5f; i >= -8.5; i -= 1)
        {
            for (int j = -1; j > -65; j -= 1)
            {
                Vector3 temp = new Vector3(i, j, 0);
                Instantiate(blocks[6], temp, transform.rotation);
            }
        }
    }

    private void DrawGrid()
    {
        for (float i = 0.5f; i <= 8.5; i += 1)
        {
            for (int j = -1; j > -70; j -= 1)
            {
                // random pro block
                Vector3 temp = new Vector3(i, j, 0);
                Instantiate(gridProp, temp, transform.rotation);
            }
        }

        for (float i = -0.5f; i >= -8.5; i -= 1)
        {
            for (int j = -1; j > -70; j -= 1)
            {
                // random pro block
                Vector3 temp = new Vector3(i, j, 0);
                Instantiate(gridProp, temp, transform.rotation);
            }
        }
    }

    private void DestroyGrid()
    {
        GameObject[] grid = GameObject.FindGameObjectsWithTag("Grid");
        for(int i = 0; i < grid.Length; i++)
        {
            Destroy(grid[i]);
        }
    }

    private void GenerateMap()
    {
        //9 em x
        //32 em y

        //Direita
        for (int j = -1; j > -60; j -= 1)
        {
            int rand = Random.Range(0, 9);
            if (j % 10 == -1)
            {
                randX = Random.Range(0, 9);
                randY = Random.Range(-2, -10);                
            }
            for (float i = 0.5f; i <= 8.5; i+=1)
            {
                Vector3 temp = new Vector3(i, j, 0);
                if (j == -1)
                {
                    Instantiate(blocks[0], temp, transform.rotation);
                }
                else if (((i - 0.5) == randX) && (j % 10 == randY))
                {                    
                    Instantiate(blocks[5], temp, transform.rotation);
                }
                else if (j % ironDiv == 0)
                {
                    if (i == (0.5 + rand))
                    {
                        int tempRand = Random.Range(1, 5);
                        Instantiate(blocks[tempRand], temp, transform.rotation);
                    }
                    else
                    {
                        Instantiate(blocks[7], temp, transform.rotation);
                    }
                }
                else if (j != -1)
                {
                    int tempRand = Random.Range(1, 20);
                    switch (tempRand)
                    {
                        case 3:
                            Instantiate(blocks[2], temp, transform.rotation);
                            break;
                        case 6:
                            Instantiate(blocks[3], temp, transform.rotation);
                            break;
                        case 9:
                            Instantiate(blocks[4], temp, transform.rotation);
                            break;
                        default:
                            Instantiate(blocks[1], temp, transform.rotation);
                            break;
                    }
                }
            }
        }

        //Esquerda
        for (int j = -1; j > -60; j -= 1)
        {
            int rand = Random.Range(0, 9);
            if (j % 10 == -1)
            {
                randX = Random.Range(-0, -9);
                randY = Random.Range(-2, -10);                
            }
            for (float i = -0.5f; i >= -8.5; i -= 1)
            {
                Vector3 temp = new Vector3(i, j, 0);
                if (j == -1)
                {
                    Instantiate(blocks[0], temp, transform.rotation);
                }
                else if (((i + 0.5) == randX) && (j % 10 == randY))
                {
                    Instantiate(blocks[5], temp, transform.rotation);
                }
                else if (j%ironDiv == 0)
                {
                    if(i== (-0.5 - rand))                    {
                        int tempRand = Random.Range(1, 5);
                        Instantiate(blocks[tempRand], temp, transform.rotation);
                    }
                    else{                       
                        Instantiate(blocks[7], temp, transform.rotation);
                    }                 
                }
                else if(j != -1)
                {
                    int tempRand = Random.Range(1, 20);
                    switch (tempRand)
                    {
                        case 3:
                            Instantiate(blocks[2], temp, transform.rotation);
                            break;
                        case 6:
                            Instantiate(blocks[3], temp, transform.rotation);
                            break;
                        case 9:
                            Instantiate(blocks[4], temp, transform.rotation);
                            break;
                        default:
                            Instantiate(blocks[1], temp, transform.rotation);
                            break;
                    }                    
                }
            }
        }
    }
       
    private void SpawnPowerUps()
    {
        chests = GameObject.FindGameObjectsWithTag("Chest");

        int[] tempVecEsq = new int[3] { 2, 2, 2 };
        int[] tempVecDir = new int[3] { 2, 2, 2 };

        for (int i = 0; i < chests.Length; i++)
        {
            if(chests[i].transform.position.x >= -8.5)
            {
                if(chests[i].transform.position.x <= -0.5)
                {
                    do
                    {
                        rand = Random.Range(0, 3);
                    } while (tempVecEsq[rand] == 0);
                    Instantiate(itens[rand], chests[i].transform.position, chests[i].transform.rotation);
                    tempVecEsq[rand] = tempVecEsq[rand] - 1;
                }
            }
        }

        for (int i = 0; i < chests.Length; i++)
        {
            if (chests[i].transform.position.x <= 8.5)
            {
                if (chests[i].transform.position.x >= 0.5)
                {
                    do
                    {
                        rand = Random.Range(0, 12);
                        rand = rand % 3;
                    } while (tempVecDir[rand] == 0);                                      
                    Instantiate(itens[rand], chests[i].transform.position, chests[i].transform.rotation);
                    tempVecDir[rand] = tempVecDir[rand] - 1;
                }
            }
        }
    }

    private void DrawPlayers()
    {
        Vector3 tempVec = new Vector3(-4.5f, 0, 0);
        Instantiate(players[0], tempVec, transform.rotation);
        tempVec = new Vector3(4.5f, 0, 0);
        Instantiate(players[1], tempVec, transform.rotation);
    }

    private void DrawWallDiv()
    {
        for (int i = 5; i > -70; i -= 1)
        {
            Vector3 temp = new Vector3(0, (i - 0.5f), 0);
            Instantiate(wallDiv, temp, transform.rotation);
        }
    }

    public void RoundWinner(GameObject winner)
    {
        round += 1;
        if(winner.GetComponent<Player>().playerOne == true)
        {
            //direita
            textRight.GetComponent<Text>().text = "WINNER";
            scoreRight += 1;
            if(scoreRight >= 2)
            {
                textLeft.GetComponent<Text>().text = "You're the big WINNER.";
                StartCoroutine("ResetTheGame");
            }
            else
            {
                StartCoroutine("NextRoundCoroutine");
            }
        }
        else
        {
            //esquerda
            textLeft.GetComponent<Text>().text = "WINNER";
            scoreLeft += 1;
            if (scoreLeft >= 2)
            {
                textLeft.GetComponent<Text>().text = "You're the big WINNER.";
                StartCoroutine("ResetTheGame");
            }
            else
            {
                StartCoroutine("NextRoundCoroutine");
            }

        }
    }


    public void RoundLoser(GameObject loser)
    {
        round += 1;
        if (loser.GetComponent<Player>().playerOne == true)
        {
            //esquerda
            textLeft.GetComponent<Text>().text = "WINNER";
            scoreLeft += 1;
            if (scoreLeft >= 2)
            {
                //textLeft.GetComponent<Text>().text = "You're the big WINNER.";
                StartCoroutine("ResetTheGame");
            }
            else
            {
                StartCoroutine("NextRoundCoroutine");
            }
        }
        else
        {
            //direita
            textRight.GetComponent<Text>().text = "WINNER";
            scoreRight += 1;
            if (scoreRight >= 2)
            {
                //textLeft.GetComponent<Text>().text = "You're the big WINNER.";
                StartCoroutine("ResetTheGame");
            }
            else
            {
                StartCoroutine("NextRoundCoroutine");
            }
        }
    }



    private void FirstRound()
    {
        ironDiv = -10;
        DrawPlayers();
        DrawScenario();
        DrawMineBack();
        GenerateMap();
        DrawGrid();
        SpawnPowerUps();
        DrawWallDiv();
        EndOfMap();
    }

    private void EndOfMap()
    {
        for (int i = 1; i < 10; i++)
        {
            Vector3 temp = new Vector3((i - 0.5f), -65, 0);
            Instantiate(blocks[0], temp, transform.rotation);
        }
        for (int i = -9; i < -0; i++)
        {
            Vector3 temp = new Vector3((i + 0.5f), -65, 0);
            Instantiate(blocks[0], temp, transform.rotation);
        }
    }

    private void DestroyAll()
    {
        GameObject[] temp;
        temp = GameObject.FindGameObjectsWithTag("Grid");
        for(int i = 0; i < temp.Length; i++)
        {
            Destroy(temp[i]);
        }
        temp = GameObject.FindGameObjectsWithTag("Blocks");
        for (int i = 0; i < temp.Length; i++)
        {
            Destroy(temp[i]);
        }
        temp = GameObject.FindGameObjectsWithTag("Iron");
        for (int i = 0; i < temp.Length; i++)
        {
            Destroy(temp[i]);
        }
        temp = GameObject.FindGameObjectsWithTag("Chest");
        for (int i = 0; i < temp.Length; i++)
        {
            Destroy(temp[i]);
        }
        temp = GameObject.FindGameObjectsWithTag("Power");
        for (int i = 0; i < temp.Length; i++)
        {
            Destroy(temp[i]);
        }
        temp = GameObject.FindGameObjectsWithTag("Stun");
        for (int i = 0; i < temp.Length; i++)
        {
            Destroy(temp[i]);
        }
        temp = GameObject.FindGameObjectsWithTag("Twist");
        for (int i = 0; i < temp.Length; i++)
        {
            Destroy(temp[i]);
        }
        temp = GameObject.FindGameObjectsWithTag("WallDiv");
        for (int i = 0; i < temp.Length; i++)
        {
            Destroy(temp[i]);
        }
        temp = GameObject.FindGameObjectsWithTag("Back");
        for (int i = 0; i < temp.Length; i++)
        {
            Destroy(temp[i]);
        }
        temp = GameObject.FindGameObjectsWithTag("Scenario");
        for (int i = 0; i < temp.Length; i++)
        {
            Destroy(temp[i]);
        }
        temp = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < temp.Length; i++)
        {
            Destroy(temp[i]);
        }
    }
}

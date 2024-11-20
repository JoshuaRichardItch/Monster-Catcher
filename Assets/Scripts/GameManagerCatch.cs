using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
//using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class GameManagerCatch : MonoBehaviour
{
    //scores
    private int p1FinalScore = 0;
    private int p2FinalScore = 0;
    //booleans
    private bool gameActive = true;
    private bool gameStarted = false;
    //player related
    private PlayerController playerController1;
    private PlayerController playerController2;
    private GameObject player1;
    private GameObject player2;
    //ui
    public TextMeshProUGUI timeText;
    public Image[] monsterImages;
    public TextMeshProUGUI p1Score;
    private string p1Default = "P1 x ";
    public TextMeshProUGUI trueP1Score;
    public TextMeshProUGUI p2Score;
    private string p2Default = "P2 x ";
    public TextMeshProUGUI trueP2Score;
    public TextMeshProUGUI winner;
    public TextMeshProUGUI the2PlayerButton;
    public TextMeshProUGUI tutorial;
    //timers
    float smallTimer = 2f;
    float bigTimer = 90f;
    float timeLimit = 5f;
    float dormantTimer = 0f;
    //goo through list
    private int currentID = -1;
    //list of monsters
    public GameObject[] monsterPrefabs;
    /*monster point multiplier
    //2
    private int pointArctulet = 5;
    //14
    private int pointBlesslug = 50;
    //6
    private int pointBlobabooey = 10;
    //5
    private int pointBlossol = 8;
    //16
    private int pointCryptimber = 100;
    //8
    private int pointDejaRoo = 15;
    //3
    private int pointHeateater = 6;
    //12
    private int pointHummaestro = 35;
    //11
    private int pointKingriffon = 25;
    //15
    private int pointLionetri = 75;
    //7
    private int pointMalp = 14;
    //13
    private int pointPlox = 40;
    //4
    private int pointPorcuspine = 7;
    //10
    private int pointSunkengler = 20;
    //9
    private int pointUnusualShrimp = 16;
    //0
    private int pointWiretapir = 1;
    //1
    private int pointWistowl = 4;
    */
    //list of monster point values
    private int[] monsterPointList = { 1, 4, 5, 6, 7, 8, 10, 14, 15, 16, 20, 25, 35, 40, 50, 75, 100 };
    //random start point min and max values
    //ground
    private float groundMinX = -23.95f;//-13.0f
    private float groundMaxX = -0.95f;
    private float groundY = 4.36f;//- 14.59
    private float groundZ = 40.0f;
    //water
    private float waterMinX = -6.09f;
    private float waterMaxX = 0.9f;
    private float waterY = -1.69f;
    private float waterMinZ = -23.82f;
    private float waterMaxZ = -12.85f;
    //sky
    private float skyMinX = -23.95f;
    private float skyMaxX = -6.95f;
    private float skyMinY = 11.89f;
    private float skyMaxY = 22.4f;
    private float skyZ = 40.0f;
    //fire
    private float fireY = 37.5f;
    private float fireMinZ = -30.0f;
    private float fireMaxZ = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        player1 = GameObject.Find("/Players/Reticle1");
        player2 = GameObject.Find("/Players/Reticle2");
        playerController1 = player1.GetComponent<PlayerController>();
        playerController2 = player2.GetComponent<PlayerController>();
        //monsterPointList = [pointWiretapir, pointWistowl, pointArctulet, pointHeateater, pointPorcuspine, pointBlossol, pointBlobabooey, pointMalp, pointDejaRoo, pointUnusualShrimp, pointSunkengler, pointKingriffon, pointHummaestro, pointPlox, pointBlesslug, pointLionetri, pointCryptimber];

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (!gameStarted)
        {
            if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                gameStarted = true;
                the2PlayerButton.gameObject.SetActive(false);
                tutorial.gameObject.SetActive(false);
            }
        }
        else
        {
            if (gameActive)
            {
                smallTimer += Time.deltaTime;
                if (smallTimer > timeLimit)
                {
                    //StartCoroutine(SpawnMonsterRoutine());
                    //Debug.Log(Random.Range(1, 100));

                    SpawnGroundMonster();
                    SpawnWaterMonster();
                    SpawnSkyMonster();
                    smallTimer = 0f;
                }
                bigTimer -= Time.deltaTime;
                if (bigTimer < 80f)
                {
                    timeLimit = 4f;
                }
                if (bigTimer < 60f)
                {
                    timeLimit = 3f;
                }
                if (bigTimer < 40f)
                {
                    timeLimit = 2f;
                }
                if (bigTimer < 20f)
                {
                    timeLimit = 1f;
                }
                if (bigTimer < 0f)
                {
                    gameActive = false;
                    p1Score.gameObject.SetActive(true);
                    p2Score.gameObject.SetActive(true);
                    trueP1Score.gameObject.SetActive(true);
                    trueP2Score.gameObject.SetActive(true);
                    Destroy(player1);
                    Destroy(player2);
                }

                void SpawnGroundMonster()
                {
                    int randomID = UnityEngine.Random.Range(7, 13);
                    Instantiate(monsterPrefabs[randomID], new Vector3(UnityEngine.Random.Range(groundMinX, groundMaxX), groundY, groundZ), monsterPrefabs[0].transform.rotation);
                }
                void SpawnWaterMonster()
                {
                    int randomID = UnityEngine.Random.Range(0, 7);
                    if (randomID < 3)
                    {
                        Instantiate(monsterPrefabs[randomID], new Vector3(UnityEngine.Random.Range(waterMinX, waterMaxX), waterY, UnityEngine.Random.Range(waterMinZ + 20, waterMaxZ + 20)), monsterPrefabs[1].transform.rotation);
                    }
                    else
                    {
                        Instantiate(monsterPrefabs[randomID], new Vector3(UnityEngine.Random.Range(waterMinX, waterMaxX), waterY, UnityEngine.Random.Range(waterMinZ, waterMaxZ)), monsterPrefabs[1].transform.rotation);
                    }
                }
                void SpawnSkyMonster()
                {
                    int randomID = UnityEngine.Random.Range(13, 17);
                    if (randomID == 13)
                    {
                        Instantiate(monsterPrefabs[randomID], new Vector3(UnityEngine.Random.Range(skyMinX, skyMaxX), fireY, UnityEngine.Random.Range(fireMinZ, fireMaxZ)), monsterPrefabs[2].transform.rotation);
                    }
                    else
                    {
                        Instantiate(monsterPrefabs[randomID], new Vector3(UnityEngine.Random.Range(skyMinX, skyMaxX), UnityEngine.Random.Range(skyMinY, skyMaxY), skyZ), monsterPrefabs[2].transform.rotation);
                    }
                }
                timeText.text = Math.Truncate(bigTimer).ToString();
            }
            else
            {
                if (Input.GetKeyUp(KeyCode.Alpha1) || Input.GetKeyUp(KeyCode.Alpha2))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
                dormantTimer += Time.deltaTime;
                if (dormantTimer > 1f)
                {
                    dormantTimer = 0f;

                    if (currentID < 16)
                    {
                        //StartCoroutine(ScoreThing());

                        thisFunction();
                    }
                    else
                    {
                        monsterImages[16].gameObject.SetActive(false);
                        if (p1FinalScore > p2FinalScore)
                        {
                            winner.text = "P1 Wins";
                        }
                        else if (p2FinalScore > p1FinalScore)
                        {
                            winner.text = "P2 Wins";
                        }
                        else
                        {
                            winner.text = "Tie";
                        }
                        winner.gameObject.SetActive(true);

                    }
                }
            }
        }

        //IEnumerator ScoreThing()
        void thisFunction()
        {
            currentID++;
            int multiplier;
            int currentScoreP1;
            int currentScoreP2;
            multiplier = monsterPointList[currentID];
            currentScoreP1 = playerController1.playerCaptures[currentID] * multiplier;
            currentScoreP2 = playerController2.playerCaptures[currentID] * multiplier;
            p1Score.text = p1Default + playerController1.playerCaptures[currentID].ToString();
            p2Score.text = p2Default + playerController2.playerCaptures[currentID].ToString();
            p1FinalScore += currentScoreP1;
            p2FinalScore += currentScoreP2;
            trueP1Score.text = "Score: " + p1FinalScore.ToString();
            trueP2Score.text = "Score: " + p2FinalScore.ToString();
            monsterImages[currentID].gameObject.SetActive(true);
            if (currentID != 0)
            {
                monsterImages[currentID - 1].gameObject.SetActive(false);
            }
        }
    }
}

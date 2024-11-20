using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class MonsterBehavior : MonoBehaviour
{
    private PlayerController playerController1;
    private PlayerController playerController2;
    private GameObject player1;
    private GameObject player2;
    public int monsterID = 0;
    public int hp = 1;
    public string spawnMethod;
    public float speed = 1.0f;
    private float fishOutOfWater = 4.0f;
    private float leftLimit = -69.0f;
    private bool turnBack = false;
    private bool startFunc = true;
    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.Find("/Players/Reticle1");
        player2 = GameObject.Find("/Players/Reticle2");
        playerController1 = player1.GetComponent<PlayerController>();
        playerController2 = player2.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        MonsterManagerMethod();
        if (transform.position.z < leftLimit)
        {
            Destroy(gameObject);
        }
    }

    void MonsterManagerMethod()
    {
        if (spawnMethod == "Ground")
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        else if (spawnMethod == "Water")
        {
            StartCoroutine(BackIntoWater());
            if (transform.position.y < fishOutOfWater && !turnBack)
            {
                transform.Translate(Vector3.up * Time.deltaTime * speed);
            }
            else if (turnBack)
            {
                transform.Translate(Vector3.down * Time.deltaTime * speed);
                if (startFunc)
                {
                    //StartCoroutine(psiFishKill());
                    startFunc = false;
                }
            } 
        }
        else if (spawnMethod == "Sky")
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        else if (spawnMethod == "Fire")
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
            transform.Translate(Vector3.left * Time.deltaTime * speed * Random.Range(-2, 2));
        }
        if (transform.position.y < -8)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator BackIntoWater()
        {
            yield return new WaitForSeconds(15 / speed);
            turnBack = true;
        }

    /*IEnumerator psiFishKill()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }*/

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        hp--;
        if (hp <= 0)
        {
            Destroy(gameObject);
            if (other.CompareTag("scallop"))
            {
                playerController1.playerCaptures[monsterID]++;
                //Debug.Log("Player 1 has captured " + playerController1.playerCaptures[monsterID] + " of monster #" + monsterID);
            }
            else if (other.CompareTag("skibidi"))
            {
                playerController2.playerCaptures[monsterID]++;
                //Debug.Log("Player 2 has captured " + playerController2.playerCaptures[monsterID] + " of monster #" + monsterID);
            }
        }
    }
}

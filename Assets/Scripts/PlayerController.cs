using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerID;
    private int reticleSpeed = 36;
    public GameObject discPrefab;
    private float bottomBound = 3.0f;
    private float topBound = 30.0f;
    private float leftBound = -42.0f;
    private float rightBound = 29.0f;
    public int[] playerCaptures =
    {
        //no monsters captured yet
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
    };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //different player controls
        if (playerID == 1)
        {
            //-----------------------Player 1----------------------------
            //speed change
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Z)) {reticleSpeed = 72;}
            if (Input.GetKey(KeyCode.B) || Input.GetKey(KeyCode.C)) {reticleSpeed = 18;}
            //move reticle
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.up * Time.deltaTime * reticleSpeed);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.right * Time.deltaTime * reticleSpeed);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector3.down * Time.deltaTime * reticleSpeed);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.left * Time.deltaTime * reticleSpeed);
            }
            //launch disc
            if (Input.GetKeyDown(KeyCode.V) || Input.GetKeyDown(KeyCode.X))
            {
                Instantiate(discPrefab, new Vector3 (20, transform.position.y, transform.position.z), discPrefab.transform.rotation);
            }
        } else if (playerID == 2) {
            //-----------------------Player 2----------------------------
            //speed change
            if (Input.GetKey(KeyCode.Alpha4) || Input.GetKey(KeyCode.Alpha7)) { reticleSpeed = 72; }
            if (Input.GetKey(KeyCode.Alpha6) || Input.GetKey(KeyCode.Alpha9)) { reticleSpeed = 18; }
            //move reticle
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(Vector3.up * Time.deltaTime * reticleSpeed);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(Vector3.right * Time.deltaTime * reticleSpeed);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(Vector3.down * Time.deltaTime * reticleSpeed);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(Vector3.left * Time.deltaTime * reticleSpeed);
            }
            //launch disc
            if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Alpha8))
            {
                Instantiate(discPrefab, new Vector3(20, transform.position.y, transform.position.z), discPrefab.transform.rotation);
            }
        }
        //define boundaries
        if (transform.position.y > topBound)
        {
            transform.position = new Vector3(transform.position.x, topBound, transform.position.z);
        }
        if (transform.position.y < bottomBound)
        {
            transform.position = new Vector3(transform.position.x, bottomBound, transform.position.z);
        }
        if (transform.position.z < leftBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, leftBound);
        }
        if (transform.position.z > rightBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, rightBound);
        }
        //back to normal
        reticleSpeed = 36;
    }
}

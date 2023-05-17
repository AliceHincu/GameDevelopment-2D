using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public Transform player;
    public int score;
    [SerializeField] private TextMeshPro ScoreTxt;
    [SerializeField] private GameObject stickMan;
    [SerializeField] private GameObject otherPlayer;

    private Rigidbody rb;
    public bool gameState;/*
    private Vector3 mouseStartPos, playerStartPos;*/

    private Camera camera;

    /**
     * Player movement
    */
    public KeyCode leftKey;
    public KeyCode rightKey;
    public bool startMoving = false;
    public float playerSpeed = 250, roadSpeed = 1;
    public bool isPushed = false;
    private static float PUSH_DELAY_VALUE = 0.5f;
    private float pushDelay = PUSH_DELAY_VALUE;

    /**
     * Power 1
     */
    public KeyCode pushPlayerKey;
    public float pushPower = 200f;
    public bool canPush = true;
    private static int PUSH_COUNTER_VALUE = 3;
    [SerializeField] private TMP_Text pushCounterText;
    private int pushCounter = 0;
    private float currentElapsedTime = 0;
    [SerializeField] private GameObject powerCorner; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = transform;
        score = 0;
        ChangeScore(0);
        camera = Camera.main;
        pushCounterText.SetText("");
    }

    // Update is called once per frame
    void Update()
    {
        if(startMoving)
        {
            stickMan.GetComponent<Animator>().SetBool("run", true);
            MoveThePlayer();
            if(pushCounter <= 0)
            {
                PlayerPowers();
            }
            Power1Count();
        }
        if(isPushed)
        {
            pushDelay -= Time.deltaTime;
            if(pushDelay <= 0)
            {
                isPushed = false;
                pushDelay = PUSH_DELAY_VALUE;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gate"))
        {
            var gateManager = other.GetComponent<GateManager>();
            Debug.Log(gateManager.randomNumber);
            Debug.Log(gateManager.isNegativeScore);
            if (gateManager.multiply)
            {
                if(gateManager.isNegativeScore)
                    ChangeScore(Mathf.RoundToInt(score / gateManager.randomNumber));
                else
                    ChangeScore(score * gateManager.randomNumber);
            }
            else
            {
                if (gateManager.isNegativeScore)
                    ChangeScore(score - gateManager.randomNumber);
                else
                    ChangeScore(score + gateManager.randomNumber);
            }
        }
    }

    private void ChangeScore(int number)
    {
        score = number;
        ScoreTxt.text = score.ToString();
    }

    void MoveThePlayer()
    {
        float moveHorizontal = 0;
        if(Input.GetKey(leftKey))
        {
            moveHorizontal = -1;
        } else if (Input.GetKey(rightKey))
        {
            moveHorizontal = 1;
        }
        
        if(!isPushed)
        {
           rb.velocity = new Vector3(moveHorizontal, 0f, 0f) * playerSpeed * Time.deltaTime; // Creates velocity in direction of value equal to keypress (WASD). rb.velocity.y deals with falling + jumping by setting velocity to y. 
        }
        transform.position += transform.forward * roadSpeed * Time.deltaTime; // move on 
    }

    void PlayerPowers()
    {
        if(Input.GetKey(pushPlayerKey))
        {
            otherPlayer.GetComponent<PlayerManager>().Push(transform.position.x);
            pushCounter = PUSH_COUNTER_VALUE;
            pushCounterText.SetText(pushCounter.ToString());
        }
    }

    void Push(float xPlayerPosition)
    {
        float pushDirectionX = xPlayerPosition - transform.position.x;
        rb.AddForce(new Vector3(-pushDirectionX, 0f, 0f).normalized * pushPower, ForceMode.Impulse);
        isPushed = true;
    }

    void Power1Count()
    {
        if (pushCounter > 0)
        {
            currentElapsedTime += Time.deltaTime;
            if (currentElapsedTime >= 1)
            {
                pushCounter--;
                pushCounterText.SetText(pushCounter.ToString());
                currentElapsedTime = 0;
            }

            if (pushCounter == 0)
            {
                pushCounterText.SetText("");
            }
        }
    }

    public void StopMoving()
    {
        startMoving = false;
        stickMan.GetComponent<Animator>().SetBool("run", false);
        rb.velocity = new Vector3(0f, 0f, 0f);
    }
}


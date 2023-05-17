using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelScript : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public GameObject finish;
    public Camera camera;
    [SerializeField] private TMP_Text text;
    public Button button;

    private int count = 5;
    private float currentElapsedTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        button.gameObject.SetActive(false);
        SetCount(count);
    }

    // Update is called once per frame
    void Update()
    {
        if(count >= 0)
        {
            CountDown();
        } 
    }

    private void CountDown()
    {
        currentElapsedTime += Time.deltaTime;
        if (currentElapsedTime >= 1)
        {
            count--;
            SetCount(count);
            currentElapsedTime = 0;
        }

        if (count == 0)
        {
            text.SetText("Start!");
            player1.GetComponent<PlayerManager>().startMoving = true;
            player2.GetComponent<PlayerManager>().startMoving = true;
            camera.GetComponent<CameraScript>().startMoving = true;
        }

        if (count < 0)
        {
            text.SetText("");
        }
    }

    private void SetCount(int count)
    {
        text.SetText(count.ToString());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // announce winner
            int player1Score = player1.GetComponent<PlayerManager>().score;
            int player2Score = player2.GetComponent<PlayerManager>().score;
            string winner = player1Score > player2Score ? "Player1 Won!!!" : (player1Score < player2Score ? "Player2 Won!!" : "Draw :)");
            text.SetText(winner);

            // stop moving
            camera.GetComponent<CameraScript>().startMoving = false;
            player1.GetComponent<PlayerManager>().StopMoving();
            player2.GetComponent<PlayerManager>().StopMoving();

            // try again
            button.gameObject.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
    }
}

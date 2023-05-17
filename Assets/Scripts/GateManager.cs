using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GateManager : MonoBehaviour
{
    public TextMeshPro GateNo;
    public int randomNumber;
    public bool multiply;
    public bool isNegativeScore;
    [SerializeField] private GameObject gateScreen;

    private Color redColor = new Color(0.95f, 0.2f, 0.26f, 0.75f);
    private Color greenColor = new Color(0.33f, 0.85f, 0.56f, 0.75f);
    void Start()
    {
        // set color
        isNegativeScore = (Random.value > 0.7f);
        multiply = (Random.value > 0.5f);
        if (isNegativeScore)
            gateScreen.GetComponent<Renderer>().material.SetColor("_Color", redColor);
        else
            gateScreen.GetComponent<Renderer>().material.SetColor("_Color", greenColor);

        // set gate score number
        if (multiply)
        {
            randomNumber = Random.Range(2, 3);
            if (isNegativeScore)
                GateNo.text = "/" + randomNumber.ToString();
            else
                GateNo.text = "X" + randomNumber.ToString();
        }
        else
        {
            randomNumber = Random.Range(10, 100);

            if (randomNumber % 2 != 0)
                randomNumber += 1;

            if (isNegativeScore)
                GateNo.text = "-" + randomNumber.ToString();
            else
                GateNo.text = "+" + randomNumber.ToString();
        }

        Debug.Log(isNegativeScore);
    }

}
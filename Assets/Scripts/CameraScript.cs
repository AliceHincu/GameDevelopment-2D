using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float roadSpeed = 1;
    public bool startMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(startMoving)
        {
            Vector3 moveDir = new Vector3(0f, 0f, roadSpeed * Time.deltaTime);
            transform.position += moveDir;
        }
    }
}

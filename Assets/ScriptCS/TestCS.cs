using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCS : MonoBehaviour
{
    int Testpoint = 1;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Testpoint <= 10)
        {
            Debug.Log("teat is : " + Testpoint);
            Testpoint++;
        }

    }
}

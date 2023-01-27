using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Count : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
    public float sumCount = 0;
    public GameObject GameOverPanel;

    public void CountKilled(float count)
    {
        sumCount += count;
        Debug.Log(sumCount);
        if (sumCount == 8)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            GameOverPanel.SetActive(true);
        }

    }
}

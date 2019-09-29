using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private Transform playInitial;
    [SerializeField]
    private Transform exitInitial;

    private Vector2 playFinal;
    private Vector2 exitFinal;

    [SerializeField]
    private Transform cursor;

    //0 is for play
    //1 is for exit
    public int currentPosition;
    // Start is called before the first frame update
    void Start()
    {
        playFinal = playInitial.position;
        playFinal.x -= 2;
        exitFinal = exitInitial.position;
        exitFinal.x -= 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && currentPosition == 0)
        {
            cursor.position = exitFinal;
            currentPosition = 1;
        }else if (Input.GetKeyDown(KeyCode.UpArrow) && currentPosition == 1)
        {
            cursor.position = playFinal;
            currentPosition = 0;
        }else if (Input.GetKeyDown(KeyCode.DownArrow) && currentPosition == 1)
        {
            cursor.position = playFinal;
            currentPosition = 0;
        }else if (Input.GetKeyDown(KeyCode.UpArrow) && currentPosition == 0)
        {
            cursor.position = exitFinal;
            currentPosition = 1;
        }

        if (Input.GetKeyDown(KeyCode.Space) && currentPosition == 0)
        {
            SceneManager.LoadScene(1);
        }else if (Input.GetKeyDown(KeyCode.Space) && currentPosition == 1)
        {
            Application.Quit();
        }

    }
}

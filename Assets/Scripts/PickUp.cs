using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PickUp : MonoBehaviour
{

    private int _count;
    bool gameHasEnded = false;

    public GameObject points0;
    public GameObject points1;
    public GameObject points2;
    public GameObject points3;

    public GameObject openDoor;
    public GameObject win;
    public GameObject lose;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            LosingTheGame();
        }
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            _count = _count + 1;
            SetCountText();
        }
        if (other.gameObject.CompareTag("Door") && _count >= 3)
        {
            WinningTheGame();
        }
    }

    void SetCountText()
    {

        switch (_count)
        {
            case 0:

                points0.SetActive(true);
                points1.SetActive(false);
                points2.SetActive(false);
                points3.SetActive(false);

                break;
            case 1:

                points0.SetActive(false);
                points1.SetActive(true);
                points2.SetActive(false);
                points3.SetActive(false);

                break;
            case 2:

                points0.SetActive(false);
                points1.SetActive(false);
                points2.SetActive(true);
                points3.SetActive(false);

                break;
            case 3:

                points0.SetActive(false);
                points1.SetActive(false);
                points2.SetActive(false);
                points3.SetActive(true);

                break;
            default:
                points0.SetActive(true);
                points1.SetActive(false);
                points2.SetActive(false);
                points3.SetActive(false);

                break;
        }


        if (_count >= 3)
        {
            openDoor.SetActive(true);
           // win.text = "Door Opened!";
        }
    }
    void WinningTheGame()
    {
        if (gameHasEnded == false)
        {
            openDoor.SetActive(false);
            win.SetActive(true);
            gameHasEnded = true;
            Invoke("Restart", 2f);
        }

    }

    void LosingTheGame()
    {
        if (gameHasEnded != true)
        {
            // YOU LOST
            openDoor.SetActive(false);
            lose.SetActive(true);
            Invoke("Restart", 2f);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PickUp : MonoBehaviour
{

    public Text countText;
    public Text win;
    public Text endGame;
    public int count;
    bool gameHasEnded = false;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
        if (other.gameObject.CompareTag("Door") && count >= 3)
        {
            EndTheGame();
        }
    }

    void SetCountText()
    {
        countText.text = "Keys collected: " + count.ToString();
        if (count >= 3)
        {
            win.text = "Door Opened!";
        }
    }
    void EndTheGame()
    {
        if (gameHasEnded == false)
        {
            endGame.text = "You WON";
            gameHasEnded = true;
            Invoke("Restart", 2f);
        }

    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

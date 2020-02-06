using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PickUp : MonoBehaviour
{
    private int _startGame = 1;
    private int _mainMenu = 0;
    private float _waitToStart = 2f;
    private int _count;
    private bool _gameHasEnded = false;
    private bool _youWon = false;

    [SerializeField] private GameObject[] _points = new GameObject[4];

    [SerializeField] private GameObject _openDoor;
    [SerializeField] private GameObject _win;
    [SerializeField] private GameObject _lose;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            LosingTheGame();
        }
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            _count += 1;
            SetCountText();
        }
        if (other.gameObject.CompareTag("Door") && _count >= 3)
        {
            WinningTheGame();
        }
    }
    private void _LoadPointsNumber(bool p1, bool p2, bool p3, bool p4)
    {
        _points[0].SetActive(p1);
        _points[1].SetActive(p2);
        _points[2].SetActive(p3);
        _points[3].SetActive(p4);
    }
    void SetCountText()
    {
        switch (_count)
        {
            case 0:
                _LoadPointsNumber(true, false, false, false);
            break;
            case 1:
                _LoadPointsNumber(false, true, false, false);
            break;
            case 2:
                _LoadPointsNumber(false, false, true, false);
            break;
            case 3:
                _LoadPointsNumber(false, false, false, true);
            break;
            default:
                _LoadPointsNumber(true, false, false, false);
            break;
        }

        if (_count >= 3)
        {
            _openDoor.SetActive(true);
        }
    }
    void WinningTheGame()
    {
        if (_gameHasEnded == false)
        {
            _gameHasEnded = true;
            _openDoor.SetActive(false);
            _win.SetActive(true);
            _youWon = true;
            StartCoroutine("Restart");
        }
    }

    void LosingTheGame()
    {
        if (_gameHasEnded == false)
        {
            _gameHasEnded = true;
            _openDoor.SetActive(false);
            _lose.SetActive(true);
            StartCoroutine("Restart");
        }
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(_waitToStart);
        if (_youWon)
            SceneManager.LoadScene(_mainMenu);
        else
            SceneManager.LoadScene(_startGame);
    }
}

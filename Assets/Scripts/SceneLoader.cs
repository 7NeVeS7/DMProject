using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject begin;
    public GameObject instruction;
    private float _waitToStart = 4f;
    private int _startGame = 1;
    
    public void LoadScene()
    {

        StartCoroutine("Instruct");

    }

    IEnumerator Instruct()
    {
        begin.SetActive(false);
        instruction.SetActive(true);
           yield return new WaitForSeconds(_waitToStart);
        SceneManager.LoadScene(_startGame);
    }
}

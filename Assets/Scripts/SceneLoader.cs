using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{


    public GameObject Begin;
    public GameObject Instruction;

    public void LoadScene()
    {

        StartCoroutine("Instruct");


    }


    IEnumerator Instruct()
    {

        Begin.SetActive(false);
        Instruction.SetActive(true);
           yield return new WaitForSeconds(4);
        SceneManager.LoadScene(1);

    }
}

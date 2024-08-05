using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameEnd : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MainMenuTimer());
    }

    IEnumerator MainMenuTimer()
    {
        yield return new WaitForSeconds(7f);
        SceneManager.LoadScene(0);
    }
}

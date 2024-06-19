using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        StartCoroutine(waitime());
    }

    public void Quit()
    {
        StartCoroutine(Waitime());
    }
    IEnumerator waitime()
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("Level 01");

    }

    IEnumerator Waitime()
    {
        yield return new WaitForSeconds(0.2f);
        Application.Quit();

    }
}

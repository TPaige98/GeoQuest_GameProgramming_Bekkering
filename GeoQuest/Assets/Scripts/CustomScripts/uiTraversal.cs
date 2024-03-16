using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class uiTraversal : MonoBehaviour
{
    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void quitGame()
    {
        Application.Quit();

        //UnityEditor.EditorApplication.isPlaying = false;
    }
}

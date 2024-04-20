using System.Collections;
using System.Collections.Generic;
//using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class uiTraversal : MonoBehaviour
{
    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void LoadMain()
    {
        SceneManager.LoadScene("MenuScreen");
    }

    public void quitGame()
    {
        //Application.Quit();

        //UnityEditor.EditorApplication.isPlaying = false;
    }
}

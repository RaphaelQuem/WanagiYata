using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInput : MonoBehaviour
{
    public void LoadOnClick()
    {
        SceneManager.LoadScene("teste");
    }
    public void QuitOnClick()
    {
        Application.Quit();
    }

}

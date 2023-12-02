using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeleteMenu : MonoBehaviour
{
    public void DeleteBack()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    public void DeleteObj()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 7);
    }

    public void DeleteProd()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 9);
    }

    public void DeleteLink()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 11);
    }

}

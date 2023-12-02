using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EditMenu : MonoBehaviour
{
    public void EditBack()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
    }

    public void EditObj()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 12);
    }

    public void EditProd()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 14);
    }

}

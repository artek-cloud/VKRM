using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AdminMenu : MonoBehaviour
{
  
    public void AdminBack()
    { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 4); 
    }

    public void AdminAdd()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void AdminDelete()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void AdminEdit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }

}

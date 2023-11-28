using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text userName;
    public Text typeAdmin;
    public Text typeView;

    //Переменные для управления кнопкой администрирования
    public Button adminButton;


    // Start is called before the first frame update
    void Start()
    {
        userName.text = LogIn.userName + "!";
        if (LogIn.userType == "1")
        {
            typeAdmin.enabled = true;
            adminButton.enabled = true;
        }
        else
            typeView.enabled = true;
    }

    public void ObjectSearch()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ProductSearch()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void Exit()
    {
        Application.Quit();
    }
}

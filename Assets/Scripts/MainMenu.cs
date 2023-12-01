using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Mono.Data.Sqlite;

public class MainMenu : MonoBehaviour
{
    public Text userName;
    public Text typeAdmin;
    public Text typeView;

    //Переменные для управления кнопкой администрирования
    public Button adminButton;

    private string dbName = "URI=file:TechpriborDB.db";
    private string servDbName = "URI=file:D:/ServDB.db";


    // Start is called before the first frame update
    void Start()
    {
        CreateDB();
        userName.text = LogIn.userName + "!";
        if (LogIn.userType == "1")
        {
            typeAdmin.enabled = true;
            adminButton.gameObject.SetActive(true);
        }
        else
            typeView.enabled = true;
    }

    public void CreateDB()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "create table if not exists objects (id numeric, obozn varchar(255));";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
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

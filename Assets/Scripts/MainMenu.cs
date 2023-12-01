using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Text userName;
    public Text typeAdmin;
    public Text typeView;

    //Переменные для управления кнопкой администрирования
    public Button adminButton;

    private string dbName = "URI=file:TechpriborDB.db";
    private string servDbName = "URI=file:D:/Unity/VKRM/TechpriborDB.db";


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
                command.CommandText = "CREATE TABLE if not exists Objects (ID    INTEGER NOT NULL UNIQUE, Obozn VARCHAR(255) NOT NULL UNIQUE, Naim  VARCHAR(255) NOT NULL, PRIMARY KEY(ID AUTOINCREMENT));";
                command.ExecuteNonQuery();
                command.CommandText = "CREATE TABLE if not exists Products (ID   INTEGER NOT NULL UNIQUE, Obozn VARCHAR(255) NOT NULL UNIQUE, Naim  VARCHAR(255) NOT NULL, PRIMARY KEY(ID AUTOINCREMENT));";
                command.ExecuteNonQuery();
                command.CommandText = "CREATE TABLE OPReference (ID    INTEGER NOT NULL UNIQUE, ObjectID  INTEGER NOT NULL, ProductID INTEGER NOT NULL, Kolvo INTEGER, PRIMARY KEY(ID AUTOINCREMENT), FOREIGN KEY(ProductID) REFERENCES Products(ID), FOREIGN KEY(ObjectID) REFERENCES Objects(ID));";
                command.ExecuteNonQuery();
                using (var connection2 = new SqliteConnection(servDbName))
                {
                    using (var command2 = connection2.CreateCommand())
                    {
                        command2.CommandText = "select * from Objects;";
                        using (IDataReader reader = command2.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                command.CommandText = "insert into Objects values(" + Convert.ToInt32(reader["ID"]) + ", " + (reader["Obozn"].ToString()) + ", " + (reader["Naim"].ToString()) + ");";
                                command.ExecuteNonQuery();
                            }
                        }
                    }
                }
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

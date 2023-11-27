using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;

public class LogIn : MonoBehaviour
{

    public InputField logIn;
    public InputField password;
    //для получения логина и пароля использовать logIn.text и password.text
    //поле Type таблицы Users: 0 - просмотр, 1 - администратор

    private string servDbName = "URI=file:D:/ServDB.db";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LogingIn()
    {
        using (var connection = new SqliteConnection(servDbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "select * from Users u where Login = '" + logIn.text + "' and Password = '" + password.text + "'";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        if (reader["Login"].ToString() != "")
                            Debug.Log(reader["Login"].ToString() + " и " + reader["Password"].ToString() + " и " + reader["Name"].ToString());
                        else
                            Debug.Log("Данной учетной записи не существует!");
                    }
                    reader.Close();
                }    
            }
            connection.Close();
        }
    }

   
}

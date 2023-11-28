using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LogIn : MonoBehaviour
{

    public InputField logIn;
    public InputField password;
    public Text errorText;
    //для получения логина и пароля использовать logIn.text и password.text
    //поле Type таблицы Users: 0 - просмотр, 1 - администратор

    private string servDbName = "URI=file:D:/ServDB.db";

    public static string userName;
    public static string userType;

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
                    /** while(reader.Read())
                     {
                         if (reader["Login"].ToString() != "")
                             Debug.Log(reader["Login"].ToString() + " и " + reader["Password"].ToString() + " и " + reader["Name"].ToString());
                         else
                             Debug.Log("Данной учетной записи не существует!");
                     } **/
                    if (reader.Read())
                    {
                        userName = reader["Name"].ToString();
                        userType = reader["Type"].ToString();
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                        return;
                    }
                    else
                        errorText.enabled = true;
                    reader.Close();
                }    
            }
            connection.Close();
        }
    }

    public void Exit()
    {
        Application.Quit();
    }


}

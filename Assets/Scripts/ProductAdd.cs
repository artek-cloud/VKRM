using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;

public class ProductAdd : MonoBehaviour
{
    public Text objectObozn;
    public Text objectNaim;
    public Text errorText;
    public Text successText;

    private string dbName = "URI=file:D:/ServDB.db";

    public void ProductAdding()
    {
        if (objectObozn.text == "" || objectNaim.text == "")
        {
            successText.enabled = false;
            errorText.enabled = true;
            return;
        }
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "insert into Products(Obozn, Naim) values('" + objectObozn.text + "', '" + objectNaim.text + "');";
                command.ExecuteNonQuery();
            }
            connection.Close();
            errorText.enabled = false;
            successText.enabled = true;
        }
    }

    public void ObjectAddBack()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 4);
    }
}

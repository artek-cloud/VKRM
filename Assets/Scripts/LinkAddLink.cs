using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;
using System;

public class LinkAddLink : MonoBehaviour
{

    public Text objectObozn;
    public Text productObozn;
    public Text kolvo;
    public Text errorText;

    private string dbName = "URI=file:D:/ServDB.db";

    // Start is called before the first frame update
    void Start()
    {
        objectObozn.text = LinkAddObject.objectON;
        productObozn.text = LinkAddProduct.objectON;
    }

    public void LinkAdding()
    {
        if (kolvo.text == "")
        {
            errorText.enabled = true;
            return;
        }
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "insert into OPReference(ObjectID, ProductID, Kolvo) values(" + LinkAddObject.objectID + ", " + LinkAddProduct.objectID + ", " + Convert.ToInt32(kolvo.text) + ");";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 7);
    }

    public void LinkAddBack()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}

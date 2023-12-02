using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;
using System;

public class LinkDeleteConfirm : MonoBehaviour
{
    public Text objectObozn;

    private string dbName = "URI=file:D:/ServDB.db";
    // Start is called before the first frame update
    void Start()
    {
        objectObozn.text = LinkDelete.objectON;
    }

    public void ConfirmBack()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void ObjDeleting()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "delete from OPReference where ID = " + LinkDelete.objectID;
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}

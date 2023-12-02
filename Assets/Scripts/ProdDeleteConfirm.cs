using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;
using System;

public class ProdDeleteConfirm : MonoBehaviour
{
    public Text objectObozn;

    private string dbName = "URI=file:D:/ServDB.db";
    // Start is called before the first frame update
    void Start()
    {
        objectObozn.text = ProdDelete.objectON;
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
                command.CommandText = "delete from OPReference where ProductID = " + ProdDelete.objectID;
                command.ExecuteNonQuery();
                command.CommandText = "delete from Products where ID = " + ProdDelete.objectID;
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}

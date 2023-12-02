using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;

public class ProdEditType : MonoBehaviour
{
    public InputField objObozn;
    public InputField objNaim;
    public Text errorText;
    public Text successText;

    private string dbName = "URI=file:D:/ServDB.db";

    // Start is called before the first frame update
    void Start()
    {
        objObozn.text = ProdEdit.objectOb;
        objNaim.text = ProdEdit.objectNa;
    }

    public void ObjectAdding()
    {
        if (objObozn.text == "" || objNaim.text == "")
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
                command.CommandText = "update Products set Obozn = '" + objObozn.text + "', Naim = '" + objNaim.text + "' where ID = " + ProdEdit.objectID + ";";
                command.ExecuteNonQuery();
            }
            connection.Close();
            errorText.enabled = false;
            successText.enabled = true;
        }
    }

    public void ObjectAddBack()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}

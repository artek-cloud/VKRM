using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using Mono.Data.Sqlite;
using UnityEngine.UI;

public class DataBase : MonoBehaviour
{
    // Start is called before the first frame update

    public Text objectsList;
    public Text objectSearch;
    public static Text objectSearchResult;
   
    private string dbName = "URI=file:TechpriborDB.db";
    void Start()
    {

        DisplayObjects();
        
    }

    // Update is called once per frame
    void Update()
    {
        objectSearchResult = objectSearch;
        //DisplayObjects();
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

    public void AddObject(int id, string obozn)
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "insert into objects values(" + id + ", '" + obozn + "');";
                command.ExecuteNonQuery();
            }
            connection.Close();

        }
    }

    public void DisplayObjects()
    {
        objectsList.text = "";

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "select * from Objects o ";

                if(objectSearch.text != "")
                {
                command.CommandText = command.CommandText + "where o.obozn like '%" + objectSearch.text + "%' ";
                }

                command.CommandText = command.CommandText + "order by ID;";
                

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        objectsList.text += reader["ID"] + "\t\t" + reader["Obozn"] + "\n";

                    reader.Close();
                }
            }
            connection.Close();
        }
    }

    public void ClearTable()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "delete from objects;";
                command.ExecuteNonQuery();
            }
            connection.Close();

        }
    }
}

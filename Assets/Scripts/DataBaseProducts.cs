using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using Mono.Data.Sqlite;
using UnityEngine.UI;

public class DataBaseProducts : MonoBehaviour
{
    // Start is called before the first frame update

    public Text productsList;
    public Text productSearch;

    private string dbName = "URI=file:TechpriborDB.db";
    void Start()
    {

        DisplayProducts();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateDB()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "create table if not exists products (id numeric, obozn varchar(255));";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public void AddProduct(int id, string obozn)
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "insert into products values(" + id + ", '" + obozn + "');";
                command.ExecuteNonQuery();
            }
            connection.Close();

        }
    }

    public void DisplayProducts()
    {
        productsList.text = "Список изделий\n";

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "select * from Products order by ID;";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        productsList.text += reader["ID"] + "\t\t" + reader["Obozn"] + "\n";

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
                command.CommandText = "delete from products;";
                command.ExecuteNonQuery();
            }
            connection.Close();

        }
    }
}

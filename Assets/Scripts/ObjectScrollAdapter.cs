using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;

public class ObjectScrollAdapter : MonoBehaviour
{
    
public RectTransform prefab;
public int countElements;
public RectTransform content;
public Text objectSearch;

private string dbName = "URI=file:TechpriborDB.db";

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

public void UpdateItems()
{
int modelsCount = 0;
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
            modelsCount = modelsCount + 1;
            reader.Close();
        }
    }
    connection.Close();
}
}

/*IEnumerator GetItems (int count, System.Action<ItemModel[]> callback)
{
    var results = new ItemModel[count];

}*/


public class ItemModel
{
    public string title;
}

public class ItemView
{
public Text titleText;
}

}

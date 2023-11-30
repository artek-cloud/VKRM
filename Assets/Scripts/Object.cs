using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;


public class Object : MonoBehaviour
{
    // Start is called before the first frame update

        public Text searchResult;

        private string dbName = "URI=file:TechpriborDB.db";

    void Start()
    {

        DisplayObject();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ObjectSearchBack()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    public void DisplayObject()
    {

        //var database = GetComponent<DataBase>();

        //searchResult.text = "Результат\n";
        searchResult.text = ObjectScrollAdapter.objectON;

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "select o.obozn as object, p.obozn as product, opr.kolvo from Objects o inner join OPReference opr on(opr.ObjectID = o.id)inner join Products p on(p.id = opr.ProductID)where o.obozn = '" + DataBase.objectSearchResult.text + "';";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        searchResult.text += reader["product"] + "\t\t" + reader["kolvo"] + "\n";

                    reader.Close();
                }
            }
            connection.Close();
        }

    }
}

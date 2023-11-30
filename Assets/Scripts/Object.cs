using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;
using System;


public class Object : MonoBehaviour
{
    // Start is called before the first frame update

        public Text searchResult;
    public RectTransform prefab;
    private int modelsCount;
    public RectTransform content;

    private string dbName = "URI=file:TechpriborDB.db";

    void Start()
    {

        //Debug.Log(ObjectScrollAdapter.objectID);
        searchResult.text = ObjectScrollAdapter.objectON;
        UpdateItems();
        
    }


    public void ObjectSearchBack()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    public void UpdateItems()
    {
        modelsCount = 0;
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "select p.ID from Objects o inner join OPReference opr on(opr.ObjectID = o.ID) inner join Products p on(p.ID = opr.ProductID) where o.ID = " + ObjectScrollAdapter.objectID + " order by p.ID;";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        modelsCount = modelsCount + 1;
                    }
                    reader.Close();
                }
            }
            connection.Close();
        }
        StartCoroutine(GetItems(modelsCount, results => OnReceivedModels(results)));
    }

    void OnReceivedModels(ItemModel[] models)
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
        foreach (var model in models)
        {
            var instance = GameObject.Instantiate(prefab.gameObject) as GameObject;
            instance.transform.SetParent(content, true);
            InitializeItemView(instance, model);
        }
    }

    void InitializeItemView(GameObject viewGameObject, ItemModel model)
    {
        ItemView view = new ItemView(viewGameObject.transform);
        view.titleText.text = model.title + " - " + model.title2 + "\nКоличество = " + model.kolvo;
    }

    IEnumerator GetItems(int count, System.Action<ItemModel[]> callback)
    {
        yield return new WaitForSeconds(0.001f);
        var results = new ItemModel[count];
        int i = 0;
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "select p.Obozn, p.Naim, opr.Kolvo from Objects o inner join OPReference opr on(opr.ObjectID = o.ID) inner join Products p on(p.ID = opr.ProductID) where o.ID = " + ObjectScrollAdapter.objectID + " order by p.ID;";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results[i] = new ItemModel();
                        results[i].title = (reader["Obozn"].ToString());
                        results[i].title2 = (reader["Naim"].ToString());
                        results[i].kolvo = Convert.ToInt32(reader["Kolvo"]);
                        i++;
                    }
                    reader.Close();
                }
            }
            connection.Close();
        }
        callback(results);
    }

    public class ItemModel
    {
        public string title;
        public string title2;
        public int kolvo;
    }

    public class ItemView
    {
        public Text titleText;

        public ItemView(Transform rootView)
        {
            titleText = rootView.Find("TitleText").GetComponent<Text>();
        }
    }

}


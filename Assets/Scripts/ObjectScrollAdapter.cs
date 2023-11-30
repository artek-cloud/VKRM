using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class ObjectScrollAdapter : MonoBehaviour
{
    
public RectTransform prefab;
private int modelsCount;
public RectTransform content;
public Text objectSearch;

    public static int objectID;
    public static string objectON; //Обозначение + наименование 

private string dbName = "URI=file:TechpriborDB.db";

// Start is called before the first frame update
void Start()
{
        //заполнение кэша из базы на сервере
    UpdateItems();
}

// Update is called once per frame
/*void Update()
{
    UpdateItems();
}*/

    public void UpdateItems()
{
modelsCount = 0;
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
    foreach(var model in models)
    {
        var instance = GameObject.Instantiate(prefab.gameObject) as GameObject;
        instance.transform.SetParent(content, true);
        InitializeItemView(instance, model);
    }
}

void InitializeItemView(GameObject viewGameObject, ItemModel model)
{
    ItemView view = new ItemView(viewGameObject.transform);
        view.id = model.id;
    view.titleText.text = model.title + "\n" + model.title2;
        view.clickButton.onClick.AddListener(
            ()=>
            {
                objectID = view.id;
                objectON = view.titleText.text;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
                //Debug.Log("Выбран " + /**view.titleText.text**/ view.id + ".");
            }
            );
}

IEnumerator GetItems (int count, System.Action<ItemModel[]> callback)
{
    yield return new WaitForSeconds(0.001f);
    var results = new ItemModel[count];
    int i = 0;
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
                while(reader.Read())
                {
                    results[i] = new ItemModel();
                        results[i].id = Convert.ToInt32(reader["ID"]);
                    results[i].title = (reader["Obozn"].ToString());
                        results[i].title2 = (reader["Naim"].ToString());
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
        public int id;
    public string title;
        public string title2;
    }

public class ItemView
{
        public int id;
public Text titleText;
        public Button clickButton;

        public ItemView(Transform rootView)
        {
            titleText = rootView.Find("TitleText").GetComponent<Text>();
            clickButton = rootView.Find("ClickButton").GetComponent<Button>();
        }
    }

}

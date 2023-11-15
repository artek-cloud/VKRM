using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;

public class ObjectScrollAdapter : MonoBehaviour
{
    
public RectTransform prefab;
private int modelsCount;
public RectTransform content;
public Text objectSearch;

private string dbName = "URI=file:TechpriborDB.db";

// Start is called before the first frame update
void Start()
{
    UpdateItems();
}

// Update is called once per frame
/*void Update()
{
    UpdateItems();
}*/

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


    public void SearchClear()
    {
        objectSearch.text = "";
        UpdateItems();
    }

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
    view.titleText.text = model.title;
        view.clickButton.onClick.AddListener(
            ()=>
            {
                Debug.Log("Выбран " + view.titleText.text + ".");
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
                    results[i].title = (reader["Obozn"].ToString());
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
}

public class ItemView
{
public Text titleText;
        public Button clickButton;

        public ItemView(Transform rootView)
        {
            titleText = rootView.Find("TitleText").GetComponent<Text>();
            clickButton = rootView.Find("ClickButton").GetComponent<Button>();
        }
    }

}

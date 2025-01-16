using UnityEngine;
using Realms;

public class DatabaseManager : MonoBehaviour
{
    private static DatabaseManager _instance;

    public static DatabaseManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Database manager's Instance not found");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    public Realm GetRealmInstance()
    {
        var config = new RealmConfiguration
        {
            SchemaVersion = 1,
            ShouldDeleteIfMigrationNeeded = true
        };

        return Realm.GetInstance(config);
    }
}

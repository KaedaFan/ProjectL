using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    //While there is only one player, I use instance to simplify access.
    private static PlayerBehaviour _instance;
    public static PlayerBehaviour Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Player's Instance not found. (PlayerBehaviour script)");
            }
            return _instance;
        }
    }
    public Health HealthPlayer;

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

        HealthPlayer = GetComponent<Health>();
    }

    public void Die()
    {
        PlayerDeathManager.Instance.OnPlayerDeath();
    }
}

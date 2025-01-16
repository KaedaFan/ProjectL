using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathManager : MonoBehaviour
{
    private static PlayerDeathManager _instance;
    public static PlayerDeathManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Player death manager's Instance is null");
            }
            return _instance;
        }
    }

    [SerializeField] private GameObject _deathUI;

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

    private void Start()
    {
        _deathUI.SetActive(false);
    }

    public void OnPlayerDeath()
    {
        Time.timeScale = 0f;

        if (_deathUI != null)
        {
            _deathUI.SetActive(true);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (_deathUI != null) _deathUI.SetActive(false);
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;

        if (_deathUI != null) _deathUI.SetActive(false);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

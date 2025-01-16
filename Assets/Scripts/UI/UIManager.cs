using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UIManager's Instance not found");
            }
            return _instance;
        }
    }

    [SerializeField] private Joystick _joystick;
    [SerializeField] private Button _inventoryButton;
    [SerializeField] private Button _shootButton;
    [SerializeField] private TMP_Text _textForShoot;

    public Joystick Joysick => _joystick;
    public Button InventoryButton => _inventoryButton;
    public Button ShootButton => _shootButton;
    public TMP_Text TextForShoot => _textForShoot;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (_shootButton != null)
        {
            _shootButton.onClick.RemoveAllListeners();

            GameObject player = PlayerBehaviour.Instance.gameObject;
            PlayerShoot playerShoot = player.GetComponent<PlayerShoot>();

            _shootButton.onClick.AddListener(playerShoot.Shoot);
        }
    }
}

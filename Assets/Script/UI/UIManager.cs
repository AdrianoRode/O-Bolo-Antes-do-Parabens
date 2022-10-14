using System.Collections;
using Ez;
using Script.GameManager;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class UIManager : MonoBehaviour, IUI
{
    [SerializeField]private GameObject textInput;
    [SerializeField]private GameObject storeUI;
    [SerializeField]private GameObject pauseUI;
    [SerializeField]private TextMeshProUGUI coinTxt;
    [SerializeField]private TextMeshProUGUI ammoTxt;

    private bool shop;
    private int value;
    private CreateMenu createMenu;

    public static UIManager uiManager;

    void Awake()
    {
        if (uiManager == null)
        {
            uiManager = this;
        }
        coinTxt = FindObjectOfType<TextMeshProUGUI>();
        createMenu = FindObjectOfType<CreateMenu>();

    }
    void Update()
    {
        coinTxt.text = Game.coins.ToString();
    }
    
    public void CloseUI()
    {
        shop = false;
    }

    public void OnGamePaused()
    {
        pauseUI.SetActive(true);
    }
    public IEnumerable OpenShop(bool s)
    {
        shop = s;
        storeUI.SetActive(shop);
        yield return null;
    }
    public IEnumerable InputUI(bool b)
    {
        textInput.SetActive(b);
        yield return null;
    }

    public IEnumerable TxtNewValue(int i)
    {
        ammoTxt.text = i.ToString();
        yield return null;
    }

    public bool? OnShop()
    {
        return shop;
    }
    
}

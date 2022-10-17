using System.Collections;
using Script.GameManager;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour, IUI
{
    [Header("Input Text")]
    [SerializeField]private GameObject textInput;
    
    [Header("Store UI")]
    [SerializeField]private GameObject storeUI;
    
    [Header("Configs UI")]
    [SerializeField]private GameObject pauseUI;
    
    [Header("HUD Values")]
    [SerializeField]private TextMeshProUGUI coinTxt;

    private bool shop;
    private int value;
    
    public static UIManager uiManager;

    void Awake()
    {
        if (uiManager == null)
        {
            uiManager = this;
        }
        
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

    public bool? OnShop()
    {
        return shop;
    }
    
}

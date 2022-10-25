using System.Collections;
using Ez;
using Script.GameManager;
using Script.Weapon;
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
    [SerializeField]private TextMeshProUGUI ammoTxt;

    private bool shop;
    private int value;
    
    public static UIManager uiManager;

    void Awake()
    {
        if (uiManager == null)
        {
            uiManager = this;
        }
        Game.Manager.pauseGame.AddListener(PausingGame);
    }

    void Update()
    {
        coinTxt.text = Game.coins.ToString();
    }

    public void OnWeaponAmmoTest(WeaponSO weapon)
    {
        ammoTxt.text = weapon.ammo.ToString() + " / " + weapon.maxAmmo.ToString();
    }

    public void CloseUI()
    {
        shop = false;
    }

    void PausingGame()
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

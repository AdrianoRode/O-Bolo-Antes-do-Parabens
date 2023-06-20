using System.Collections;
using DG.Tweening;
using Script.Player;
using Syrinj;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Ez;

public class UIManager : MonoBehaviour, IUI
{
    [Header("Input Text")]
    [SerializeField]private GameObject textInput;
    
    [Header("Store UI")]
    [SerializeField]private GameObject storeUI;
    
    [Header("Configs UI")]
    [SerializeField]private GameObject pauseUI;

    [Header("HUD Values")] 
    [SerializeField]private TextMeshProUGUI playerLife;
    [SerializeField]private TextMeshProUGUI coinTxt;
    [SerializeField]private TextMeshProUGUI ammoTxt;
    [SerializeField]private TextMeshProUGUI waterTxt;
    [SerializeField]private Slider staminaBar;
    [SerializeField]private Slider bossLifeBar;
    [SerializeField]private Image weaponIcon;
    [Inject]public GameManagerSO game;

    [Header("Objectives Text")] 
    [SerializeField]private TextMeshProUGUI objectiveAccomplished;
    [SerializeField]private TextMeshProUGUI newObjectiveWarning;
    [SerializeField]private TextMeshProUGUI nextObjective;
    private PlayerLife pl;
    private PlayerControl pc;
    public GameObject boss;
    private bool shop;
    private bool bossIsAlive;

    void Start()
    {
        pl = FindObjectOfType<PlayerLife>();
        pc = FindObjectOfType<PlayerControl>();
        UpdatePlayerLifeTxt();
        EarnCoin();
      
    }

    void Update()
    {
        if(boss != null)
        {
            var bh = boss.Request<IArmor, int?>(_=>_.GetHealth());
            bossLifeBar.value = (float)(bh ?? 0);
        }    
        staminaBar.value = pc.stamina;
    }
    public void UpdatePlayerLifeTxt()
    {
        playerLife.text = "Vidas: " + pl.health.Value.ToString();
    }

    public void EarnCoin()
    {
        coinTxt.text = "Brigadeiros: " + game.coins.Value.ToString();
    }
    public void OnWeaponAmmoTest(WeaponSO weapon)
    {
        ammoTxt.text = weapon.ammo.ToString() + " / " + weapon.reserveAmmo.ToString();
        weaponIcon.sprite = weapon.icon;
    }

    public void ObjectiveAccomplished()
    {
        var objectiveFinished = objectiveAccomplished.transform;
        var anotherObjective = newObjectiveWarning.transform;
        var sequence = DOTween.Sequence();
        
        nextObjective.text = "Objetivo: Derrote o restante dos oponentes!";

        sequence.Append(objectiveFinished.DOLocalMoveY(480f, 150f * Time.deltaTime).SetEase(Ease.OutElastic))
            .Append(objectiveFinished.DOLocalMoveY(580f, 150f * Time.deltaTime).SetEase(Ease.InElastic))
            .Append(anotherObjective.DOLocalMoveY(480f, 150f * Time.deltaTime).SetEase(Ease.OutElastic))
            .Append(anotherObjective.DOLocalMoveY(580f, 150f * Time.deltaTime).SetEase(Ease.InElastic)).OnComplete(DisableObjectiveTxt);
        
    }

    void DisableObjectiveTxt()
    {
        objectiveAccomplished.gameObject.SetActive(false);
        newObjectiveWarning.gameObject.SetActive(false);
    }

    public void CloseUI()
    {
        shop = false;
    }
    public void BossAlive()
    {
        bossIsAlive = true;
    }

    public void PausingGame()
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

    public IEnumerable WaterCollected(bool active)
    {
        waterTxt.gameObject.SetActive(active);

        var water = waterTxt.transform;
        var sequence = DOTween.Sequence();
        waterTxt.text = "Você coletou água para a pistola!";

        sequence.Append(water.DOLocalMoveY(-490f, 150f * Time.deltaTime).SetEase(Ease.OutElastic))
            .Append(water.DOLocalMoveY(-590f, 150f * Time.deltaTime).SetEase(Ease.InElastic));
        yield return null;
    }

    public bool? OnShop()
    {
        return shop;
    }
    
}

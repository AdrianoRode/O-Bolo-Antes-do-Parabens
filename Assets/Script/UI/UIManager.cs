using System.Collections;
using DG.Tweening;
using Syrinj;
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
    [Inject]public GameManagerSO game;

    [Header("Objectives Text")] 
    [SerializeField]private TextMeshProUGUI objectiveAccomplished;
    [SerializeField]private TextMeshProUGUI newObjective;

    private bool shop;
    private int value;

    public void EarnCoin()
    {
        coinTxt.text = "Moedas: " + game.coins.Value.ToString();
    }
    public void OnWeaponAmmoTest(WeaponSO weapon)
    {
        ammoTxt.text = weapon.ammo.ToString() + " / " + weapon.maxAmmo.ToString();
    }

    public void ObjectiveAccomplished()
    {
        var objectiveFinished = objectiveAccomplished.transform;
        var anotherObjective = newObjective.transform;
        var sequence = DOTween.Sequence();
        
        sequence.Append(objectiveFinished.DOLocalMoveY(480f, 150f * Time.deltaTime).SetEase(Ease.OutElastic))
            .Append(objectiveFinished.DOLocalMoveY(580f, 150f * Time.deltaTime).SetEase(Ease.InElastic))
            .Append(anotherObjective.DOLocalMoveY(480f, 150f * Time.deltaTime).SetEase(Ease.OutElastic))
            .Append(anotherObjective.DOLocalMoveY(580f, 150f * Time.deltaTime).SetEase(Ease.InElastic)).OnComplete(DisableObjectiveTxt);
    }

    void DisableObjectiveTxt()
    {
        objectiveAccomplished.gameObject.SetActive(false);
        newObjective.gameObject.SetActive(false);
    }

    public void CloseUI()
    {
        shop = false;
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

    public bool? OnShop()
    {
        return shop;
    }
    
}

using System.Collections;
using DG.Tweening;
using ScriptableObjectArchitecture;
using Syrinj;
using UnityEngine;

public class ObjectiveLogic : MonoBehaviour, IArmor
{
    private int health = 150;
    private Material takeDamage;
    public BoolVariable objectiveCompleted;

    void Start()
    {
        takeDamage = GetComponent<MeshRenderer>().material;
    }
    public IEnumerable ApplyDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            objectiveCompleted.Value = true;
            gameObject.SetActive(false);
        }
        var sequence = DOTween.Sequence();
        sequence.Append(takeDamage.DOColor(Color.red, 2f * Time.deltaTime))
            .Append(takeDamage.DOColor(takeDamage.color, 2f * Time.deltaTime));
        
        yield return null;
    }

    public int? GetHealth()
    {
        return health;
    }
}

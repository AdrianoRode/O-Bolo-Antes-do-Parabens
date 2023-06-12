using System.Collections;
using DG.Tweening;
using ScriptableObjectArchitecture;
using Syrinj;
using UnityEngine;

public class ObjectiveLogic : MonoBehaviour, IArmor
{
    private int health = 150;
    private Renderer renderer;
    public BoolVariable objectiveCompleted;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }
    public IEnumerable ApplyDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            objectiveCompleted.Value = true;
            gameObject.SetActive(false);
        }
        renderer.material.SetColor("_BaseColor", Color.red);
        StartCoroutine(ChangeColor());
        
        yield return null;
    }

    public int? GetHealth()
    {
        return health;
    }

    public IEnumerator ChangeColor()
    {
        yield return new WaitForSeconds(2f * Time.deltaTime);
        renderer.material.SetColor("_BaseColor", Color.white);
    }
}

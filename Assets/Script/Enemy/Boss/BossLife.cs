using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using ScriptableObjectArchitecture;
using UnityEngine.SceneManagement;

public class BossLife : MonoBehaviour, IArmor
{
    public int health = 10;
    [SerializeField]private GameObject[] drop;
    private int life;
    private NewControlSystem newControlSystem;
    public BoolVariable isDead;
    public Renderer[] renderer;   
    public delegate void LifeCount();
    public event LifeCount LifeCounted;

    public IEnumerable ApplyDamage(int damage)
    {
        health -= damage;
        for(int i = 0; i < renderer.Length; i++)
        {
            renderer[i].material.SetColor("_BaseColor", Color.red);
        }

        int r = UnityEngine.Random.Range(0, drop.Length);
        if (health <= 0)
        {
            SceneManager.LoadScene("Menu");
            isDead.Value = true;
            Instantiate(drop[r], transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }

        isDead.Value = false;
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

        for(int i = 0; i < renderer.Length; i++)
        {
            renderer[i].material.SetColor("_BaseColor", Color.white);
        }
    }
}


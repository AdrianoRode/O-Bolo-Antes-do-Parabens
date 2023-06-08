using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using ScriptableObjectArchitecture;
using UnityEngine.SceneManagement;

namespace Script.Enemy
{
    public class BossLife : MonoBehaviour, IArmor
    {
        public int health = 10;
        [SerializeField]private GameObject[] drop;
        private int life;
        private Material takeDamage;
        private NewControlSystem newControlSystem;
        public BoolVariable isDead;
        public BoolVariable test;
        
        public delegate void LifeCount();
        public event LifeCount LifeCounted;
        void Start()
        {
            takeDamage = GetComponent<MeshRenderer>().material;
            newControlSystem = GetComponent<NewControlSystem>();
            
            LifeCounted += newControlSystem.OnLifeCounted;
            life = health / 2;
        }

        public IEnumerable ApplyDamage(int damage)
        {
            health -= damage;
            Debug.Log("Vida do boss: " + health);

            int r = UnityEngine.Random.Range(0, drop.Length);
            if (health <= 0)
            {
                SceneManager.LoadScene("Menu");
                isDead.Value = true;
                Instantiate(drop[r], transform.position, Quaternion.identity);
                gameObject.SetActive(false);
            }

            if (health <= 1500)
            {
                test.Value = true;
            }

            isDead.Value = false;
            var sequence = DOTween.Sequence();
            sequence.Append(takeDamage.DOColor(Color.red, 2f * Time.deltaTime))
                .Append(takeDamage.DOColor(Color.white, 2f * Time.deltaTime));
  
            yield return null;
        }

        public int? GetHealth()
        {
            return health;
        }
        
    }
}

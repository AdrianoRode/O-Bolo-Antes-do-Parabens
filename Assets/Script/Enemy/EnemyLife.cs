using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using ScriptableObjectArchitecture;
using UnityEngine.SceneManagement;

namespace Script.Enemy
{
    public class EnemyLife : MonoBehaviour, IArmor
    {
        [SerializeField]private int health = 10;
        [SerializeField]private GameObject[] drop;
        private Material takeDamage;
        public BoolVariable isDead;

        void Start()
        {
            takeDamage = GetComponent<MeshRenderer>().material;
        }

        public IEnumerable ApplyDamage(int damage)
        {
            health -= damage;
            int r = UnityEngine.Random.Range(0, drop.Length);
            if (health <= 0)
            {
                isDead.Value = true;
                Instantiate(drop[r], transform.position, Quaternion.identity);
                gameObject.SetActive(false);
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

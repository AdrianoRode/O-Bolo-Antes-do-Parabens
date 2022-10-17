using System;
using System.Collections;
using Script.GameManager;
using UnityEngine;
using DG.Tweening;

namespace Script.Enemy
{
    public class EnemyLife : MonoBehaviour, IArmor
    {
        [SerializeField]private int health = 10;
        [SerializeField]private GameObject drop;
        private Material takeDamage;


        void Start()
        {
            takeDamage = GetComponent<MeshRenderer>().material;
        }
   
        public IEnumerable ApplyDamage(int damage)
        {
            health -= damage;
            
            if (health <= 0)
            {
                Game.Manager.OnDied();
                Instantiate(drop, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
            }

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

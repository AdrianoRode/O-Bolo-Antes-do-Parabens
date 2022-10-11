using System;
using System.Collections;
using Script.GameManager;
using UnityEngine;

namespace Script.Enemy
{
    public class EnemyLife : MonoBehaviour, IArmor
    {
        [SerializeField]private int health = 10;
        [SerializeField]private MeshRenderer takeDamage;
        [SerializeField]private GameObject drop;

        public delegate void Die();
        public event Die Died;
        
        public IEnumerable ApplyDamage(int damage)
        {
            health -= damage;
            
            if (health <= 0)
            {
                Game.Manager.OnDied();
                Instantiate(drop, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
            }
            takeDamage.material.color = Color.red;
            yield return new WaitForSeconds(0.7f * Time.deltaTime);
        
            takeDamage.material.color = Color.white;
            yield return null;
        }
    
        public int? GetHealth()
        {
            return health;
        }
        
    }
}

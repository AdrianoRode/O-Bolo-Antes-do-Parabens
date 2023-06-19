using System.Collections;
using UnityEngine;
using ScriptableObjectArchitecture;

namespace Script.Enemy
{
    public class EnemyLife : MonoBehaviour, IArmor
    {
        [SerializeField]private int health = 10;
        [SerializeField]private GameObject[] drop;
        public BoolVariable isDead;
        public new Renderer renderer;

        public IEnumerable ApplyDamage(int damage)
        {
            health -= damage;
            int r = UnityEngine.Random.Range(0, drop.Length);
            StartCoroutine(ChangeColor());

            if (health <= 0)
            {
                isDead.Value = true;
                Instantiate(drop[r], transform.position, Quaternion.identity);
                gameObject.SetActive(false);
            }
            
            isDead.Value = false;
            renderer.material.SetColor("_BaseColor", Color.red);
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
}

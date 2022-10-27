using System.Collections;
using UnityEngine;

namespace Script.Player
{
    public class PlayerLife : MonoBehaviour, IArmor
    {
        [SerializeField]private MeshRenderer takeDamage;
        private int health = 1000;
        private int shield;
        private bool isInvulnerable = false;
        public float tempoDeInvulnerabilidade = 5f;
    
        public IEnumerable ApplyDamage(int damage)
        {
            if (isInvulnerable == false)
            {
                Debug.Log("Levei dano: " + health);
            
                health -= damage;
                takeDamage.material.color = Color.red;
  
                yield return new WaitForSeconds(0.7f * Time.deltaTime);
        
                takeDamage.material.color = Color.white;
            
                isInvulnerable = true;
                StartCoroutine(Invulnerable());
            
            }
        
            else
            {
                yield return null;
            }

        }
    
        public int? GetHealth()
        {
            return health; 
        }

        public IEnumerator Invulnerable()
        {
            Debug.Log("Estou invulnerável!");
            yield return new WaitForSeconds(tempoDeInvulnerabilidade * Time.deltaTime);
            isInvulnerable = false;
        }
    }
}
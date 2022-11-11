using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Script.Player
{
    public class PlayerLife : MonoBehaviour, IArmor
    {
        private Material takeDamage;
        private int health = 3;
        private int shield;
        private bool isInvulnerable;
        private float invulnerabilityTime = 150f;
        
        void Start()
        {
            takeDamage = GetComponent<MeshRenderer>().material;
        }

        public IEnumerable ApplyDamage(int damage)
        {
            if (isInvulnerable == false)
            {
                Debug.Log("Levei dano: " + health);
            
                health -= damage;

                var sequence = DOTween.Sequence();
                /*sequence.Append(takeDamage.DOColor(Color.red, 2f * Time.deltaTime))
                    .Append(takeDamage.DOColor(Color.clear, 2f * Time.deltaTime));*/
                takeDamage.DOColor(Color.red, 2f * Time.deltaTime);
                
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
            isInvulnerable = true;
            yield return new WaitForSeconds(invulnerabilityTime * Time.deltaTime);
            takeDamage.DOColor(Color.white, 2f * Time.deltaTime);
            isInvulnerable = false;
        }
    }
}
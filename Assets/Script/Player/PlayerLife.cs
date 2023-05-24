using System.Collections;
using DG.Tweening;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace Script.Player
{
    public class PlayerLife : MonoBehaviour, IArmor
    {
        public IntVariable health;

        public Material[] takeDamage;
        private int shield;
        private bool isInvulnerable;
        private float invulnerabilityTime = 150f;
        
        void Start()
        {
            health.Value = health.DefaultValue;
        }

        public IEnumerable ApplyDamage(int damage)
        {
            if (isInvulnerable == false)
            {
                health.Value -= damage;

                var sequence = DOTween.Sequence();

                for(int i = 0; i < takeDamage.Length; i++)
                {
                    takeDamage[i].DOColor(Color.red, 2f * Time.deltaTime);
                }
                /*sequence.Append(takeDamage.DOColor(Color.red, 2f * Time.deltaTime))
                    .Append(takeDamage.DOColor(Color.clear, 2f * Time.deltaTime));*/
                
                StartCoroutine(Invulnerable());
            
            }
            else
            {
                yield return null;
            }

        }
    
        public int? GetHealth()
        {
            return health.Value; 
        }

        public IEnumerator Invulnerable()
        {
            isInvulnerable = true;
            yield return new WaitForSeconds(invulnerabilityTime * Time.deltaTime);
            for(int i = 0; i < takeDamage.Length; i++)
            {
                takeDamage[i].DOColor(Color.white, 2f * Time.deltaTime);
            }
            isInvulnerable = false;
        }
    }
}
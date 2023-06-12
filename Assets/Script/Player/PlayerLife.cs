using System.Collections;
using DG.Tweening;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace Script.Player
{
    public class PlayerLife : MonoBehaviour, IArmor
    {
        public IntVariable health;
        public Renderer[] child;
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
                for(int i = 0; i < child.Length; i++)
                {
                    child[i].material.SetColor("_BaseColor", Color.red);
                }

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
            
            for(int i = 0; i < child.Length; i++)
            {
                child[i].material.SetColor("_BaseColor", Color.white);
            }
            isInvulnerable = false;
        }
    }
}
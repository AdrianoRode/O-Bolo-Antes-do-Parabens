using System.Collections;
using Ez;
using UnityEngine;

namespace Script.Weapon
{
    public class Bullet : MonoBehaviour
    {
        public int damage;
        public int speed = 50;

        private Vector3 mPrevPos;
        [SerializeField]private WeaponSO weapon;

        void Start()
        {
            StartCoroutine(DisableBullet());
        }
        void Update()
        {
            damage = weapon.damage;
            mPrevPos = transform.position;
        
            transform.Translate(new Vector3(0, 0, speed) * Time.deltaTime, Space.Self);
        
            RaycastHit[] hits = Physics.RaycastAll(new Ray(mPrevPos, (transform.position - mPrevPos).normalized), (transform.position - mPrevPos).magnitude);
        
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.CompareTag("Hitable"))
                {
                    hits[i].collider.gameObject.Send<IArmor>(_=>_.ApplyDamage(damage));
                    gameObject.SetActive(false);
                }
            }

        }

        IEnumerator DisableBullet()
        {
            yield return new WaitForSeconds(50f * Time.deltaTime);
            gameObject.SetActive(false);
        }

    }
}

using System.Collections;
using Ez;
using UnityEngine;

namespace Script.Weapon
{
    public class Gun : MonoBehaviour, IGun
    {
        [Header("Weapon")]
        [SerializeField]private WeaponSO weapon;
        private bool canShoot = true;
    
        [Header("Bullet")]
        [SerializeField]private TrailRenderer bulletTrail;
        [SerializeField]private Transform[] localFire;
        private int damage;
        private UIManager uiManager;

        public delegate void WeaponAmmo(WeaponSO weaponSo);
        public event WeaponAmmo WeaponAmmoTest;

        void Awake()
        {
            uiManager = FindObjectOfType<UIManager>();
        }

        void Start()
        {
            weapon.DefaultAttributes();
        }

        void OnEnable()
        {
            canShoot = true;
            WeaponAmmoTest += uiManager.OnWeaponAmmoTest;
        }
    
        void Update()
        {
            damage = weapon.damage;
            WeaponAmmoTest?.Invoke(weapon);
        }

        public IEnumerable Fire()
        {
            RaycastHit hit;
            int layerMask = 1 << 2;
            layerMask = ~layerMask;
        
            if (canShoot && weapon.ammo > 0)
            {
                for (int i = 0; i < localFire.Length; i++)
                {
                    if (Physics.Raycast(localFire[i].position, localFire[i].TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
                    {
                        if (hit.transform.gameObject.CompareTag("Hitable") || hit.transform.gameObject.CompareTag("Player"))
                        {
                            hit.collider.gameObject.Send<IArmor>(_=>_.ApplyDamage(damage));
                        }
                        weapon.ammo--;
                        TrailRenderer trail = Instantiate(bulletTrail, localFire[i].position, Quaternion.identity);
                        StartCoroutine(SpawnTrail(trail, hit));
                    }  
                }
            
                canShoot = false;
                StartCoroutine(Cadence().GetEnumerator());
            }

            if(weapon.ammo <= 0)
            {
                StartCoroutine(Reload().GetEnumerator());
            }
            yield return null;
        }

        public IEnumerable Reload()
        {
            yield return new WaitForSeconds(100 * Time.deltaTime);

            int i = weapon.defaultAmmo - weapon.ammo;
            if(weapon.maxAmmo - i < 0)
            {
                i = weapon.maxAmmo;
            }
            weapon.ammo += i;
            weapon.maxAmmo -= i;
            
            yield return null;
        }

        public IEnumerable Cadence()
        {
            yield return new WaitForSeconds(weapon.cadence * Time.deltaTime);
            canShoot = true;
        }

        IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit)
        {
            float time = 0;
            Vector3 startPosition = trail.transform.position;
        
            while(time < 1)
            {
                trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
                time += Time.deltaTime / trail.time;
            
                yield return null;
            }
            trail.transform.position = hit.point;
        
            Destroy(trail.gameObject, trail.time);
        }

        void OnDisable()
        {
            WeaponAmmoTest -= uiManager.OnWeaponAmmoTest;
        }
    }
}

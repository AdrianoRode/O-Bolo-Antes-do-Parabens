using Ez;
using Script.Player;
using UnityEngine;
using UnityEngine.AI;

namespace Script.Enemy
{
    public class EnemyControl : MonoBehaviour
    {
        [SerializeField]private AudioSource walkSound;
        [SerializeField]private AudioClip[] walkGrass;
        [SerializeField]private AudioClip[] walkConcrete;
        private GameObject player;
        private NavMeshAgent navmesh;
        private PlayerLife playerLife;
        private Animator anim;
        private string ground = "";
        

        void Awake()
        {
            navmesh = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
            playerLife = FindObjectOfType<PlayerLife>();
        }

        void Start()
        {
            player = GameObject.Find("Player");
            var randomMove = Random.Range(0, 3);
            anim.SetInteger("random", randomMove);
        }
    
        void Update()
        {
            MoveTowardPlayer();
            AttackingPlayer();
        }
        void MoveTowardPlayer()
        {
            navmesh.destination = player.transform.position;
        }

        public void WalkSound()
        {
            if(ground == "Grass")
            {
                walkSound.PlayOneShot(walkGrass[Random.Range(0, walkGrass.Length)]);
            }

            else if(ground == "Concrete")
            {
                walkSound.PlayOneShot(walkConcrete[Random.Range(0, walkConcrete.Length)]);
            }
            
        }

        void AttackingPlayer()
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 2f)
            {
                playerLife.gameObject.Send<IArmor>(_ => _.ApplyDamage(10));
            }
        }

        void OnTriggerEnter(Collider col)
        {
            if(col.gameObject.CompareTag("Grass"))
            {
                ground = "Grass";
            }

            else if(col.gameObject.CompareTag("Concrete"))
            {
                ground = "Concrete";
            }
        }
    }
}

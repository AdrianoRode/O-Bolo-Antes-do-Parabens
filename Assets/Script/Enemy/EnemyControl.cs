using Ez;
using Script.Player;
using UnityEngine;
using UnityEngine.AI;

namespace Script.Enemy
{
    public class EnemyControl : MonoBehaviour
    {
        private GameObject player;
        private NavMeshAgent navmesh;
        private PlayerLife playerLife;
        private Animator anim;

        void Awake()
        {
            navmesh = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
            playerLife = FindObjectOfType<PlayerLife>();
        }

        void OnEnable()
        {
            var randomMove = Random.Range(0, 3);
            anim.SetInteger("random", randomMove);
        }
        void Start()
        {
            player = GameObject.Find("Player");
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

        void AttackingPlayer()
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 2f)
            {
                playerLife.gameObject.Send<IArmor>(_ => _.ApplyDamage(10));
            }
        }
    }
}

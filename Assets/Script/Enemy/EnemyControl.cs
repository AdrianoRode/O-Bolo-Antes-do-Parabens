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
    
        void Awake()
        {
            navmesh = GetComponent<NavMeshAgent>();
            playerLife = FindObjectOfType<PlayerLife>();
        }

        void Start()
        {
            player = GameObject.Find("Player");
        }
    
        void Update()
        {
            navmesh.destination = player.transform.position;
            
            if (Vector3.Distance(transform.position, player.transform.position) < 1f)
            {
                playerLife.gameObject.Send<IArmor>(_ => _.ApplyDamage(1));
            }
            
        }

    }
}

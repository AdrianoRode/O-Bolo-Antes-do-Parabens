using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class BossControl : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private GameObject player;
    [SerializeField]private GameObject enemy;

    public int randomValue;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        navMeshAgent.destination = player.transform.position;
    }
    
    public void RandomState()
    {
        randomValue = Random.Range(1, 4);
        Debug.Log(randomValue);
    }
    public void Attack2()
    {
        var playerPos = player.transform.position;
        transform.DOLocalJump(new Vector3(playerPos.x, transform.position.y, playerPos.z), 10f, 5, 4f);
    }

    public void Attack3()
    {
        var position = player.transform.position;
        transform.DOLocalMove(new Vector3(position.x, transform.position.y, position.z), 1f);
    }

}

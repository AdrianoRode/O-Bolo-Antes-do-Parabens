using Ez;
using UnityEngine;
using UnityEngine.AI;

public class BossChildControl : MonoBehaviour
{
    private GameObject _player;
    private NavMeshAgent _navmesh;
    private bool _alreadyCollided;
    private float _timeBeforeAttack = 3.0f;
    public Transform localWeapon;

    void Awake()
    {
        _navmesh = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        _player = GameObject.Find("Player");
    }
    
    void Update()
    {
        if(_alreadyCollided)
        {
            _timeBeforeAttack -= Time.deltaTime;

            if(_timeBeforeAttack <= 0)
            {
                AttackWithWeapon();
            }
        }

        MoveTowardPlayer();
    }
    void MoveTowardPlayer()
    {
        _navmesh.destination = _player.transform.position;
    }

    void AttackWithWeapon()
    {
        float random = Random.Range(0.02f, 20f);
        Vector3 lookDirection = _player.transform.position - transform.position;
        lookDirection.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, random * Time.deltaTime);
        gameObject.Send<IGun>(_=>_.Fire(), true);
    }


    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player") && !_alreadyCollided)
        {
            _navmesh.speed = 2.0f;
            var h = col.gameObject.Request<IInventory, GameObject>(_=>_.GetWeapon());
            GameObject go = Instantiate(h, localWeapon.position, transform.localRotation) as GameObject;
            go.transform.parent = GameObject.Find("BossChild(Clone)").transform;
            go.SetActive(true);
            _alreadyCollided = true;
        }
    }

}

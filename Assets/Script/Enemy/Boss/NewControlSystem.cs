using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using DG.Tweening;

public class NewControlSystem : MonoBehaviour
{
    public enum States {IDLE, WALKING, ATTACKING}
    public States teste; 
    public Transform target;
    public int _speed = 100;

    public GameObject enemy;

    private NavMeshAgent _navMeshAgent;
    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        var step = _speed * Time.deltaTime;
        switch(teste)
        {
            case States.IDLE:
                Debug.Log("Em idle");
                break;

            case States.WALKING:
                _navMeshAgent.destination = target.position;
                _navMeshAgent.speed = 5f;
                break;

            case States.ATTACKING:
                _navMeshAgent.speed = 0f;
                Instantiate(enemy, transform.position, Quaternion.identity);
                break;

            default:
                break;
        }
        SelectState();
    }
    
    void SelectState()
    {
        if(Vector3.Distance(transform.position, target.position) > 5f)
        {
            teste = States.WALKING;
        }

        else
        {
            teste = States.IDLE;
        }
    }
}

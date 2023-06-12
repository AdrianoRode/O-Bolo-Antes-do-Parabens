using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using Ez;

public class NewControlSystem : MonoBehaviour
{
    public enum States {IDLE, WALKING, ATTACKING1, ATTACKING2, ATTACKING3}
    public States actualState; 
    public Transform target;
    public Transform Spawn;
    public GameObject enemy;
    public GameObject candy;
    private GameObject _player;
    private NavMeshAgent _navMeshAgent;
    private Animator anim;
    private float speed = 5f;
    public bool canControl = false;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        _player = GameObject.Find("Player");
        StartCoroutine(SelectState());
    }
    void Update()
    {
        if(!canControl)
        {
            return;
        }
        _navMeshAgent.speed = speed;
        _navMeshAgent.destination = target.position;
        AttackingPlayer();
    }

    void AttackingPlayer()
    {
        /*if (Vector3.Distance(transform.position, _player.transform.position) < 3.2f && actualState == States.WALKING)
        {
            _player.Send<IArmor>(_ => _.ApplyDamage(20));
            anim.SetTrigger("Attack");
        }
        else{
            //anim.SetTrigger("Walk");
        }*/
    }
    public void EnableControl()
    {
        canControl = true;
    }
    public void DisableControl()
    {
        canControl = false;
    }
    IEnumerator SelectState()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);
        anim.SetTrigger("Walk");

        speed = 5f;

        yield return new WaitForSeconds(10f);

        if(distanceToPlayer > 25f)
        {
            State(States.ATTACKING1);
        }

        else if(distanceToPlayer > 10f && distanceToPlayer < 25f)
        {
            State(States.ATTACKING2);
        }

        else if(distanceToPlayer < 6f)
        {
            State(States.ATTACKING3);
        }

        else
        {
            StartCoroutine(SelectState());
        }
    }

    void State(States seila)
    {
        actualState = seila;

        switch(actualState)
        {
            case States.WALKING:
                _navMeshAgent.speed = speed;
                _navMeshAgent.destination = target.position;
                anim.SetTrigger("Walk");
                break;

            case States.ATTACKING1:
                speed = 0f;
                _navMeshAgent.destination = target.position;

                StartCoroutine(Attack1());
                break;

            case States.ATTACKING2:
                speed = 0f;
                for(int i = 0; i < 5; i++)
                {
                    Instantiate(enemy, Spawn.position, Quaternion.identity);
                }
                StartCoroutine(Attack2());
                break;

            case States.ATTACKING3:
                _player.Send<IDebuff>(_=>_.ApplyStun(false));
                anim.SetTrigger("Hug");
                StartCoroutine(Attack3()); 
                break;

            default:
                break;
        }
    }
    IEnumerator Walk()
    {
        yield return new WaitForSeconds(4f);
        StartCoroutine(SelectState());
    }

    IEnumerator Attack1()
    {
        yield return new WaitForSeconds(2f);
        speed = 20f;
        yield return new WaitForSeconds(4f);
        actualState = States.WALKING;

        StartCoroutine(SelectState());
    }

    IEnumerator Attack2()
    {
        yield return new WaitForSeconds(4f);
        speed = 5f;
        actualState = States.WALKING;

        StartCoroutine(SelectState());
    }

    IEnumerator Attack3()
    {
        yield return new WaitForSeconds(5f);
        _player.Send<IDebuff>(_=>_.ApplyStun(true));
        actualState = States.WALKING;

        StartCoroutine(SelectState());
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            _player.Send<IArmor>(_ => _.ApplyDamage(20));
            anim.SetTrigger("Attack");
        }
    }
    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("Walk");
        }
    }
}

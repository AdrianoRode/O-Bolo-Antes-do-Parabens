using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using Ez;

public class NewControlSystem : MonoBehaviour
{
    public enum States {IDLE, WALKING, ATTACKING1, ATTACKING2, ATTACKING3, ATTACKING4}
    public States actualState; 
    public Transform target;
    public Transform Spawn;
    public GameObject enemy;
    public GameObject candy;
    private GameObject _player;
    private NavMeshAgent _navMeshAgent;
    private float speed = 5f;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _player = GameObject.Find("Player");
        StartCoroutine(SelectState());
    }
    void Update()
    {
        _navMeshAgent.speed = speed;
        _navMeshAgent.destination = target.position;

        float nai = Vector3.Distance(transform.position, _player.transform.position);
        Debug.Log(nai);
    }


    void AttackingPlayer()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) < 3.2f)
        {
            _player.Send<IArmor>(_ => _.ApplyDamage(20));
        }
    }

    public void OnLifeCounted()
    {
        Debug.Log("Vida em 50% ou menos!");
    }

    IEnumerator SelectState()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);
        speed = 5f;

        yield return new WaitForSeconds(10f);

        if(distanceToPlayer > 25f)
        {
            Teste(States.ATTACKING1);
        }

        else if(distanceToPlayer > 10f && distanceToPlayer < 25f)
        {
            Teste(States.ATTACKING2);
        }

        else if(distanceToPlayer < 6f)
        {
            Teste(States.ATTACKING3);
        }

        else
        {
            StartCoroutine(SelectState());
        }
    }

    void Teste(States seila)
    {
        actualState = seila;

        switch(actualState)
        {
            case States.ATTACKING1:
                speed = 0f;

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

                StartCoroutine(Attack3()); 
                break;

            case States.ATTACKING4:
                //Instantiate(candy, Spawn.position, Quaternion.identity);
                //A tia pouquissimas vezes atira brigadeiros que ajudam o player.
                break;

            default:
                break;
        }
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
}

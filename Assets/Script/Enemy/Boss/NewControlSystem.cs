using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using Ez;

public class NewControlSystem : MonoBehaviour
{
    public enum States {IDLE, WALKING, ATTACKING1, ATTACKING2, ATTACKING3, ATTACKING4}
    public States teste; 
    public Transform target;
    public Transform Spawn;
    public int _speed = 100;
    public GameObject enemy;
    public GameObject candy;
    private GameObject _player;
    private NavMeshAgent _navMeshAgent;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _player = GameObject.Find("Player");
    }

    void Update()
    {
        var step = _speed * Time.deltaTime;
        switch(teste)
        {
            case States.IDLE:
                break;

            case States.WALKING:
                _navMeshAgent.speed = 5f;
                _navMeshAgent.destination = target.position;
                break;

            case States.ATTACKING1:
                transform.Translate(Vector3.forward * 40f * Time.deltaTime);
                StartCoroutine(Attack1());
                break;
            case States.ATTACKING2:
                _navMeshAgent.speed = 0f;
                for(int i = 0; i < 5; i++)
                {
                    Instantiate(enemy, Spawn.position, Quaternion.identity);
                }
                teste = States.WALKING;
                break;
            case States.ATTACKING3:
                _navMeshAgent.speed = 0f;
                if(Vector3.Distance(transform.position, _player.transform.position) < 10f)
                {
                    _player.Send<IDebuff>(_=>_.ApplyStun(false));
                }
                StartCoroutine(Attack3());
                
                //Tia tira foto que pega em área, caso acerte o player, ele vai ficar stunnado por alguns segundos.
                break;
            case States.ATTACKING4:
                //Instantiate(candy, Spawn.position, Quaternion.identity);
                //A tia pouquissimas vezes atira brigadeiros que ajudam o player.
                break;

            default:
                break;
        }
    }

    public void OnLifeCounted()
    {
        Debug.Log("Vida em 50% ou menos!");
    }

    IEnumerator Attack1()
    {
        yield return new WaitForSeconds(40f * Time.deltaTime);
        teste = States.WALKING;
    }

    IEnumerator Attack3()
    {
        _navMeshAgent.speed = 5f;
        yield return new WaitForSeconds(250f * Time.deltaTime);
        _player.Send<IDebuff>(_=>_.ApplyStun(true));
        teste = States.WALKING;
    }
}

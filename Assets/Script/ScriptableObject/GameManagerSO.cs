using UnityEngine;
using ScriptableObjectArchitecture;
using Trisibo;

[CreateAssetMenu]
public class GameManagerSO : ScriptableObject
{
    [Header("Scene Management")]
    public SceneField sceneThingy;

    [Header("Player status")]
    public IntVariable coins;

    [Header("Enemy status")] 
    public BoolVariable enemyDied;

    [Header("Objective")] 
    public BoolVariable objectiveLogic;

    [Header("Objects values")]
    public GameObjectVariable enemy;
    public GameObjectVariable[] enemySpawn;

}


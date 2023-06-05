using System;
using Script.Enemy;
using Script.Player;
using UnityEngine;

namespace Script.GameManager
{
    public class EventManager : MonoBehaviour
    {
        private PlayerControl playerControl; 
        private UIManager uiManager;
        private BossLife bossLife;
        private NewControlSystem newControlSystem;

        void Start()
        {
            playerControl = FindObjectOfType<PlayerControl>();
            uiManager = FindObjectOfType<UIManager>();
            bossLife = FindObjectOfType<BossLife>();
            newControlSystem = FindObjectOfType<NewControlSystem>();

            //bossLife.LifeCounted += newControlSystem.OnLifeCounted;
    
        }

    }
}

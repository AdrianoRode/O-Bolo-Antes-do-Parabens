using Script.Enemy;
using Script.Player;
using UnityEngine;

namespace Script.GameManager
{
    public class EventManager : MonoBehaviour
    {
        private PlayerControl playerControl;
        private EnemyLife enemyLife;
        private UIManager uiManager;

        void Start()
        {
            playerControl = FindObjectOfType<PlayerControl>();
            enemyLife = FindObjectOfType<EnemyLife>();
            uiManager = FindObjectOfType<UIManager>();
        
            playerControl.CoinCollected += Game.Manager.OnCoinCollected;
            playerControl.GamePaused += Game.Manager.OnGamePaused;
            playerControl.GamePaused += uiManager.OnGamePaused;
            enemyLife.Died += Game.Manager.OnDied;
        }
    }
}

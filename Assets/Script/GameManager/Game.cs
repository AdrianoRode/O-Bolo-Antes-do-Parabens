using Ez;
using Script.Player;
using ScriptableObjectArchitecture;
using Syrinj;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

namespace Script.GameManager
{
    public class Game : MonoBehaviour
    {
        private bool objectiveCompleted;
        private bool cutsceneStillNotPlayed = true;
        private int enemiesDied;
        private UIManager uiManager;
        private PlayerLife playerLife;
        [Provides, SerializeField]private GameManagerSO gameSo;
        public PlayableDirector[] cutscene;
        public UnityEvent restartGame;
        public UnityEvent pauseGame;
        public UnityEvent cutsceneOnPlay;
        public UnityEvent cutsceneOnStop;
        public UnityEvent TestNavmesh;
        public GameObject[] houses;
        public bool bossIsAlive = false;
        public BossLife bossLife;

        void Start()
        {
            uiManager = FindObjectOfType<UIManager>();
            playerLife = FindObjectOfType<PlayerLife>();

            var r = Random.Range(0, houses.Length);
            var position = new Vector3(-6.4f, -0.44f, 0f);
            Instantiate(houses[r], houses[r].transform.position, Quaternion.identity);

            TestNavmesh.Invoke();
        }

        void Update()
        {
            //~~Código para teste, remover posteriormente!!!!~~
            if(Input.GetKeyDown(KeyCode.F5))
            {
                SceneManager.LoadScene(gameSo.sceneThingy.BuildIndex);
                gameSo.coins.Value = gameSo.coins.DefaultValue;
                gameSo.life.Value = gameSo.life.DefaultValue;
                gameSo.objectiveLogic.Value = gameSo.objectiveLogic.DefaultValue;
            }

            //~~Código para teste, remover posteriormente!!!!~~
        
            PausingGame();
            CheckPlayerLife();
            CheckBossLife();
            CheckGameState();
        }

        void PausingGame()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseGame.Invoke();
                Time.timeScale = 0;
            }
        }

        void PlayCutscene()
        {
            cutsceneOnPlay.Invoke();
            cutscene[0].Play();
            Invoke("FinishCutscene", (float)cutscene[0].duration);
        }

        void FinishCutscene()
        {
            cutsceneOnStop.Invoke();
        }
        void CheckPlayerLife()
        {
            var h = playerLife.gameObject.Request<IArmor, int?>(_ => _.GetHealth());
            
            if (h <= 0)
            {
                restartGame.Invoke();
            }
        }
        void CheckBossLife()
        {
            var h = bossLife.gameObject.Request<IArmor, int?>(_=>_.GetHealth());
            if(h <= 1500)
            {
                cutsceneOnPlay.Invoke();
                cutscene[1].Play();
                Invoke("FinishCutscene", (float)cutscene[1].duration);
            }
        }
    
        public void RestartLevel()
        {
            SceneManager.LoadScene(gameSo.sceneThingy.BuildIndex);
            gameSo.coins.Value = gameSo.coins.DefaultValue;
        }

        public void ObjectiveCompleted()
        {
            objectiveCompleted = true;
        }

        public void EnemyDied()
        {
            var random = Random.Range(0,3);
            if (gameSo.enemyDied.Value)
            {
                if (!objectiveCompleted)
                {
                    var randomSpawn = Random.Range(0, gameSo.enemySpawn.Length);
                    Instantiate(gameSo.enemy[random].Value, gameSo.enemySpawn[randomSpawn].Value.transform.position, Quaternion.identity);
                }
                else
                {

                    enemiesDied++;
                    
                    if (enemiesDied >= 13 && cutsceneStillNotPlayed)
                    {
                        PlayCutscene();
                        cutsceneStillNotPlayed = false;
                    }
                }
                
                
            }
        }
        
        void CheckGameState()
        {
            var p = uiManager.gameObject.Request<IUI, bool?>(_ => _.OnShop());
        
            if (p == true)
            {
                Time.timeScale = 0;
            }

        }

        public void OnResume()
        {
            Time.timeScale = 1;
        }

        public void BackToMenu()
        {
            SceneManager.LoadScene("Menu");
            Time.timeScale = 1;
        }

        public void PlayGame()
        {
            SceneManager.LoadScene(gameSo.sceneThingy.BuildIndex);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    
    }
}

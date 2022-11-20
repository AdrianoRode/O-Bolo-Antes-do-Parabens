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
        private bool objectiveCompleted = false;
        private bool cutsceneStillNotPlayed = true;
        private int enemiesDied;
        private UIManager uiManager;
        private PlayerLife playerLife;

        [Provides, SerializeField] private GameManagerSO gameSo;
        public PlayableDirector cutscene;
        public UnityEvent restartGame;
        public UnityEvent pauseGame;
        public UnityEvent cutsceneOnPlay;
        public UnityEvent cutsceneOnStop;

        void Start()
        {
            uiManager = FindObjectOfType<UIManager>();
            playerLife = FindObjectOfType<PlayerLife>();
        }

        void Update()
        {
            //~~Código para teste, remover posteriormente!!!!~~
            if(Input.GetKeyDown(KeyCode.F5))
            {
                SceneManager.LoadScene(gameSo.sceneThingy.BuildIndex);
                gameSo.coins.Value = gameSo.coins.DefaultValue;
                gameSo.objectiveLogic.Value = gameSo.objectiveLogic.DefaultValue;
            }
            //~~Código para teste, remover posteriormente!!!!~~
        
            PausingGame();
            CheckPlayerLife();
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
            cutscene.Play();
            Invoke("FinishCutscene", (float)cutscene.duration);
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
            if (gameSo.enemyDied.Value)
            {
                if (!objectiveCompleted)
                {
                    var randomSpawn = Random.Range(0, gameSo.enemySpawn.Length);
                    Instantiate(gameSo.enemy.Value, gameSo.enemySpawn[randomSpawn].Value.transform.position, Quaternion.identity);
                }
                else
                {
                    enemiesDied++;
                    
                    if (enemiesDied >= 16 && cutsceneStillNotPlayed)
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

        public void QuitGame()
        {
            Application.Quit();
        }
    
    }
}

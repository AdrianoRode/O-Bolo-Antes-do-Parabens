using Ez;
using Script.Player;
using Syrinj;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

namespace Script.GameManager
{
    public class Game : MonoBehaviour
    {
        [Provides, SerializeField]private GameManagerSO gameSo;
        
        private UIManager uiManager;
        private PlayerLife playerLife;
        private bool testBool = true;
        private int test;

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
        }

        public void EnemyDied()
        {
            test++;
            var randomSpawn = Random.Range(0, gameSo.enemySpawn.Length);
            Instantiate(gameSo.enemy.Value, gameSo.enemySpawn[randomSpawn].Value.transform.position, Quaternion.identity);
            
            if (test >= 10 && testBool)
            {
                PlayCutscene();
                testBool = false;
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

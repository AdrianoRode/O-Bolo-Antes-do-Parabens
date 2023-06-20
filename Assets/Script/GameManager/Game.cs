using Ez;
using Script.Player;
using Syrinj;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using Cinemachine;

namespace Script.GameManager
{
    public class Game : MonoBehaviour
    {
        private bool objectiveCompleted;
        private bool cutsceneStillNotPlayed1 = true;
        private bool cutsceneStillNotPlayed2 = true;
        private int enemiesDied;
        private static float fovCamera = 7f; 
        private UIManager uiManager;
        private PlayerLife playerLife;
        private GameObject houseParent;
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
        public CinemachineVirtualCamera camera;

        void Start()
        {
            uiManager = FindObjectOfType<UIManager>();
            playerLife = FindObjectOfType<PlayerLife>();
            houseParent = GameObject.Find("House");

            var r = Random.Range(0, houses.Length);

            GameObject house = Instantiate(houses[r], houseParent.transform);
            house.transform.parent = houseParent.transform;

            camera.m_Lens.OrthographicSize = fovCamera;
            TestNavmesh.Invoke();
        }

        void Update()
        {
            PausingGame();
            CheckPlayerLife();
            //CheckBossLife();
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

        void PlayCutscene(int n)
        {
            cutsceneOnPlay.Invoke();
            cutscene[n].Play();
            Invoke("FinishCutscene", (float)cutscene[n].duration);
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
            if(h <= 1500 && cutsceneStillNotPlayed2)
            {
                PlayCutscene(1);
                cutsceneStillNotPlayed2 = false;
            }
        }

        public void SetFullscreen(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
        }
        public void SetFov(float newFov)
        {
            fovCamera = newFov;
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
                    
                    if (enemiesDied >= 13 && cutsceneStillNotPlayed1)
                    {
                        PlayCutscene(0);
                        cutsceneStillNotPlayed1 = false;
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

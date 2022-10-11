using Ez;
using UnityEngine;

namespace Script.Player
{
    public class PlayerControl : MonoBehaviour
    {
        [SerializeField]private float playerSpeed = 2.0f;
        [SerializeField]private CharacterController controller;

        private float gravityValue = -9.81f;    
        private bool groundedPlayer;
        private bool storeAccess;
        private UIManager ui;
        private Vector3 playerVelocity;
        private Camera cam;

        public delegate void CoinCollect(int coin);
        public event CoinCollect CoinCollected;

        public delegate void GamePause();
        public event GamePause GamePaused;

        void Awake()
        {
            ui = FindObjectOfType<UIManager>();
            cam = FindObjectOfType<Camera>();
        }

        void Update()
        {
            PausingGame();   
            StoreInteration();
            PlayerMovements();
            AimRotation();

        }

        void PausingGame()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GamePaused?.Invoke();
            }
        }
        void StoreInteration()
        {
            if (Input.GetKeyDown(KeyCode.F) && storeAccess)
            {
                ui.gameObject.Send<IUI>(_ => _.OpenShop(true));
            }
        }

        void PlayerMovements()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(horizontal, gravityValue, vertical);      
            controller.Move(direction * playerSpeed * Time.deltaTime);
            
            if(Input.GetMouseButton(0))
            {
                //O True serve para mandar a mensagem para os filhos desse gameObject!
                gameObject.Send<IGun>(_=>_.Fire(), true);
            }
        }
        void AimRotation()
        {
            Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLength;

            if (groundPlane.Raycast(cameraRay, out rayLength))
            {
                Vector3 pointToLook = cameraRay.GetPoint(rayLength);
                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            }
        }

        void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.CompareTag("Store"))
            {
                storeAccess = true;
                ui.gameObject.Send<IUI>(_=>_.InputUI(true));
            }
        
            else if (col.gameObject.CompareTag("Collectable"))
            {
                CoinCollected?.Invoke(1);
            }
        }

        void OnTriggerExit(Collider col)
        {
            if (col.gameObject.CompareTag("Store"))
            {
                storeAccess = false;
                ui.gameObject.Send<IUI>(_=>_.InputUI(false));
            }
        }
    }
}

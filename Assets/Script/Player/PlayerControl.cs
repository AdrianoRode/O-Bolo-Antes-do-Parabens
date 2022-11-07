using Ez;
using ScriptableObjectArchitecture;
using Syrinj;
using UnityEngine;

namespace Script.Player
{
    public class PlayerControl: MonoBehaviour
    {
        [SerializeField]private float playerSpeed = 2.0f;
        [SerializeField]private CharacterController controller;

        private float gravityValue = -9.81f;    
        private bool groundedPlayer;
        private bool storeAccess;
        private bool canControl = true;
        private Vector3 playerVelocity;
        private UIManager ui;
        private Camera cam;
  
        void Awake()
        {
            ui = FindObjectOfType<UIManager>();
            cam = FindObjectOfType<Camera>();
        }

        void Update()
        {
            if (!canControl)
            {
                return;
            }

            StoreInteration();
            PlayerWeapon();
            PlayerMovements();
            AimRotation();

        }
        void StoreInteration()
        {
            if (Input.GetKeyDown(KeyCode.F) && storeAccess)
            {
                ui.gameObject.Send<IUI>(_ => _.OpenShop(true));
            }
        }

        void PlayerWeapon()
        {
            
            if(Input.GetMouseButton(0))
            {
                //O True serve para mandar a mensagem para os filhos desse gameObject!
                gameObject.Send<IGun>(_=>_.Fire(), true);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                gameObject.Send<IGun>(_=>_.Reload(), true);
            }
        }
        void PlayerMovements()
        {
            
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(horizontal, gravityValue, vertical);      
            controller.Move(direction * (playerSpeed * Time.deltaTime));
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

        public void EnableControl()
        {
            canControl = true;
        }
        public void DisableControl()
        {
            canControl = false;
        }

        void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.CompareTag("Store"))
            {
                storeAccess = true;
                ui.gameObject.Send<IUI>(_=>_.InputUI(true));
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

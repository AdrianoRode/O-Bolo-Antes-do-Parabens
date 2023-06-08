using System.Collections;
using Ez;
using UnityEngine;

namespace Script.Player
{
    public class PlayerControl: MonoBehaviour, IDebuff
    {
        [SerializeField]private float playerSpeed = 2.0f;
        [SerializeField]private CharacterController controller;
        [SerializeField]private GameObject localWeapon;
        [SerializeField]private WeaponSO waterPistol;
        [SerializeField]private AudioSource walkSound;
        [SerializeField]private AudioClip[] walkGrass;
        [SerializeField]private AudioClip[] walkConcrete;

        private float gravityValue = -9.81f;    
        private bool groundedPlayer;
        private bool storeAccess;
        private bool canReload;
        private bool canControl = true;

        private string ground = "";
        private Vector3 playerVelocity;
        private UIManager ui;
        private Camera cam;
        private Animator anim;


        void Awake()
        {
            ui = FindObjectOfType<UIManager>();
            cam = FindObjectOfType<Camera>();
        }

        void Start()
        {
            anim = GetComponent<Animator>();
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
                canControl = false;
            }

            if(Input.GetKeyDown(KeyCode.F) && canReload)
            {
                ui.gameObject.Send<IUI>(_=>_.WaterCollected(true));
                waterPistol.reserveAmmo += 1;
            }
        }

        void PlayerWeapon()
        {
            
            if(Input.GetMouseButton(0))
            {
                //O True serve para mandar a mensagem para os filhos desse gameObject!
                localWeapon.gameObject.Send<IGun>(_=>_.Fire(), true);
            }

            if (Input.GetKeyDown(KeyCode.R) && !Input.GetMouseButton(0))
            {
                gameObject.Send<IGun>(_=>_.Reload(), true);
            }
        }
        void PlayerMovements()
        {
              
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(horizontal, gravityValue, vertical);

            /*if(horizontal > 0 && Input.GetKey(KeyCode.LeftShift) || vertical > 0 && Input.GetKey(KeyCode.LeftShift))
            {
                controller.Move(direction * ((playerSpeed + 0.1f) * Time.deltaTime));
            }

            else if(horizontal < 0 && Input.GetKey(KeyCode.LeftShift) || vertical < 0 && Input.GetKey(KeyCode.LeftShift))
            {
                controller.Move(direction * ((playerSpeed + 0.1f) * Time.deltaTime));
            }*/     
            
            if(horizontal > 0 || horizontal < 0 || vertical > 0 || vertical < 0)
            {
                anim.SetBool("isMoving", true);
                controller.Move(direction * (playerSpeed * Time.deltaTime));
            }
            else
            {
                anim.SetBool("isMoving", false);
            }
        }

        public void WalkSound()
        {
            if(ground == "Grass")
            {
                walkSound.PlayOneShot(walkGrass[Random.Range(0, walkGrass.Length)]);
            }

            else if(ground == "Concrete")
            {
                walkSound.PlayOneShot(walkConcrete[Random.Range(0, walkConcrete.Length)]);
            }
            
        }
        void AimRotation()
        {    
            Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, localWeapon.transform.position);
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

            else if(col.gameObject.CompareTag("Sink"))
            {
                canReload = true;
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

            else if(col.gameObject.CompareTag("Sink"))
            {
                canReload = false;
                ui.gameObject.Send<IUI>(_=>_.InputUI(false));
            }
        }
        void OnControllerColliderHit(ControllerColliderHit col)
        {
             if(col.gameObject.CompareTag("Grass"))
            {
                ground = "Grass";
            }

            else if(col.gameObject.CompareTag("Concrete"))
            {
                ground = "Concrete";
            } 
        }
        public IEnumerable ApplyStun(bool condition)
        {
            canControl = condition;
            yield return null;
        }

    }
}

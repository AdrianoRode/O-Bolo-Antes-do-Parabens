using System.Collections;
using Ez;
using UnityEngine;

public class PlayerControl: MonoBehaviour, IDebuff
{
    [Header("PlayerControl")]
    [SerializeField]private float playerSpeed = 2.0f;
    [SerializeField]private CharacterController controller;

    [Header("ActualWeapon")]
    [SerializeField]private GameObject localWeapon;

    [Header("WaterPistolAttributes")]
    [SerializeField]private WeaponSO waterPistol;

    [Header("SoundEffect")]
    [SerializeField]private AudioSource walkSound;
    [SerializeField]private AudioClip[] walkGrass;
    [SerializeField]private AudioClip[] walkConcrete;
    private float gravityValue = -9.81f;    
    public float stamina = 100f;
    public bool canControl = true;
    private bool storeAccess;
    private bool canReload;
    private string ground = "";
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
              
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical"); 
            
        Vector3 direction = new Vector3(horizontalInput, gravityValue, verticalInput);
        stamina += 5f * Time.deltaTime;

        if(horizontalInput != 0 || verticalInput != 0)
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                if(stamina > 0)
                {
                    playerSpeed = 6f;
                    stamina -= 25f * Time.deltaTime;
                    anim.SetFloat("run", 1.3f);
                }

                else
                {
                    playerSpeed = 4f;
                    anim.SetFloat("run", 1f);
                }  
            }

            else
            {
                playerSpeed = 4f;
                anim.SetFloat("run", 1f);
            }
            anim.SetBool("isMoving", true);
            controller.Move(direction * (playerSpeed * Time.deltaTime));
            stamina = Mathf.Clamp(stamina, 0f, 100f);

        }
        else
        {
            anim.SetBool("isMoving", false);
        }

    }
    public void SetPositionOnCutscene(Transform newPos)
    {
        transform.position = newPos.position;
        transform.rotation = newPos.rotation;
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

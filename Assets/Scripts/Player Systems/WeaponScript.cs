using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [Header("Dictionary Values")]
    public int weaponCounter;
    public int nextWeaponCounter;

    [Header("Code Values")]
    public string weaponusing;
    public GameObject[] missilePrefab;
    [SerializeField] private GameObject ChargedShot;
    [SerializeField] public float chargeTime;
    bool isCharging;
    bool isCharged;
    public Transform spawnPos;
    //public float speed = 0.5f;
    public bool canFire;
    public bool isGrab = false;
    private float timer;
    public float timeBetweenFiring;

    [Header("Laser Stuff")]
    [SerializeField] private float _laserDistance = 100f;
    public LineRenderer lineRender;
    Transform m_transform;


    Dictionary<int, string> weaponType = new Dictionary<int, string>()
    {
        {0 , "IceBolt" },
        {1, "SparkBolt" },
        {2, "RockBolt" },
        {3, "FireBolt"},
        {4, "WaterBall" },
        {5, " FireBolt" }
    };

    private void Start()
    {
        weaponCounter = Random.Range(0, 5);
        nextWeaponCounter = Random.Range(0, 5);
        isCharging = false;
        m_transform = GetComponent<Transform>();
    }
    void Update()
    {
        ResetStats();
        GetInput();
        weaponusing = weaponType[weaponCounter];
        LookAtMouse();
        if (isCharging)
        {
            Charge();
        }
        Debug.Log(weaponType[weaponCounter]);
    }

    void fire()
    {
        if (weaponType.ContainsKey(weaponCounter) && canFire && !isGrab)
        {
            canFire = false;
            string weaponName = weaponType[weaponCounter];

            int prefabIndex = GetPrefabIndex(weaponName);
            if (prefabIndex != -1)
            {
                GameObject weaponPrefab = missilePrefab[prefabIndex];
                GameObject weaponObject = Instantiate(weaponPrefab, spawnPos.position, spawnPos.rotation);
                Rigidbody2D rb = weaponObject.GetComponent<Rigidbody2D>();
                rb.AddForce(spawnPos.right * 7f, ForceMode2D.Impulse);
            }

        }

    }
    void chargedfire()
    {
        
        lineRender.enabled = true;
       
        StartCoroutine(StopLaser());
        canFire = false;
        if (Physics2D.Raycast(m_transform.position, transform.right))
        {
            RaycastHit2D _hit = Physics2D.Raycast(m_transform.position, transform.right);
            Draw2DRay(spawnPos.position, _hit.point);
        } 
        else
        {
            Draw2DRay(spawnPos.position, spawnPos.transform.right * _laserDistance);
        }
    }
    private void disableLaser()
    {
        lineRender.enabled = false;
    }
    private IEnumerator StopLaser()
    {
        yield return new WaitForSeconds(1f);
        disableLaser();
    }
    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        lineRender.SetPosition(0, startPos);
        lineRender.SetPosition(1, endPos);

    }
    private int GetPrefabIndex(string weaponName)
    {
        for (int i = 0; i < missilePrefab.Length; i++)
        {
            if (missilePrefab[i].name == weaponName)
            {
                return i;
            }
        }
        Debug.LogError("Prefab not found for weapon: " + weaponName);
        return -1; // Return -1 if not found
    }


    
    
    void LookAtMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        
        mousePos.z = 10;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 direc = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

        transform.right = direc;
    }

    public void GetInput()
    {
        if (Input.GetButtonDown("Fire1") && canFire)
        {
            isCharging = true ;
            Charge();

        }
        if (Input.GetButtonUp("Fire1") && isCharged && canFire)
        {
            chargedfire();
            //++weaponCounter;
            weaponCounter = nextWeaponCounter;
            nextWeaponCounter = Random.Range(0, 5);
            ResetCharge();
        }
        else if (Input.GetButtonUp("Fire1") && !isCharged && canFire)
        {
            fire();
            weaponCounter = nextWeaponCounter;
            nextWeaponCounter = Random.Range(0, 5);
            ResetCharge();
        }

    }
    public void ResetCharge()
    {
        chargeTime = 0;
        isCharging = false;
        isCharged = false;
    }
    public void Charge()
    {
        chargeTime += Time.deltaTime;

        if (chargeTime >= 2f)
        {
            isCharging=false;
            isCharged = true;
        }
        
    }
    public void ResetStats()
    {
        if (weaponCounter >= 5)
        {
            weaponCounter = 0;
        }
        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }
    }
}
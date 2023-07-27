using RPGCharacterAnims.Actions;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class IdleEvent : MonoBehaviour
{
    private static IdleEvent gameModel = null;
    public static IdleEvent Instance() { return gameModel; }
   
    public Animator animator;
    public bool useCurves = true;
    public float useCurvesHeight = 0.5f;

    private Rigidbody rb;
    public float forwardSpeed = 2f;
    public float rotateSpeed = 2.0f;
    public float runSpeed= 5f;
    public float jumpPower = 3.0f;
    public float bombPower = 30f;
    public Vector3 bomb ;
    private Vector3 Ori;
    private Vector3 Orate;
    private float moveTime = 0;
    private float RunningStart= 1.2f;
    private Vector3 playerMove;
    private  CapsuleCollider col;
    private float orgColHight;
    private Vector3 orgVectColCenter;
   
    private GameObject cameraObject;
    private float v = 0f;
    private float h = 0f;
    public static bool noMove;
    public static bool win ;

    public GameObject weapon;
    public bool wea=false;
    bool useingweapon;
    public bool isMove;
    public Transform fire;

    // Start is called before the first frame update

    public float Hp;
    public float MaxHp;
    // public GameObject hpBer;
    GameObject hitbar;
    bool openUIbar = false;
    private void Awake()
    {
        if (gameModel == null)
        {
            gameModel = this;
            //if (UImanger.Instance.game1win)
            {
                DontDestroyOnLoad(gameObject);
            }
            //else { Destroy(gameObject); }
        }
        else { Destroy(gameObject); }
    }

    private IEnumerator NoMove(float time)
    {
        noMove = true;
        yield return new WaitForSeconds(time);
        
        if (Hp > 0) { noMove = false; }
        if (useingweapon) { weapon.GetComponent<BoxCollider>().enabled = false;
            //Debug.Log("collider" + weapon.GetComponent<BoxCollider>().enabled);
        }
    }
    void Start()
    {
        rb=this.gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();
        col = gameObject.GetComponent<CapsuleCollider>();
        orgColHight = col.height;
        orgVectColCenter = col.center;
        cameraObject = GameObject.FindWithTag("MainCamera");
        Hp = 10;
        MaxHp = 10;
        Ori = transform.position;
       Orate = transform.eulerAngles;
        noMove = false;
       
        win = false;
        

    }
    public void ModifyHp(float num)
    {
        Hp += num;
        if (Hp > 10)
        {
            Hp = 10;
        }
        else if (Hp <= 0)
        {
            Hp = 0;
            noMove = true;
            animator.SetBool("dead", true);
           
           
            StartCoroutine(UImanger.Instance.Gameover());
        }else if (Hp > 0) { 
            animator.SetBool("dead", false);
            
        }
         UImanger.Instance.UpdateHpBar();
    }
   
    // Update is called once per frame
    void Update()
    {
        useingweapon = animator.GetBool("weapon");
        if (wea)
        {

            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(NoMove(1.5f));
                animator.SetBool("weapon", !useingweapon);

            }
            if (useingweapon){
                animator.SetBool("attack", Input.GetMouseButtonDown(0));
                Swordray();
                if (Input.GetMouseButtonDown(0))
                {
                    weapon.GetComponent<BoxCollider>().enabled = true;
                    //Debug.Log("collider"+ weapon.GetComponent<BoxCollider>().enabled);
                    StartCoroutine(NoMove(1f));
                }
                //weapon.GetComponent<BoxCollider>().enabled = false;
            }
  
        }
    }


    private void FixedUpdate()
    {
       
        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");
        animator.SetFloat("walk", v);
        animator.SetFloat("RL", h);
        isMove = (v > 0.1);

        if (useingweapon) 
        {
            weaponation();
        }
        else
        {
            animator.applyRootMotion = true;
            rb.useGravity = true;
            playerMove = noMove ? new Vector3(0, 0, 0) : new Vector3(0, 0, v);
            moveTime = isMove ? (moveTime + Time.fixedDeltaTime) : 0;
            bool isRun = (RunningStart <= moveTime);

            if (isMove && noMove == false)
            {
                transform.Rotate(0, h * rotateSpeed, 0);
                float moveSpeed = isRun ? runSpeed : forwardSpeed;
                playerMove *= moveSpeed;

                //Debug.Log(moveTime);
            }

            animator.SetBool("run", isRun);
            playerMove = transform.TransformDirection(playerMove);

            if (Input.GetButtonDown("Jump"))
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Blend Tree run"))
                {
                    if (!animator.IsInTransition(0))
                    {
                        rb.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
                        animator.SetBool("jump", true);
                    }
                }
            }
            transform.localPosition += playerMove * Time.fixedDeltaTime;
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Blend Tree run"))
            {
                if (useCurves)
                {
                    resetCollider();
                }

            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("jump"))
            {
                cameraObject.SendMessage("setCameraPositionJumpView");  // ジャンプ中のカメラに変更
                                                                        // ステートがトランジション中でない場合
                if (!animator.IsInTransition(0))
                {

                    // 以下、カーブ調整をする場合の処理
                    if (useCurves)
                    {
                        // 以下JUMP00アニメーションについているカーブJumpHeightとGravityControl
                        // JumpHeight:JUMP00でのジャンプの高さ（0〜1）
                        // GravityControl:1⇒ジャンプ中（重力無効）、0⇒重力有効
                        float jumpHeight = animator.GetFloat("jumpHeight");
                        float gravityControl = animator.GetFloat("GControl");
                        if (gravityControl > 0)
                            rb.useGravity = false;  //ジャンプ中の重力の影響を切る

                        // レイキャストをキャラクターのセンターから落とす
                        Ray ray = new Ray(transform.position + Vector3.up, -Vector3.up);
                        RaycastHit hitInfo = new RaycastHit();
                        // 高さが useCurvesHeight 以上ある時のみ、コライダーの高さと中心をJUMP00アニメーションについているカーブで調整する
                        if (Physics.Raycast(ray, out hitInfo))
                        {
                            if (hitInfo.distance > useCurvesHeight)
                            {
                                col.height = orgColHight - jumpHeight;          // 調整されたコライダーの高さ
                                float adjCenterY = orgVectColCenter.y + jumpHeight;
                                col.center = new Vector3(0, adjCenterY, 0); // 調整されたコライダーのセンター
                            }
                            else
                            {
                                // 閾値よりも低い時には初期値に戻す（念のため）					
                                resetCollider();
                            }
                        }
                    }
                    // Jump bool値をリセットする（ループしないようにする）				
                    animator.SetBool("jump", false);
                }
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
            {

                if (useCurves)
                {
                    resetCollider();
                }

            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Blend Tree walk"))
            {
                if (useCurves)
                {
                    resetCollider();
                }
            }
        }
        
    }
    private void weaponation()
    {
        animator.applyRootMotion = false;
        //float mouseX = Input.GetAxis("Mouse X");
        bool move = (v != 0 || h != 0);
        if (move)
        {
            Vector3 playerfd = noMove ? Vector3.zero : Vector3.forward * v;
            Vector3 playerrt = noMove ? Vector3.zero : Vector3.right * h;
            playerMove = playerfd + playerrt;
            transform.Rotate(0, h * rotateSpeed, 0);
            playerMove *= 3f;
        }
        playerMove = transform.TransformDirection(playerMove);
        transform.position += playerMove * Time.fixedDeltaTime;
        animator.SetBool("move", move);
    }
  void resetCollider()
    {
        
        col.height = orgColHight;
        col.center = orgVectColCenter;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (Hp!=0f) {
            if (collision.gameObject.tag == "Bomb")
            {
                StartCoroutine(NoMove(1.5f));
                animator.SetTrigger("Injuried");
                rb.AddRelativeForce(bomb * bombPower, ForceMode.VelocityChange);
                instBomb.count--;
                ModifyHp(-1);

            }
            if (collision.gameObject.tag == "Bombx")
            {

                animator.SetTrigger("Injuried");
                StartCoroutine(NoMove(1.5f));
                startPosition(1);
                ModifyHp(-2);
            }
            if (collision.gameObject.tag == "tonic")
            {
                ModifyHp(2);
                instBomb.count--;
            }
            if (collision.gameObject.tag == "Fire")
            {
                ModifyHp(-1);
            } 
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "OHS03Polyart")
        {
            win = true;
            UImanger.Instance.Task.text = "You Win";
            animator.SetBool("win", win);
            StartCoroutine(NoMove(3f));
            StartCoroutine(wintime());
        }
    }
    private IEnumerator wintime()
    {
        yield return new WaitForSeconds(3f);
        win = false;    
        animator.SetBool("win", win);
        //startPosition(2);
        UImanger.Instance.NextLevel();
    }
    public void startPosition(int sceneslevel)
    {
        Vector3[] levelposition = new Vector3[2];
        Vector3[] levelrotation = new Vector3[2];
        levelposition[0] = Ori; levelposition[1] = new Vector3(11f,0,91f);
        levelrotation[0]=Orate; levelrotation[1]=new Vector3(0,120f,0);
        transform.localPosition = levelposition[sceneslevel-1];
        transform.localEulerAngles= levelrotation[sceneslevel-1];
    }
    public void Swordray()
    {
        RaycastHit hit;

        if (Physics.BoxCast(fire.position,new Vector3(0.5f,0.5f,0.5f), fire.forward,out hit,Quaternion.identity,2f))
        {
            //Debug.Log("sss" + hit.collider.name);
            
            if (hit.transform.GetComponent<badnpc>() != null)
            {
                openUIbar = true;
                hit.transform.GetComponent<badnpc>().UIbar.SetActive(openUIbar);
                hitbar = hit.transform.GetComponent<badnpc>().UIbar;

                //Debug.Log("血量" + hit.transform.GetComponent<badnpc>().UIbar.activeInHierarchy);
            }
        }
        else if(hitbar!=null)
        { 
            hitbar.SetActive(false);
            hitbar=null;
        }
        
        
    }
    /*public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(fire.position, new Vector3(0.5f, 0.5f, 0.5f));
    }*/

}

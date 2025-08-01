using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public float jump;
    public int Health;
    public HealthBarController healthBarController;

    public static bool RightFace = true;

    [SerializeField] private LayerMask Ground;
    private Enemy[] enemies;

    private Rigidbody2D rigid;
    private BoxCollider2D box;
    public Animator animator;
    public MenuController Mn;

    public static bool isDamage;
    public static bool isDied;
    public static bool isAttack;

    public static float fr;
    public int Direction; 

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        enemies = UnityEngine.Object.FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        Mn = FindFirstObjectByType<MenuController>();
        fr = Input.GetAxis("Horizontal");

        Speed = 5f;
        jump = 6.5f;
        Health = 100;
        healthBarController.SetHealth(Health , 100);

        isDamage = false;
        isDied = false;
        isAttack = false;

        Direction = 1;
        RightFace = true;
    }

    void Update()
    {
        #if !UNITY_ANDROID
            fr = Input.GetAxis("Horizontal") * Direction;
        #endif

        if(rigid.bodyType != RigidbodyType2D.Static) 
        {
            rigid.linearVelocity = new Vector2(fr * Speed , rigid.linearVelocity.y);
            if((fr < 0 && RightFace) || (fr > 0 && !RightFace)) {
                flip();
            }
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) {
                Jump();
            }
        }
        if(fr != 0) {
            animator.SetBool("Run" ,true);
        }
        if (fr == 0){
            animator.SetBool("Run" , false);
        }
        if(Input.GetKeyDown(KeyCode.Q)){
            Attack();
        }
        jumpAnimation();
        Die();
    }

    public void flip() 
    {
        Vector3 CurrentScale = gameObject.transform.localScale;
        CurrentScale.x *=-1;
        gameObject.transform.localScale = CurrentScale;
        RightFace = !RightFace;
        
        Vector3 CurrentHealthBarScale = healthBarController.slider.gameObject.transform.localScale;
        CurrentHealthBarScale.x *= -1;
        healthBarController.slider.transform.localScale = CurrentHealthBarScale;
    }

    public bool isOnGround() {
        return Physics2D.BoxCast(box.bounds.center , box.bounds.size , 0f , Vector2.down , 0.1f , Ground);
    }

    public void Jump()
    {
        if(isOnGround()) {
            rigid.linearVelocity = new Vector2(rigid.linearVelocity.x , jump);
        }
    }

    public void jumpAnimation() 
    {
        if(!isOnGround()){
            animator.SetBool("jump" , true);
        }
        else{
            animator.SetBool("jump" , false);
        }
    }

    public void TakeHit()
    {
        if (!isDamage && !isDied)
        {
            if(isAttack) {
                ExitAttackMode();
            }
            EnterDamageMode();
            Invoke("ReduceHealth", 0.5f);
            Invoke("ExitDamageMode", 0.5f);
        }
    }

    public void ReduceHealth() {
        Health -= 10;
        healthBarController.SetHealth(Health , 100);
        Debug.Log("Health Now :" + Health);
    }

    
    public void Die()
    {
        if ((Health <= 0 || GetCurrentPositionY() < CameraFollow.minLimits.y - 8) && !isDied) {
            isDied = true;
            animator.SetTrigger("died");
            rigid.bodyType = RigidbodyType2D.Static;
            GameOverController.Show = true;
        }
    }

    public void Attack()
    {
        if(isOnGround() && !isDied && !isAttack)
        {
            EnterAttackMode();
            foreach(Enemy enemy in enemies) 
            {
                if(enemy == null || enemy.isDead) {
                    continue;
                }
                float distanceX = Mathf.Abs(enemy.GetCurrentPositionX() - GetCurrentPositionX());
                float distanceY = Mathf.Abs(enemy.GetCurrentPositionY() - GetCurrentPositionY());
                if(distanceX <= 2.2f && distanceY <= 0.5) 
                {
                    if((GetCurrentPositionX() <= enemy.GetCurrentPositionX() && RightFace) || GetCurrentPositionX() > enemy.GetCurrentPositionX() && !RightFace) {
                        if(enemy.isAttack) {
                            enemy.ExitAttackMode();
                        }
                        enemy.TakeHit();
                    }
                }
                if(isAttack) {
                    Invoke("ExitAttackMode" , 1f);
                }
            }
        }
    }

    public void EnterAttackMode()
    {
        isAttack = true;
        animator.SetBool("Attack" , isAttack);
        rigid.bodyType = RigidbodyType2D.Static;
    }

    public void ExitAttackMode()
    {
        isAttack = false;
        animator.SetBool("Attack" , isAttack);
        rigid.bodyType = RigidbodyType2D.Dynamic;
    }

    public void EnterDamageMode() {
        isDamage = true;
        animator.SetBool("Damage" , isDamage);
    }

    public void ExitDamageMode() {
        isDamage = false;
        animator.SetBool("Damage" , isDamage);
    }

    public float GetCurrentPositionX() {
        return gameObject.transform.position.x;
    }

    public float GetCurrentPositionY() {
        return gameObject.transform.position.y;
    }

    public void restartGame() {
        Mn.restartGame();
    }
}
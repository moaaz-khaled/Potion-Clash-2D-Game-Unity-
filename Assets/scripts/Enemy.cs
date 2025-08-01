using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rigid;
    private PlayerController player;

    private float Speed;
    private int hitsToDie;
    private bool MoveRight;
    private bool faceRight;

    public bool isWalk;
    public bool isDead;
    public bool isDamage;
    public bool isAttack;


    private float MaxMoveRight;
    private float MinMoveRight;
    private float initialPositionX;
    private float initialPositionY;

    private float CurrentPositionX , CurrentPositionY;
    private float DistanceBetweenEnemyAndPlayer , VerticalDistanceBetweenEnemyAndPlayer;

    void Start() {
        animator = GetComponent<Animator>();
        player = FindFirstObjectByType<PlayerController>();
        rigid = GetComponent<Rigidbody2D>();

        initialPositionX = gameObject.transform.position.x;
        initialPositionY = gameObject.transform.position.y;

        MaxMoveRight = initialPositionX + 2.5f;
        MinMoveRight = initialPositionX - 2.5f;

        Speed = 1.5f;
        hitsToDie = 3;
        MoveRight = true;
        faceRight = true;

        isWalk = true;
        isDead = false;
        isDamage = false;
        isAttack = false;
    }

    void Update()
    {
        CurrentPositionX = gameObject.transform.position.x;
        CurrentPositionY = gameObject.transform.position.y;

        DistanceBetweenEnemyAndPlayer = Vector2.Distance(gameObject.transform.position , player.gameObject.transform.position);
        VerticalDistanceBetweenEnemyAndPlayer = Mathf.Abs(gameObject.transform.position.y - player.gameObject.transform.position.y);

        if(MoveRight && DistanceBetweenEnemyAndPlayer > 2.1f)
        {
            CurrentPositionX += Speed * Time.deltaTime; 
            gameObject.transform.position = new Vector3(CurrentPositionX , CurrentPositionY , 0);
            if(gameObject.transform.position.x >= MaxMoveRight) {
                MoveRight = false;
            }
        }

        if(!MoveRight && DistanceBetweenEnemyAndPlayer > 2.1f)
        {
            CurrentPositionX -= Speed * Time.deltaTime; 
            gameObject.transform.position = new Vector3(CurrentPositionX , CurrentPositionY , 0);
            if(gameObject.transform.position.x <= MinMoveRight) {
                MoveRight = true;
            }
        }
    
        if((MoveRight && !faceRight) || (!MoveRight && faceRight)) {
            flip();
        }

        animator.SetBool("Walk" , isWalk);
        
        if (PlayerController.isDied && !isDead) {
            idelMode();
            return;
        }
        if(!isDamage && DistanceBetweenEnemyAndPlayer <= 2.2 && VerticalDistanceBetweenEnemyAndPlayer <= 0.5)
            Attack();
        else
            ExitAttackMode();
    }

    void flip() {
        Vector3 CurrentScale = gameObject.transform.localScale;
        CurrentScale.x *=-1;
        gameObject.transform.localScale = CurrentScale;
        faceRight = !faceRight;
    }

    public void TakeHit() 
    {
        EnterDamageMode();
        hitsToDie--;
        if (hitsToDie <= 0) {
            Die();
        }
        Invoke("ExitDamageMode", 0.75f);
    }

    private void Die()
    {
        if(hitsToDie <= 0)
        {
            isDead = true;
            animator.SetBool("Died" , isDead);
            rigid.bodyType = RigidbodyType2D.Static;
            Destroy(gameObject , 1.5f);
        }
    }

    private void Attack()
    {
        if (DistanceBetweenEnemyAndPlayer <= 2.2f && !isDead && !PlayerController.isDied && !PlayerController.isAttack)
        {
            if (faceRight && player.gameObject.transform.position.x < gameObject.transform.position.x) {
                flip();
                MoveRight = !MoveRight; 
            }
            if (!faceRight && player.gameObject.transform.position.x > gameObject.transform.position.x) {
                flip();
                MoveRight = !MoveRight; 
            }

            animator.SetBool("Walk", false);
            if(!isDamage){
                EnterAttackMode();
            }
        }
        
        else {
            ExitAttackMode();
            if(!isDead){
                isWalk = true;
                animator.SetBool("Walk", isWalk);
            }
            player.ExitDamageMode();
        }
    }

    public void idelMode() 
    {
        isAttack = false;
        isWalk = false;
        animator.SetBool("Attack", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Damage", false);
        animator.SetBool("Died", false);
    }

    public void EnterAttackMode()
    {
        isAttack = true;
        animator.SetBool("Attack" , isAttack);
        rigid.bodyType = RigidbodyType2D.Static;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x , initialPositionY + 0.3f , 0);
    }

    public void ExitAttackMode()
    {
        isAttack = false;
        animator.SetBool("Attack" , isAttack);
        rigid.bodyType = RigidbodyType2D.Dynamic;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x , initialPositionY , 0);
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

    private void HitPlayer(){
        player.TakeHit();
    }
}
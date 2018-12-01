using UnityEngine;
using System.Collections;

public abstract class ACharacter : MonoBehaviour
{
    public GameObject[] attackObj;

    #region Stats
    [Header("Stats")]

    [SerializeField]
    protected float Health;
    [SerializeField]
    protected float Speed;

    public float AttackDamage;
    [SerializeField]
    protected float AttackSpeed;
    [SerializeField]
    protected float AttackRange;

    protected string Name;
    protected float MaxHealth;

    protected float InvulnableTime;

    protected bool bMayAttack = true;
    protected bool IsDead = false;
    protected float nextAttack;
    protected bool IsInvulable = false;
    protected bool IsWalking = false;


    protected new Rigidbody2D rigidbody;
    protected GameController gameController;
    protected Animator anim;
    #endregion

    protected DIRECTIONS faceDirection;

    protected void Awake()
    {
        rigidbody = GetComponentInChildren<Rigidbody2D>();
        gameController = FindObjectOfType<GameController>();
        anim = GetComponent<Animator>();
    }

    abstract protected void MoveController();

    abstract protected void FaceDirection();

    public virtual void CalculateHealth(float damage)
    {
        Health -= damage;

        //Debug.Log(Health);

        if (Health <= 0)
        {
            IsDead = true;
        }
    }

    protected void CheckDeath()
    {
        if (IsDead)
        {
            Destroy(gameObject);
        }
    }

    protected IEnumerator MakeInvulable()
    {
        IsInvulable = true;
        yield return new WaitForSeconds(InvulnableTime);
        IsInvulable = false;
    }

    //protected IEnumerator WaitForAttack()
    //{
    //    bMayAttack = false;
    //    yield return new WaitForSeconds(1 / AttackSpeed);
    //    Destroy(attackInstance);
    //    //anim.SetBool("IsAttacking", false);
    //    bMayAttack = true;
    //}


    protected abstract void Attack();
}
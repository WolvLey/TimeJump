using UnityEngine;
using System.Collections;

[System.Serializable]
public struct Stats
{
    public string Name;
    public float MaxHealth;
    public float Health;
    public float Speed;

    public float AttackDamage;
    public float AttackSpeed;
    public float AttackRange;
}

public enum DIRECTIONS
{
    NORTH, EAST, SOUTH, WEST, CENTER
}

public abstract class ACharacter : MonoBehaviour
{
    public GameObject[] attackObj;

    [SerializeField]
    protected Stats Stats;

    protected float InvulnableTime;

    protected bool IsDead = false;
    protected float nextAttack;

    protected bool IsInvulable = false;
    protected bool IsWalking = false;

    protected new Rigidbody2D rigidbody;
    protected GameController gameController;
    protected Animator anim;

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
        Stats.Health -= damage;

        if (Stats.Health <= 0)
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

    protected abstract void Attack();
}
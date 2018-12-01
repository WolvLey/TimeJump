using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : ASpecialCharacter
{

    private enum EQWEAPON
    {
        SWORD = 0, MAGIC = 1
    }

    [Header("Inventar")]
    public string eqWeapon;

    // Use this for initialization
    void Start()
    {
        eqWeapon = "sword";
    }

    private void Update()
    {
        CheckDeath();
        ChangeEquipment();
    }

    private void ChangeEquipment()
    {
        if (Input.GetKeyDown("f"))
        {
            eqWeapon = "sword";
        }
        if (Input.GetKeyDown("g"))
        {
            eqWeapon = "magic";
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveController();

        Attack();
    }

    /// <summary>
    /// Calculates incomming damage
    /// Invoked by other enemy
    /// </summary>
    /// <param name="damage"> Amount of damage that is been invoked </param>
    public override void CalculateHealth(float damage)
    {
        if (!IsInvulable)
        {
            Health -= damage;

            gameController.SendMessage("UpdatePlayerUI", Health / MaxHealth);

            if (Health <= 0)
            {
                IsDead = true;
            }
            StartCoroutine(MakeInvulable());
        }
    }

    //TODO: change Name?
    protected override void MoveController()
    {
        FaceDirection();

        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        if (v != 0 || h != 0)
        {
            anim.SetBool("IsWalking", true);

            anim.SetFloat("X", h);
            anim.SetFloat("Y", v);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }

        Vector2 deltaForce = new Vector2(h, v);

        //TODO: Rather rigidbody.velocity?
        rigidbody.position += deltaForce * Time.fixedDeltaTime * Speed;
    }

    protected override void FaceDirection()
    {
        if (Input.GetAxisRaw("Vertical") == 1)
        {
            faceDirection = DIRECTIONS.NORTH;
            return;
        }
        else if (Input.GetAxisRaw("Vertical") == -1)
        {
            faceDirection = DIRECTIONS.SOUTH;
            return;
        }


        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            faceDirection = DIRECTIONS.EAST;
            return;
        }
        else if (Input.GetAxisRaw("Horizontal") == -1)
        {
            faceDirection = DIRECTIONS.WEST;
            return;
        }
    }

    protected override void Attack()
    {
        if (eqWeapon == "sword")
        {
            // TODO: Observe potential buffer overflow?
            if (Input.GetMouseButton(0) && Time.time > nextAttack)
            {
                Debug.Log(Time.time);
                nextAttack = Time.time + 1 / AttackSpeed;

                //anim.SetBool("IsAttacking", true);

                //Debug.Log("Attack!");

                ImitAttack(transform.position, false);
            }
        }

        if (eqWeapon == "magic")
        {
            if (Input.GetMouseButton(0) && bMayAttack && Time.time > nextAttack)
            {
                nextAttack = Time.time + AttackSpeed;

                ImitAttack(transform.position, true);
            }
        }
    }

    private void ImitAttack(Vector3 attackObjPosition, bool isProjectile)
    {
        switch (faceDirection)
        {
            case DIRECTIONS.NORTH:
                attackObjPosition.y += AttackRange;
                break;
            case DIRECTIONS.EAST:
                attackObjPosition.x += AttackRange;
                break;
            case DIRECTIONS.SOUTH:
                attackObjPosition.y -= AttackRange;
                break;
            case DIRECTIONS.WEST:
                attackObjPosition.x -= AttackRange;
                break;
            case DIRECTIONS.CENTER:
                break;
            default:
                break;
        }

        if (isProjectile)
        {
            GameObject projectileInstance = Instantiate(attackObj[(int)EQWEAPON.MAGIC], attackObjPosition, Quaternion.identity);

            switch (faceDirection)
            {
                case DIRECTIONS.NORTH:
                    projectileInstance.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 5);
                    break;
                case DIRECTIONS.EAST:
                    projectileInstance.GetComponent<Rigidbody2D>().velocity = new Vector3(5, 0);
                    break;
                case DIRECTIONS.SOUTH:
                    projectileInstance.GetComponent<Rigidbody2D>().velocity = new Vector3(5, 0);
                    break;
                case DIRECTIONS.WEST:
                    projectileInstance.GetComponent<Rigidbody2D>().velocity = new Vector3(-5, 0);
                    break;
                case DIRECTIONS.CENTER:
                    Destroy(projectileInstance);
                    break;
                default:
                    Destroy(projectileInstance);
                    break;
            }

            Destroy(projectileInstance, .5f);
        }
        else
        {
            GameObject attackInstance = Instantiate(attackObj[(int)EQWEAPON.SWORD], attackObjPosition, Quaternion.identity,
                transform);

            Destroy(attackInstance, .5f);
        }
    }
}
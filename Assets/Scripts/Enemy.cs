using UnityEngine;

public class Enemy : ACharacter
{

    public float speed = 1f;
    public float accurate = 100.0f;
    private Vector3 playerPos;

    private void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    protected override void Attack()
    {
        // throw new System.NotImplementedException();
    }

    protected override void FaceDirection()
    {
        // throw new System.NotImplementedException();
    }

    protected override void MoveController()
    {
        MirrorPlayerPattern();
    }

    private void Update()
    {
        CheckDeath();
        MoveController();
    }

    protected void FollowPlayerPattern()
    {
        Vector3 direction = playerPos - this.transform.position;

        Debug.DrawLine(transform.position, playerPos);

        if (Vector3.Distance(transform.position, playerPos) >= accurate)
        {
            transform.Translate(direction.normalized * Time.deltaTime * speed);
        }
    }

    protected void MirrorPlayerPattern()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        // TODO: Physics?
        transform.Translate(-h * Time.deltaTime * speed, v * Time.deltaTime * speed, 0);
    }
}
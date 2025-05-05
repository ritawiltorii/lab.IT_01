using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour, IGameFlow
{
    [SerializeField] Game_State state = Game_State.NONE;
    [Space]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float forceValue;
    [SerializeField] float angleRotation;
    [SerializeField] float rotSpeed;

    [SerializeField, Range(1, 10)] float fallingMultiplerVal;
    [Header("Animation"), Space]
    [SerializeField] Animator anim;

    [SerializeField] bool isAlive = false;


    private void Update()
    {
        if (isAlive)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                rb.AddForce(Vector3.up * forceValue);
            if (rb.velocity.y > 0)
            {
                rb.gravityScale = 1;
                //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles
                //                                            , Vector3.forward * angleRotation
                //                                            , Time.deltaTime * rotSpeed);
            }
            else if (rb.velocity.y < 0)
            {
                rb.gravityScale = fallingMultiplerVal;
                //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles
                //                            , Vector3.back * angleRotation 
                //                            , Time.deltaTime * rotSpeed);
            }
            anim.Play(KeyName.FLY);
        }
        else
            anim.Play(KeyName.IDLE);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(KeyName.GROUND))
        {
            InGameManager.Instance.GameOver();
        }
        if(collision.gameObject.CompareTag(KeyName.OBSTACLE))
            InGameManager.Instance.GameOver();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(KeyName.TRIGGER))
        {
            if (collision.GetComponentInParent<Pipe>() != null)
                InGameManager.Instance.AddPoint((int)collision.GetComponentInParent<Pipe>().GetTypePipe());
        }
    }
    public void GamePrepare()
    {
        transform.position = Vector3.zero;
        rb.velocity = Vector3.zero;
        rb.simulated = false;
        isAlive = false;
        state = Game_State.PREPARE;
    }

    public void GameStart()
    {
        isAlive = true;
        rb.simulated = true;
        state = Game_State.START;
    }

    public void GamePause()
    {
        isAlive = false;
        rb.simulated = false;
        state = Game_State.PAUSE;
    }

    public void GameResume()
    {
        isAlive = true;
        rb.isKinematic = true;
        state = Game_State.RESUME;

    }

    public void GameOver()
    {
        isAlive = false;
        rb.simulated = false;
        state = Game_State.OVER;
    }

    public void GameWin()
    {
        isAlive = false;
        rb.simulated = false;
        state = Game_State.WIN;
    }
}
public struct KeyName
{
    public const string IDLE = "Idle", FLY = "Fly", GROUND = "Ground", OBSTACLE = "Obstacle";
    public const string TRIGGER = "Trigger";
}

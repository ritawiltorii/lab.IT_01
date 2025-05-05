using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] Type_Pipe type;
    [SerializeField] Rigidbody2D rb;
    
    private float speed;
    public Type_Pipe GetTypePipe() => type;
    bool isSetup = false;
    
    private void Awake()
    {
        isSetup = false;
    }
    // Viet 1 method: Set up position, set up speed cho pipe
    public void Setup(Vector2 position, float speed)
    {
        transform.position = position;
        this.speed = speed;
        isSetup = true;
    }

    // Viet 1 method: Thay doi trang thai cua ong, lam cho ong di chuyen
    // Viet controller cho pipe
    // Neu chua setup, pipe khong di chuyen
    private void Update()
    {
        if (!isSetup) return;
        rb.velocity = Vector2.left * speed;
    }
}
public enum Type_Pipe
{
    One = 1,
    Two = 2
}

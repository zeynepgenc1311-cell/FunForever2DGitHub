using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Input al
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // Çapraz hareketi engelle
        if (Mathf.Abs(x) > 0) y = 0;
        else if (Mathf.Abs(y) > 0) x = 0;

        // float değerleri -1,0,1 olarak ayarla (transition tetiklemek için kesin)
        float moveX = x > 0 ? 1 : (x < 0 ? -1 : 0);
        float moveY = y > 0 ? 1 : (y < 0 ? -1 : 0);

        // Animator parametrelerine gönder
        anim.SetFloat("MoveX", moveX);
        anim.SetFloat("MoveY", moveY);
        anim.SetBool("IsMoving", moveX != 0 || moveY != 0);

        // Rigidbody ile hareket ettir
        rb.velocity = new Vector2(moveX, moveY) * moveSpeed;
    }
}

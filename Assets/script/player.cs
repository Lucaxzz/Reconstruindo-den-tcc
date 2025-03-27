using UnityEngine;

public class player : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidade de movimento
    public Animator animator;

    private Rigidbody2D rb;
    private bool facingRight = true; // Direção inicial do personagem
    private Vector3 originalScale; // Guarda a escala original do personagem

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        originalScale = transform.localScale; // Salva a escala original
    }

    private void Update()
    {
        // Verifica se o personagem está se movendo
        if (Input.GetAxis("Horizontal") != 0)
        {
            // Está correndo
            animator.SetBool("taCorrendo", true);
        }
        else
        {
            // Está parado
            animator.SetBool("taCorrendo", false);
        }

        // Movimento para a esquerda e direita
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Flip para inverter a direção do personagem
        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }

        // Garante que a escala do personagem seja a original
        transform.localScale = new Vector3(originalScale.x * (facingRight ? 1 : -1), originalScale.y, originalScale.z);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        // Inverte a direção do personagem
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}

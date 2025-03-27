using UnityEngine;

public class PetFollow : MonoBehaviour
{
    public Transform target; // Referência ao transform do jogador que o pet deve seguir.
    public float followSpeed = 3.0f; // Velocidade de seguimento do pet quando próximo.
    public float maxFollowSpeed = 5.0f; // Velocidade máxima de seguimento do pet quando distante.
    public float boostedFollowSpeed = 7.0f; // Velocidade extra quando o pet está atrás do jogador e o jogador está correndo.
    public float followDistance = 2.0f; // Distância para começar a seguir o jogador.
    public float distanceForBoost = 3.0f; // Distância para aplicar o impulso extra
    public float stopDistance = 1.0f; // Distância para parar o movimento
    public float flipThreshold = 0.1f; // Margem para o flip
    public Animator animator; // Componente Animator para controlar animações

    private Vector3 initialScale; // Escala inicial do pet.
    private bool facingRight = true; // Direção inicial do pet

    private void Start()
    {
        initialScale = transform.localScale; // Salva a escala inicial do pet.
        if (animator == null) // Verifica se o Animator está atribuído
        {
            animator = GetComponent<Animator>(); // Obtém o componente Animator
        }
    }

    private void Update()
    {
        if (target != null)
        {
            // Calcule a direção horizontal do pet para o jogador.
            Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
            Vector3 moveDirection = targetPosition - transform.position;

            // Verifique a distância entre o pet e o jogador
            float distanceToTarget = Mathf.Abs(moveDirection.x);

            // Determine a velocidade de seguimento com base na distância
            float speed = Mathf.Lerp(followSpeed, maxFollowSpeed, Mathf.InverseLerp(followDistance, distanceForBoost, distanceToTarget));

            // Adiciona um impulso extra se o pet estiver atrás do jogador e o jogador estiver correndo
            float playerSpeed = target.GetComponent<Rigidbody2D>().velocity.x;
            if (playerSpeed > 0 && moveDirection.x < 0) // Se o jogador está correndo para frente e o pet está atrás
            {
                speed = boostedFollowSpeed;
            }

            // Se a distância for menor que a distância de parada, pare o movimento
            if (distanceToTarget < stopDistance)
            {
                speed = 0;
                animator.SetBool("taCorrendo", false);
            }
            else
            {
                // Mantenha o movimento apenas no eixo X.
                moveDirection = new Vector3(moveDirection.x, 0, 0).normalized;
                transform.position += moveDirection * speed * Time.deltaTime;

                // Atualize a direção do pet com base na direção do movimento
                if (moveDirection.x > flipThreshold && !facingRight) // Se movendo para a direita e está virado para a esquerda
                {
                    Flip();
                }
                else if (moveDirection.x < -flipThreshold && facingRight) // Se movendo para a esquerda e está virado para a direita
                {
                    Flip();
                }

                // Atualize a animação para correndo
                animator.SetBool("taCorrendo", true);
            }
        }
        else
        {
            // Atualize a animação para idle se não houver alvo
            animator.SetBool("taCorrendo", false);
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;

        // Inverte a escala no eixo X imediatamente
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
}

using UnityEngine;

public class arvore : MonoBehaviour
{
    public int maxHitCount = 2; // Número de vezes que a árvore pode ser atingida antes de desaparecer

    private int hitCount = 0; // Contador de hits recebidos

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se a água colidiu com a árvore de forma case-insensitive
        if (other.CompareTag("Agua"))
        {
            hitCount++; // Incrementa o contador de hits

            if (hitCount >= maxHitCount)
            {
                // Destrói o GameObject da árvore
                Destroy(gameObject);
            }

            // Destrói o GameObject da água
            Destroy(other.gameObject);
        }
    }
}

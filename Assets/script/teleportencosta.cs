using UnityEngine;

public class teleportencosta : MonoBehaviour
{
    [SerializeField] private Transform destination; // Arraste um GameObject vazio aqui

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Verifica se o jogador entrou no trigger
        {
            other.transform.position = destination.position; // Move o jogador para o destino
        }
    }
}

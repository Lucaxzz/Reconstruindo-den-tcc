using UnityEngine;

public class bttp : MonoBehaviour
{
    [SerializeField] private Transform destination; // O local onde o jogador será teleportado
    private GameObject player; // Para armazenar a referência do jogador
    private bool canTeleport = false; // Verifica se o jogador está dentro do trigger

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Verifica se o objeto que entrou no trigger é o jogador
        {
            player = other.gameObject; // Armazena a referência do jogador
            canTeleport = true; // Ativa a possibilidade de teleportar o jogador
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Quando o jogador sai do trigger
        {
            canTeleport = false; // Desativa a possibilidade de teleportar
            player = null; // Limpa a referência do jogador
        }
    }

    private void Update()
    {
        // Se o jogador está no trigger e pressiona "E", teleporta o jogador para o destino
        if (canTeleport && player != null && Input.GetKeyDown(KeyCode.E) && destination != null)
        {
            player.transform.position = new Vector3(destination.position.x, destination.position.y, player.transform.position.z); // Mantém a posição Z original
        }
    }
}

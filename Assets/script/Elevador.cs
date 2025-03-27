using UnityEngine;

public class Elevador : MonoBehaviour
{
    public Transform destino; // Ponto para onde o jogador será teletransportado
    public Transform jogador; // Transform do jogador
    public float distanciaParaAcionar = 3.0f; // Distância para interação

    private Vector3 ultimaPosicao; // Armazena a última posição do jogador
    private bool noDestino = false; // Verifica se o jogador está no destino

    private void Update()
    {
        // Verifica se o jogador está próximo do elevador
        if (Vector3.Distance(jogador.position, transform.position) <= distanciaParaAcionar)
        {
            // Verifica se a tecla E foi pressionada
            if (Input.GetKeyDown(KeyCode.E))
            {
                AlternarPosicao();
            }
        }
    }

    void AlternarPosicao()
    {
        if (noDestino)
        {
            // Teletransporta o jogador de volta para a última posição
            jogador.position = ultimaPosicao;
        }
        else
        {
            // Armazena a posição atual do jogador antes de mover para o destino
            ultimaPosicao = jogador.position;

            // Teletransporta o jogador para o destino
            if (destino != null)
            {
                jogador.position = destino.position;
            }
        }

        // Alterna o estado da posição
        noDestino = !noDestino;
    }
}

using UnityEngine;

public class Gerador : MonoBehaviour
{
    public GameObject fundoEscuro; // Sprite do fundo escuro
    public GameObject fundoClaro; // Sprite do fundo claro
    public GameObject alavanca; // Alavanca da porta
    public Transform jogador; // Transform do jogador
    public float distanciaParaAcionar = 3.0f; // Distância para interação
    public Sprite geradorNormal; // Sprite normal do gerador
    public Sprite geradorFuncionando; // Sprite do gerador funcionando

    private bool geradorAtivado = false; // Verifica se o gerador já foi ativado
    private SpriteRenderer spriteRenderer; // SpriteRenderer do gerador

    private void Start()
    {
        // Inicialmente, o fundo está escuro e o gerador está no estado normal
        if (fundoEscuro != null && fundoClaro != null)
        {
            fundoClaro.SetActive(false);
        }

        // Obtém o componente SpriteRenderer do gerador
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = geradorNormal;
        }
    }

    private void Update()
    {
        // Verifica se o jogador está próximo do gerador
        if (Vector3.Distance(jogador.position, transform.position) <= distanciaParaAcionar)
        {
            // Verifica se a tecla E foi pressionada
            if (Input.GetKeyDown(KeyCode.E))
            {
                AcionarGerador();
            }
        }
    }

    void AcionarGerador()
    {
        if (!geradorAtivado)
        {
            // Ativa o fundo claro e desativa o fundo escuro
            if (fundoEscuro != null && fundoClaro != null)
            {
                fundoEscuro.SetActive(false);
                fundoClaro.SetActive(true);
            }

            // Marca o gerador como ativado
            geradorAtivado = true;

            // Troca o sprite do gerador para o estado funcionando
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = geradorFuncionando;
            }

            // Permite que a alavanca funcione
            if (alavanca != null)
            {
                alavanca.GetComponent<porta>().ativarAlavanca(); // Método para ativar a alavanca
            }
        }
    }
}

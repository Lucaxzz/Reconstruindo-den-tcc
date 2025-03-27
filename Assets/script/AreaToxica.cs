using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AreaToxica : MonoBehaviour
{
    public GameObject canvasEstamina; // O Canvas da estamina (contém a barra)
    public Slider barraEstamina; // A barra de estamina (Slider)
    public float estaminaMaxima = 100f; // Estamina máxima
    public float consumoPorSegundo = 20f; // Redução de estamina por segundo dentro da área tóxica
    public float regeneracaoPorSegundo = 10f; // Regeneração da estamina fora da área tóxica
    private float estaminaAtual; // Valor atual da estamina
    private bool dentroDaAreaToxica = false; // Se o jogador está na área tóxica
    private bool estaminaVisivel = false; // Se o canvas da estamina está visível
    private bool estaminaEsmaecendo = false; // Se a barra de estamina está diminuindo lentamente
    private bool regenerandoEstamina = false; // Controle de regeneração

    void Start()
    {
        estaminaAtual = estaminaMaxima;
        barraEstamina.maxValue = estaminaMaxima;
        barraEstamina.value = estaminaAtual;
        canvasEstamina.SetActive(false); // O canvas da estamina começa invisível
    }

    void Update()
    {
        // Se o jogador estiver dentro da área tóxica
        if (dentroDaAreaToxica)
        {
            DiminuirEstamina();
        }
        else if (!dentroDaAreaToxica && estaminaAtual < estaminaMaxima)
        {
            RegenerarEstaminaGradualmente();
        }

        // Atualiza a visibilidade da barra de estamina
        AtualizarVisibilidadeBarraEstamina();
    }

    void DiminuirEstamina()
    {
        if (estaminaAtual > 0)
        {
            estaminaAtual -= consumoPorSegundo * Time.deltaTime;
            barraEstamina.value = estaminaAtual;
        }
        else
        {
            // Quando a estamina acabar
            estaminaAtual = 0;
            barraEstamina.value = estaminaAtual;
        }

        // Se a estamina está baixa, mantém a barra visível
        if (!estaminaVisivel && estaminaAtual < estaminaMaxima)
        {
            canvasEstamina.SetActive(true); // Torna o canvas da estamina visível
            estaminaVisivel = true;
        }
    }

    void RegenerarEstaminaGradualmente()
    {
        if (estaminaAtual < estaminaMaxima)
        {
            // A regeneração vai gradualmente até o valor máximo
            estaminaAtual += regeneracaoPorSegundo * Time.deltaTime;
            barraEstamina.value = estaminaAtual;

            if (estaminaAtual >= estaminaMaxima)
            {
                estaminaAtual = estaminaMaxima;
                barraEstamina.value = estaminaAtual;
                regenerandoEstamina = false; // Regeneração terminada
            }
        }
    }

    void AtualizarVisibilidadeBarraEstamina()
    {
        // Se a estamina está cheia, começa o esmaecimento da barra
        if (estaminaAtual == estaminaMaxima && !estaminaEsmaecendo)
        {
            StartCoroutine(EsmaecerEstamina());
        }

        // Se a estamina ainda não estiver cheia, mantém a barra visível
        if (estaminaAtual < estaminaMaxima && !estaminaVisivel)
        {
            canvasEstamina.SetActive(true); // Torna o canvas visível enquanto regenerando
            estaminaVisivel = true;
        }
    }

    // Esmaecer a barra de estamina quando estiver cheia
    IEnumerator EsmaecerEstamina()
    {
        estaminaEsmaecendo = true;
        while (estaminaAtual == estaminaMaxima)
        {
            // Reduz a visibilidade da barra suavemente (transição)
            Color cor = barraEstamina.fillRect.GetComponent<Image>().color;
            cor.a = Mathf.Lerp(cor.a, 0.2f, 0.1f); // Transição de opacidade
            barraEstamina.fillRect.GetComponent<Image>().color = cor;

            yield return new WaitForSeconds(0.1f);
        }

        estaminaEsmaecendo = false;
    }

    // Detecta quando o jogador entra ou sai da área tóxica
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dentroDaAreaToxica = true;
            // A estamina começa cheia quando o jogador entra na área tóxica
            estaminaAtual = estaminaMaxima;
            barraEstamina.value = estaminaAtual;
            estaminaVisivel = true;
            canvasEstamina.SetActive(true); // Torna o canvas da estamina visível
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dentroDaAreaToxica = false;
            // Começa a regenerar a estamina quando o jogador sair da área tóxica
            regenerandoEstamina = true;
        }
    }
}

using UnityEngine;

public class porta : MonoBehaviour
{
    public GameObject portaFechada;
    public GameObject portaAberta;
    public Sprite alavancaAcionado; // Sprite para o estado acionado
    public Transform jogador;
    public float distanciaParaAcionar = 3.0f;

    private SpriteRenderer spriteRenderer;
    private bool acionada = false; // Verifica se a alavanca já foi acionada
    private bool podeSerAcionada = false; // Verifica se a alavanca pode ser acionada

    private void Start()
    {
        // Obtém o componente SpriteRenderer da alavanca
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (podeSerAcionada && Vector3.Distance(jogador.position, transform.position) <= distanciaParaAcionar)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Acionar();
            }
        }
    }

    void Acionar()
    {
        if (!acionada)
        {
            if (portaFechada.activeSelf)
            {
                portaFechada.SetActive(false);
                portaAberta.SetActive(true);

                if (spriteRenderer != null)
                {
                    spriteRenderer.sprite = alavancaAcionado;
                }

                acionada = true;
            }
        }
    }

    // Função pública para ativar a alavanca
    public void ativarAlavanca()
    {
        podeSerAcionada = true;
    }
}

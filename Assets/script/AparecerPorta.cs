using UnityEngine;

public class AparecerPorta : MonoBehaviour
{
    public GameObject porta; // Objeto da porta
    public Transform posicaoFinal; // Posição final da porta quando subir
    public float velocidadeSubida = 2f; // Velocidade da animação
    private bool portaAtiva = false;
    private bool portaSubiu = false;
    private bool jogadorPerto = false;

    void Start()
    {
        if (porta != null)
            porta.SetActive(false); // Deixa a porta invisível no começo
    }

    void Update()
    {
        if (portaAtiva && !portaSubiu)
        {
            SubirPorta();
        }

        if (portaSubiu && jogadorPerto && Input.GetKeyDown(KeyCode.E))
        {
            InteragirComPorta();
        }
    }

    public void AtivarPorta()
    {
        if (porta != null)
        {
            porta.SetActive(true);
            portaAtiva = true;
            Debug.Log("Porta ativada!");
        }
    }

    void SubirPorta()
    {
        if (porta.transform.position.y < posicaoFinal.position.y)
        {
            porta.transform.position = Vector3.MoveTowards(porta.transform.position, posicaoFinal.position, velocidadeSubida * Time.deltaTime);
            Debug.Log("Porta subindo...");
        }
        else
        {
            portaSubiu = true;
            Debug.Log("Porta chegou ao topo!");
        }
    }

    void InteragirComPorta()
    {
        Debug.Log("Porta aberta! Próximo nível ativado.");
        // Aqui você pode colocar a lógica para mudar de cena ou outra ação desejada.
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = false;
        }
    }
}

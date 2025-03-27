using UnityEngine;
using System.Collections;

public class bau : MonoBehaviour
{
    public Sprite openChestSprite; // Sprite do baú aberto
    public GameObject hiddenItem; // Item que será dropado
    public float jumpHeight = 2f; // Altura do pulo
    public float jumpDistance = 2f; // Distância lateral
    public float jumpDuration = 0.5f; // Tempo do pulo
    public float bounceHeight = 0.5f; // Altura da quicada
    public float bounceDuration = 0.2f; // Duração da quicada
    public float stayTime = 120f; // Tempo que o item fica na posição final (2 minutos)
    public float floatSpeed = 1f; // Velocidade da flutuação
    public float floatAmplitude = 0.2f; // Altura da flutuação

    private SpriteRenderer spriteRenderer;
    private bool isOpened = false;
    private Coroutine floatCoroutine; // Para controlar a flutuação
    private bool isInTrigger = false; // Verifica se o jogador está dentro do trigger

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        hiddenItem.SetActive(false); // Esconde o item no começo
    }

    void Update()
    {
        // Verifica se o jogador apertou "E" e está dentro do trigger
        if (isInTrigger && Input.GetKeyDown(KeyCode.E) && !isOpened)
        {
            OpenChest();
        }
    }

    // Método para detectar quando o jogador entra no trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Verifica se o objeto é o jogador
        {
            isInTrigger = true; // O jogador entrou no trigger
        }
    }

    // Método para detectar quando o jogador sai do trigger
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Verifica se o objeto é o jogador
        {
            isInTrigger = false; // O jogador saiu do trigger
        }
    }

    // Método para abrir o baú e fazer a interação
    void OpenChest()
    {
        isOpened = true;
        spriteRenderer.sprite = openChestSprite; // Troca o sprite do baú
        hiddenItem.SetActive(true); // Ativa o item
        StartCoroutine(MoveItem(hiddenItem.transform));
    }

    // Método para mover o item de forma suave
    IEnumerator MoveItem(Transform item)
    {
        if (item == null) yield break; // ❗ Evita erros se o objeto foi destruído

        float elapsedTime = 0;
        Vector2 startPos = item.position;
        Vector2 endPos = startPos + new Vector2(jumpDistance, 0); // Move para o lado

        // Primeiro pulo em arco
        while (elapsedTime < jumpDuration)
        {
            if (item == null) yield break; // ❗ Verifica se o objeto foi destruído

            float t = elapsedTime / jumpDuration;
            float height = Mathf.Sin(t * Mathf.PI) * jumpHeight; // Faz um arco suave
            item.position = Vector2.Lerp(startPos, endPos, t) + new Vector2(0, height);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ajusta posição final
        if (item != null)
        {
            item.position = endPos;
        }

        // Pequena quicada
        elapsedTime = 0;
        Vector2 bounceStart = endPos;
        Vector2 bouncePeak = bounceStart + new Vector2(0, bounceHeight); // Ponto mais alto da quicada

        while (elapsedTime < bounceDuration)
        {
            if (item == null) yield break;

            float t = elapsedTime / bounceDuration;
            float height = Mathf.Sin(t * Mathf.PI) * bounceHeight;
            item.position = Vector2.Lerp(bounceStart, bouncePeak, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // O item agora fica na posição final e começa a flutuar
        if (item != null)
        {
            item.position = bounceStart;
            floatCoroutine = StartCoroutine(FloatEffect(item));
        }

        // Espera 2 minutos antes de desaparecer
        yield return new WaitForSeconds(stayTime);

        // Para a flutuação antes de resetar
        if (floatCoroutine != null)
        {
            StopCoroutine(floatCoroutine);
            floatCoroutine = null;
        }

        // Desativa o item sem erro
        if (item != null && item.gameObject.activeSelf)
        {
            item.gameObject.SetActive(false);
        }
    }

    // Método para fazer o item flutuar
    IEnumerator FloatEffect(Transform item)
    {
        Vector2 originalPos = item.position;
        float elapsedTime = 0;

        while (item != null && item.gameObject.activeSelf) // ✅ Garante que o item existe e está ativo
        {
            elapsedTime += Time.deltaTime * floatSpeed;
            float newY = originalPos.y + Mathf.Sin(elapsedTime) * floatAmplitude;
            item.position = new Vector2(originalPos.x, newY);
            yield return null;
        }
    }
}

using UnityEngine;
using TMPro;
using System.Collections;

public class nuvem : MonoBehaviour
{
    public GameObject textPanel; // Painel de texto
    public TMP_Text textDisplay; // Objeto de texto TMP
    public string firstMessage; // Primeiro texto a ser exibido
    public string secondMessage; // Segundo texto a ser exibido
    public float textSpeed = 0.05f; // Velocidade da digitação
    public float delayBetweenTexts = 1f; // Tempo antes do segundo texto
    public float panelHideDelay = 1f; // Tempo antes do painel sumir após o segundo texto
    public float fadeDuration = 0.5f; // Tempo de esmaecimento

    private bool hasTriggered = false; // Para garantir que só acontece uma vez
    private GameObject playerObj; // Referência ao jogador
    private Animator playerAnimator; // Referência ao Animator do jogador
    private player playerScript; // Referência ao script de movimento
    private CanvasGroup panelCanvasGroup; // Para controlar o fade-out

    void Start()
    {
        textPanel.SetActive(false); // Esconde o painel no início
        panelCanvasGroup = textPanel.GetComponent<CanvasGroup>();

        if (panelCanvasGroup == null)
        {
            panelCanvasGroup = textPanel.AddComponent<CanvasGroup>(); // Adiciona se não existir
        }

        panelCanvasGroup.alpha = 0; // Começa invisível
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            playerObj = other.gameObject;
            playerAnimator = playerObj.GetComponent<Animator>();
            playerScript = playerObj.GetComponent<player>();

            if (playerScript != null)
            {
                playerScript.enabled = false; // Desativa o script de movimento
            }

            if (playerAnimator != null)
            {
                playerAnimator.SetBool("taCorrendo", false); // Garante que fique em idle
            }

            StartCoroutine(ShowTextSequence());
        }
    }

    IEnumerator ShowTextSequence()
    {
        textPanel.SetActive(true);
        yield return StartCoroutine(FadePanel(1)); // Esmaecimento para aparecer
        yield return StartCoroutine(TypeText(firstMessage));
        yield return new WaitForSeconds(delayBetweenTexts);
        yield return StartCoroutine(TypeText(secondMessage));
        yield return new WaitForSeconds(panelHideDelay);
        yield return StartCoroutine(FadePanel(0)); // Esmaecimento para desaparecer
        textPanel.SetActive(false);

        if (playerScript != null)
        {
            playerScript.enabled = true; // Reativa o script de movimento depois do diálogo
        }
    }

    IEnumerator TypeText(string message)
    {
        textDisplay.text = "";
        foreach (char letter in message.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    IEnumerator FadePanel(float targetAlpha)
    {
        float startAlpha = panelCanvasGroup.alpha;
        float time = 0;

        while (time < fadeDuration)
        {
            panelCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        panelCanvasGroup.alpha = targetAlpha;
    }
}

using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class objsendoscaneado : MonoBehaviour
{
    public GameObject scanPanel; // Painel do escaneamento
    public RectTransform scanPanelTransform; // Transform do painel para animação
    public TMP_Text scanText; // Texto de exibição das informações
    public GameObject scanImageObject; // Imagem do objeto escaneado
    public UnityEngine.UI.Button closeButton; // Botão para fechar
    public string objectInfo; // Informação do objeto
    public float textSpeed = 0.05f; // Velocidade da animação de texto
    public float panelMoveSpeed = 5f; // Velocidade da animação do painel
    public float movementDisableTime = 1f; // Tempo antes de permitir fechar com movimento

    private bool isNear;
    private bool isTyping;
    private bool isPanelActive;
    private bool canCloseFromMovement;

    public MissionManager missionManager; // Referência ao MissionManager

    private Vector3 panelStartPos;
    private Vector3 panelEndPos;
    private Vector3 panelHidePos;

    void Start()
    {
        scanPanel.SetActive(false);
        closeButton.onClick.AddListener(CloseScan);
        scanImageObject.SetActive(false);

        // Define as posições para animação do painel
        panelStartPos = new Vector3(scanPanelTransform.anchoredPosition.x, -Screen.height, 0);
        panelEndPos = new Vector3(scanPanelTransform.anchoredPosition.x, 0, 0);
        panelHidePos = new Vector3(scanPanelTransform.anchoredPosition.x, -Screen.height, 0);
        scanPanelTransform.anchoredPosition = panelStartPos;
    }

    void Update()
    {
        if (isNear && Input.GetKeyDown(KeyCode.F) && !isTyping)
        {
            StartCoroutine(ShowScanPanel());
            missionManager.CompleteMission(gameObject); // Conclui a missão ao escanear
        }

        // Fecha o painel se o jogador se mover após 1 segundo
        if (isPanelActive && canCloseFromMovement && (Input.GetKeyDown(KeyCode.Space) || Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            StartCoroutine(HideScanPanel());
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = false;
        }
    }

    IEnumerator ShowScanPanel()
    {
        scanPanel.SetActive(true);
        scanImageObject.SetActive(true);
        isPanelActive = true;
        canCloseFromMovement = false;

        // Animação do painel subindo
        yield return StartCoroutine(MovePanel(panelStartPos, panelEndPos, panelMoveSpeed));

        // Efeito de digitação do texto
        yield return StartCoroutine(TypeText(objectInfo));

        closeButton.gameObject.SetActive(true);
        yield return new WaitForSeconds(movementDisableTime);
        canCloseFromMovement = true;
    }

    IEnumerator HideScanPanel()
    {
        isPanelActive = false;

        // Animação do painel descendo
        yield return StartCoroutine(MovePanel(panelEndPos, panelHidePos, panelMoveSpeed));

        scanPanel.SetActive(false);
        scanImageObject.SetActive(false);
        scanPanelTransform.anchoredPosition = panelStartPos;
    }

    IEnumerator MovePanel(Vector3 start, Vector3 end, float speed)
    {
        float elapsedTime = 0;
        while (elapsedTime < 1f)
        {
            scanPanelTransform.anchoredPosition = Vector3.Lerp(start, end, elapsedTime);
            elapsedTime += Time.deltaTime * speed;
            yield return null;
        }
        scanPanelTransform.anchoredPosition = end;
    }

    IEnumerator TypeText(string text)
    {
        scanText.text = "";
        closeButton.gameObject.SetActive(false); // Esconde o botão enquanto digita
        isTyping = true;

        foreach (char letter in text.ToCharArray())
        {
            scanText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
        closeButton.gameObject.SetActive(true); // Exibe o botão ao final
    }

    void CloseScan()
    {
        StartCoroutine(HideScanPanel());
    }
}

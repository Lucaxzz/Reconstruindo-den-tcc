using UnityEngine;
using UnityEngine.UI;

public class botaomissao : MonoBehaviour
{
    public GameObject missionPanel; // Painel de missões
    public Button closeButton; // Botão de fechar

    void Start()
    {
        missionPanel.SetActive(false); // Garante que o painel começa oculto
        
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(CloseMissionPanel); // Adiciona a função ao botão de fechar
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) // Detecta quando a tecla Tab é pressionada
        {
            ToggleMissionPanel();
        }
    }

    public void ToggleMissionPanel()
    {
        missionPanel.SetActive(!missionPanel.activeSelf); // Alterna entre mostrar/esconder
    }

    public void CloseMissionPanel()
    {
        missionPanel.SetActive(false); // Fecha o painel
    }
}

using UnityEngine;
using UnityEngine.UI;

public class interaçaoobj : MonoBehaviour
{
    public Image inventoryItemIcon; // Imagem do item no inventário (UI)
    public Sprite itemIcon; // Ícone do item que será mostrado no inventário
    public MissionManager missionManager; // Referência ao MissionManager

    private bool isNear = false; // Verifica se o jogador está perto do item

    void Update()
    {
        if (isNear && Input.GetKeyDown(KeyCode.E)) // Pressiona "E" para pegar o item
        {
            PickupItem();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = true; // Jogador está perto do item
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = false; // Jogador saiu da área do item
        }
    }

    void PickupItem()
    {
        // Ativa o ícone do item no inventário
        inventoryItemIcon.sprite = itemIcon; // Define o ícone do item
        inventoryItemIcon.gameObject.SetActive(true); // Ativa a imagem no Canvas

        // Concluir missão associada ao item
        if (missionManager != null)
        {
            missionManager.CompleteMission(gameObject); // Conclui a missão ao pegar o item
        }

        Destroy(gameObject); // Remove o item da cena
    }
}

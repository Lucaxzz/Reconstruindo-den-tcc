using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    public GameObject inventorySlot; // Slot do inventário onde o item aparecerá
    public Sprite itemIcon; // Ícone do item no inventário
    public MissionManager missionManager; // Referência ao MissionManager

    private bool isNear;
    private GameObject player;

    void Start()
    {
        if (inventorySlot != null)
            inventorySlot.SetActive(false); // Oculta o ícone no início
    }

    void Update()
    {
        if (isNear && Input.GetKeyDown(KeyCode.E)) // Pressiona "E" para pegar
        {
            PickupItem();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = true;
            player = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = false;
        }
    }

    void PickupItem()
    {
        if (inventorySlot != null)
        {
            inventorySlot.SetActive(true); // Ativa o ícone no inventário
            inventorySlot.GetComponent<Image>().sprite = itemIcon; // Define o ícone
        }

        // Concluir missão associada ao item
        if (missionManager != null)
        {
            missionManager.CompleteMission(gameObject);
        }

        Destroy(gameObject); // Remove o item da cena
    }
}

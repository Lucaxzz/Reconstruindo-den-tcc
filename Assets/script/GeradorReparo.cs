using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GeradorReparo : MonoBehaviour
{
    public GameObject[] geradorSlots; // Locais onde os itens aparecerão no gerador
    public Sprite geradorConsertadoSprite; // Imagem do gerador consertado
    public List<Image> inventarioSlots; // Slots do inventário (UI)
    public SpriteRenderer geradorSpriteRenderer; // Referência ao SpriteRenderer do gerador

    private bool jogadorPerto = false;
    private bool geradorConsertado = false;

    void Start()
    {
        foreach (GameObject slot in geradorSlots)
        {
            if (slot != null)
                slot.SetActive(false); // Desativa os slots dos itens no gerador no início
        }
    }

    void Update()
    {
        if (jogadorPerto && !geradorConsertado && Input.GetKeyDown(KeyCode.E))
        {
            if (TemTodosOsItensNoInventario()) 
            {
                AdicionarItensAoGerador();
            }
            else
            {
                Debug.Log("Ainda faltam itens no inventário!");
            }
        }
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

    bool TemTodosOsItensNoInventario()
    {
        int itensColetados = 0;
        foreach (Image slot in inventarioSlots)
        {
            if (slot != null && slot.gameObject.activeSelf && slot.sprite != null)
            {
                itensColetados++;
            }
        }
        Debug.Log("Itens coletados: " + itensColetados + "/" + geradorSlots.Length);
        return itensColetados == geradorSlots.Length;
    }

    void AdicionarItensAoGerador()
    {
        int index = 0;
        foreach (Image slot in inventarioSlots)
        {
            if (slot != null && slot.gameObject.activeSelf && slot.sprite != null)
            {
                geradorSlots[index].SetActive(true);
                geradorSlots[index].GetComponent<SpriteRenderer>().sprite = slot.sprite;
                
                // Remove o item do inventário
                slot.sprite = null;
                slot.gameObject.SetActive(false);

                index++;
            }
        }

        if (index >= geradorSlots.Length)
        {
            ConsertarGerador();
        }
    }

    void ConsertarGerador()
    {
        if (geradorSpriteRenderer != null && geradorConsertadoSprite != null)
        {
            geradorSpriteRenderer.sprite = geradorConsertadoSprite;
            geradorConsertado = true;
            Debug.Log("Gerador consertado!");

            // Ativar a porta
            AparecerPorta portaScript = FindObjectOfType<AparecerPorta>();
            if (portaScript != null)
            {
                portaScript.AtivarPorta();
            }
            else
            {
                Debug.LogError("Script da porta não encontrado!");
            }
        }
    }
}

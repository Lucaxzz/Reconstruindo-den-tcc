using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public string cena;
    public GameObject optionsPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("fase1");
    }
   public void QuitGame()
    {
        //editor unity
        UnityEditor.EditorApplication.isPlaying = false;
        //jogo completo
       //pplication.Quit();
    }

    public void ShowOptions()
    {
        optionsPanel.SetActive(true);
    }
    public void BackToMenu()
    {
        optionsPanel.SetActive(false);
    }

}

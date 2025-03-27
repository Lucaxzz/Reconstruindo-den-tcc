using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class MissionManager : MonoBehaviour
{
    [System.Serializable]
    public class Mission
    {
        public string missionName; // Nome da missão (ex: "scan_relic")
        public TMP_Text missionText; // Referência ao texto da missão no Canvas
        public GameObject missionObject; // Objeto da missão no mundo
    }

    public List<Mission> missions = new List<Mission>(); // Lista de missões
    public Color incompleteColor = Color.white;
    public Color completeColor = Color.green;

    private Dictionary<GameObject, Mission> missionDictionary = new Dictionary<GameObject, Mission>();

    void Start()
    {
        // Configura as missões no dicionário para acesso rápido
        foreach (Mission mission in missions)
        {
            if (mission.missionObject != null && mission.missionText != null)
            {
                missionDictionary[mission.missionObject] = mission;
                mission.missionText.color = incompleteColor; // Define cor inicial
            }
        }
    }

    public void CompleteMission(GameObject missionObject)
    {
        if (missionDictionary.ContainsKey(missionObject))
        {
            Mission mission = missionDictionary[missionObject];
            mission.missionText.color = completeColor;
            mission.missionText.fontStyle = FontStyles.Strikethrough; // Risco no texto
        }
        else
        {
            Debug.LogWarning("Objeto de missão não encontrado!");
        }
    }
}

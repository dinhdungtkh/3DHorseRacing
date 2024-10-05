using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameControllerNew : MonoBehaviour
{
    [SerializeField]
    public List<Horse> Horses = new List<Horse>();

    [SerializeField]
    private GameObject horsePrefab;
    [SerializeField]
    private Transform[] spawnPoints;
    public float TrackLength = 2000f;
    public TMP_Text HorseList;
    [SerializeField]
    private GameObject ResultPanel;
    [SerializeField]
    private TMP_Text resultPanelText;
    [SerializeField]
    public List<Horse> finishedHorses = new List<Horse>();
    [SerializeField]
    public float currentLeadDistance = 0 ;
    public bool firstPrized = false;
    [SerializeField]
    protected GameObject UIGameplay;
    public void StartRace()
    {
       
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GameObject newHorse = Instantiate(horsePrefab, spawnPoints[i].position, Quaternion.identity);
            newHorse.transform.parent = GameObject.Find("/HorseList/HorseContainer").transform;
            Horses.Add(newHorse.GetComponent<Horse>());
        }

        for (int i = 0; i < Horses.Count; i++)
        {
            if (Horses[i].ID != 1)
            {
                Horses[i].RandomizeBodyMaterial();
                Horses[i].ID = i + 1;
                Horses[i].transform.gameObject.name = "Horse " + Horses[i].ID;
                Horses[i].Name = "Horse " + (i + 1);
            }
            Horses[i].Speed = 25f;
            Horses[i].StartMove();
        }
        HorseList.transform.gameObject.SetActive(true);
        UIGameplay.SetActive(true);
    }

    public void Awake()
    {
       
    }
    public void Start()
    {
        
        AudioController.PlaySound(Soundnames.BACKGROUND, 1);
        Invoke("StartRace", 3f);
        ResultPanel.SetActive(false);
       
    }

    private void FixedUpdate()
    {
        SortHorsesByDistance();
        UpdateHorseListUI();
        CheckFinishedHorses();
    }

    private void CheckFinishedHorses()
    {
        firstPrized = false;
        for (int i = 0; i < Horses.Count; i++)
        {
            if (Horses[i].transform.position.z >= TrackLength && !finishedHorses.Contains(Horses[i]))
            {
                finishedHorses.Add(Horses[i]);
                if (finishedHorses.Count > 0) { firstPrized = true; }
            }
        }

        if (finishedHorses.Count > 0 && finishedHorses.Count == Horses.Count)
        {
            HorseList.text = " ";
            UIGameplay.SetActive(false);
            Invoke("ShowResultPanel", 1f);
        }
    }

    private void ShowResultPanel()
    {
        ResultPanel.SetActive(true);
        string resultText = "Results:\n";
        resultText += $"<color=#FFD700>First Place {finishedHorses[0].Name}</color>\n";
        resultText += $"<color=#C0C0C0>Second Place {finishedHorses[1].Name}</color>\n";
        resultText += $"<color=#CD7F32>Third Place {finishedHorses[2].Name}</color>\n";
        for (int i = 3; i < finishedHorses.Count; i++)
        {
            resultText += i + "th Place " + finishedHorses[i].Name + "\n";
        }
        resultPanelText.text = resultText; // Update the result panel text
    }


    private void UpdateHorseListUI()
    {
        HorseList.text = "List:\n";

        for (int i = 0; i < Horses.Count; i++)
        {
            if (i == 0)
            {
                HorseList.text += $"<color=#FFD700>{Horses[i].Name} {(int)Horses[i].DistanceCovered}</color>\n";
            }
            else if (i == 1)
            {
                HorseList.text += $"<color=#C0C0C0>{Horses[i].Name} {(int)Horses[i].DistanceCovered}</color>\n";
            }
            else if (i == 2)
            {
                HorseList.text += $"<color=#CD7F32>{Horses[i].Name} {(int)Horses[i].DistanceCovered}</color>\n";
            }
            else
            {
                HorseList.text += $"{Horses[i].Name} {(int)Horses[i].DistanceCovered}\n";
            }
        }
        currentLeadDistance = Horses[0].DistanceCovered;
    }

    private void SortHorsesByDistance()
    {
        Horses.Sort((a, b) => (b.transform.position.z).CompareTo(a.transform.position.z));
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerNew : MonoBehaviour
{
    [SerializeField]
    public List<Horse> Horses = new List<Horse>();

    [SerializeField]
    private GameObject horsePrefab;
    [SerializeField]
    private Transform[] spawnPoints;
    public float TrackLength  = 2000f;

    public void AddHorse()
    {
        Horse horse = new Horse();
        Horses.Add(horse);
    }

    public void StartRace()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GameObject newHorse = Instantiate(horsePrefab, spawnPoints[i].position, Quaternion.identity);
            Horses.Add(newHorse.GetComponent<Horse>());
        }

        for (int i =0; i < Horses.Count; i++)
        {
            Horses[i].RandomizeBodyMaterial();
            Horses[i].ID = i + 1;
            Horses[i].Name = "Horse " + (i + 1);
            Horses[i].Speed = Random.Range(10f, 20f);
            Horses[i].StartMove();
        }
    }

    public void Start()
    {
       Invoke("StartRace", 3f);
    }

}

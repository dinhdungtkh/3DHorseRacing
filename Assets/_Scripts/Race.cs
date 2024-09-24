using UnityEngine;

public class Race : MonoBehaviour
{
    public GameObject Cube4;

    private void OnTriggerEnter(Collider other)
    {
        Cube4.SetActive(true);
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Race : MonoBehaviour
{
    public GameObject Cube4;

    public GameObject uiBoard; 
    private GameControllerNew gameController; 
    //public TMP_Text horseStatusText;

   
    private void Start() 
    {
        gameController = FindObjectOfType<GameControllerNew>(); 
        UpdateHorseStatus();
    }

    private void OnTriggerEnter(Collider other)
    {
        Cube4.SetActive(true);
        UpdateHorseStatus(); 
    }

    private void UpdateHorseStatus()
    {
        // Giả sử GameControllerNew có phương thức GetHorseStatus() trả về trạng thái ngựa
        //string horseStatus = gameController.GetHorseStatus(); 
       // horseStatusText.text = horseStatus; // Cập nhật text hiển thị trạng thái ngựa
    }  

   
}

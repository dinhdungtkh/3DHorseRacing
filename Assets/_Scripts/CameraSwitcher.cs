using System.Collections;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;
    public Camera camera3;
    public AudioListener audioListener1;
    public AudioListener audioListener2;
    public AudioListener audioListener3;
    [SerializeField]
    private GameControllerNew controllerNew;
    [SerializeField]
    private HorseCameraController horseCameraController;
    private bool camera2isActive = false;
    private void SwitchToCamera2()
    {
        StartCoroutine(switchCamera2());
    } 

     IEnumerator switchCamera2()
    {
        camera1.gameObject.SetActive(false);
        camera2.gameObject.SetActive(true);
        audioListener1.enabled = false;
        audioListener2.enabled = true;
        Time.timeScale = 1f / 6;
        yield return new WaitForSeconds(6);
        Time.timeScale = 1f;
        camera2.gameObject.SetActive(false);
        camera1.gameObject.SetActive(true);
        audioListener2.enabled = false;
        
        audioListener1.enabled = true;

    }


    public void ResetCamera()
    {
        Time.timeScale = 1f;
        camera2.gameObject.SetActive(false);
        camera3.gameObject.SetActive(false);
        camera1.gameObject.SetActive(true);
        audioListener1.enabled = true;
        audioListener2.enabled = false;
        audioListener3.enabled = false;

    }

    public void MoveToFinalCam()
    {
        camera2.gameObject.SetActive(false);
        camera1.gameObject.SetActive(false);
        camera3.gameObject.SetActive(true);
        audioListener1.enabled = false;
        audioListener2.enabled = false;
        audioListener3.enabled = true;
    }

    void Start()
    {
        ResetCamera();
        camera2isActive = false;

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetCamera();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            SwitchToCamera2();
        }

        if (controllerNew.currentLeadDistance > controllerNew.TrackLength - 10 && !camera2isActive)
        {
            camera2isActive = true;
            SwitchToCamera2();
        }
        else if (controllerNew.currentLeadDistance <= controllerNew.TrackLength - 10 && camera2isActive && horseCameraController.isFinished)
        {
            MoveToFinalCam();
            camera2isActive = false; 
        }

        if (controllerNew.finishedHorses.Count == 4 && camera2isActive)
        {
            ResetCamera();
            horseCameraController.isFinished = true;
        }

    }
}
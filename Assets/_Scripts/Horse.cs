using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Horse : MonoBehaviour
{
    [SerializeField]
    private Rigidbody m_horseRb;
    [SerializeField]
    private Animator m_animator;
    [SerializeField]
    private Transform m_character;
    public int ID;
    [SerializeField]
    private Material[] bodyMaterials;
    [SerializeField]
    private SkinnedMeshRenderer skinnedMeshRenderer; 



    public string Name;
    public float Speed;
    public float Position;
    private float InitialSpeed = 0;
    public float DistanceCovered = 0;
    [SerializeField]
    private  float lastRandomizedDistance = 0;
    private GameControllerNew controllerNew;

    public TMP_Text nameText;


    public void Start()
    {
        m_horseRb = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
        controllerNew = FindObjectOfType<GameControllerNew>();
        if (ID != 1)
        {
            RandomizeBodyMaterial();
            nameText.text = Name.ToString();
            nameText.color = Color.white;
        }
        else
        {
            Name = "Player";
            nameText.text = "Player";
            nameText.color = Color.yellow;
        }
        lastRandomizedDistance = 0;
    }
    
    

    private void FixedUpdate()
    {
        DistanceCovered = m_character.position.z;
        if (DistanceCovered >= controllerNew.TrackLength)
        {
            m_animator.SetBool("isRunning", false);
            m_horseRb.velocity = Vector3.Lerp(m_horseRb.velocity, Vector3.zero, Time.deltaTime);
            return;
        }
        float leadDistance = controllerNew.currentLeadDistance;
        bool gap = true;
        if (leadDistance - DistanceCovered >= 30 && !gap )
        {
            Speed += 2f;
        }
        if (DistanceCovered >= lastRandomizedDistance + 100)
        {
            lastRandomizedDistance = DistanceCovered;
           Speed = Random.Range(25f, 27f);
          
        } 
          if ( controllerNew.TrackLength - 200   < DistanceCovered && DistanceCovered < controllerNew.TrackLength - 150) 
            {
            Speed = Random.Range(30f, 32f);
            }
          
        m_horseRb.velocity = new Vector3(0, 0, InitialSpeed + Speed);

    }

    public void StartMove()
    {
        PlayRunningAnimation();
    }
    private void PlayRunningAnimation()
    {
        m_animator.SetBool("isRunning", true);
        m_animator.Play("Rig_Gallop_Harsh_RootMotion");
    }

     public void RandomizeBodyMaterial()
    {
            if (skinnedMeshRenderer != null && bodyMaterials.Length > 0)
            {
            int randomIndex = Random.Range(0, bodyMaterials.Length);
            skinnedMeshRenderer.material = bodyMaterials[randomIndex];
           // Debug.Log(bodyMaterials[randomIndex]);
           // Debug.Log(skinnedMeshRenderer.materials[0]);
            } 
    }
}

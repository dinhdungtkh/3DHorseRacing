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
    private static float lastRandomizedDistance = 0;
    private GameControllerNew controllerNew;

    public void Start()
    {
        m_horseRb = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
        m_animator.SetBool("isRunning", false);
        controllerNew = FindObjectOfType<GameControllerNew>();
       
    }
    private void PlayRunningAnimation()
    {
        m_animator.SetBool("isRunning", true);
        m_animator.Play("Rig_Gallop_Harsh_RootMotion");
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
        if (DistanceCovered >= lastRandomizedDistance + 100)
        {
            lastRandomizedDistance = DistanceCovered;
            //Debug.Log("Ready to randomize");
            Speed = Random.Range(10f, 20f);
            //  Debug.Log($"CurrentSpeed {Speed}");
        }
        m_horseRb.velocity = new Vector3(0, 0, InitialSpeed + Speed);

    }

    public void StartMove()
    {
        PlayRunningAnimation();
    }
    

     public void RandomizeBodyMaterial()
    {
            if (skinnedMeshRenderer != null && bodyMaterials.Length > 0)
            {
            int randomIndex = Random.Range(0, bodyMaterials.Length);
            skinnedMeshRenderer.materials[0] = bodyMaterials[randomIndex];
            Debug.Log(skinnedMeshRenderer.materials[0]);
            } 
    }
}

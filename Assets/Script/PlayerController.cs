using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance; //Método Singleton

    [SerializeField] private TextMeshProUGUI gameOverText; //Variável Game Over
    [SerializeField] private ParticleSystem explosionParticle; //Particula da Explosão

    [SerializeField] private ParticleSystem particulaD; //Particula de poeria perna direita
    [SerializeField] private ParticleSystem particulaE; //Particula de poeria perna esquerda

    private AudioSource playerAudio; //Áudio

    private Rigidbody playerRb;

    public float gravityModifier = 1f;
    public float jumpForce = 10f;
    public bool isOnGround = true;

    //animação
    private Animator playerAnim;

    //Game Over
    public bool gameOver = false;


    private void Awake()
    {
        // Singleton
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }


    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }
    public void OnJump(InputValue value)
    {
        if (value.isPressed && isOnGround)
        {
            playerAudio.PlayOneShot(playerAudio.clip, 1.0f);
            
            //Parar animação das partículas de poeira nos pés
            particulaD.Stop();
            particulaE.Stop();

            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            isOnGround = false;

            //animação
            playerAnim.SetTrigger("Jump_trig");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !gameOver)
        {
            //Ativar animação das partículas de poeira nos pés
            particulaD.Play();
            particulaE.Play();
            isOnGround = true;
        }
        //Morte do jogador
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            //Parar animação das partículas de poeira nos pés
            particulaD.Stop();
            particulaE.Stop();

            gameOver = true;
            gameOverText.gameObject.SetActive(true);
            explosionParticle.Play();
            Destroy(collision.gameObject);
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
        }
    }

    private void FixedUpdate()
    {
        playerRb.AddForce(Vector3.down * 
            (gravityModifier -1) * Physics.gravity.magnitude, ForceMode.Acceleration);

    }
}

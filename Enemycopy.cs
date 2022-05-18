using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public Transform player;
    private float speed = 2f;
    private Animator anim;
    private bool gameOver = false;
    public GameObject playerObj;
    public Animator playerAnim;
    public PlayerMovement playerMove;
    public Transform sword;
    public Transform hand;
    public bool isDefending = false;
    public bool restart = false;
    int spawnTime = 10;
    public TextMeshProUGUI scoreText;
    int score;

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", spawnTime, spawnTime);
    }

    private void Update()
    {
        Vector3 relativePos = player.position - transform.position;
        transform.rotation = Quaternion.LookRotation(relativePos);
        anim = GetComponent<Animator>();
        scoreText.text = score.ToString();
        score = (int)Time.timeSinceLevelLoad;

        if (gameOver == false)
        {
            transform.Translate(speed * Time.deltaTime * relativePos.normalized, Space.World);
        }

        else if (gameOver == true)
        {
            playerMove.enabled = false;
            playerAnim.SetBool("isDead", true);
            playerAnim.SetBool("isRunning", false);
            anim.SetBool("hasWon", true);
            Invoke("EnableCanv", 3f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Bobby")
        {
            gameOver = true;
            collision.gameObject.GetComponent<Quaternion>().SetLookRotation(player.position);
        }
    }

    void EnableCanv()
    {
        SceneManager.LoadScene("RestartScreen");
    }

    void SpawnEnemy()
    {
        Instantiate(gameObject, new Vector3(0f, 0f, 7f), Quaternion.identity);
    }
}

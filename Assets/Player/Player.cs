using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    // Singleton pattern
    static public Player player;

    // Movement
    [HideInInspector] public float h, v;
    [HideInInspector] public Vector3 destinationPoint;

    // Animation
    Animator anim;

    // Imagen
    [HideInInspector] public Color spriteColor;

    // Overlay
    Vector3 checker;
    [SerializeField] LayerMask isCollision;
    [HideInInspector] public Collider2D coll;
    Vector3 lastInput;

    private void Awake()
    {
        if (player == null)
        {
            player = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

    } // Singleton pattern

    // Start is called before the first frame update
    void Start()
    {
        // Movement
        destinationPoint = transform.position;

        // Animation
        anim = GetComponent<Animator>();

        // Imagen
        spriteColor = GetComponent<SpriteRenderer>().material.color;
    }

    // ------------------------------------------------------------------ \\

    void Movement()
    {
        // With keys
        transform.position = Vector3.MoveTowards(transform.position, destinationPoint, 5 * Time.deltaTime);
        if (transform.position == destinationPoint)
        {
            if (h == 0)
            {
                v = Input.GetAxisRaw("Vertical");
            }
            if (v == 0)
            {
                h = Input.GetAxisRaw("Horizontal");
            }

            if (h != 0 || v != 0)
            {
                lastInput = new Vector3(h, v, 0);
            }

            checker = transform.position + lastInput;
            coll = Physics2D.OverlapCircle(checker, 0.3f, isCollision);
            if (coll != null)
            {
                destinationPoint = transform.position;
            }
            else
            {
                destinationPoint = transform.position + new Vector3(h, v, 0);
            }
        }

    } // End Movement

    void Animation()
    {
        if (h != 0 || v != 0)
        {
            anim.SetBool("walking", true);
            anim.SetFloat("h", h);
            anim.SetFloat("v", v);
        }
        else
        {
            anim.SetBool("walking", false);
        }

    } // End Animation

    // ------------------------------------------------------------------ \\

    // Update is called once per frame
    void Update()
    {
        Movement();
        Animation();        
    }

    // ------------------------------------------------------------------ \\

    public void Transparency()
    {
        spriteColor.a = 1f;
        GetComponent<SpriteRenderer>().material.color = spriteColor;
    }

    // ------------------------------------------------------------------ \\

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("start"))
        {
            GameManager.gameManager.activateHistory(0);
        }
        else if (collision.gameObject.CompareTag("hike"))
        {
            GameManager.gameManager.activateHistory(5);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("stop"))
        {
            GameManager.gameManager.activateHistory(6);
        }
        else if (collision.gameObject.CompareTag("forest"))
        {
            GameManager.gameManager.activateHistory(7); 
        }
        else if (collision.gameObject.CompareTag("roads"))
        {
            GameManager.gameManager.activateHistory(8);
        }
        else if (collision.gameObject.CompareTag("flashbackUno"))
        {
            GameManager.gameManager.activateHistory(10);
        }
        else if (collision.gameObject.CompareTag("transparency"))
        {
            Transparency();
        }
        else if (collision.gameObject.CompareTag("back"))
        {
            GameManager.gameManager.activateHistory(12);
        }
        else if (collision.gameObject.CompareTag("twon"))
        {
            GameManager.gameManager.activateHistory(15);
        }
        else if (collision.gameObject.CompareTag("two"))
        {
            GameManager.gameManager.activateHistory(19);
        }
        else if (collision.gameObject.CompareTag("woman"))
        {
            GameManager.gameManager.activateHistory(20);
        }
        else if (collision.gameObject.CompareTag("bye"))
        {
            GameManager.gameManager.activateHistory(21);
        }
        else if (collision.gameObject.CompareTag("end"))
        {
            GameManager.gameManager.activateHistory(22);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("start"))
        {
            Destroy(collision.gameObject);
            Destroy(Auxiliar.aux.gameObject);           
        }
        else if (collision.gameObject.CompareTag("hike"))
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("forest"))
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("roads"))
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("flashbackUno"))
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("back"))
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("twon"))
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("two"))
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("woman"))
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("bye"))
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("end"))
        {
            Destroy(collision.gameObject);
        }
    }

    // ------------------------------------------------------------------ \\
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(checker, 0.3f);
    }
}

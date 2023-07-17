using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    // Movimiento
    Vector3 destination;
    Vector3 initPos;
    bool move;
    // Velocidad 
    [SerializeField] float velocidad;

    // Animaciones
    Animator anim;

    private void Start()
    {
        // Movimiento
        destination = transform.position;
        initPos = transform.position;

        // Animaciones
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();        
    }

    void EnemyMove()
    {        
        transform.position = Vector3.MoveTowards(transform.position, destination, velocidad * Time.deltaTime);
        if (transform.position == destination)
        {
            anim.SetBool("down", false);
            anim.SetBool("up", false);
            anim.SetBool("right", false);
            anim.SetBool("left", false);
            if (!move)
            {
                StartCoroutine(Stop());
            }
        }
    }

    IEnumerator Stop()
    {
        move = true;
        yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        move = false;
        DestinationCalculer();
    }

    void DestinationCalculer()
    {
        destination = initPos + Random.insideUnitSphere * 5;
        Vector3 aux = destination;
        aux.z = 0;
        destination = aux;

        CambioAnim();
    }

    void CambioAnim()
    {
        bool right = false, up = false, left = false, down = false;
        Vector3 fromPosToDestVert = new Vector3(transform.position.x, destination.y, 0) - transform.position;
        Vector3 fromPosToDestHori = new Vector3(destination.x, transform.position.y, 0) - transform.position;

        if(destination.x > transform.position.x) //Dest dcha.
        {
            right = true;
        }
        else if(destination.x < transform.position.x) //Dest inz.
        {
            left = true;
        }

        if (destination.y > transform.position.y) //Dest arr.
        {
            up = true;
        }
        else if (destination.y < transform.position.y) //Dest abj,.
        {
            down = true;
        }

        if(right && up && fromPosToDestVert.sqrMagnitude > fromPosToDestHori.sqrMagnitude)
        {
            anim.SetBool("up", true); // Arriba
            right = false;
            up = false;
        }
        else if(right && up && fromPosToDestVert.sqrMagnitude < fromPosToDestHori.sqrMagnitude)
        {
            anim.SetBool("right", true); // Derecha
            right = false;
            up = false;
        }
        else if (left && up && fromPosToDestVert.sqrMagnitude > fromPosToDestHori.sqrMagnitude)
        {
            anim.SetBool("up", true); // Arriba
            left = false;
            up = false;
        }
        else if (left && up && fromPosToDestVert.sqrMagnitude < fromPosToDestHori.sqrMagnitude)
        {
            anim.SetBool("left", true); // Izquierda
            left = false;
            up = false;
        }
        else if (right && down && fromPosToDestVert.sqrMagnitude > fromPosToDestHori.sqrMagnitude)
        {
            anim.SetBool("down", true); // Abajo
            right = false;
            down = false;
        }
        else if (right && down && fromPosToDestVert.sqrMagnitude < fromPosToDestHori.sqrMagnitude)
        {
            anim.SetBool("down", true); // Derecha
            right = false;
            down = false;
        }
        else if (left && down && fromPosToDestVert.sqrMagnitude > fromPosToDestHori.sqrMagnitude)
        {
            anim.SetBool("down", true); // Abajo
            left = false;
            down = false;
        }
        else if (left && down && fromPosToDestVert.sqrMagnitude < fromPosToDestHori.sqrMagnitude)
        {
            anim.SetBool("left", true); // Izquierda
            left = false;
            down = false;
        }
    }
}

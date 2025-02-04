using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MovePlatformUNactivated : MonoBehaviour
{
    public Transform posA, posB;
    public float speed;
    private Vector3 targetPos;
    private List<GameObject> players;

    private void Awake()
    {
        players = GetActivePlayers();
    }

    public void MoveToA()
    {
        targetPos = posA.position;
        StartCoroutine(MovePlatform(targetPos));
    }

    public void MoveToB()
    {
        targetPos = posB.position;
        StartCoroutine(MovePlatform(targetPos));
    }

    private System.Collections.IEnumerator MovePlatform(Vector3 destination)
    {
        while (Vector2.Distance(transform.position, destination) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = destination; // Snap to the final position.
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var p in players)
        {
            if (collision.CompareTag(p.tag))
            {
                collision.transform.parent = this.transform;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (var p in players)
        {
            if (collision.CompareTag(p.tag))
            {
                collision.transform.parent = null;
            }
        }
    }

    private List<GameObject> GetActivePlayers()
    {
        List<GameObject> activePlayers = new List<GameObject>();
        for (int i = 1; i <= 4; i++)
        {
            GameObject player = GameObject.FindWithTag("Player " + i);
            if (player != null)
            {
                activePlayers.Add(player);
            }
        }
        return activePlayers;
    }

}

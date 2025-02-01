using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MovingPlatform : MonoBehaviour
{
    public Transform posA, posB;
    public float speed;
    Vector3 targetPos;
    private List<GameObject> players;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetPos = posB.position;
        players = GetActivePlayers();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, posA.position) < 0.05f)
        {
            targetPos = posB.position;
        }

        if (Vector2.Distance(transform.position, posB.position) < 0.05f)
        {
            targetPos = posA.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

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

        // Check for each player tag and add active players to the list
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

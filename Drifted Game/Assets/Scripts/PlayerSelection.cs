using System;
using System.Collections;
using UnityEngine;

public class PlayerSelection : MonoBehaviour
{
    ArrayList players = new ArrayList();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void addPlayer(String name) 
    {
            players.Add(name);
    }
}

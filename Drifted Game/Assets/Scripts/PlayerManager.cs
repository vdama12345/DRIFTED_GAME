using UnityEngine;
using System.Collections;
using System;

public class PlayerManager : MonoBehaviour
{
   static ArrayList players = new ArrayList();
    public static bool addPlayer(String name) 
    {
        for(int i = 0; i < players.Count; i++) {
            if(players[i].Equals(name)) {
                players.Remove(name);
                return false;
            }
        }

        players.Add(name);
        return true;
    }

    public static ArrayList getList() {
        return players;
    }
}
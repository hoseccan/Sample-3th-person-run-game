using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public static GameLogic I;
    public Transform player;
    public GameObject deathScreen;
    public GameObject gameScreen;

    void Start()
    {
        I = this;
    }

    public void Catched()
    {
        gameScreen.SetActive(false);
        deathScreen.SetActive(true);
        player.GetComponent<PlayerControll>().enabled = false;
        player.GetComponent<Animator>().SetBool("IsRun", false);
        player.GetComponent<Animator>().SetBool("IsWalk", false);
        player.GetComponent<Animator>().SetTrigger("IsDeath");
    }
}

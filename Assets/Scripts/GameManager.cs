using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject textObj;
    public void GameOver()
    {
        player.gameObject.SetActive(false);
        textObj.gameObject.SetActive(true);
    }
}

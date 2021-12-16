using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private JokerJailbreak jokerJailbreak;
    public GameObject highScorePanel;

    // Start is called before the first frame update
    void Start()
    {
        jokerJailbreak = FindObjectOfType<JokerJailbreak>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    public bool HasWon()
    {
        for (int i = 1; i < 8; i += 2)
        {
            if (jokerJailbreak.walls[i].Count <= 0)
            {
                if (jokerJailbreak.onJoker.Count <= 0)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void Win()
    {
        highScorePanel.SetActive(true); // show win game panel
        print("You Have Won");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtons : MonoBehaviour
{

    private JokerJailbreak jokerJailbreak;
    public GameObject highScorePanel; // final win game panel
    
    // Start is called before the first frame update
    void Start()
    {
        jokerJailbreak = FindObjectOfType<JokerJailbreak>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAgain()
    {
        highScorePanel.SetActive(false); // hide win game panel
        ResetScene();
    }

    public void ResetScene()
    {
        // Clear selected cards();
        jokerJailbreak.selectedCards.Clear();

        // find and remove all cards
        UpdateSprite[] cards = FindObjectsOfType<UpdateSprite>();
        foreach (UpdateSprite card in cards)
        {
            Destroy(card.gameObject);
        }
        // deal new cards
        FindObjectOfType<JokerJailbreak>().PlayCards();
    }
}

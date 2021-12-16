using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UserInput : MonoBehaviour
{

    private JokerJailbreak jokerJailbreak;
    private ScoreKeeper scoreKeeper;
    private int deckClickCounter = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        jokerJailbreak = FindObjectOfType<JokerJailbreak>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    // Update is called once per frame
    void Update()
    {
        GetMouseClick();
    }

    void GetMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10));
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if(hit)
            {
                //What has been hit? Deck, Card ...
                if (hit.collider.CompareTag("Deck"))
                {
                    //clicked deck
                    Deck();
                }
                else if (hit.collider.CompareTag("Card"))
                {
                    //clicked card
                    Card(hit.collider.gameObject);
                }
            }
        }
    }
    void Deck()
    {
        //deck click actions
        print("Clicked on Deck");
        if (jokerJailbreak.onJoker.Count > 0 && jokerJailbreak.onJoker.Last().GetComponent<Selectable>().selected == true)
        {
            SubtractFromSum(jokerJailbreak.onJoker.Last());
            jokerJailbreak.selectedCards.Remove(jokerJailbreak.onJoker.Last());
            jokerJailbreak.onJoker.Last().GetComponent<Selectable>().selected = false;
        }

        deckClickCounter++;
        jokerJailbreak.DealFromDeck(deckClickCounter);
    }
    void Card(GameObject selected)
    {
        //card click actions
        print("Clicked on Card");

        if (selected.GetComponent<Selectable>().faceUp == false && selected.GetComponent<Selectable>().name == jokerJailbreak.walls[selected.GetComponent<Selectable>().wall].Last()) // if card clicked on is faced down
        {
            selected.GetComponent<Selectable>().faceUp = true;
            
        }

        else if (selected.GetComponent<Selectable>().faceUp == true) 
        {
            //Select card
            if (selected.GetComponent<Selectable>().selected == false)
            {
                selected.GetComponent<Selectable>().selected = true;
                jokerJailbreak.selectedCards.Add(selected); // add to the list

                AddToSum(selected);
            }
            //Deselect card
            else
            {
                selected.GetComponent<Selectable>().selected = false;
                jokerJailbreak.selectedCards.Remove(selected); // remove from the list

                SubtractFromSum(selected);
            }
            print("Red Cards sum: " + jokerJailbreak.redCardSum);
            print("Black Cards sum: " + jokerJailbreak.blackCardSum);
            if (jokerJailbreak.blackCardSum != 0 && jokerJailbreak.redCardSum != 0 && jokerJailbreak.blackCardSum == jokerJailbreak.redCardSum) // checks if red and black sums are equal and not 0
            {
                print("Broken");
                Break();
            }
        }
    }
    void AddToSum(GameObject selected)
    {
        if (selected.GetComponent<Selectable>().suit == 'C' || selected.GetComponent<Selectable>().suit == 'S')
        {
            jokerJailbreak.blackCardSum += selected.GetComponent<Selectable>().value; // update black cards sum
        }
        else if (selected.GetComponent<Selectable>().suit == 'H' || selected.GetComponent<Selectable>().suit == 'D')
        {
            jokerJailbreak.redCardSum += selected.GetComponent<Selectable>().value; // update red cards sum
        }
    }
    void SubtractFromSum(GameObject selected)
    {
        if (selected.GetComponent<Selectable>().suit == 'C' || selected.GetComponent<Selectable>().suit == 'S')
        {
            jokerJailbreak.blackCardSum -= selected.GetComponent<Selectable>().value; // update black cards sum
        }
        else if (selected.GetComponent<Selectable>().suit == 'H' || selected.GetComponent<Selectable>().suit == 'D')
        {
            jokerJailbreak.redCardSum -= selected.GetComponent<Selectable>().value; // update red cards sum
        }
    }
    void Break()
    {
        foreach (GameObject card in jokerJailbreak.selectedCards)
        {
            if (card.GetComponent<Selectable>().isOnJoker)
            {
                jokerJailbreak.onJoker.Remove(card);
            }
            else
            {
                jokerJailbreak.walls[card.GetComponent<Selectable>().wall].Remove(card.name);
            }
            Destroy(card);
        }

        jokerJailbreak.selectedCards.Clear();
        jokerJailbreak.redCardSum = 0;
        jokerJailbreak.blackCardSum = 0;

        if (scoreKeeper.HasWon())
        {
            scoreKeeper.Win();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class JokerJailbreak : MonoBehaviour
{
    public Sprite[] cardFaces;
    public GameObject cardPrefab;
    public GameObject jokerPos;
    public GameObject[] wallPos;

    public static string[] suits = new string[] { "C", "D", "H", "S" };
    public static string[] values = new string[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
    public List<string>[] walls;
    public List<GameObject> onJoker = new List<GameObject>();

    private List<string> wall0 = new List<string>();
    private List<string> wall1 = new List<string>();
    private List<string> wall2 = new List<string>();
    private List<string> wall3 = new List<string>();
    private List<string> wall4 = new List<string>();
    private List<string> wall5 = new List<string>();
    private List<string> wall6 = new List<string>();
    private List<string> wall7 = new List<string>();

    public List<string> deck;

    public List<GameObject> selectedCards;
    internal int blackCardSum = 0;
    internal int redCardSum = 0;

    // Start is called before the first frame update
    void Start()
    {
        walls = new List<string>[] { wall0, wall1, wall2, wall3, wall4, wall5, wall6, wall7 };
        //cornerWalls = new List<string>[] { };
        PlayCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayCards()
    {
        foreach (List<string> wall in walls)
        {
            wall.Clear();
        }
        onJoker.Clear();

        blackCardSum = 0;
        redCardSum = 0;

        deck = GenerateDeck();
        Shuffle(deck);

        // test the cards in the deck
        foreach(string card in deck)
        {
            print(card);
        }
        JokerJailbreakSort();
        StartCoroutine(JokerJailbreakDeal());
    }

    public static List<string> GenerateDeck()
    {
        List<string> newDeck = new List<string>();

        foreach(string s in suits)
        {
            foreach(string v in values)
            {
                newDeck.Add(v + s);
            }
        }

        return newDeck;
    }

    void Shuffle<T>(List<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;
        while(n > 1)
        {
            int k = random.Next(n);
            n--;
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }

    IEnumerator JokerJailbreakDeal()
    {
        for(int i = 0; i < 8; i++)
        {
            float yOffset = 0;
            float xOffset = 0;
            float zOffset = 0.03f;
            foreach(string card in walls[i])
            {

                yield return new WaitForSeconds(0.04f);
                GameObject newCard = Instantiate(cardPrefab, new Vector3(wallPos[i].transform.position.x - xOffset, wallPos[i].transform.position.y - yOffset, wallPos[i].transform.position.z - zOffset), Quaternion.identity, wallPos[i].transform);
                newCard.name = card;
                newCard.GetComponent<Selectable>().wall = i; // assign which wall this card is on
                if (card == walls[i][walls[i].Count - 1])
                {
                    newCard.GetComponent<Selectable>().faceUp = true;
                }

                switch (i)
                {
                    case 0:
                        //print("Top Left");
                        xOffset = xOffset + 0.125f;
                        yOffset = yOffset - 0.125f;
                        break;
                    case 1:
                        //print("Top Center");
                        yOffset = yOffset - 0.125f;
                        break;
                    case 2:
                        //print("Top Right");
                        xOffset = xOffset - 0.125f;
                        yOffset = yOffset - 0.125f;
                        break;
                    case 3:
                        //print("Center Right");
                        xOffset = xOffset - 0.125f;
                        break;
                    case 4:
                        //print("Bottom Right");
                        xOffset = xOffset - 0.125f;
                        yOffset = yOffset + 0.125f;
                        break;
                    case 5:
                        //print("Bottom Center");
                        yOffset = yOffset + 0.125f;
                        break;
                    case 6:
                        //print("Bottom Left");
                        xOffset = xOffset + 0.125f;
                        yOffset = yOffset + 0.125f;
                        break;
                    case 7:
                        //print("Center Left");
                        xOffset = xOffset + 0.125f;
                        break;
                    default:
                        print("No Offset");
                        break;
                }
                zOffset = zOffset + 0.03f; 
            }
        }
    }

    void JokerJailbreakSort()
    {
        for(int i = 0; i < 7; i++)
        {
            for (int j = 1; j < 8; j += 2)
            {
                walls[j].Add(deck.Last<string>());
                deck.RemoveAt(deck.Count - 1);
            }
                
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 8; j += 2)
            {
                walls[j].Add(deck.Last<string>());
                deck.RemoveAt(deck.Count - 1);
            }

        }

        //for(int j = 0; j < 8; j++)
        //{
        //    walls[j].Add(deck.Last<string>());
        //    deck.RemoveAt(deck.Count - 1);
        //}
    }

    public void DealFromDeck(int counter)
    {
        float zOffset = 0.03f * counter;
        if (deck.Count > 0)
        {
            string card = deck.Last<string>();
            GameObject NewJailCard = Instantiate(cardPrefab, new Vector3(jokerPos.transform.position.x, jokerPos.transform.position.y, jokerPos.transform.position.z - zOffset), Quaternion.identity, jokerPos.transform);
            NewJailCard.name = card;
            NewJailCard.GetComponent<Selectable>().isOnJoker = true; // assign which wall this card is on
            onJoker.Add(NewJailCard);
            NewJailCard.GetComponent<Selectable>().faceUp = true;
            deck.RemoveAt(deck.Count - 1);
        }
        
    }
}

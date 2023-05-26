using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class GameController : MonoBehaviour
{
    public Text timefield;
    public Text wordToFindField;
    private float time;
    private string chosenWord;
    private string hiddenWord;
    public GameObject[] hangman;
    private int fails;
    public GameObject winText;
    public GameObject loseText;
    private bool GameEnd = false;
    private string[] words = File.ReadAllLines(@"Assets/words.txt");
    public GameObject replay;


    // Start is called before the first frame update
    void Start()
    {

        chosenWord = words[Random.Range(0, words.Length)];

        for (int i = 0; i < chosenWord.Length; i++)  //bt2sm al 7rof 
        {

            char letter = chosenWord[i];


            if (char.IsWhiteSpace(letter))
            {
                hiddenWord += " ";
            }
            else
            {
                hiddenWord += "_";
            }
        }


        wordToFindField.text = hiddenWord;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameEnd == false)
        {
            time += Time.deltaTime;
            timefield.text = time.ToString();
        }
    }
    private void OnGUI()
    {
        Event e = Event.current;
        if (e.type == EventType.KeyDown && e.keyCode.ToString().Length == 1)
        {
            string pressedletter = e.keyCode.ToString();
            Debug.Log("keydown event was triggred " + pressedletter);
            if (chosenWord.Contains(pressedletter)) //chech if letter is on the word 
            {
                int i = chosenWord.IndexOf(pressedletter); // at which position it exists 
                while (i != -1)
                {
                    hiddenWord = hiddenWord.Substring(0, i) + pressedletter + hiddenWord[(i + 1)..];
                    Debug.Log(hiddenWord);
                    chosenWord = chosenWord.Substring(0, i) + "_" + chosenWord[(i + 1)..];
                    Debug.Log(chosenWord);
                    i = chosenWord.IndexOf(pressedletter);
                }
                wordToFindField.text = hiddenWord;
            }
            // hangman parts
            else
            {
                hangman[fails].SetActive(true);
                fails++;
            }
            if (fails == hangman.Length)
            {
                loseText.SetActive(true);
                replay.SetActive(true);
                GameEnd = true;
            }
            if (!hiddenWord.Contains("_"))
            {
                winText.SetActive(true);
                replay.SetActive(true);
                GameEnd = true;

            }
        }
    }
}
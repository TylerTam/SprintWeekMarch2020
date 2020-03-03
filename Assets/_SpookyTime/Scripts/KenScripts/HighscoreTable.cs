using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreTable : MonoBehaviour
{
    [SerializeField] Transform entryContainer;
    [SerializeField] Transform entryTemplate;
    List<Transform> highscoreEntryTransformList;

    bool isScoreAdded = false;
    void OnEnable()
    {

        entryTemplate.gameObject.SetActive(false);

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        //// how to add highscor

        //sort entry list by score
        for(int i = 0; i < highscores.highscoreEntryList.Count; i++) {
        for ( int j = i +1; j< highscores.highscoreEntryList.Count; j++) {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                {
                    //swap
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }

        highscoreEntryTransformList = new List<Transform>();
            foreach(HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }    
    }

    // add score to the board when player dies
    private void Update()
    {
        if(TemporaryDataContainer.TemporaryIsPlayerAlive == false & isScoreAdded == false)
        {
            isScoreAdded = true;
            AddHighscoreEntry(TemporaryDataContainer.TemporaryScoreInt);
            TemporaryDataContainer.TemporaryScoreInt = 0;
        }
        if(TemporaryDataContainer.TemporaryIsPlayerAlive == true)
        {
            isScoreAdded = false;
        }
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        //change heights between each score here
        float templateHeight = 30f;

            Transform entryTransform = Instantiate(entryTemplate, container);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
            entryTransform.gameObject.SetActive(true);

            int rank = transformList.Count + 1;
            string rankString;
            switch (rank)
            {
                default:
                    rankString = rank + "TH"; break;

                case 1: rankString = "1ST"; break;
                case 2: rankString = "2ND"; break;
                case 3: rankString = "3RD"; break;
            }

            entryTransform.Find("ScoreBoardRankingText").GetComponent<Text>().text = rankString;

            int score = highscoreEntry.score;

            entryTransform.Find("ScoreBoardScoreText").GetComponent<Text>().text = score.ToString();

        transformList.Add(entryTransform);
        
    }

    public static void AddHighscoreEntry(int score)
    {
        // create highscore entry
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score};

        // load saved highscores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        // add new entry to highscores
        highscores.highscoreEntryList.Add(highscoreEntry);

        // save updated highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        
    }
}

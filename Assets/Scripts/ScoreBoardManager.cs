using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System;
using TMPro;

public class ScoreBoardManager : MonoBehaviour
{

    public GameObject scorePrefab;
    public GameObject listScore;

    private Score[] scores;

    // Start is called before the first frame update
    void Start()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://spaceshooting-8ca14.firebaseio.com/.json");
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        var dict = JsonConvert.DeserializeObject<Dictionary<string, Score>>(jsonResponse);
        scores = new Score[dict.Count];
        dict.Values.CopyTo(scores, 0);
        Array.Sort(scores, delegate (Score score1, Score score2) {
            return score1.score.CompareTo(score2.score);
        });
        Array.Reverse(scores);

       for (int i = 0; i < scores.Length; i++)
        {
            GameObject go = (GameObject)Instantiate(scorePrefab);

            go.transform.parent = listScore.transform;
            go.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            go.transform.Find("Rank").GetComponent<TextMeshProUGUI>().text = string.Format("#{0}", i+1);
            go.transform.Find("Score").GetComponent<TextMeshProUGUI>().text = string.Format("{0}", scores[i].score);
            go.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = scores[i].username;
            var minutes = (int)scores[i].ellapsedTime / 60;
            var seconds = (int)scores[i].ellapsedTime % 60;
            
            go.transform.Find("Timer").GetComponent<TextMeshProUGUI>().text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
  
}

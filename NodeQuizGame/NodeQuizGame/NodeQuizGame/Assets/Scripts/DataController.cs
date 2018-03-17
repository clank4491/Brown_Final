using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class DataController : MonoBehaviour {

    public RoundData[] allRoundData;

    public string gameDataFilePath = "/SteamingAssets/data.json";
    void Start () {
        DontDestroyOnLoad(gameObject);
        LoadGameData();
        SceneManager.LoadScene("MenuScreen");	
	}
	
	public RoundData GetCurrentRoundData () {
        return allRoundData [0];
	}

    private void LoadGameData()
    {
        string filePath = Application.dataPath + gameDataFilePath;

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);

            GameData loadedData = JsonUtility.FromJson<GameData>(dataAsJson);

            allRoundData = loadedData.questionsAndAnswers;
        }
        else
        {
            Debug.Log("Suicide is the option.");
        }
    }
}

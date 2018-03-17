using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoundData {
    public string name;
    public int timeLimitInSeconds;
    public int pointsAddedForCorrectAnswer;
    public QuestionData[] questions;
}

//public class GameData
//{
//    [Header("Questions and Answers")]
//    public RoundData[] questionsAndAnswers;
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QandAdata:RoundData
{

}

[System.Serializable]
public class GameData
{
    [Header("Questions and Answers")]
    public QandAdata[] questionsAndAnswers;
}
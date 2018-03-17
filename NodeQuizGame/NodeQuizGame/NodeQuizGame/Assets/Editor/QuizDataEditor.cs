using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using SocketIO;

public class QuestionDataEditor : EditorWindow {

    string gameDataFilePath = "/SteamingAssets/data.json";
    public GameData editorData;
    static GameObject server;
    static SocketIOComponent socket;
    Vector2 scrollPos;
    private bool isSocketInit;

    [MenuItem("Window/Game Data Editor")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(QuestionDataEditor)).Show();
    }

    void OnGUI()
    {
        if (!isSocketInit)
        {
            server = GameObject.Find("DataController");
            socket = server.GetComponent<SocketIOComponent>();
            socket.On("receiveServerData", ReceiveServerData);
            isSocketInit = true;
        }
        
        if (editorData != null)
        {
            EditorGUILayout.BeginVertical();
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            // Display the data from json
            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty serializedProperty = serializedObject.FindProperty("editorData");
            EditorGUILayout.PropertyField(serializedProperty, true);
            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.EndScrollView();
            if (GUILayout.Button("Save Game Data"))
            {
                SaveGameData();
            }
            if (GUILayout.Button("Send Game Data"))
            {
                SendGameData();
            }
            EditorGUILayout.EndVertical();
        }

        if (GUILayout.Button("Load Game Data"))
        {
            LoadGameData();
        }

        if (GUILayout.Button("Load Server Data"))
        {
            LoadServerData();
        }
    }

    void LoadGameData()
    {
        string filePath = Application.dataPath + gameDataFilePath;

        if (File.Exists(filePath))
        {
            string gameData = File.ReadAllText(filePath);
            editorData = JsonUtility.FromJson<GameData>(gameData);
        }
        else
        {
            editorData = new GameData();
        }
    }

    void LoadServerData()
    {
        socket.Emit("load data");
    }

    void SaveGameData()
    {
        string jsonObj = JsonUtility.ToJson(editorData);
        string filePath = Application.dataPath + gameDataFilePath;
        File.WriteAllText(filePath, jsonObj);
    }

    void SendGameData()
    {
        string jsonObj = JsonUtility.ToJson(editorData);
        socket.Emit("send data", new JSONObject(jsonObj));
    }

    void ReceiveServerData(SocketIOEvent e)
    {
        Debug.Log("Received data from server");
        editorData = JsonUtility.FromJson<GameData>(e.data.ToString());
        Repaint();
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataCollector : MonoBehaviour {

    public bool recordInEditor;

    public static string currentLevel;

    public static float totalPlayTime;
    public static float currentPlayTime;

    public static int totalDeath;
    public static int currentDeath;


    int fileIndex = 1;
    string fileName;
    string path;

    void Start() {
#if UNITY_EDITOR
        if (!recordInEditor) {
            this.enabled = false;
            return;
        }
#else
        
#endif
        path = Application.dataPath;
        if(!System.IO.Directory.Exists(path + "/SaveData")){
            System.IO.Directory.CreateDirectory(path + "/SaveData");
        }
        fileName = "save" + fileIndex + ".txt";
        while(System.IO.File.Exists(path + "/SaveData/" + fileName)) {
            fileIndex++;
            fileName = "save" + fileIndex + ".txt";
        }
        if(SceneManager.GetActiveScene().buildIndex == 0) {
            StreamWriter sw = new StreamWriter(path + "/SaveData/" + fileName, false);
            sw.Flush();
            sw.Close();
            UpdateSceneInfo();
        }
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if(scene.name.Equals("LoadingScene")) {
            String data = "----------------------------------------------------\n" +
                            "Level name: " + currentLevel + "\n" +
                            "Play time: " + FloatTimeToString(currentPlayTime) + "\n" +
                            "Death: " + currentDeath + "\n" +
                            "----------------------------------------------------";
            WriteToFile(data);
        } else {
            UpdateSceneInfo();
        }
    }

    void Update() {
        totalPlayTime = totalPlayTime + Time.deltaTime;
        currentPlayTime = currentPlayTime + Time.deltaTime;
    }

    private void OnApplicationQuit() {
        print("Quit the game");
        if (this.enabled == false) {
            return;
        }
        String data =   "----------------------------------------------------\n" +
                        "Total play time: " + FloatTimeToString(totalPlayTime) + "\n" +
                        "Total death: " + totalDeath + "\n" +
                        "----------------------------------------------------";
        WriteToFile(data);
    }

    void UpdateSceneInfo() {
        currentLevel = SceneManager.GetActiveScene().name;
        currentPlayTime = 0f;
        currentDeath = 0;
    }

    public static void RecordOneDeath() {
        totalDeath++;
        currentDeath++;
    }

    void WriteToFile(String text) {
        StreamWriter sw = new StreamWriter(path + "/SaveData/" + fileName, true);
        sw.AutoFlush = true;
        sw.Write(text + "\n");
        //sw.Flush();
        sw.Close();
        print("Write " + text + " to file:" + fileName);
    }

    string FloatTimeToString(float time) {
        int min = (int)time / 60;
        int second = (int)time % 60;
        return min.ToString() + "m" + second.ToString() + "s" + "(" + time + "s)";
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using System.ComponentModel;

public class LogWinningPirates : MonoBehaviour
{
    public static LogWinningPirates LogSingleton { get; private set; }

    private string _path;
    [SerializeField]
    private string _fileName = "LatestSimRun";
    [SerializeField]
    private bool _logOnlyLatest = true;

    private void Awake()
    {
        if (LogSingleton != null) Destroy(this);
        else LogSingleton = this;

       _path = Application.dataPath + "/PirateLogs/" + _fileName + ".txt";
    }

    public void LogPirateGeneration(int generation, AgentData data)
    {
        if (generation < 1)
        {
            if (!_logOnlyLatest) CreateNewFileName();
            CreateLogFile();
        }  
        LogPirate(generation, data);        
    } 

    private void CreateNewFileName()
    {
        _path = Application.dataPath + "/PirateLogs/SimRun_"
            + System.DateTime.Now.Month + "-"
            + System.DateTime.Now.Day + "_"
            + System.DateTime.Now.TimeOfDay.Hours + "H"
            + System.DateTime.Now.TimeOfDay.Minutes + "M"
            + System.DateTime.Now.Second + "S"
            + ".txt";
    }

    private void CreateLogFile()
    {
        File.WriteAllText(_path,
            "\n Simulation run \n " 
            + System.DateTime.Now + "\n \n"
            + "|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||| \n \n");

    }

    private void LogPirate(int generation, AgentData data)
    {
        if (File.Exists(_path))
        {
            File.AppendAllText(_path,
                  "==================================================== \n"
                + "Generation: " + generation + "\n\n"
                

                + "Total points: " + data.totalPoints + "\n"
                + " - Points from boxes: " + data.pointsFromBoxes + "\n"
                + " - Points from kills: " + data.pointsFromKills + "\n"
                + "----------------------------------------- \n\n"

                + " - Steps: " + data.steps + "\n"
                + " - Ray radius: " + data.rayRadius + "\n"
                + " - Sight: " + data.sight + "\n"
                + " - Moving speed: " + data.movingSpeed + "\n"
                + " - Random direction: " + data.randomDirectionValue + "\n"
                + " - Enemy weight: " + data.enemyWeight + "\n"
                + " - Enemy distance factor: " + data.enemyDistanceFactor + "\n"
                + " - Bullet weight: " + data.bulletWeight + "\n"
                + " - Bullet distance factor: " + data.bulletDistanceFactor + "\n"
                + " - Box weight: " + data.boxWeight + "\n"
                + " - Box distance factor: " + data.boxDistanceFactor + "\n"
                + " - Projectile Speed: " + data.projectileSpeed + "\n"
                + " - Fire Rate Per Minute: " + data.fireRatePerMinute + "\n"
                + "==================================================== \n\n\n\n"
                );

            Debug.Log("Generation number " + generation + " logged.");
        }
    }
}

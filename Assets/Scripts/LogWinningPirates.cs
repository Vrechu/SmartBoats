using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using System.ComponentModel;

public class LogWinningPirates : MonoBehaviour
{
    public static LogWinningPirates LogSingleton { get; private set; }

    [SerializeField]
    private string _path;

    private void Awake()
    {
        if (LogSingleton != null) Destroy(this);
        else LogSingleton = this;
    }



    public void LogPirateGeneration(int generation, AgentData data)
    {
        if (!File.Exists(_path))
        {
            CreateNewFileName();
            CreateLogFile();
        }  
        LogPirate(generation, data);
        
    } 


    private void CreateNewFileName()
    {
        _path = Application.dataPath + "/PirateLogs/SimRun_.txt";

        _path = Application.dataPath + "/PirateLogs/SimRun_"
            + System.DateTime.Now.Month + "-"
            + System.DateTime.Now.Day + "_"
            + System.DateTime.Now.TimeOfDay.Hours + "H"
            + System.DateTime.Now.TimeOfDay.Minutes + "M"
            + System.DateTime.Now.Second + "S"
            + ".txt";

        Debug.Log("month: " + System.DateTime.Now.Month);
        Debug.Log("day: " + System.DateTime.Now.Day);
        Debug.Log("hours: " + System.DateTime.Now.TimeOfDay.Hours);
        Debug.Log("minutes: " + System.DateTime.Now.TimeOfDay.Minutes);
    }

    private void CreateLogFile()
    {
        File.WriteAllText(_path,
            "\n Simulation run \n " 
            + System.DateTime.Now + "\n \n"
            + "|||||||||||||||||||||||||||||||||||||||||||||||||||| \n \n");

    }

    private void LogPirate(int generation, AgentData data)
    {
        if (File.Exists(_path))
        {
            File.AppendAllText(_path,
                  "---------------------------------------------------- \n"
                + "Generation: " + generation + "\n"
                + "---------------------------------------------------- \n\n"

                + "Steps: " + data.steps + "\n"
                + "Ray radius: " + data.rayRadius + "\n"
                + "Sight: " + data.sight + "\n"
                + "Moving speed: " + data.movingSpeed + "\n"
                + "Random direction: " + data.randomDirectionValue + "\n"
                + "Enemy weight: " + data.enemyWeight + "\n"
                + "Enemy distance factor: " + data.enemyDistanceFactor + "\n"
                + "Bullet weight: " + data.bulletWeight + "\n"
                + "Bullet distance factor: " + data.bulletDistanceFactor + "\n"
                + "Box weight: " + data.boxWeight + "\n"
                + "Box distance factor: " + data.boxDistanceFactor + "\n"
                + "Projectile Speed: " + data.projectileSpeed + "\n"
                + "Fire Rate Per Minute: " + data.fireRatePerMinute + "\n\n"
                + "==================================================== \n\n\n"
                );

            Debug.Log("Generation number " + generation + " logged.");
        }
    }
}

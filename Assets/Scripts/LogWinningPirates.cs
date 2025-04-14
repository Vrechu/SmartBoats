using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using System.ComponentModel;
using UnityEngine.Analytics;
using System.Data;

public class LogWinningPirates : MonoBehaviour
{
    public static LogWinningPirates LogSingleton { get; private set; }

    private string _logPath;
    private string _infoPath;
    [SerializeField]
    private string _fileName = "LatestSimRun";
    [SerializeField]
    private bool _logOnlyLatest = true;

    private void Awake()
    {
        if (LogSingleton != null) Destroy(this);
        else LogSingleton = this;

       _logPath = Application.dataPath + "/PirateLogs/" + _fileName + ".txt";
        _infoPath = Application.dataPath + "/PirateLogs/" + _fileName + "info.txt";
    }

    /// <summary>
    /// Creates a .txt file when called on the first generation and appends it with pirate generation data when called on subsewuent generations.
    /// </summary>
    /// <param name="generation">generation number</param>
    /// <param name="genData">pirate data</param>
    public void LogPirateGeneration(int generation, AgentData genData)
    {        
        LogPirate(generation, genData);
    }

    public void CreateLog(SimData simData, AgentData startGenData)
    {
        if (!_logOnlyLatest) CreateNewFileNames();
        CreateLogFiles(simData, startGenData);
    }

    private void CreateNewFileNames()
    {
        string dateIndication =
             System.DateTime.Now.Day + "-"
            + System.DateTime.Now.Month;

        _infoPath = FileName( "Info", dateIndication, 0);
        _logPath = FileName( "Log", dateIndication, 0);
    }


    private string FileName(string logType, string dateIndication, int number = 0)
    {
        string file = Application.dataPath + "/PirateLogs/" + logType + "_" + dateIndication + "_nr" +  number + ".txt";
        
        if (!File.Exists(file)) return file;
        return FileName(logType, dateIndication, number+1);
    }

    /// <summary>
    /// Creates a .txt file with at the indicated path.
    /// </summary>
    private void CreateLogFiles(SimData simData, AgentData startingGenData)
    {
        File.WriteAllText(_infoPath,

            "///////////////////////////////////////////////////////////////////////////////////// \n"

            + "  - SIMULATION RUN -\n  "
            + System.DateTime.Now + "\n"

            + "///////////////////////////////////////////////////////////////////////////////////// \n"

            + "  Simulation settings \n"
            + "   - Simulation time         ; " + simData.SimulationTime + "\n"
            + "   - Pirate amount           ; " + simData.PirateCount + "\n"
            + "   - Box amount              ; " + simData.BoxCount + "\n"
            + "   - Mutation factor         ; " + simData.MutationFactor + "\n"
            + "   - Mutation chance         ; " + simData.MutationChance + "\n"
            + "   - Pirate parent size      ; " + simData.PirateParentSize + "\n"
            + "   - Box point worth         ; " + AgentLogic.boxPoints + "\n"
            + "   - Kill point worth        ; " + AgentLogic.killPoints + "\n"

            + "  Starting Pirate stats \n"
            + "   - Steps                   ; " + startingGenData.steps + "\n"
            + "   - Ray radius              ; " + startingGenData.rayRadius + "\n"
            + "   - Sight                   ; " + startingGenData.sight + "\n"
            + "   - Moving speed            ; " + startingGenData.movingSpeed + "\n"
            + "   - Random direction        ; " + startingGenData.randomDirectionValue + "\n"
            + "   - Enemy weight            ; " + startingGenData.enemyWeight + "\n"
            + "   - Enemy distance factor   ; " + startingGenData.enemyDistanceFactor + "\n"
            + "   - Bullet weight           ; " + startingGenData.bulletWeight + "\n"
            + "   - Bullet distance factor  ; " + startingGenData.bulletDistanceFactor + "\n"
            + "   - Box weight              ; " + startingGenData.boxWeight + "\n"
            + "   - Box distance factor     ; " + startingGenData.boxDistanceFactor + "\n"
            + "   - Projectile Speed        ; " + startingGenData.projectileSpeed + "\n"
            + "   - Fire Rate Per Minute    ; " + startingGenData.fireRatePerMinute + "\n"

            + "/////////////////////////////////////////////////////////////////////////////////////");

        File.WriteAllText(_logPath,
         "Generation ; Total points ; Points from boxes ; Points from kills ; Steps "
               + $"; Ray radius ; Sight ; moving speed ; Random direction ; Enemy weight ; Enemy distance factor ; Bullet weight "
               + $"; Bullet distance factor ; Box weight ; Box distance factor ; projectile Speed ; Fire rate per minute \n");
    }

    /// <summary>
    /// Logs appends the data of the latest pirate to the file.
    /// </summary>
    /// <param name="generation">the pirate generation number</param>
    /// <param name="data">the agant data of the pirate to be logged</param>
    private void LogPirate(int generation, AgentData data)
    {
        if (File.Exists(_logPath))
        {
            File.AppendAllText(_logPath,
                   /*"  Generation winner; " + generation + "\n"
                

                + "  Total points               ; " + data.totalPoints + "\n"
                + "   - Points from boxes       ; " + data.pointsFromBoxes + "\n"
                + "   - Points from kills       ; " + data.pointsFromKills + "\n"

                + "   - Steps                   ; " + data.steps + "\n"
                + "   - Ray radius              ; " + data.rayRadius + "\n"
                + "   - Sight                   ; " + data.sight + "\n"
                + "   - Moving speed            ; " + data.movingSpeed + "\n"
                + "   - Random direction        ; " + data.randomDirectionValue + "\n"
                + "   - Enemy weight            ; " + data.enemyWeight + "\n"
                + "   - Enemy distance factor   ; " + data.enemyDistanceFactor + "\n"
                + "   - Bullet weight           ; " + data.bulletWeight + "\n"
                + "   - Bullet distance factor  ; " + data.bulletDistanceFactor + "\n"
                + "   - Box weight              ; " + data.boxWeight + "\n"
                + "   - Box distance factor     ; " + data.boxDistanceFactor + "\n"
                + "   - Projectile Sseed        ; " + data.projectileSpeed + "\n"
                + "   - Fire rate per minute    ; " + data.fireRatePerMinute + "\n\n"*/
                   
                   $"{generation} ; {data.totalPoints} ; {data.pointsFromBoxes} ; {data.pointsFromKills} ; {data.steps}"
                   + $" ; {data.rayRadius} ; {data.sight} ; {data.movingSpeed} ; {data.randomDirectionValue} ; {data.enemyWeight}" +
                   $" ; {data.enemyDistanceFactor} ; {data.bulletWeight} ; {data.bulletDistanceFactor} ; {data.boxWeight} ; {data.boxDistanceFactor}" +
                   $" ; {data.projectileSpeed} ; {data.fireRatePerMinute} \n"
                );

            Debug.Log("Generation number " + generation + " logged.");
        }
    }
}

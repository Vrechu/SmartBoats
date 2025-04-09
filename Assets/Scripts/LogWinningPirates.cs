using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using System.ComponentModel;
using UnityEngine.Analytics;

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
        if (!_logOnlyLatest) CreateNewFileName();
        CreateLogFile(simData, startGenData);
    }

    /// <summary>
    /// Generates a fileName based on the current date and time.
    /// </summary>
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

    /// <summary>
    /// Creates a .txt file with at the indicated path.
    /// </summary>
    private void CreateLogFile(SimData simData, AgentData startingGenData)
    {
        File.WriteAllText(_path,

            "///////////////////////////////////////////////////////////////////////////////////// \n\n"

            + "  - SIMULATION RUN -\n  "
            + System.DateTime.Now + "\n\n"

            + "///////////////////////////////////////////////////////////////////////////////////// \n\n"

            + "  Simulation settings: \n"
            + "   - Pirate amount:          " + simData.PirateCount + "\n"
            + "   - Box amount:             " + simData.BoxCount + "\n"
            + "   - Mutation factor:        " + simData.MutationFactor + "\n"
            + "   - mutation chance:        " + simData.MutationChance + "\n"
            + "   - Pirate parent size:     " + simData.PirateParentSize + "\n"
            + "   - Simulation time:        " + simData.SimulationTime + "\n\n"

            + "  Starting Pirate stats: \n"
            + "   - Steps:                  " + startingGenData.steps + "\n"
            + "   - Ray radius:             " + startingGenData.rayRadius + "\n"
            + "   - Sight:                  " + startingGenData.sight + "\n"
            + "   - Moving speed:           " + startingGenData.movingSpeed + "\n"
            + "   - Random direction:       " + startingGenData.randomDirectionValue + "\n"
            + "   - Enemy weight:           " + startingGenData.enemyWeight + "\n"
            + "   - Enemy distance factor:  " + startingGenData.enemyDistanceFactor + "\n"
            + "   - Bullet weight:          " + startingGenData.bulletWeight + "\n"
            + "   - Bullet distance factor: " + startingGenData.bulletDistanceFactor + "\n"
            + "   - Box weight:             " + startingGenData.boxWeight + "\n"
            + "   - Box distance factor:    " + startingGenData.boxDistanceFactor + "\n"
            + "   - Projectile Speed:       " + startingGenData.projectileSpeed + "\n"
            + "   - Fire Rate Per Minute:   " + startingGenData.fireRatePerMinute + "\n"

            + "///////////////////////////////////////////////////////////////////////////////////// \n\n\n\n");
    }

    /// <summary>
    /// Logs appends the data of the latest pirate to the file.
    /// </summary>
    /// <param name="generation">the pirate generation number</param>
    /// <param name="data">the agant data of the pirate to be logged</param>
    private void LogPirate(int generation, AgentData data)
    {
        if (File.Exists(_path))
        {
            File.AppendAllText(_path,
                  "====================================================// \n"
                + "  Generation " + generation + " winner: \n\n"
                

                + "  Total points: " + data.totalPoints + "\n"
                + "   - Points from boxes:      " + data.pointsFromBoxes + "\n"
                + "   - Points from kills:      " + data.pointsFromKills + "\n\n"

                + "  Stats: \n"
                + "   - Steps:                  " + data.steps + "\n"
                + "   - Ray radius:             " + data.rayRadius + "\n"
                + "   - Sight:                  " + data.sight + "\n"
                + "   - Moving speed:           " + data.movingSpeed + "\n"
                + "   - Random direction:       " + data.randomDirectionValue + "\n"
                + "   - Enemy weight:           " + data.enemyWeight + "\n"
                + "   - Enemy distance factor:  " + data.enemyDistanceFactor + "\n"
                + "   - Bullet weight:          " + data.bulletWeight + "\n"
                + "   - Bullet distance factor: " + data.bulletDistanceFactor + "\n"
                + "   - Box weight:             " + data.boxWeight + "\n"
                + "   - Box distance factor:    " + data.boxDistanceFactor + "\n"
                + "   - Projectile Speed:       " + data.projectileSpeed + "\n"
                + "   - Fire Rate Per Minute:   " + data.fireRatePerMinute + "\n"
                + "==================================================== \n\n\n\n"
                );

            Debug.Log("Generation number " + generation + " logged.");
        }
    }
}

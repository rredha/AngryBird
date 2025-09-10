using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using Project.Scripts.Runtime.Angrybird.Managers;
using Project.Scripts.Runtime.Angrybird.Utils;
using UnityEngine;

namespace Project.Scripts.Runtime.Core.SessionManager
{
    public class SessionManager : MonoBehaviour
    {
        public static SessionManager Instance { get; private set; }
        public Session Session { get; set; }

        public List<SessionMetrics> SessionMetrics; 

        public event EventHandler SessionEnd;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            
            SessionMetrics = new List<SessionMetrics>();
        }

        private void OnEnable()
        {
            SessionEnd += OnSessionEnd_SaveData;
        }
        private void OnDisable()
        {
            SessionEnd -= OnSessionEnd_SaveData;
        }

        #region Methods
        public void AddMetric(SessionMetrics sessionMetrics)
        {
            SessionMetrics.Add(sessionMetrics);
        }
        private static void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public void Log(List<SessionMetrics> metricsList)
        {
            foreach (var metric in metricsList)
            {
               metric.Log(); 
            } 
        }
        
        public void Export()
        {
            var sessionID = Session.LoginTime.ToString("yyyyMMddHHmm");
            var dataPath = Session.User.DataPath;
            var dirPath = dataPath + "/by-sessions/" + sessionID;
            
            CreateDirectory(dirPath);
            
            using (var writer = new StreamWriter($"{dirPath}/stats.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(SessionMetrics);
            }
            Debug.Log("Exported Successfully");
        }

        private void LoadGameConfiguration(string configurationDataCSV)
        {
            /* configuration data in csv is a config file for the session
            / attached the user
            */
            GameConfigurationSO gameConfig = new GameConfigurationSO();
            Session.GameConfiguration = gameConfig;
        }
        private void OnSessionEnd_SaveData(object sender, EventArgs e)
        {
            // get all data from survey that were answered.
            // get all time related data
            // get all score related data
            
            // save latest configuration from in game
            // if ingame config is different from imported config
            // then imported config = in game config.
        }
        #endregion
    }
}
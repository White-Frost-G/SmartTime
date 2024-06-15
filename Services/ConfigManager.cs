using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using IWshRuntimeLibrary;
using File = System.IO.File;


namespace SmartTime.Services
{
    internal class ConfigManager
    {
        private static string filePath = AppDomain.CurrentDomain.BaseDirectory + "config.json";
        public static Config Config { get; set; } = new Config();

        public static void Load()
        {
            if (!File.Exists(filePath)){
                FirstTimeOpen();
            }
            string data = File.ReadAllText(filePath);
            Config = JsonConvert.DeserializeObject<Config>(data);
        }

        private static void FirstTimeOpen()
        {
            string workingDir = AppDomain.CurrentDomain.BaseDirectory;
            object shDesktop = (object)"Desktop";
            WshShell shell = new WshShell();
            //Create AutoStart ShortCut
            string autoStartPath =Path.Combine( Environment.GetFolderPath(Environment.SpecialFolder.StartMenu), "Programs\\Startup", "SmartTime.lnk");
            
            IWshShortcut AutoShortcut = (IWshShortcut)shell.CreateShortcut(autoStartPath);
            AutoShortcut.WorkingDirectory = workingDir;
            AutoShortcut.TargetPath = AppDomain.CurrentDomain.BaseDirectory + @"\SmartTime.exe";
            AutoShortcut.Arguments = "withoutUI";
            AutoShortcut.Save();



            //Create ShortCut in desktop
            string desktopPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "SmartTime.lnk");
            
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(desktopPath);
            shortcut.WorkingDirectory = workingDir;
            shortcut.TargetPath = AppDomain.CurrentDomain.BaseDirectory + @"\SmartTime.exe";
            shortcut.Save(); 


            Save();
        }

        public static void Save() 
        {
            string data = JsonConvert.SerializeObject(Config);
            File.WriteAllText(filePath, data);
        }
    }
    class Config
    {
        public string Token { get; set; }
        //Seconds
        public float WorkTime { get; set; }

        public List<string> AppsForBlock { get; set; }
        public Config() 
        {
            Token = "Token";
            WorkTime = 3600;
            AppsForBlock = new List<string>();
        }
    }
}

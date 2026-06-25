using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Common.Saving
{
    public static class FileSaveManager
    {
        private static string savePath = "save";
        private static string extensionName = ".json";
        public static void SaveFile(ISaveable saveable)
        {
            SaveFile(saveable.GetFileName(), saveable);
        }

        public static void SaveFile(string fileName, object content)
        {
            string fileContent = JsonConvert.SerializeObject(content, Formatting.Indented);
            string directoryPath = Path.GetDirectoryName(Path.Combine(Application.persistentDataPath, savePath, fileName));
            try
            {
                Directory.CreateDirectory(directoryPath);
            }
            catch (Exception e)
            {
                CustomLogger.LogError(e.Message, LogGroupFlags.SaveAndLoad);
            }

            string fullPath = Path.Combine(Application.persistentDataPath, savePath, fileName);
            try
            {
                File.WriteAllText(fullPath + extensionName, fileContent);
            }
            catch (Exception e)
            {
                CustomLogger.LogError(e.Message);
            }
        }

        public async static Task SaveFilesAsync(ICollection<ISaveable> saveables, string rootPath, string directory = "")
        {
            List<Task> fileWriteTasks = new();

            string directoryPath = Path.GetDirectoryName(Path.Combine(rootPath, savePath, directory));
            try
            {
                Directory.CreateDirectory(directoryPath);
            }
            catch (Exception e)
            {
                CustomLogger.LogError(e.Message, LogGroupFlags.SaveAndLoad);
            }

            foreach (var saveable in saveables)
            {
                string fullPath = Path.Combine(directory, saveable.GetFileName() + extensionName);
                string fileContent = JsonConvert.SerializeObject(saveable.GetContent(), Formatting.None);

                fileWriteTasks.Add(Task.Run(() =>
                {
                    File.WriteAllText(fullPath, fileContent);
                }));
            }

            await Task.WhenAll(fileWriteTasks);
        }

        public static void LoadFile(ref ISaveable saveable)
        {
            LoadFile(saveable.GetFileName(), ref saveable);
        }
        public static void LoadFile<T>(string fileName, ref T loadableObject)
        {
            string fullPath = Path.Combine(Application.persistentDataPath, savePath, fileName);
            try
            {
                string content = File.ReadAllText(fullPath + extensionName);
                JsonConvert.PopulateObject(content, loadableObject);
            }
            catch (Exception e)
            {
                CustomLogger.LogError(e.Message, LogGroupFlags.SaveAndLoad);
                loadableObject = default;
            }
        }

        public async static Task<Dictionary<string, string>> LoadFiles(string directory = "")
        {
            Dictionary<string, string> fileContents = new();
            string directoryPath = Path.GetDirectoryName(Path.Combine(Application.persistentDataPath, savePath, directory));
            if (!Directory.Exists(directoryPath))
            {
                CustomLogger.LogError("Directory: " + directoryPath + " doesn't exist");
                return fileContents;
            }
            var fileNames = Directory.GetFiles(directoryPath);
            var readTasks = fileNames.Select(async fileName =>
            {
                try
                {
                    string data = await File.ReadAllTextAsync(fileName);
                    return new KeyValuePair<string, string>(Path.GetFileNameWithoutExtension(fileName), data);
                }
                catch (Exception ex)
                {
                    CustomLogger.LogError($"Error in file {fileName}: {ex.Message}");
                    return default;
                }
            });

            var results = await Task.WhenAll(readTasks);
            var filteredResults = results.Where(r => !r.Equals(default(KeyValuePair<string, string>)));

            foreach (var result in filteredResults)
            {
                fileContents.Add(result.Key, result.Value);
            }
            return fileContents;
        }

        public static void DeleteFile(string fileName)
        {
            string path = Path.Combine(Application.persistentDataPath, savePath, fileName);
            if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);

                }
                catch (Exception error)
                {
                    CustomLogger.LogError($"File deletion: {path} was not successful", LogGroupFlags.SaveAndLoad);
                    CustomLogger.LogError(error.Message, LogGroupFlags.SaveAndLoad);
                }
            }
        }

        public static void DeleteAllFiles(string path = "")
        {
            path = Path.Combine(Application.persistentDataPath, savePath, path);
            if (Directory.Exists(path))
            {
                try
                {
                    Directory.Delete(path, true);
                }
                catch (Exception error)
                {
                    CustomLogger.LogError($"Directory deletion: {path} was not successful", LogGroupFlags.SaveAndLoad);
                    CustomLogger.LogError(error.Message, LogGroupFlags.SaveAndLoad);
                }
            }
        }

        public static List<string> GetDirectories(string path = "", bool fullPath = false)
        {
            path = Path.Combine(Application.persistentDataPath, savePath, path);
            try
            {
                var directories = Directory.GetDirectories(path).ToList();
                if (fullPath)
                {
                    return directories;
                }
                for (int i = 0; i < directories.Count; i++)
                {
                    directories[i] = Path.GetFileName(directories[i]);
                }
                return directories;
            }
            catch (Exception ex)
            {
                CustomLogger.LogError("Couldn't get directories", LogGroupFlags.SaveAndLoad);
                CustomLogger.LogError(ex.Message);
                return new();
            }
            
        }

        public static void CreateProfile(string profileName)
        {
            string path = Path.Combine(Application.persistentDataPath, savePath, profileName);
            if (Directory.Exists(path))
            {
                return;
            }
            Directory.CreateDirectory(path);
        }

        public static DirectoryInfo GetMetaDataOfProfile(string profileName)
        {
            string path = Path.Combine(Application.persistentDataPath, savePath, profileName);
            if (!Directory.Exists(path))
            {
                return null;
            }
            return new DirectoryInfo(path);
        }
    }
}

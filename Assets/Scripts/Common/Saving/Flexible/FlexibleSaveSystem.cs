using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Saving.Flexible
{
    public static class FlexibleSaveSystem
    {
        private const string DefaultSaveFileName = "Save";
        private static string CurrentProfile = "Profile1";
        private static Dictionary<string, SaveFile> saveFiles = new();

        public static T Load<T>(string key, T defaultValue = default, string fileName = DefaultSaveFileName)
        {
            fileName = GetFileNameWithProfile(fileName);
            if (saveFiles.TryGetValue(fileName, out var saveFile))
            {
                return saveFile.Load(key, defaultValue);
            }

            SaveFile newSaveFile = new(fileName);
            saveFiles.Add(fileName, newSaveFile);

            return newSaveFile.Load(key, defaultValue);
        }

        public static void LoadGame()
        {
            foreach (var saveFile in saveFiles.Values)
            {
                saveFile.LoadContent();
            }
        }

        public static void Save<T>(string key, T value, string fileName = DefaultSaveFileName)
        {
            fileName = GetFileNameWithProfile(fileName);
            if (saveFiles.TryGetValue(fileName, out var saveFile))
            {
                saveFile.Save(key, value);
                return;
            }
            SaveFile newSaveFile = new(fileName);
            saveFiles.Add(fileName, newSaveFile);
            newSaveFile.Save(key, value);
        }

        public static void SaveGame()
        {
            foreach (var saveFile in saveFiles.Values)
            {
                saveFile.SaveContent();
            }
        }

        public static async Task SaveGameParallel(string persistentDataPath)
        {
            await FileSaveManager.SaveFilesAsync(saveFiles.Values.ToArray(), persistentDataPath, CurrentProfile);
        }

        public static void SaveFiles(ICollection<string> fileNames)
        {
            var fileNamesWithProfile = GetFileNamesWithProfile(fileNames);
            foreach (var saveFile in saveFiles.Values)
            {
                if (fileNamesWithProfile.Contains(saveFile.GetFileName()))
                {
                    saveFile.SaveContent();
                }
            }
        }

        public static void Delete(bool clearCache = true, string fileName = DefaultSaveFileName)
        {
            fileName = GetFileNameWithProfile(fileName);
            FileSaveManager.DeleteFile(fileName);
            if (!clearCache)
            {
                return;
            }
            if (saveFiles.TryGetValue(fileName, out var saveFile))
            {
                saveFiles.Remove(fileName);
            }
        }

        public static void DeleteProfile(bool clearCache = true)
        {
            FileSaveManager.DeleteAllFiles(CurrentProfile);
            if (!clearCache)
            {
                return;
            }
            saveFiles = new();
        }

        public static void DeleteAllSaveFiles(bool clearCache = true)
        {
            FileSaveManager.DeleteAllFiles();
            if (!clearCache)
            {
                return;
            }
            saveFiles = new();
        }

        public static List<string> GetAvailableProfiles()
        {
            return FileSaveManager.GetDirectories();
        }

        public static void ChangeProfile(string newProfile)
        {
            SaveGame();
            saveFiles = new();
            CurrentProfile = newProfile;
        }

        private static string GetFileNameWithProfile(string fileName)
        {
            return Path.Combine(CurrentProfile, fileName);
        }

        private static List<string> GetFileNamesWithProfile(ICollection<string> fileNames)
        {
            List<string> newFileNames = new();
            foreach (var name in fileNames)
            {
                newFileNames.Add(Path.Combine(CurrentProfile, name));
            }
            return newFileNames;
        }
    }
}

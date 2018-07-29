using PCLStorage;
using System.Threading.Tasks;

namespace PLSport.Data
{
    // File to keep in memory user log when application is off

    public static class Settings
    {
        public static async Task<string[]> Read()
        {
            return (await (await FileSystem.Current.LocalStorage.CreateFileAsync("Settings.txt", CreationCollisionOption.OpenIfExists)).ReadAllTextAsync()).Split('\n');
        }

        public static async Task Write(string EnterpriseUrl, string UserCode)
        {
            if (EnterpriseUrl == null && UserCode == null)
            {
                await (await FileSystem.Current.LocalStorage.CreateFileAsync("Settings.txt", CreationCollisionOption.OpenIfExists)).WriteAllTextAsync($"");
            }
            else
            {
                await (await FileSystem.Current.LocalStorage.CreateFileAsync("Settings.txt", CreationCollisionOption.OpenIfExists)).WriteAllTextAsync($"{EnterpriseUrl}\n{UserCode}");
            }
        }
    }
}

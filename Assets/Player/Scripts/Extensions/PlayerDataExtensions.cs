using CityPop.Player.Constants;
using CityPop.Player.Data;

namespace CityPop.Player.Extensions
{
    public static class PlayerDataExtensions
    {
        public static string GetSaveFilePath(this PlayerData playerData)
        {
            return $"{PlayerDataConstants.PersistenceFolderPath}/{playerData.Guid}.{PlayerDataConstants.PersistenceFileType}";
        }
    }
}
using System.Threading.Tasks;

namespace DAATS.UserData.Interface
{
    public interface ISetUserSettingsData
    {
        Task WriteSavedSettings(UserSettingsData settings);
    }
}
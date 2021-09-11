using System.Threading.Tasks;

namespace DAATS.UserData.Interface
{
    public interface ISetUserProgressData
    {
        Task WriteCompletedLevel(LevelData beatenLevel, LevelProgress levelProgress);
    }
}
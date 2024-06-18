using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure;

public interface ISaveLoadService : IService
{
    void SaveProgress();
    PlayerProgress LoadProgress();
}
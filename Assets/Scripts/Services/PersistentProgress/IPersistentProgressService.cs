using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure;

public interface IPersistentProgressService : IService
{
    PlayerProgress Progress { get; set; }
}
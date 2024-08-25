using System.Collections.Generic;

namespace ClockApp
{
    public interface IPlayerStopwatchGateway : IOverWriteableGateway<PlayerStopwatchModel, PlayerStopwatchPrimaryKey> { }
    
    public class PlayerStopwatchGateway : IPlayerStopwatchGateway
    {
        public PlayerStopwatchModel Get(PlayerStopwatchPrimaryKey primaryKey)
        {
            return PlayerStopwatchRepository.I.Get(primaryKey);
        }

        public PlayerStopwatchModel GetOwner()
        {
            return PlayerStopwatchRepository.I.GetOwner();
        }

        public List<PlayerStopwatchModel> GetAll()
        {
            return PlayerStopwatchRepository.I.GetAll();
        }

        public void Save(PlayerStopwatchModel model)
        {
            PlayerStopwatchRepository.I.Save(model);
        }

        public void Remove(PlayerStopwatchModel model)
        {
            PlayerStopwatchRepository.I.Remove(model);
        }
    }
}
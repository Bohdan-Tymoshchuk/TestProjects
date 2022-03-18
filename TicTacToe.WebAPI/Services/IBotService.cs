namespace TicTacToe.WebAPI.Services
{
    public interface IBotService
    {
        public Task<bool?> MakeBotMove(Guid botId, Guid gameId);
    }
}

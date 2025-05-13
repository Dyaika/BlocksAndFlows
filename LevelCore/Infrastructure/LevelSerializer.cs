using LevelCore.Models;
using LevelCore.Models.DTO;

namespace LevelCore.Infrastructure
{
    public static class LevelSerializer
    {
        public static Level Deserialize(string json)
        {
            var level = System.Text.Json.JsonSerializer.Deserialize<LevelDTO>(json);
            return LevelMapper.Map(level);
        }

        public static string Serialize(Level level)
        {
            var temp = LevelMapper.Map(level);
            return System.Text.Json.JsonSerializer.Serialize(temp);
        }
    }
}
using System.Linq;
using LevelCore.Models;
using LevelCore.Models.DTO;

namespace LevelCore.Infrastructure
{
    internal static class LevelMapper
    {
        public static Level Map(LevelDTO dto)
        {
            return new Level(dto.Width, dto.Height, dto.Blocks.Select(Map).ToArray());
        }
        
        public static LevelDTO Map(Level model)
        {
            return new LevelDTO()
            {
                Blocks = model.Blocks.Select(Map).ToArray(),
                Height = model.Height,
                Width = model.Width,
            };
        }
        
        public static Block Map(BlockDTO dto)
        {
            return new Block(dto.Filters.Select(Map).ToArray(), dto.Offset, (BlockType)dto.Type, dto.IsStatic);
        }
        
        public static BlockDTO Map(Block model)
        {
            return new BlockDTO()
            {
                Id = model.Id,
                Type = (byte)model.Type,
                IsStatic = model.IsStatic,
                Offset = model.Offset,
                Filters = model.Filters.Select(Map).ToArray()
            };
        }
        
        public static Filter Map(FilterDTO dto)
        {
            return new Filter(dto.ColorId, dto.ShapeId);
        }
        
        public static FilterDTO Map(Filter model)
        {
            return new FilterDTO()
            {
                ColorId = model.ColorId,
                ShapeId = model.ShapeId
            };
        }
    }
}
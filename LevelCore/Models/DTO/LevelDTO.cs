namespace LevelCore.Models.DTO
{
    internal class LevelDTO
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public BlockDTO[] Blocks { get; set; }
    }
}
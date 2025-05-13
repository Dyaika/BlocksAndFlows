namespace LevelCore.Models.DTO
{
    internal class BlockDTO
    {
        public int Offset { get; set; }
        public byte Type { get; set; }
        public bool IsStatic { get; set; }
        public FilterDTO[] Filters { get; set; }
        public int Id { get; set; }
    }
}
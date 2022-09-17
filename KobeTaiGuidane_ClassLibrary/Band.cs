namespace KobeTaiGuidane_ClassLibrary
{
    public class Band
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public short Rank { get; set; }
        public List<Character>? Members { get; set; }
    }
}

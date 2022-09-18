namespace KobeTaiGuidane_ClassLibrary
{
    public class Character
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public byte Health { get; set; }
        public byte Mood { get; set; }
        public byte StarQuality { get; set; }
        public Band Band { get; set; }
        public List<Skill>? Skills { get; set; }
    }
}

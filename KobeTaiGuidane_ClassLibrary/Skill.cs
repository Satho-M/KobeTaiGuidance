namespace KobeTaiGuidane_ClassLibrary
{
    public class Skill
    {
        public string Name { get; set; }
        public uint Id { get; set; }
        public byte Level { get; set; }
        public List<Skill>? Requirements { get; set; }
    }
}
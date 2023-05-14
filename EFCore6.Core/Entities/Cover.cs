namespace EFCore6.Core.Entities
{
    public class Cover
    {
        public Cover()
        {
            Artists = new List<Artist>();
        }
        public int CoverId { get; set; }
        public string DesignIdeas { get; set; }
        public bool DigitalOnly { get; set; }

        // many to many
        public List<Artist> Artists { get; set; }
    }
}

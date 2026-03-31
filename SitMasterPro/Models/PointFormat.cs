namespace SitMasterPro.Models
{
    public class PointFormat
    {
        public string Name { get; set; } = string.Empty;
        public string Format { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public override string ToString() => Name;
    }
}

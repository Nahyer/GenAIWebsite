public class GenAI
{
    public int Id { get; set; }
    public string GenAIName { get; set; }
    public string Summary { get; set; }

    public string ImageFilename { get; set; } = "default.png";
    public string AnchorLink { get; set; } = "https://www.google.com";
    public int Like { get; set; } = 0;
}

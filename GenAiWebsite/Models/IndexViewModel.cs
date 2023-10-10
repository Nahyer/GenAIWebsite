//IndexViewModel
namespace GenAiWebsite.Models
{
    public class IndexViewModel
    {
        public bool IsAuthenticated { get; set; }
        public List<GenAI> GenAIList { get; set; }
    }
}
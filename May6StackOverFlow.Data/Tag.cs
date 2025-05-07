namespace May6StackOverFlow.Data
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<QuestionTags> QuestionTags { get; set; }
    }
}
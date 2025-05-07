namespace May6StackOverFlow.Data
{
    public class QuestionsAnswerRepository
    {
        private string _connectionString;
        public QuestionsAnswerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace May6StackOverFlow.Data
{
    public class QuestionsAnswerRepository
    {
        private string _connectionString;
        public QuestionsAnswerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Question> GetAllQuestions()
        {
            var ctx = new QuestionAnswerContext(_connectionString);
            return ctx.Questions.Include(q => q.QuestionTags)
                .ThenInclude(t => t.Tag)
                .Include(q => q.Answers)
                .OrderByDescending(q => q.DatePosted)
                .ToList();
        }
        public void AddQuestion(Question question, List<string> tags)
        {
            using var ctx = new QuestionAnswerContext(_connectionString);
            ctx.Questions.Add(question);
            ctx.SaveChanges();
            foreach (string tag in tags)
            {
                Tag t = GetTag(tag);
                int tagId;
                if (t == null)
                {
                    tagId = AddTag(tag);
                }
                else
                {
                    tagId = t.Id;
                }
                ctx.QuestionTags.Add(new QuestionTags
                {
                    QuestionId = question.Id,
                    TagId = tagId
                });
            }

            ctx.SaveChanges();
        }
        private Tag GetTag(string name)
        {
            using var ctx = new QuestionAnswerContext(_connectionString);
            return ctx.Tags.FirstOrDefault(t => t.Name == name);
        }

        private int AddTag(string name)
        {
            using var ctx = new QuestionAnswerContext(_connectionString);
            var tag = new Tag { Name = name };
            ctx.Tags.Add(tag);
            ctx.SaveChanges();
            return tag.Id;
        }
        public Question GetQuestionsById(int id)
        {
            var ctx = new QuestionAnswerContext(_connectionString);
            return ctx.Questions
                .Include(q => q.QuestionTags)
                .ThenInclude(qt => qt.Tag)
                .Include(q => q.Answers)
                .ThenInclude(a => a.User)
                .Include(q => q.User)
                .FirstOrDefault(q => q.Id == id);

        }
        public void AddAnswer(Answer answer)
        {
            var ctx = new QuestionAnswerContext(_connectionString);
            ctx.Answers.Add(answer);
            ctx.SaveChanges();
        }
    }
}

namespace QuizMaker
{
    public class Quiz
    {
        public Quiz() {}

        public List<Question> Questions { get; set; } = [];

        public void Add(Question question) => Questions.Add(question);
    }
}

namespace QuizMaker
{
    public class Question
    {
        public string Text { get; set; } = "";
        public readonly List<Answer> _answers = [];

        public Question() { }
        public Question(string s) { Text = s; }

        public override bool Equals(object? obj) => obj is Question other ? Text == other.Text : false;
        public override int GetHashCode() => Text?.GetHashCode() ?? 0;
        public void Add(Answer anwser) => _answers.Add(anwser);
    }
}

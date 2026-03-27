namespace QuizMaker
{
    public class Question
    {
        public string Text { get; set; } = "";
        public readonly List<Answer> Answers = [];
        public bool IsAllCorrect
        {
            get
            {
                bool allCorrect = false;
                foreach (Answer a in Answers)
                    if (a.IsAnswerCorrect)
                        allCorrect = true;
                return allCorrect;
            }
        }

        public Question() { }
        public Question(string s) { Text = s; }

        public override bool Equals(object? obj) => obj is Question other && Text == other.Text;
        public override int GetHashCode() => Text?.GetHashCode() ?? 0;
        public void Add(Answer anwser) => Answers.Add(anwser);

        public double CalcuateScore(List<int> choices)
        {
            double score = 0;
            foreach (int i in choices)
                score += Answers[i].Score;
            return score;
        }
    }
}

namespace QuizMaker
{
    public class QuestionUI(Question question, int questionNumer, int totalNumberQuestions)
    {
        private readonly List<Answer> answers = question.Answers;

        public double Play()
        {
            DisplayQuestion();
            DisplayOptions();

            List<int> choices = ReadOptions();

            if (choices.Count != 0)
                DisplayAnswers(choices);
            else if (Logics.IsAllCorrect(answers))
                Console.WriteLine("You're right; all answers are incorrect.");
            else
                Console.Write("There are correct answers!");
            Console.WriteLine();

            return Logics.CalcuateScore(answers, choices);
        }

        private void DisplayQuestion()
        {
            Console.WriteLine($"Question {questionNumer} / {totalNumberQuestions}:");
            Console.WriteLine(question.Text);
            Console.WriteLine();
        }

        private List<Answer> DisplayOptions()
        {
            Console.WriteLine($"You have {answers.Count} options:");
            for (int i = 1; i <= answers.Count; ++i)
                Console.WriteLine($"{i}: {answers[i - 1].Text}");
            return answers;
        }

        private static List<int> ReadOptions()
        {
            Console.Write("Please pick (one or mutilple by typing multiple numers): ");
            string? s = Console.ReadLine();
            Console.WriteLine();

            if (s == null || s == "")
                return [];

            List<int> choices = [];
            foreach (char c in s)
                if (int.TryParse(c.ToString(), out _))
                    choices.Add(c);
            return choices;
        }

        private void DisplayAnswers(List<int> choices)
        {
            Console.WriteLine("You choose:");
            foreach (int i in choices)
            {
                string result = answers[i].IsAnswerCorrect ? "And it's a correct answer" : "But it's a wrong anwser";
                double points = answers[i].Score;
                Console.WriteLine($"{i + 1} - {answers[i].Text}");
                Console.WriteLine($"{result} - points: {points}");
            }
        }
    }
}

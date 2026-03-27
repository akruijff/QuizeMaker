namespace QuizMaker
{
    public class QuizUI()
    {
        public static void Play(Quiz? quiz)
        {
            if (quiz is null)
            {
                ShowQuizRequred();
                return;
            }

            int i = 0;
            double score = 0;
            IEnumerable<Question> questions = quiz.Questions.Shuffle();
            foreach (Question q in questions)
                score += new QuestionUI(q, ++i, questions.Count()).Play();

            DisplayScore(score);
        }

        private static void ShowQuizRequred() => Console.WriteLine("There is no quiz stored. Please enter a quiz first.");

        private static void DisplayScore(double score)
        {
            Console.WriteLine("FIN");
            Console.WriteLine($"You're score is: {score}");
            Console.WriteLine();

            Console.WriteLine("Press any key to continue to the menu");
            Console.ReadKey(true);
            Console.WriteLine();
        }
    }
}

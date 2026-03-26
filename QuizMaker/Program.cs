namespace QuizMaker
{
    internal class Program
    {
        private static string FILE = "quiz.xml";

        /*
         * What program has multiple choices, random questions and user scoring? A quiz maker program! Design a 
         * program which asks the user for a list of questions, multiple choice answers for each question and the 
         * ability to specify which answer(s) are right. The program will then add those questions and answers to 
         * a list that gets stored on your harddisk using serialisation. The program can then read that file, 
         * randomly pick a question and its answers for the user to choose from. When the user chooses an answer 
         * the program will determine if the answer chosen is correct. It should also keep a score for the end 
         * when playing through all questions of the quiz.
         * 
         * rocket Tips: First we need to create our repository of questions and answers. A simple way to do this 
         * is to have your program open up and ask the user to enter questions and their answers. Each question 
         * and the answers they add will be stored in a file (serialization is your friend).
         * 
         * Think about how you could mark which answers are the correct ones.
         * 
         * An example might be “What color is the sky?” and list “red, blue, green” as the answers. The answer 
         * “Blue” is marked as the right answer (which you obviously don’t show the user taking the quiz). When 
         * the user selects the answer it gets compared it to the correct one stored. Each successful answer can 
         * be added to a counter variable. At the end of the quiz their score is this counter variable over the 
         * number of questions asked.
         * 
         * Added Difficulty: Support multiple answers as correct.
         * 
         * Checklist:
         * - design an object / class  that holds: 1 question, many answers, store the correct answer => think of a 
         *   way how you store that
         * - keep a store of multiples of that object
         * - implement a selection to allow the user to either create quizzes or play the game
         * - mechanism to select a random question
         * - store/read the quiz data in/from a file.
         */
        static void Main(string[] args)
        {
            Console.WriteLine("QuizMaker");
            Console.WriteLine();

            bool isExitRequested = false;
            var menu = new Menu("Menu:");
            menu.AddMenuItem('0', "Exit", () => isExitRequested = true);
            menu.AddMenuItem('1', "New quiz", NewQuiz);
            menu.AddMenuItem('2', "Play quiz", PlayQuiz);

            while (!isExitRequested)
            {
                menu.Display();
                char c = Menu.PromptAndReadChoice("Please pick an option: ");
                menu.InvokeMenuItem(c);
            }
        }

        static void NewQuiz()
        {
            var quiz = new Quiz();
            while (true)
            {
                Console.WriteLine("Please enter a question (leave empty to store the quiz): ");
                Question? question = UI.ReadQuestion();
                if (question is null)
                    break;
                Console.WriteLine();

                while (true)
                {
                    Console.WriteLine($"  Q: {question.Text}");
                    Console.WriteLine("  Please enter a(nother) answer (leave empty to enter new question): ");
                    Console.Write("  ");
                    Answer? answer = UI.ReadAwnser();
                    if (answer is null)
                        break;
                    Console.WriteLine();

                    Console.Write("  Is this a correct answer to the question? [Y/N] ");
                    answer.IsAnswerCorrect = UI.ReadBool();
                    Console.WriteLine();

                    Console.Write("  Please enter a score: ");
                    answer.Score = UI.ReadDouble();
                    Console.WriteLine();

                    question.Add(answer);
                }
                quiz.Add(question);
            }
            if (quiz.Questions.Count > 0)
                Persistence.SaveQuiz(quiz, FILE);
        }

        static void PlayQuiz()
        {
            Quiz? quiz = Persistence.ReadQuiz(FILE);
            if (quiz is null)
            {
                Console.WriteLine("There is no quiz stored. Please enter a quiz first.");
                return;
            }

            int i = 0;
            double score = 0;
            IEnumerable<Question> questions = quiz.Questions.Shuffle();
            foreach (Question q in questions)
                score += PlayQuestion(q, ++i, questions.Count());

            Console.WriteLine("FIN");
            Console.WriteLine($"You're score is: {score}");
            Console.WriteLine();

            Console.WriteLine("Press any key to continue to the menu");
            Console.ReadKey(true);
            Console.WriteLine();
        }

        private static double PlayQuestion(Question question, int questionNumer, int totalNumberQuestions)
        {
            List<Answer> answers = question.Answers;
            DisplayQuestion(question, questionNumer, totalNumberQuestions);
            DisplayOptions(answers);
            List<int> choices = ReadOptions();
            bool allCorrect = IsAllCorrect(answers);
            double score = CalcuateScore(answers, choices);
            ProcessPlayerChoice(answers, choices, !allCorrect);
            return score;
        }

        private static void DisplayQuestion(Question question, int questionNumer, int totalNumberQuestions)
        {
            Console.WriteLine($"Question {questionNumer} / {totalNumberQuestions}:");
            Console.WriteLine(question.Text);
            Console.WriteLine();
        }

        private static List<Answer> DisplayOptions(List<Answer> answers)
        {
            Console.WriteLine($"You have {answers.Count} options:");
            for (int i = 1; i <= answers.Count; ++i)
                Console.WriteLine($"{i}: {answers[i - 1].Text}");
            return answers;
        }

        private static bool IsAllCorrect(List<Answer> answers)
        {
            bool allCorrect = false;
            foreach (Answer a in answers)
                if (a.IsAnswerCorrect)
                    allCorrect = true;
            return allCorrect;
        }

        private static List<int> ReadOptions()
        {
            Console.Write("Please pick (one or mutilple by typing multiple numers): ");
            string? s = Console.ReadLine();
            Console.WriteLine();

            if (s == null || s == "")
                return [];

            List<int> choices = new();
            foreach (char c in s)
                choices.Add(c - '1');
            return choices;
        }

        private static void ProcessPlayerChoice(List<Answer> answers, List<int> choices, bool allIncorrect)
        {
            if (choices.Count == 0)
            {
                if (allIncorrect)
                    Console.WriteLine("You're right; all answers are incorrect.");
                else
                    Console.Write("There are correct answers!");
            }
            else
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
            Console.WriteLine();
        }

        private static double CalcuateScore(List<Answer> answers, List<int> choices)
        {
            double score = 0;
            foreach (int i in choices)
                score += answers[i].Score;
            return score;
        }
    }
}

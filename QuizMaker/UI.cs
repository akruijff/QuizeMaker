namespace QuizMaker
{
    public class UI
    {
        public static Question? ReadQuestion()
        {
            string? s = Console.ReadLine();
            return s is null or "" ? null : new Question(s);
        }

        public static Answer? ReadAwnser()
        {
            string? s = Console.ReadLine();
            return s is null or "" ? null : new Answer(s);
        }

        public static bool ReadBool()
        {
            while (true)
            {
                char c = Console.ReadKey(true).KeyChar;
                switch (c)
                {
                    case 'Y' or 'y': return true;
                    case 'N' or 'n': return false;
                }
            }
        }

        public static double ReadDouble()
        {
            while (true)
            {
                string? s = Console.ReadLine();
                if (double.TryParse(s, out double result))
                    return result;
            }
        }
    }
}

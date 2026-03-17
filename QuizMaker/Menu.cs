namespace QuizMaker
{
    public sealed class Menu(params string[] headers)
    {
        private readonly string[] _headers = (string[])headers.Clone();
        private readonly Dictionary<char, (string Text, Action action)> _items = [];

        public void AddMenuItem(char key, string text, Action action) => _items[key] = (text, action);

        public void Display()
        {
            DisplayHeader();
            DisplayOptions();
        }

        private void DisplayHeader()
        {
            foreach (var header in _headers)
                Console.WriteLine(header);
        }

        private void DisplayOptions()
        {
            foreach (var item in _items)
                Console.WriteLine($"{item.Key} - {item.Value.Text}");
        }

        public static char PromptAndReadChoice(string msg)
        {
            Console.WriteLine();
            Console.Write(msg);
            char c = Console.ReadKey().KeyChar;
            Console.WriteLine();
            Console.WriteLine();
            return c;
        }

        public void InvokeMenuItem(char c)
        {
            if (_items.TryGetValue(c, out var item))
                item.action.Invoke();
        }
    }
}
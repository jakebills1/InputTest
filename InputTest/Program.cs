using System;
using System.Reflection;
namespace InputTest
{
    class UserInput
    {
        public object Value;
        private string rawInput;
        public UserInput(string s)
        {
            this.rawInput = s;
            Value = this.ParseInput(rawInput);
        }
        public object ParseInput(string s)
        {
            // if input can be parsed as an int
            if(Int32.TryParse(s, out int i))
            {
                return new NumberInput(i); // the value field will be a NumberInput
            }
            else if (s.Equals("quit")) // the value field will be an ExitInput
            {
                return new ExitInput();
            }
            // default to Text, if input is not numeric or quit
            return new TextInput(s);
        }
        public string Info() // returns a string referring to the value type of the object contained in value
        {
            Type t = this.Value.GetType();
            return t.Name == "TextInput" ? "string" : "number";
        }
        public dynamic Read() // returns the value in Value
        {
            Type t = this.Value.GetType();
            PropertyInfo prop = t.GetProperty("Value");
            return prop.GetValue(this.Value, null);
        }
    }
    class NumberInput
    {
        private int value;
        public int Value { get; set; }

        public NumberInput(int input)
        {
            this.value = input;
            this.Value = value;
        }
    }
    class TextInput
    {
        private string value;
        public string Value { get; set; }
        public TextInput(string input)
        {
            this.value = input;
            this.Value = value;
        }
    }
    class ExitInput
    { }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("Enter a word or a number, then press enter (or type 'quit', then press enter to exit application) : ");
            UserInput u =  new UserInput(Console.ReadLine()); // read input into UserInputs Value property
            while (!(u.Value is ExitInput)) // while the input is not of type ExitInput, continue input loop
            {
                Console.WriteLine("You entered " + u.Read() + ", which is a " + u.Info()) ;
                Console.WriteLine("Enter a word or a number, then press enter ( or type 'quit', then press enter to exit application ) : ");
                u = new UserInput(Console.ReadLine());   
            }
            Console.WriteLine("You entered 'quit'. Goodbye!");
            return;
        }
    }
}

using System;
using System.Reflection;
namespace InputTest
{
    class UserInput
    {
        public object Value;
        private string rawInput;
        /// <summary>
        /// constructor for a UserInput object.
        /// </summary>
        /// <param name="s">The text entered by a user, as returned by Console.ReadLine</param>
        public UserInput(string s)
        {
            this.rawInput = s;
            Value = this.ParseInput(rawInput);
        }
        /// <summary>
        /// a method to test whether the user has inputted a valid number or a string
        /// </summary>
        /// <param name="s">The raw input string from the user</param>
        /// <returns>Either returns a NumberInput object with the value of s if the input can be parsed as a number, an ExitInput if it was the string 'quit', or a TextInput in all other cases </returns>
        public object ParseInput(string s)
        {

            if (Int32.TryParse(s, out int i)) // if input can be parsed as an int
            {
                return new NumberInput(i); // the value field will be a NumberInput~
            }
            else if (s.Equals("quit")) // the value field will be an ExitInput
            {
                return new ExitInput();
            }
            // default to Text, if input is not numeric or quit
            return new TextInput(s);
        }
        /// <summary>
        /// Returns the value of the object in the field Value
        /// </summary>
        /// <returns>Either the string "string" or the string "number", depending on whether the Value field is a TextInput or NumberInput</returns>
        public string Info() // returns a string referring to the value type of the object contained in value
        {
            if (this.Value is TextInput)
            {
                return "string";
            }
            else
            {
                return "number";
            }
        }
        /// <summary>
        /// A method to read the Value property from the field value
        /// </summary>
        /// <returns>The Value property in the field Value</returns>
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
            UserInput u = new UserInput(Console.ReadLine()); // read input into UserInputs Value property
            while (!(u.Value is ExitInput)) // while the input is not of type ExitInput, continue input loop
            {
                Console.WriteLine("You entered " + u.Read() + ", which is a " + u.Info());
                Console.WriteLine("Enter a word or a number, then press enter ( or type 'quit', then press enter to exit application ) : ");
                u = new UserInput(Console.ReadLine());
            }
            Console.WriteLine("You entered 'quit'. Goodbye!");
            return;
        }
    }
}

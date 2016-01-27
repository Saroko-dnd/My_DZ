namespace MVVMLight_Sample.Model
{
    public class Client
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Client()
        {
        }

        public Client(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", FirstName, LastName);
        }
    }
}

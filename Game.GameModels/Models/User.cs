namespace Game.GameModels.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public bool Isingame { get; set; }

        public User()
        {
            Login = string.Empty;
        }

        public override string ToString()
        {
            return $"Login: {Login}";
        }
    }
}

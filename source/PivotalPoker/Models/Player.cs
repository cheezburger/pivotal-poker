namespace PivotalPoker.Models
{
    public class Player
    {
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Player))
                return false;

            return Name == ((Player) obj).Name;
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
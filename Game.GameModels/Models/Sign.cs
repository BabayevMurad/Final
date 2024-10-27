namespace Game.GameModels.Models
{
    public class Sign
    {

        public int Index { get; set; }

        public SignEnum SignEnum { get; set; }

        public int UserId { get; set; }

        public Sign(SignEnum @enum, int Id)
        {
            SignEnum = @enum;

            UserId = Id;
        }

        public Sign() { }

        public override string ToString()
        {
            return $"Index: {Index}; SignEnum{SignEnum} UserId:{UserId}";
        }
    }
}

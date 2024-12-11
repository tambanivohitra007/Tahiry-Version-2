namespace Fihirana_database.Classes
{
    internal class ChantModel
    {
        public string Description { get; set; }
        public string Range { get; set; }

        public ChantModel(string description, string range)
        {
            Description = description;
            Range = range;
        }
    }
}

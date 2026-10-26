namespace MilitaryApp.Core
{
    /// <summary>
    /// Класс, описывающий сущность "Военнослужащий".
    /// </summary>
    public class Soldier
    {
        /// <summary>Уникальный идентификационный номер.</summary>
        public int Id { get; set; }

        /// <summary>ФИО военнослужащего.</summary>
        public string FullName { get; set; }

        /// <summary>Воинское звание.</summary>
        public string Rank { get; set; }

        /// <summary>Занимаемая должность.</summary>
        public string Position { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {FullName} - {Rank} ({Position})";
        }
    }
}
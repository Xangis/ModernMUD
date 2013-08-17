namespace MUDEngine
{
    /// <summary>
    /// Monk skill definition.
    /// </summary>
    public class MonkSkill
    {
        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pointvalues"></param>
        public MonkSkill(string name, int[] pointvalues)
        {
            Name = name;
            _points = pointvalues; // We may not want to allow variable length arrays for this value.
        }

        private int[] _points = new int[TraditionData.Table.Length];
        public string Name { get; set; }

        public int[] Points
        {
            get { return _points; }
            set { _points = value; }
        }

        /// <summary>
        /// Table of monk skills.
        /// </summary>
        public static MonkSkill[] Table = 
        {
            new MonkSkill(
                "reserved",
                new int[]{  }
            ),

            new MonkSkill(
                "Bear Stance",
                new[]{ 5 }
            ),

            new MonkSkill(
                "Cat Stance",
                new int[]{  }
            ),

            new MonkSkill(
                "Cobra Stance",
                new int[] {  }
            ),

            new MonkSkill(
                "Crane Stance",
                new[] { 8 }
            ),

            new MonkSkill(
                "Dragon Stance",
                new int[] {  }
            ),

            new MonkSkill(
                "Dragonfly Stance",
                new int[] {  }
            ),

            new MonkSkill(
                "Hawk Stance",
                new int[] {  }
            ),

            new MonkSkill(
                "Leopard Stance",
                new int[] {  }
            ),

            new MonkSkill(
                "Mantis Stance",
                new int[] {  }
            ),

            new MonkSkill(
                "Monkey Stance",
                new int[] {  }
            ),

            new MonkSkill(
                "Snake Stance",
                new int[] {  }
            ),

            new MonkSkill(
                "Tiger Stance",
                new int[] {  }
            ),

            new MonkSkill(
                "Parry",
                new[] { 3 }
            ),

            new MonkSkill(
                "Grace of the Leopard",
                new[] { 10 }
            ),

            new MonkSkill(
                "Fortitude I",
                new[] { 1 }
            ),

            new MonkSkill(
                "Fortitude II",
                new[] { 10 }
            ),

            new MonkSkill(
                "Might I",
                new[] { 1 }
            ),

            new MonkSkill(
                "Might II",
                new[] { 10 }
            ),

            new MonkSkill(
                "Coordination I",
                new[] { 1 }
            ),

            new MonkSkill(
                "Coordination II",
                new[] { 10 }
            ),

            new MonkSkill(
                "Coordination III",
                new[] { 25 }
            ),

            new MonkSkill(
                "Strength I",
                new[] { 1 }
            ),

            new MonkSkill(
                "Strength II",
                new[] { 10 }
            ),

            new MonkSkill(
                "Strength III",
                new int[] {  }
            ),

            new MonkSkill(
                "Constitution I",
                new[] { 1 }
            ),

            new MonkSkill(
                "Constitution II",
                new[] { 10 }
            ),

            new MonkSkill(
                "Constitution III",
                new int[] { }
            ),

            new MonkSkill(
                "Dexterity I",
                new[] { 1 }
            ),

            new MonkSkill(
                "Dexterity II",
                new[] { 10 }
            ),

            new MonkSkill(
                "Dexterity III",
                new int[] {  }
            ),

            new MonkSkill(
                "Agility I",
                new[] { 1 }
            ),

            new MonkSkill(
                "Agility II",
                new[] { 10 }
            ),

            new MonkSkill(
                "Agility III",
                new int[] { }
            ),

            new MonkSkill(
                "Wisdom I",
                new int[] {  }
            ),

            new MonkSkill(
                "Wisdom II",
                new int[] {  }
            ),

            new MonkSkill(
                "Wisdom III",
                new int[] { }
            ),

            new MonkSkill(
                "Intelligence I",
                new int[] {  }
            ),

            new MonkSkill(
                "Intelligence II",
                new int[] {  }
            ),

            new MonkSkill(
                "Intelligence III",
                new int[] { }
            ),

            new MonkSkill(
                "Charisma I",
                new int[] {  }
            ),

            new MonkSkill(
                "Charisma II",
                new int[] {  }
            ),

            new MonkSkill(
                "Charisma III",
                new int[] { }
            ),

            new MonkSkill(
                "Power I",
                new int[] {  }
            ),

            new MonkSkill(
                "Power II",
                new int[] {  }
            ),

            new MonkSkill(
                "Power III",
                new int[] { }
            ),

            new MonkSkill(
                "Form of Mind I",
                new int[] {  }
            ),

            new MonkSkill(
                "Form of Mind II",
                new int[] {  }
            ),

            new MonkSkill(
                "Form of Mind III",
                new int[] { }
            ),

            new MonkSkill(
                "Unity and Purification I",
                new int[] {  }
            ),

            new MonkSkill(
                "Unity and Purification II",
                new int[] {  }
            ),

            new MonkSkill(
                "Unity and Purification III",
                new int[] { }
            ),

            new MonkSkill(
                "Purity of Spirit I",
                new int[] {  }
            ),

            new MonkSkill(
                "Purity of Spirit II",
                new int[] {  }
            ),

            new MonkSkill(
                "Purity of Spirit III",
                new int[] { }
            ),

            new MonkSkill(
                "Stability of Body I",
                new int[] {  }
            ),

            new MonkSkill(
                "Stability of Body II",
                new int[] {  }
            ),

            new MonkSkill(
                "Stability of Body III",
                new int[] { }
            ),

            new MonkSkill(
                "Dance of the Golden Lotus I",
                new int[] {  }
            ),

            new MonkSkill(
                "Dance of the Golden Lotus II",
                new int[] {  }
            ),

            new MonkSkill(
                "Dance of the Golden Lotus III",
                new int[] { }
            ),

            new MonkSkill(
                "Purity of Chi I",
                new int[] {  }
            ),

            new MonkSkill(
                "Purity of Chi II",
                new int[] {  }
            ),

            new MonkSkill(
                "Purity of Chi III",
                new int[] { }
            ),

            new MonkSkill(
                "Stone Palm",
                new int[] { }
            ),

            new MonkSkill(
                "Ivory Palm",
                new int[] { }
            ),

            new MonkSkill(
                "Jade Palm",
                new int[] { }
            ),

            new MonkSkill(
                "Iron Palm",
                new int[] { }
            ),

            new MonkSkill(
                "Emerald Palm",
                new int[] { }
            ),

            new MonkSkill(
                "Dragon Palm",
                new int[] { }
            ),

            new MonkSkill(
                "Water Through the Reeds I",
                new int[] {  }
            ),

            new MonkSkill(
                "Water Through the Reeds II",
                new int[] {  }
            ),

            new MonkSkill(
                "Water Through the Reeds III",
                new int[] { }
            ),

            new MonkSkill(
                "Lion Strike",
                new int[] { }
            ),

            new MonkSkill(
                "Spider Strike",
                new int[] { }
            ),

            new MonkSkill(
                "Phoenix Strike",
                new int[] { }
            ),

            new MonkSkill(
                "Cobra Strike",
                new int[] { }
            ),

            new MonkSkill(
                "Tiger Strike",
                new int[] { }
            ),

            new MonkSkill(
                "Mantis Strike",
                new int[] { }
            ),

            new MonkSkill(
                "Falcon Strike",
                new int[] { }
            ),

            new MonkSkill(
                "Scorpion Strike",
                new int[] { }
            ),

            new MonkSkill(
                "Poison the Spirit Atemi",
                new int[] { }
            ),

            new MonkSkill(
                "Withering Flesh Atemi",
                new int[] { }
            ),

            new MonkSkill(
                "Neural Atemi",
                new int[] { }
            ),

            new MonkSkill(
                "Breathstealer Atemi",
                new int[] { }
            ),

            new MonkSkill(
                "Lotus Blossom Atemi",
                new int[] { }
            ),

            new MonkSkill(
                "Wrist Lock",
                new int[] { }
            ),

            new MonkSkill(
                "Tigers Leap",
                new int[] { }
            ),

            new MonkSkill(
                "Throw",
                new int[] { }
            ),

            new MonkSkill(
                "Feign Death",
                new int[] { }
            ),

            new MonkSkill(
                "Might of the Tiger",
                new int[] { }
            ),

            new MonkSkill(
                "Art of Stealth",
                new int[] { }
            ),

            new MonkSkill(
                "Art of Hiding",
                new int[] { }
            ),

            new MonkSkill(
                "Art of Vanishing",
                new int[] { }
            ),

            new MonkSkill(
                "Way of the Snake",
                new int[] { }
            ),

            new MonkSkill(
                "Wind in the Reeds",
                new int[] { }
            ),

            new MonkSkill(
                "Dance of the Golden Lotus",
                new int[] { }
            ),

            new MonkSkill(
                "Purity of Chi",
                new int[] { }
            ),

            new MonkSkill(
                "Fury of Heaven",
                new int[] { }
            ),

            new MonkSkill(
                "Whirlwind",
                new int[] { }
            ),

            new MonkSkill(
                "Anti-Bear",
                new int[] { }
            ),

            new MonkSkill(
                "Anti-Snake",
                new int[] { }
            ),

            new MonkSkill(
                "Anti-Cobra",
                new int[] { }
            )
        };
    }
}

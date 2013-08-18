using System;
using System.Collections.Generic;
using System.Text;

namespace ModernMUD
{
    /// <summary>
    /// This class exists so that we can convert a spell number that was set in
    /// an editor, such as DE, into a string and back again with fast lookup.
    /// 
    /// This is really just a bidectional dictionary.
    /// 
    /// Since spells are a game design decision, this should not be hard-coded,
    /// but rather configurable via the spell editor or other tool and loaded
    /// from a file, or better yet, spells should not be stored in objects as
    /// numbers, since object-attached spells are the only reason this
    /// cross-reference exists.
    /// 
    /// TODO: Something more elegant than this hard-coded solution.
    /// </summary>
    public static class SpellNumberToTextMap
    {
        static Dictionary<int, String> _numberToString;
        static Dictionary<String, int> _stringToNumber;

        /// <summary>
        /// Adds an entry to the cross-reference.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="name"></param>
        public static void AddEntry(int number, String name)
        {
            _numberToString.Add(number, name);
            _stringToNumber.Add(name, number);
        }

        /// <summary>
        /// Gets the spell number from the spell's name.
        /// </summary>
        /// <returns>The spell's number, or -1 if not found.</returns>
        public static int GetSpellNumberFromString(String name)
        {
            if (_stringToNumber.ContainsKey(name))
            {
                return _stringToNumber[name];
            }

            return -1;
        }

        /// <summary>
        /// Gets the spell's name from its number.
        /// </summary>
        /// <returns>The spell's name, or String.Empty if not found.</returns>
        public static String GetSpellNameFromNumber(int number)
        {
            if (_numberToString.ContainsKey(number))
            {
                return _numberToString[number];
            }
            return String.Empty;
        }

        /// <summary>
        /// Static constructor, populates the dictionary.
        /// 
        /// Add items to this list to allow them in the game (some things may
        /// be left out intentionally as being too powerful or disruptive).
        /// </summary>
        static SpellNumberToTextMap()
        {
            _numberToString = new Dictionary<int,string>();
            _stringToNumber = new Dictionary<string,int>();

            AddEntry(1, "armor");
            AddEntry(2, "teleport");
            AddEntry(3, "bless");
            AddEntry(4, "blindness");
            AddEntry(5, "burning hands");
            AddEntry(7, "charm person");
            AddEntry(8, "chill touch");
            AddEntry(9, "full heal");
            AddEntry(10, "cone of cold");
            AddEntry(14, "cure blindness");
            AddEntry(15, "cure critical");
            AddEntry(16, "cure light");
            AddEntry(17, "curse");
            AddEntry(18, "continual light");
            AddEntry(19, "detect invisibility");
            AddEntry(20, "minor creation");
            AddEntry(21, "flamestrike");
            AddEntry(22, "dispel evil");
            AddEntry(23, "earthquake");
            AddEntry(25, "energy drain");
            AddEntry(26, "fireball");
            AddEntry(27, "harm");
            AddEntry(28, "heal");
            AddEntry(29, "invisibility");
            AddEntry(30, "lightning bolt");
            AddEntry(31, "locate object");
            AddEntry(32, "magic missile");
            AddEntry(33, "poison");
            AddEntry(34, "protection from evil");
            AddEntry(35, "remove curse");
            AddEntry(36, "stoneskin");
            AddEntry(37, "shocking grasp");
            AddEntry(38, "sleep");
            AddEntry(39, "giant strength");
            AddEntry(41, "haste");
            AddEntry(43, "remove poison");
            AddEntry(44, "sense life");
            AddEntry(45, "identify");
            AddEntry(47, "firestorm");
            AddEntry(50, "frost breath");
            AddEntry(54, "fear");
            AddEntry(56, "vitality");
            AddEntry(57, "cure serious");
            AddEntry(61, "minor globe");
            AddEntry(64, "vigorize light");
            AddEntry(65, "vigorize serious");
            AddEntry(66, "vigorize critical");
            AddEntry(71, "enfeeblement");
            AddEntry(72, "dispel good");
            AddEntry(73, "dexterity");
            AddEntry(75, "aging");
            AddEntry(76, "cyclone");
            AddEntry(79, "vitalize mana");
            AddEntry(81, "protection from good");
            AddEntry(82, "animate dead");
            AddEntry(83, "levitation");
            AddEntry(84, "fly");
            AddEntry(85, "water breathing");
            AddEntry(87, "gate");
            AddEntry(90, "detect evil");
            AddEntry(91, "detect good");
            AddEntry(92, "detect magic");
            AddEntry(93, "dispel magic");
            AddEntry(95, "mass invis");
            AddEntry(96, "protection from fire");
            AddEntry(97, "protection from cold");
            AddEntry(98, "protection from lightning");
            AddEntry(99, "darkness");
            AddEntry(100, "minor paralysis");
            AddEntry(102, "slowness");
            AddEntry(103, "wither");
            AddEntry(104, "protection from gas");
            AddEntry(105, "protection from acid");
            AddEntry(106, "infravision");
            AddEntry(107, "prismatic spray");
            AddEntry(108, "fireshield");
            AddEntry(109, "color spray");
            AddEntry(110, "incendiary cloud");
            AddEntry(111, "ice storm");
            AddEntry(112, "disintegrate");
            AddEntry(113, "cause light");
            AddEntry(114, "cause serious");
            AddEntry(115, "cause critical");
            AddEntry(116, "acid blast");
            AddEntry(124, "sunray");
            AddEntry(126, "feeblemind");
            AddEntry(127, "silence");
            AddEntry(128, "turn undead");
            AddEntry(131, "coldshield");
            AddEntry(134, "vampiric touch");
            AddEntry(136, "protection from undead");
            AddEntry(141, "barkskin");
            AddEntry(144, "major globe");
            AddEntry(145, "embalm");
            AddEntry(147, "shadow breath2");
            AddEntry(156, "agitation");
            AddEntry(157, "adrenaline control");
            AddEntry(160, "ballistic attack");
            AddEntry(163, "combat mind");
            AddEntry(170, "domination");
            AddEntry(172, "ego whip");
            AddEntry(179, "flesh armor");
            AddEntry(180, "inertial barrier");
            AddEntry(181, "inflict pain");
            AddEntry(186, "plague");
            AddEntry(188, "soulshield");
            AddEntry(190, "mass heal");
            AddEntry(192, "spirit armor");
            AddEntry(197, "pythonsting");
            AddEntry(201, "pantherspeed");
            AddEntry(204, "hawkvision");
            AddEntry(206, "mending");
            AddEntry(212, "malison");
            AddEntry(214, "greater mending");
            AddEntry(215, "lionrage");
            AddEntry(221, "elephantstrength");
            AddEntry(223, "scathing wind");
            AddEntry(228, "sustenance");
            AddEntry(234, "lesser mending");
            AddEntry(236, "true seeing");
            AddEntry(238, "ravenflight");
            AddEntry(248, "enlarge");
            AddEntry(249, "reduce");
            AddEntry(254, "iceball");
            AddEntry(263, "blur");
            AddEntry(268, "raise spectre");
            AddEntry(271, "raise lich");
            AddEntry(275, "cure disease");
            AddEntry(279, "sense followers");
            AddEntry(286, "stornogs spheres");
            AddEntry(300, "alacrity");
            AddEntry(306, "choke");
            AddEntry(330, "mirage arcana");
            AddEntry(332, "hellfire");
        }
    }
}

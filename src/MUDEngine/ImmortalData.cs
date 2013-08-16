using System;

namespace MUDEngine
{
    /// <summary>
    /// Represents data specific to immortal-level individuals.
    /// </summary>
	public class ImmortalData
	{
        private string _immortalSkills;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ImmortalData()
        {
            AppearMessage = String.Empty;
            DisappearMessage = String.Empty;
            ImmortalSkills = String.Empty;
            InvisLevel = 0;
            ImmortalColor = String.Empty;
            ImmtalkFlags = ImmortalChat.IMMTALK_DEATHS | ImmortalChat.IMMTALK_ON | ImmortalChat.IMMTALK_PETITION | 
                ImmortalChat.IMMTALK_LOGINS | ImmortalChat.IMMTALK_NEWBIE;
        }

        /// <summary>
        /// Message shown when immortal appears in the room.
        /// </summary>
        public string AppearMessage { get; set; }

        /// <summary>
        /// Immortal chat flags.
        /// </summary>
        public int ImmtalkFlags { get; set; }

        /// <summary>
        /// Message shown when immortal disappears from the room.
        /// </summary>
        public string DisappearMessage { get; set; }

        /// <summary>
        /// Immortal-level skills available to the character.
        /// </summary>
        public string ImmortalSkills
        {
            get
            {
                return _immortalSkills;
            }
            set
            {
                _immortalSkills = value;
            }
        }

        /// <summary>
        /// Ansi color code for immortal's news entries and echoes.
        /// </summary>
        public string ImmortalColor { get; set; }

        /// <summary>
        /// Invisibility level of the character (those below this level can't see the immortal).
        /// </summary>
        public int InvisLevel { get; set; }

        /// <summary>
        /// Checks whether the character is authorized to use a particular immortal skill.
        /// </summary>
        /// <param name="skllnm"></param>
        /// <returns></returns>
        public bool Authorized(string skllnm)
        {
            if (!MUDString.NameContainedIn(skllnm, _immortalSkills))
            {
                return false;
            }

            return true;
        }
	}
}

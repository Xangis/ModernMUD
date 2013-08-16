using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using ModernMUD;

namespace MUDEngine
{
    /// <summary>
    /// Represents a bounty placed on the head of a player or a guild's members.
    /// </summary>
    [Serializable]
    public class Bounty
    {
        public static List<Bounty> _bounties = new List<Bounty>();

        public enum BountyType
        {
            none = 0,
            guild,
            player,
        };

        public string Name { get; set; }
        public int Amount { get; set; } // Cash price in copper pieces.
        public DateTime Expiration { get; set; }
        public BountyType Type { get; set; }
        public int Quantity { get; set; }
        public bool AllowDuplicates { get; set; } // For guild bounties, can the same member be killed more than once?
        public string Kills { get; set; } // For guild bounties, a list of the names of guildmembers who have been killed.

        /// <summary>
        /// Gets the total number of bounties in the game.
        /// </summary>
        public static int Count
        {
            get
            {
                return _bounties.Count;
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public Bounty()
        {
            Name = String.Empty;
            Amount = 0;
            Expiration = DateTime.Now + TimeSpan.FromDays(60.0);
            Type = BountyType.player;
            Quantity = 1;
            AllowDuplicates = true;
            Kills = String.Empty;
        }

        /// <summary>
        /// Gets the total cash value of all outstanding bounties.
        /// </summary>
        /// <returns></returns>
        public int GetTotalBountyValue()
        {
            int value = 0;
            foreach (Bounty bounty in _bounties)
            {
                value += bounty.Amount * bounty.Quantity;   
            }
            return value;
        }

        /// <summary>
        /// Adds a bounty on a player.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="amount"></param>
        public void AddPlayerBounty(string name, int amount)
        {
            Bounty bounty = new Bounty();
            bounty.Type = BountyType.player;
            bounty.Name = name;
            bounty.Amount = amount;
        }

        /// <summary>
        /// Adds a bounty on a guild.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="amount"></param>
        /// <param name="quantity"></param>
        /// <param name="duplicates"></param>
        public void AddGuildBounty(string name, int amount, int quantity, bool duplicates)
        {
            Bounty bounty = new Bounty();
            bounty.Type = BountyType.guild;
            bounty.Name = name;
            bounty.Amount = amount;
            bounty.Quantity = quantity;
            bounty.AllowDuplicates = duplicates;
        }

        /// <summary>
        /// Checks whether there was a bounty on the victim's head and rewards the killer
        /// if they killed a bountied player or guildmember.  Called upon victim death.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="victim"></param>
        public void CheckBounty(CharData ch, CharData victim)
        {
            foreach (Bounty bounty in _bounties)
            {
                if (bounty.Name == victim._name)
                {
                    // Award bounty and delete it.
                }
            }
        }

        /// <summary>
        /// Saves the bounty list.
        /// </summary>
        /// <returns></returns>
        public static bool Save()
        {
            string filename = FileLocation.SystemDirectory + FileLocation.BountyFile;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Bounty>));
                Stream stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
                serializer.Serialize(stream, _bounties);
                stream.Close();
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Exception saving bounties: " + ex);
                return false;
            }
        }

        /// <summary>
        /// Loads the bounty list.
        /// </summary>
        /// <returns></returns>
        public static bool Load()
        {
            string filename = FileLocation.SystemDirectory + FileLocation.BountyFile;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Bounty>));
                Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.None);
                _bounties = (List<Bounty>)serializer.Deserialize(stream);
                stream.Close();
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Exception in Bounty.Load(): " + ex);
                _bounties = new List<Bounty>();
                return false;
            }
        }

        /// <summary>
        /// Shows a list of the outstanding bounties to a player.
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static void ShowBounties(CharData ch)
        {
            // TODO: Print all bounties to the player.
        }
    };
}

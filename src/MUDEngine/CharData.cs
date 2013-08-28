using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;
using ModernMUD;

namespace MUDEngine
{
    /// <summary>
    /// One character (PC or NPC).
    /// </summary>
    [Serializable]
    public class CharData
    {
        public enum FlyLevel
        {
            ground = 0,
            low,
            medium,
            high
        }

        // Not saved, handled at runtime.
        [XmlIgnore]
        public CharData _master;
        // List, not saved.
        [XmlIgnore]
        public CharData _groupLeader;
        // Not saved, handled at runtime.
        [XmlIgnore]
        public CharData _fighting;
        // Not saved, handled at runtime.
        [XmlIgnore]
        public CharData _replyTo;
        // Not saved, handled at runtime.
        [XmlIgnore]
        public CharData _riding;
        // Not saved, handled at runtime.
        [XmlIgnore]
        public CharData _rider;
        // Not saved, handled at runtime.
        [XmlIgnore]
        public CharData _nextInGroup;
        // Not saved, handled at runtime.
        [XmlIgnore]
        public List<CharData> _followers;
        // Special function is saved as a string.
        [XmlIgnore]
        public List<MobSpecial> _specFun;
        public string _specFunNames;
        // Death function is saved as a string.
        [XmlIgnore]
        public MobSpecial _deathFun;
        public string _deathFunName;
        // TODO: Save this as an index number so we can reconnect at load.
        public MobTemplate _mobTemplate;
        // Not saved, handled at runtime.
        [XmlIgnore]
        public ChatterBot _chatterBot;
        // Not saved, handled at runtime.
        [XmlIgnore]
        public SocketConnection _socket;
        public List<Affect> _affected = new List<Affect>();
        public List<Object> _carrying = new List<Object>();
        [XmlIgnore]
        public Room _inRoom;
        // Don't need to persist this variable.
        [XmlIgnore]
        public Room _wasInRoom;
        public string _name;
        public string _shortDescription;
        public string _fullDescription;
        public string _description;
        public int _maxHitpoints;
        public MobTemplate.Sex _sex;
        // Not saving the whole class, just the number.
        [XmlIgnore]
        public CharClass _charClass;
        public int _charClassNum;
        public int _level;
        public int _trustLevel;
        public Race.Size _size;
        public TimeSpan _played;
        public int _castingSpell;
        public int _castingTime;
        public int _permStrength;
        public int _permIntelligence;
        public int _permWisdom;
        public int _permDexterity;
        public int _permConstitution;
        public int _permAgility;
        public int _permCharisma;
        public int _permPower;
        public int _permLuck;
        public int _modifiedStrength;
        public int _modifiedIntelligence;
        public int _modifiedWisdom;
        public int _modifiedDexterity;
        public int _modifiedConstitution;
        public int _modifiedAgility;
        public int _modifiedCharisma;
        public int _modifiedPower;
        public int _modifiedLuck;
        public string _chatterBotName;
        public DateTime _logon;
        public DateTime _saveTime;
        public DateTime _lastNote;
        public int _timer;
        public int _wait;
        public int _hitpoints;
        public int _currentMana;
        public int _maxMana;
        public int _currentMoves;
        public int _maxMoves;
        public int _experiencePoints;
        // Action flags are multi-purpose: for players they're PLAYER_ bits, for mobs they're
        // ACT_ bits,
        public int[] _actionFlags = new int[ Limits.NUM_ACTION_VECTORS ];
        public int[] _affectedBy = new int[ Limits.NUM_AFFECT_VECTORS ];
        public int _position;
        public int _carryWeight;
        public int _carryNumber;
        public int[] _savingThrows = new int[ 5 ];	// added by Xangis
        public int _alignment;
        public int _hitroll;
        public int _damroll;
        public int _armorPoints;
        public int _wimpy;
        public TalkChannel _deaf;
        // MobProgActList *	mpDescriptor._actFlags;
        public int _mpactnum;
        // Handled at runtime, does not persist.
        [XmlIgnore]
        public EnemyData _hunting;
        // Handled at runtime, does not persist.
        [XmlIgnore]
        public List<EnemyData> _hating;
        // Handled at runtime, does not persist.
        [XmlIgnore]
        public EnemyData _fearing;
        public Race.DamageType _resistant;
        public Race.DamageType _immune;
        public Race.DamageType _susceptible;
        public Race.DamageType _vulnerable;
        public FlyLevel _flyLevel;
        public int _loadRoomIndexNumber;
        private int _race;
        protected Coins _money = new Coins();
        private static int _numCharData;
        // Handled at runtime.  Rage factor is the current mood of the mobile.  It is typically 
        // modified due to friendly or unfriendly socials, chat conversation, and other factors
        // that may cause the mob to flip out and attack a player even if it's not normally
        // aggressive.  It's essentially "that's the last straw" factor -- no mob is going to
        // tolerate constant abuse.
        [XmlIgnore]
        public int _rageFactor;
        // Should this object be deleted next time around?
        [XmlIgnore]
        public bool _deleteMe;

        public CharData()
        {
            ++_numCharData;
            _followers = new List<CharData>();
            _specFun = new List<MobSpecial>();
            _hating = new List<EnemyData>();
            _lastNote = new DateTime();
            _sex = 0;
            _chatterBot = null;
            _chatterBotName = String.Empty;
            _charClass = CharClass.ClassList[0];
            _trustLevel = 0;
            _played = new TimeSpan();
            _permStrength = MUDMath.Dice( 3, 31 ) + 7;
            _permIntelligence = MUDMath.Dice( 3, 31 ) + 7;
            _permWisdom = MUDMath.Dice( 3, 31 ) + 7;
            _permDexterity = MUDMath.Dice( 3, 31 ) + 7;
            _permConstitution = MUDMath.Dice( 3, 31 ) + 7;
            _permAgility = MUDMath.Dice( 3, 31 ) + 7;
            _permCharisma = MUDMath.Dice( 3, 31 ) + 7;
            _permPower = MUDMath.Dice( 3, 31 ) + 7;
            _permLuck = MUDMath.Dice( 3, 31 ) + 7;
            _modifiedStrength = 0;
            _modifiedIntelligence = 0;
            _modifiedWisdom = 0;
            _modifiedCharisma = 0;
            _modifiedAgility = 0;
            _modifiedDexterity = 0;
            _modifiedLuck = 0;
            _modifiedConstitution = 0;
            _modifiedPower = 0;
            _timer = 0;
            _wait = 0;
            _rageFactor = 0;
            _experiencePoints = 0;
            _mpactnum = 0;;
            _loadRoomIndexNumber = StaticRooms.GetRoomNumber("ROOM_NUMBER_LIMBO");
            _carryWeight = 0;
            _carryNumber = 0;
            _alignment = 0;
            _hitroll = 0;
            _damroll = 0;
            _wimpy = 0;
            _deaf = 0;
            _logon = DateTime.Now;
            _armorPoints = 100;
            _position = Position.standing;
            _level = 0;
            _race = 0;
            _hitpoints = 20;
            _maxHitpoints = 20;
            _currentMana = 100;
            _maxMana = 0;
            _currentMoves = 150;
            _maxMoves = 150;
            _money.Copper = 0;
            _money.Silver = 0;
            _money.Gold = 0;
            _money.Platinum = 0;
            _resistant = Race.DamageType.none;
            _immune = Race.DamageType.none;
            _susceptible = Race.DamageType.none;
            _vulnerable = Race.DamageType.none;
            _flyLevel = 0;
            _size = Race.Size.medium;
            int count;
            for( count = 0; count < Limits.NUM_ACTION_VECTORS; count++ )
            {
                _actionFlags[ count ] = 0;
            }
            return;
        }

        ~CharData()
        {
            try
            {
                // Remove all pointers to ch in CharList.
                CharData worldChar;
                for (int i = (Database.CharList.Count - 1); i >= 0; i--)
                {
                    worldChar = Database.CharList[i];
                    // Many of these will already have been checked and Reset before this point.
                    // We're doing this to be sure that nobody is pointing to us that shouldn't
                    // be.  It doesn't hurt to check things again, and this is our last line of
                    // defense.
                    if (worldChar._replyTo == this)
                    {
                        worldChar._replyTo = null;
                    }
                    if (worldChar._fighting == this)
                    {
                        worldChar._fighting = null;
                    }
                    if (worldChar._rider == this)
                    {
                        worldChar._rider = null;
                    }
                    if (worldChar._riding == this)
                    {
                        worldChar._riding = null;
                    }
                    if (worldChar._hunting && worldChar._hunting.Who == this)
                    {
                        worldChar._hunting.Name = String.Empty;
                    }
                    if (worldChar._fearing && worldChar._fearing.Who == this)
                    {
                        worldChar._fearing.Who = null;
                        worldChar._fearing.Name = String.Empty;
                    }
                    foreach (EnemyData hhf in worldChar._hating)
                    {
                        if (hhf.Who == this)
                        {
                            worldChar._hating.Remove(hhf);
                        }
                    }
                    if (worldChar._master == this)
                    {
                        worldChar._master = null;
                    }
                    if (worldChar._nextInGroup == this)
                    {
                        worldChar._nextInGroup = _nextInGroup;
                    }
                    if (worldChar._groupLeader == this)
                    {
                        worldChar._groupLeader = null;
                    }
                    if (worldChar._followers != null)
                    {
                        worldChar._followers.Clear();
                    }
                    // If it's us, it's time to leave the CharList.
                    if (worldChar == this)
                    {
                        Database.CharList.Remove(worldChar);
                    }
                }

                _carrying.Clear();

                for (int i = (_affected.Count - 1); i >= 0; i-- )
                {
                    RemoveAffect(_affected[i]);
                }
            }
            catch (Exception ex)
            {
                Log.Trace("Exception in destructor: " + ex);
            }
            --_numCharData;
        }

        public bool RemoveBlindness()
        {
            bool retval = false;

            if( IsAffected( Affect.AFFECT_BLIND ) )
                retval = true;
            RemoveAffect(Affect.AFFECT_BLIND);
            return retval;
        }

        public bool RemovePoison()
        {
            bool retval = false;
            if( IsAffected( Affect.AFFECT_POISON ) )
                retval = true;
            RemoveAffect(Affect.AFFECT_POISON);
            for (int i = (_affected.Count - 1); i >= 0; i--)
            {
                if (_affected[i].HasBitvector(Affect.AFFECT_POISON))
                {
                    // strip the affect
                    RemoveAffect( _affected[i] );
                }
            }
            return retval;
        }

        public static int Count
        {
            get
            {
                return _numCharData;
            }
        }

        /// <summary>
        /// Finds an equipped object based on its _name.
        /// </summary>
        /// <param name="argument"></param>
        /// <returns></returns>
        public Object GetObjWear( string argument )
        {
            string arg = String.Empty;
            CharData ch = this;

            int number = MUDString.NumberArgument( argument, ref arg );
            int count = 0;
            foreach( Object obj in _carrying )
            {
                if( obj.WearLocation != ObjTemplate.WearLocation.none
                        && CanSeeObj( ch, obj )
                        && ( MUDString.NameContainedIn( arg, obj.Name )
                             || Database.GetExtraDescription( arg, obj.ExtraDescription ).Length > 0
                             || Database.GetExtraDescription( arg, obj.ObjIndexData.ExtraDescriptions ).Length > 0 ) )
                {
                    if( ++count == number )
                        return obj;
                }
            }

            count = 0;
            foreach( Object obj in ch._carrying )
            {
                if( obj.WearLocation != ObjTemplate.WearLocation.none
                        && CanSeeObj( ch, obj )
                        && MUDString.NameIsPrefixOfContents( arg, obj.Name ) )
                {
                    if( ++count == number )
                        return obj;
                }
            }

            return null;
        }

        /// <summary>
        /// Retrieves a character's age in years.
        /// </summary>
        /// <returns></returns>
        public int GetAge()
        {
            // Age is calculated based on our MUD time units as defined in limits.h
            TimeSpan age;

            if( IsNPC() )
            {
                age = new TimeSpan(((Database.SystemData.CurrentTime - _logon + _played ).Ticks /
                        Limits.TIMESPAN_GAME_YEAR.Ticks));
            }
            else
            {
                age =  new TimeSpan( ( Database.SystemData.CurrentTime - ( (PC)this ).Birthdate ).Ticks / Limits.TIMESPAN_GAME_YEAR.Ticks );
            }
            return Race.RaceList[ _race ].BaseAge + (int)(age.Ticks / Limits.TIMESPAN_GAME_YEAR.Ticks);
        }

        /// <summary>
        /// Retrieve character's current strength.
        /// </summary>
        /// <returns></returns>
        public int GetCurrStr()
        {
            int mod = Race.RaceList[ _race ].StrModifier;
            int plus = _modifiedStrength;
            int max = Race.RaceList[ _race ].StrModifier;

            if( !IsNPC() )
            {
                PC pc = (PC)this;
                mod += pc.RaceStrMod;
                plus += pc.MaxStrMod;
                max += ( pc.RaceStrMod + pc.MaxStrMod );
            }

            return Macros.Range( 1, ( _permStrength * mod / 100 ) + plus, max );
        }

        /// <summary>
        /// Retrieve character's maximum strength.
        /// </summary>
        /// <returns></returns>
        public int GetMaxStr()
        {
            if (!IsNPC())
            {
                return Race.RaceList[_race].StrModifier + ((PC)this).RaceStrMod + ((PC)this).MaxStrMod;
            }

            return Race.RaceList[ _race ].StrModifier;
        }

        /// <summary>
        /// Retrieve character's current intelligence.
        /// </summary>
        /// <returns></returns>
        public int GetCurrInt()
        {
            int mod = Race.RaceList[ _race ].IntModifier;
            int plus = _modifiedIntelligence;
            int max = Race.RaceList[ _race ].IntModifier;

            if( !IsNPC() )
            {
                PC pcdata = (PC)this;
                mod += pcdata.RaceIntMod;
                plus += pcdata.MaxIntMod;
                max += ( pcdata.RaceIntMod + pcdata.MaxIntMod );
            }

            return Macros.Range( 1, ( _permIntelligence * mod / 100 ) + plus, max );
        }

        /// <summary>
        /// Retrieve character's maximum intelligence.
        /// </summary>
        /// <returns></returns>
        public int GetMaxInt()
        {
            if (!IsNPC())
            {
                return Race.RaceList[_race].IntModifier + ((PC)this).RaceIntMod + ((PC)this).MaxIntMod;
            }
        
            return Race.RaceList[ _race ].IntModifier;
        }

        /// <summary>
        /// Retrieve character's current wisdom.
        /// </summary>
        /// <returns></returns>
        public int GetCurrWis()
        {
            int mod = Race.RaceList[ _race ].WisModifier;
            int plus = _modifiedWisdom;
            int max = Race.RaceList[ _race ].WisModifier;

            if( !IsNPC() )
            {
                PC pcdata = (PC)this;
                mod += pcdata.RaceWisMod;
                plus += pcdata.MaxWisMod;
                max += ( pcdata.RaceWisMod + pcdata.MaxWisMod );
            }

            return Macros.Range( 1, ( _permWisdom * mod / 100 ) + plus, max );
        }

        /// <summary>
        /// Gets or sets the character's race.
        /// </summary>
        public int RaceNumber
        {
            get { return _race; }
            set { _race = value; }
        }

        /// <summary>
        /// Retrieve character's maximum wisdom.
        /// </summary>
        /// <returns></returns>
        public int GetMaxWis()
        {
            if( !IsNPC() )
                return Race.RaceList[ _race ].WisModifier + ( (PC)this ).RaceWisMod + ( (PC)this ).MaxWisMod;

            return Race.RaceList[ _race ].WisModifier;
        }

        /// <summary>
        /// Retrieve character's current dexterity.
        /// </summary>
        /// <returns></returns>
        public int GetCurrDex()
        {
            int mod = Race.RaceList[ _race ].DexModifier;
            int plus = _modifiedDexterity;
            int max = Race.RaceList[ _race ].DexModifier;

            if( !IsNPC() )
            {
                PC pcdata = (PC)this;
                mod += pcdata.RaceDexMod;
                plus += pcdata.MaxDexMod;
                max += ( pcdata.RaceDexMod + pcdata.MaxDexMod );
            }

            return Macros.Range( 1, ( _permDexterity * mod / 100 ) + plus, max );
        }

        /// <summary>
        /// Retrieve character's maximum dexterity.
        /// </summary>
        /// <returns></returns>
        public int GetMaxDex()
        {
            if (!IsNPC())
            {
                return Race.RaceList[_race].DexModifier + ((PC)this).RaceDexMod + ((PC)this).MaxDexMod;
            }

            return Race.RaceList[ _race ].DexModifier;
        }

        /// <summary>
        /// Retrieve character's current constitution.
        /// </summary>
        /// <returns></returns>
        public int GetCurrCon()
        {
            int mod = Race.RaceList[ _race ].ConModifier;
            int plus = _modifiedConstitution;
            int max = Race.RaceList[ _race ].ConModifier;

            if( !IsNPC() )
            {
                PC pcdata = (PC)this;
                mod += pcdata.RaceConMod;
                plus += pcdata.MaxConMod;
                max += ( pcdata.RaceConMod + pcdata.MaxConMod );
            }

            return Macros.Range( 1, ( _permConstitution * mod / 100 ) + plus, max );
        }

        /// <summary>
        /// Retrieve character's maximum constitution.
        /// </summary>
        /// <returns></returns>
        public int GetMaxCon()
        {
            if( !IsNPC() )
                return Race.RaceList[ _race ].ConModifier + ( (PC)this ).RaceConMod + ( (PC)this ).MaxConMod;

            return Race.RaceList[ _race ].ConModifier;
        }

        /// <summary>
        /// Retrieve a character's current agility.
        /// </summary>
        /// <returns></returns>
        public int GetCurrAgi()
        {
            int mod = Race.RaceList[ _race ].AgiModifier;
            int plus = _modifiedAgility;
            int max = Race.RaceList[ _race ].AgiModifier;

            if( !IsNPC() )
            {
                PC pcdata = (PC)this;
                mod += pcdata.RaceAgiMod;
                plus += pcdata.MaxAgiMod;
                max += ( pcdata.RaceAgiMod + pcdata.MaxAgiMod );
            }

            return Macros.Range( 1, ( _permAgility * mod / 100 ) + plus, max );
        }

        /// <summary>
        /// Retrieve character's maximum agility.
        /// </summary>
        /// <returns></returns>
        public int GetMaxAgi()
        {
            if (!IsNPC())
            {
                return Race.RaceList[_race].AgiModifier + ((PC)this).RaceAgiMod + ((PC)this).MaxAgiMod;
            }

            return Race.RaceList[ _race ].AgiModifier;
        }

        /// <summary>
        /// Retrieve character's current charisma.
        /// </summary>
        /// <returns></returns>
        public int GetCurrCha()
        {
            int mod = Race.RaceList[ _race ].ChaModifier;
            int plus = _modifiedCharisma;
            int max = Race.RaceList[ _race ].ChaModifier;

            if( !IsNPC() )
            {
                PC pcdata = (PC)this;
                mod += pcdata.RaceChaMod;
                plus += pcdata.MaxChaMod;
                max += ( pcdata.RaceChaMod + pcdata.MaxChaMod );
            }

            return Macros.Range( 1, ( _permCharisma * mod / 100 ) + plus, max );
        }

        /// <summary>
        /// Retrieve character's maximum charisma.
        /// </summary>
        /// <returns></returns>
        public int GetMaxCha()
        {
            if (!IsNPC())
            {
                return Race.RaceList[_race].ChaModifier + ((PC)this).RaceChaMod + ((PC)this).MaxChaMod;
            }

            return Race.RaceList[ _race ].ChaModifier;
        }

        /// <summary>
        /// Retrieve character's current power.
        /// </summary>
        /// <returns></returns>
        public int GetCurrPow()
        {
            int mod = Race.RaceList[ _race ].PowModifier;
            int plus = _modifiedPower;
            int max = Race.RaceList[ _race ].PowModifier;

            if( !IsNPC() )
            {
                PC pcdata = (PC)this;
                mod += pcdata.RacePowMod;
                plus += pcdata.MaxPowMod;
                max += ( pcdata.RacePowMod + pcdata.MaxPowMod );
            }

            return Macros.Range( 1, ( _permPower * mod / 100 ) + plus, max );
        }

        /// <summary>
        /// Retrieve character's maximum power.
        /// </summary>
        /// <returns></returns>
        public int GetMaxPow()
        {
            if (!IsNPC())
            {
                return Race.RaceList[_race].PowModifier + ((PC)this).RacePowMod + ((PC)this).MaxPowMod;
            }

            return Race.RaceList[ _race ].PowModifier;
        }

        /// <summary>
        /// Retrieve character's current luck.
        /// </summary>
        /// <returns></returns>
        public int GetCurrLuck()
        {
            int mod = Race.RaceList[ _race ].LukModifier;
            int plus = _modifiedLuck;
            int max = Race.RaceList[ _race ].LukModifier;

            if( !IsNPC() )
            {
                PC pcdata = (PC)this;
                mod += pcdata.RaceLukMod;
                plus += pcdata.MaxLukMod;
                max += ( pcdata.RaceLukMod + pcdata.MaxLukMod );
            }

            return Macros.Range( 1, ( _permLuck * mod / 100 ) + plus, max );
        }

        /// <summary>
        /// Retrieve character's maximum luck.
        /// </summary>
        /// <returns></returns>
        public int GetMaxLuk()
        {
            if (!IsNPC())
            {
                return Race.RaceList[_race].LukModifier + ((PC)this).RaceLukMod + ((PC)this).MaxLukMod;
            }

            return Race.RaceList[ _race ].LukModifier;
        }

        /// <summary>
        /// Returns a characters natural race.
        /// 
        /// Used for things were we want the player's non-polymorphed race, like frags, languages, etc.
        /// </summary>
        /// <returns></returns>
        public int GetOrigRace()
        {
            int mod = 0;

            foreach (Affect affect in _affected)
            {
                foreach (AffectApplyType apply in affect.Modifiers)
                {
                    if (apply.Location == Affect.Apply.race)
                    {
                        mod += apply.Amount;
                    }
                }
            }
            return _race - mod;
        }

        /// <summary>
        /// Mob hitpoints are not based on their con *except* when they have modifiers.
        /// Farly complex math on con-modified mobs.
        /// </summary>
        /// <returns></returns>
        public int GetMaxHit()
        {
            int value;

            if( !IsNPC() )
                value = ( ( _maxHitpoints * GetCurrCon() ) / 100 ) + ( (PC)this ).HitpointModifier;
            else if( _modifiedConstitution == 0 )
                value = _maxHitpoints;
            else
                value = _maxHitpoints * ( 100 + _modifiedConstitution ) / 100;

            if( value < 1 )
                value = 1;

            return value;
        }

        /// <summary>
        /// This is a function that returns a characters *percieved* race, allowing those
        /// affected by change self, and possibly disguise/doppleganger to appear as a race
        /// other than their own without actually changing their race.
        /// </summary>
        /// <returns></returns>
        public int GetRace()
        {
            if (!IsAffected(Affect.AFFECT_CHANGE_SELF) && !IsAffected(Affect.AFFECT_POLYMORPH))
            {
                return _race;
            }

            // Check for change self
            foreach (Affect affect in _affected)
            {
                if (affect.HasBitvector(Affect.AFFECT_CHANGE_SELF) || affect.HasBitvector(Affect.AFFECT_POLYMORPH))
                {
                    foreach (AffectApplyType apply in affect.Modifiers)
                    {
                        return _race - apply.Amount;
                    }
                }
            }

            return _race;
        }

        /// <summary>
        /// Checks whether the current character is at racewar with the supplied character.
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public bool IsRacewar( CharData ch )
        {
            // Gods don't participate in racewars.
            if( IsImmortal() || ch.IsImmortal() )
            {
                return false;
            }

            // NPCs are not a part of the war.
            if( IsNPC() || ch.IsNPC() )
            {
                return false;
            }

            // We check using original race because racewars are based on the side a person is *really* on at heart.
            if( Race.RaceList[ GetOrigRace() ].WarSide != Race.RaceList[ ch.GetOrigRace() ].WarSide )
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns "A &+rDuergar&n" as opposed to "&+rDuergar".
        /// * Also "An &+bOgre&n" vs. "&+bOgre&n".
        /// </summary>
        /// <returns></returns>
        public string RaceName()
        {
            string buf = "A";
            if( MUDString.IsVowel( Race.RaceList[GetRace()].Name ) )
            {
                buf += "n ";
            }
            else
            {
                buf += " ";
            }

            buf += Race.RaceList[ GetRace() ].ColorName;

            return buf;
        }

        /// <summary>
        /// Gets the character's racewar side.
        /// </summary>
        /// <returns></returns>
        public Race.RacewarSide GetRacewarSide()
        {
            return Race.RaceList[ GetOrigRace() ].WarSide;
        }

        /// <summary>
        /// This should not normally be used outside of character creation.
        /// </summary>
        /// <param name="newrace"></param>
        public void SetPermRace( int newrace )
        {
            _race = newrace;
        }

        /// <summary>
        /// Returns amount of cash cash a player has denominated in copper pieces.
        /// </summary>
        /// <returns></returns>
        public int GetCash()
        {
            return ( _money.Copper + ( _money.Silver * 10 ) +
                    ( _money.Gold * 100 ) + ( _money.Platinum * 1000 ) );
        }

        /// <summary>
        /// Remove the specified amount of coins from the player.
        /// </summary>
        /// <param name="amount">The amount of money in copper pieces.</param>
        public void SpendCash( int amount )
        {
            if( GetCash() < amount )
            {
                Log.Error( "SpendCash(): Spending more money than player has.", 0 );
                _money.Copper = 0;
                _money.Silver = 0;
                _money.Gold = 0;
                _money.Platinum = 0;
            }

            // Note that this will automatically convert a player's coins
            // to the most efficient type to carry.
            int value = GetCash();
            _money.Copper = 0;
            _money.Silver = 0;
            _money.Gold = 0;
            _money.Platinum = 0;
            ReceiveCash( value - amount );
        }

        /// <summary>
        /// Increments characters's cash on hand by a specific amount, in copper pieces.
        /// </summary>
        /// <param name="amount">The amount to increment by in copper pieces.</param>
        public void ReceiveCash( int amount )
        {
            int number = amount / 1000;
            _money.Platinum += number;
            amount -= number * 1000;
            number = amount / 100;
            _money.Gold += number;
            amount -= number * 100;
            number = amount / 10;
            _money.Silver += number;
            amount -= number * 10;
            _money.Copper += amount;

            return;
        }

        /// <summary>
        /// Gets the amount of copper that a character has.
        /// </summary>
        /// <returns></returns>
        public int GetCopper()
        {
            return _money.Copper;
        }

        /// <summary>
        /// Gets the amounts of silver that a character has.
        /// </summary>
        /// <returns></returns>
        public int GetSilver()
        {
            return _money.Silver;
        }

        /// <summary>
        /// Gets the amount of gold that the character has.
        /// </summary>
        /// <returns></returns>
        public int GetGold()
        {
            return _money.Gold;
        }

        /// <summary>
        /// Gets the amount of platinum that the character has.
        /// </summary>
        /// <returns></returns>
        public int GetPlatinum()
        {
            return _money.Platinum;
        }

        /// <summary>
        /// This should not normally be called for a player, and is more often used for nulling
        /// coins on a mob that shouldn't have them (i.e. summoned).
        /// </summary>
        /// <param name="copper"></param>
        /// <param name="silver"></param>
        /// <param name="gold"></param>
        /// <param name="platinum"></param>
        public void SetCoins( int copper, int silver, int gold, int platinum )
        {
            _money.Copper = copper;
            _money.Silver = silver;
            _money.Gold = gold;
            _money.Platinum = platinum;
        }

        /// <summary>
        /// Increments the character's copper pieces by a specific amount.
        /// </summary>
        /// <param name="amount">The amount of copper to add.</param>
        public void ReceiveCopper( int amount )
        {
            _money.Copper += amount;
        }

        /// <summary>
        /// Increments the character's silver pieces by a specific amount.
        /// </summary>
        /// <param name="amount">The amount of silver to add.</param>
        public void ReceiveSilver( int amount )
        {
            _money.Silver += amount;
        }

        /// <summary>
        /// Increments the character's gold pieces by a specific amount.
        /// </summary>
        /// <param name="amount">The amount of gold to add.</param>
        public void ReceiveGold( int amount )
        {
            _money.Gold += amount;
        }

        /// <summary>
        /// Increments the character's platinum pieces by a specific amount.
        /// </summary>
        /// <param name="amount">The amount of platinum to add.</param>
        public void ReceivePlatinum( int amount )
        {
            _money.Platinum += amount;
        }

        /// <summary>
        /// Decrements the player's copper by a specific amount.
        /// </summary>
        /// <param name="amount">The amount of copper to remove.</param>
        public void SpendCopper( int amount )
        {
            _money.Copper -= amount;
        }

        /// <summary>
        /// Decrements the player's silver by a specific amount.
        /// </summary>
        /// <param name="amount">The amount of silver to remove.</param>
        public void SpendSilver(int amount)
        {
            _money.Silver -= amount;
        }

        /// <summary>
        /// Decrements the player's gold by a specific amount.
        /// </summary>
        /// <param name="amount">The amount of gold to remove.</param>
        public void SpendGold(int amount)
        {
            _money.Gold -= amount;
        }

        /// <summary>
        /// Decrements the player's platinum by a specific amount.
        /// </summary>
        /// <param name="amount">The amount of platinum to remove.</param>
        public void SpendPlatinum(int amount)
        {
            _money.Platinum -= amount;
        }

        /// <summary>
        /// Should not be used for loading PCs.  PCs should call PC.LoadFile();
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static CharData LoadFile( string filename )
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer( typeof( CharData ) );
                XmlTextReader xtr = new XmlTextReader(new StreamReader(filename));
                CharData data = (CharData)serializer.Deserialize( xtr );
                xtr.Close();
                // Perform any necessary data repair.
                //
                // Extend affect vectors if the number has been increased.
                if (data._affectedBy.Length < Limits.NUM_AFFECT_VECTORS)
                {
                    data.ExtendAffectData();
                }
                return data;
            }
            catch( Exception ex )
            {
                Log.Trace( "Unable to load player file: " + filename + ". This may be a new player. Exception is:" + ex );
                return null;
            }
        }

        /// <summary>
        /// Extends a character's affect vector data. Used when the number of affect
        /// vectors is increased. Allows seamless forward-versioning of XML storage.
        /// </summary>
        protected void ExtendAffectData()
        {
            if (_affectedBy.Length < Limits.NUM_AFFECT_VECTORS)
            {
                int[] oldData = _affectedBy;
                _affectedBy = new int[Limits.NUM_AFFECT_VECTORS];
                for (int i = 0; i < oldData.Length; i++)
                {
                    _affectedBy[i] = oldData[i];
                }
                // Objects also need to be extended if the number of affect vectors
                // has been extended.
                foreach (Object obj in _carrying)
                {
                    if (obj.AffectedBy.Length < Limits.NUM_AFFECT_VECTORS)
                    {
                        int[] oldObjData = obj.AffectedBy;
                        obj.AffectedBy = new int[Limits.NUM_AFFECT_VECTORS];
                        for (int i = 0; i < oldObjData.Length; i++)
                        {
                            obj.AffectedBy[i] = oldObjData[i];
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Saves the player file by serializing it to disk as XML.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public virtual bool SaveFile( string filename )
        {
            XmlTextWriter xtw = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(GetType());
                xtw = new XmlTextWriter(new StreamWriter(filename));
                serializer.Serialize(xtw, this);
                xtw.Flush();
                xtw.Close();
                return true;
            }
            catch( Exception ex )
            {
                Log.Error( "Error saving player file " + filename + " exception: " + ex );
                if (xtw != null)
                {
                    xtw.Close();
                }
                return false;
            }
        }

        /// <summary>
        /// Sets an affect bit on the character.
        /// </summary>
        /// <param name="bvect"></param>
        public void SetAffectBit(Bitvector bvect)
        {
            _affectedBy[ bvect.Group ] |= bvect.Vector;
            return;
        }

        /// <summary>
        /// Called by RemoveAffect() method.  If you want to remove an affect bit, do so
        /// through RemoveAffect().
        /// </summary>
        /// <param name="bvect"></param>
        private void RemoveAffectBit( Bitvector bvect )
        {
            _affectedBy[ bvect.Group ] &= ~( bvect.Vector );
            return;
        }

        /// <summary>
        /// Toggles an affect bit on the character.
        /// </summary>
        /// <param name="bvect"></param>
        public void ToggleAffectBit( Bitvector bvect )
        {
            _affectedBy[ bvect.Group ] ^= bvect.Vector;
            return;
        }
            
        /// <summary>
        /// Sets an action bit on the character.
        /// </summary>
        /// <param name="bvect"></param>
        public void SetActionBit(Bitvector bvect)
        {
            _actionFlags[bvect.Group] |= bvect.Vector;
            return;
        }

        /// <summary>
        /// Removes an action bit from the character.
        /// </summary>
        /// <param name="bvect"></param>
        public void RemoveActionBit(Bitvector bvect)
        {
            _actionFlags[bvect.Group] &= ~(bvect.Vector);
            return;
        }

        /// <summary>
        /// Toggles an action bit on the character.
        /// </summary>
        /// <param name="bvect"></param>
        public void ToggleActionBit(Bitvector bvect)
        {
            _actionFlags[bvect.Group] ^= bvect.Vector;
            return;
        }

        /// <summary>
        /// Checks whether the character has an action bit set.
        /// </summary>
        /// <param name="bvect"></param>
        /// <returns></returns>
        public bool HasActionBit(Bitvector bvect)
        {
            if( Macros.IsSet( _actionFlags[ bvect.Group ], bvect.Vector ) )
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets a character's sex represented as a string.
        /// </summary>
        /// <returns></returns>
        public string GetSexString()
        {
            return _sex.ToString();
        }

        /// <summary>
        /// Gets the pronoun appropriate to a character's sex.
        /// </summary>
        /// <returns></returns>
        public string GetSexPronoun()
        {
            if (_sex == MobTemplate.Sex.female)
            {
                return "she";
            }
            else if (_sex == MobTemplate.Sex.male)
            {
                return "he";
            }
            else
            {
                return "it";
            }
        }

        /// <summary>
        /// Gets the string representation for a sex value.
        /// </summary>
        /// <param name="sex"></param>
        /// <returns></returns>
        public static string GetSexString(int sex)
        {
            MobTemplate.Sex sx = (MobTemplate.Sex)sex;
            return sx.ToString();
        }

        /// <summary>
        /// Checks whether the character is an NPC.
        /// </summary>
        /// <returns></returns>
        public bool IsNPC()
        {
            return HasActionBit( MobTemplate.ACT_IS_NPC );
        }

        /// <summary>
        /// Checks whether the player's trust level is at least of immortal level and that
        /// they have valid immortal data.
        /// </summary>
        /// <returns></returns>
        public bool IsImmortal()
        {
            return ( (GetTrust() >= Limits.LEVEL_AVATAR) && (((PC)this).ImmortalData != null ));
        }

        public bool IsHero()
        {
            return ( GetTrust() >= Limits.LEVEL_HERO );
        }

        /// <summary>
        /// Is the character of good alignment?
        /// </summary>
        /// <returns></returns>
        public bool IsGood()
        {
            return ( _alignment >= 350 );
        }

        /// <summary>
        /// Is the character of neutral alignment?
        /// </summary>
        /// <returns></returns>
        public bool IsNeutral()
        {
            return ( !IsGood() && !IsEvil() );
        }

        /// <summary>
        /// Is the character of evil alignment?
        /// </summary>
        /// <returns></returns>
        public bool IsEvil()
        {
            return ( _alignment <= -350 );
        }

        /// <summary>
        /// Does the character hate the victim?
        /// </summary>
        /// <param name="victim"></param>
        /// <returns></returns>
        public bool IsHating( CharData victim )
        {
            foreach( EnemyData enemy in _hating )
            {
                if (enemy.Who == victim)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Is the character awake?
        /// </summary>
        /// <returns></returns>
        public bool IsAwake()
        {
            return ( _position > Position.sleeping );
        }

        /// <summary>
        /// Is the character outdoors?
        /// </summary>
        /// <returns></returns>
        public bool IsOutside()
        {
            if (!_inRoom)
            {
                return false;
            }

            if (_inRoom.HasFlag(RoomTemplate.ROOM_INDOORS) || _inRoom.TerrainType == TerrainType.inside ||
                _inRoom.TerrainType == TerrainType.underground_indoors)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Mobs can't ignore anyone.
        /// </summary>
        /// <param name="victim"></param>
        /// <returns>false</returns>
        public virtual bool IsIgnoring( CharData victim )
        {
            return false;
        }

        /// <summary>
        /// Mobs can't ignore anyone.
        /// </summary>
        /// <param name="victim"></param>
        public virtual void StartIgnoring( CharData victim )
        {
        }

        /// <summary>
        /// Mobs can't ignore anyone.
        /// </summary>
        /// <param name="victim"></param>
        public virtual void StopIgnoring( CharData victim )
        {
            return;
        }

        /// <summary>
        /// Mobs can't consent anyone.
        /// </summary>
        /// <param name="victim"></param>
        public virtual void StartConsenting( CharData victim )
        {
            if (IsNPC())
                return;

            if (!((PC)this).Consenting.Contains(victim))
            {
                ((PC)this).Consenting.Add(victim);
            }
        }

        /// <summary>
        /// Mobs can't consent anyone.
        /// </summary>
        /// <param name="victim"></param>
        public virtual void StopConsenting( CharData victim )
        {
            if (IsNPC())
                return;

            if (!((PC)this).Consenting.Contains(victim))
            {
                ((PC)this).Consenting.Remove(victim);
            }
        }

        /// <summary>
        /// Checks whether the character is below ground.
        /// </summary>
        /// <returns></returns>
        public bool IsUnderground()
        {
            if( !_inRoom )
            {
                return false;
            }

            if( _inRoom.TerrainType == TerrainType.underground_wild
                || _inRoom.TerrainType == TerrainType.underground_city
                || _inRoom.TerrainType == TerrainType.underground_indoors
                || _inRoom.TerrainType == TerrainType.underground_swimmable_water
                || _inRoom.TerrainType == TerrainType.underground_unswimmable_water
                || _inRoom.TerrainType == TerrainType.underground_no_ground )
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks whether the character is undead.
        /// </summary>
        /// <returns></returns>
        public bool IsUndead()
        {
            return ( GetRace() == Race.RACE_VAMPIRE || GetRace() == Race.RACE_GHOST || GetRace() == Race.RACE_DRACOLICH || GetRace() == Race.RACE_UNDEAD );
        }

        /// <summary>
        /// Checks whether the character is an elemental.
        /// </summary>
        /// <returns></returns>
        public bool IsElemental()
        {
            return ( GetRace() == Race.RACE_AIR_ELE || GetRace() == Race.RACE_FIRE_ELE || GetRace() == Race.RACE_EARTH_ELE || GetRace() == Race.RACE_WATER_ELE );
        }

        /// <summary>
        /// Checks whether the character is able to talk.
        /// </summary>
        /// <returns></returns>
        public bool CanSpeak()
        {
            if( IsAffected( Affect.AFFECT_MUTE ) || HasInnate( Race.RACE_MUTE ) )
            {
                return false;
            }
            if( _inRoom != null && _inRoom.HasFlag( RoomTemplate.ROOM_SILENT ))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Something has happened to make the character stop meditating. Make them that way and print a message.
        /// </summary>
        public void BreakMeditate()
        {
            if (!IsNPC() && HasActionBit(PC.PLAYER_MEDITATING))
            {
                SocketConnection.Act( "$n&n is disrupted from meditation.", this, null, null, SocketConnection.MessageTarget.room );
                SocketConnection.Act( "Your meditation is disrupted.", this, null, null, SocketConnection.MessageTarget.character );
                RemoveActionBit( PC.PLAYER_MEDITATING );
            }
        }

        /// <summary>
        /// Something has happened to cause the character to turn visible. Make them that way and print a message.
        /// </summary>
        public void BreakInvisibility()
        {
            
            if( IsAffected( Affect.AFFECT_INVISIBLE ) )
            {
                RemoveAffect( Affect.AFFECT_INVISIBLE );
                RemoveAffect( Affect.AFFECT_HIDE );
                RemoveAffect( Affect.AFFECT_MINOR_INVIS );
                SocketConnection.Act( "$n&n snaps into visibility.", this, null, null, SocketConnection.MessageTarget.room );
                SendText( "You snap into visibility.\r\n" );
            }
            if (HasActionBit(PC.PLAYER_WIZINVIS) && !IsImmortal())
            {
                RemoveActionBit(PC.PLAYER_WIZINVIS);
            }

        }

        /// <summary>
        /// Something has happened to interrupt the character's memorization. Make them that way and print a message.
        /// </summary>
        public void BreakMemorization()
        {
            if (!IsNPC() && HasActionBit(PC.PLAYER_MEMORIZING))
            {
                SocketConnection.Act( "$n&n abandons $s studies.", this, null, null, SocketConnection.MessageTarget.room );
                SocketConnection.Act( "You abandon your studies.", this, null, null, SocketConnection.MessageTarget.character );
                RemoveActionBit(PC.PLAYER_MEMORIZING);
            }
        }

        /// <summary>
        /// Can the character move under their own power?
        /// </summary>
        /// <returns></returns>
        public bool CanMove()
        {
            if( IsAffected( Affect.AFFECT_HOLD ) )
            {
                return false;
            }
            if( IsAffected( Affect.AFFECT_MINOR_PARA ) )
            {
                return false;
            }
            if( IsAffected( Affect.AFFECT_BOUND ) )
            {
                return false;
            }
            if (!IsAwake())
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Is the character able to fly?
        /// </summary>
        /// <returns></returns>
        public bool CanFly()
        {
            if (IsAffected(Affect.AFFECT_FLYING) || HasInnate(Race.RACE_FLY))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Is the character blind?
        /// </summary>
        /// <returns></returns>
        public bool IsBlind()
        {
            if (!IsNPC() && HasActionBit(PC.PLAYER_GODMODE))
            {
                return false;
            }

            if( IsAffected( Affect.AFFECT_BLIND ) )
            {
                SendText( "&+LYou can't see a thing!\r\n" );
                return true;
            }

            return false;
        }

        /// <summary>
        /// Does the character have free will (not charmed)?
        /// </summary>
        /// <returns></returns>
        public bool IsFreewilled()
        {
            if (HasActionBit(MobTemplate.ACT_PET) && _master)
            {
                return false;
            }
            if( IsAffected( Affect.AFFECT_CHARM ) )
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Retrieve a character's trusted level for permission checking.
        /// </summary>
        /// <returns></returns>
        public int GetTrust()
        {
            if( _socket && _socket.Original )
            {
                return _socket.Original.GetTrust();
            }

            if( _trustLevel != 0 )
            {
                return _trustLevel;
            }

            if( IsNPC() && _level >= Limits.LEVEL_HERO )
            {
                return Limits.LEVEL_HERO - 1;
            }
            return _level;
        }

        /// <summary>
        /// Retrieves a character's current hitroll for a given weapon location.
        /// </summary>
        /// <param name="weapon"></param>
        /// <returns></returns>
        public int GetHitroll( ObjTemplate.WearLocation weapon )
        {
            ObjTemplate.WearLocation otherWeapon;

            if( weapon == ObjTemplate.WearLocation.hand_one )
                otherWeapon = ObjTemplate.WearLocation.hand_two;
            else if( weapon == ObjTemplate.WearLocation.hand_two )
                otherWeapon = ObjTemplate.WearLocation.hand_one;
            else if( weapon == ObjTemplate.WearLocation.hand_three )
                otherWeapon = ObjTemplate.WearLocation.hand_one;
            else if( weapon == ObjTemplate.WearLocation.hand_four )
                otherWeapon = ObjTemplate.WearLocation.hand_one;
            else
            {
                string buf = "GetHitroll(): Invalid weapon location " + weapon + " on " + _name + ".";
                Log.Error( buf, 0 );
                return 0;
            }

            int hitr = _hitroll + StrengthModifier.Table[ GetCurrStr() ].HitModifier;

            if (_charClass.ClassNumber == CharClass.Names.monk || _charClass.ClassNumber == CharClass.Names.mystic)
            {
                int count;
                hitr += MonkStance.GetMonkStance(( (PC)this ).Stance ).HitrollModifier;
                for( count = 0; count < 5; ++count )
                {
                    if (_level >= MonkStance.GetMonkStance(((PC)this).Stance).HitPlus[count])
                        hitr++;
                }
            }

            Object wield = Object.GetEquipmentOnCharacter( this, weapon );
            Object otherWield = Object.GetEquipmentOnCharacter( this, otherWeapon );

            if (_charClass.ClassNumber == CharClass.Names.antipaladin && wield && wield.ItemType == ObjTemplate.ObjectType.weapon
                    && wield.HasFlag( ObjTemplate.ITEM_TWOHANDED ) )
                hitr += _level / 9;

            if (_charClass.ClassNumber == CharClass.Names.paladin && wield && wield.ItemType == ObjTemplate.ObjectType.weapon
                    && wield.HasFlag( ObjTemplate.ITEM_TWOHANDED ) )
                hitr += _level / 9;

            if (_charClass.ClassNumber == CharClass.Names.ranger && wield && wield.ItemType == ObjTemplate.ObjectType.weapon
                    && otherWield && otherWield.ItemType == ObjTemplate.ObjectType.weapon )
                hitr += _level / 9;

            otherWield = Object.GetEquipmentOnCharacter( this, otherWeapon );
            if( !otherWield )
            {
                return Math.Min( hitr, _level );
            }

            if( otherWield.ItemType != ObjTemplate.ObjectType.weapon )
            {
                return Math.Min( hitr, _level );
            }

            foreach( Affect aff in otherWield.ObjIndexData.Affected )
            {
                foreach (AffectApplyType apply in aff.Modifiers)
                {
                    if (apply.Location == Affect.Apply.hitroll)
                    {
                        hitr -= apply.Amount;
                    }
                }
            }
            foreach( Affect aff in otherWield.Affected )
            {
                foreach (AffectApplyType apply in aff.Modifiers)
                {
                    if (apply.Location == Affect.Apply.hitroll)
                    {
                        hitr -= apply.Amount;
                    }
                }
            }

            return Math.Min( hitr, _level );
        }

        /// <summary>
        /// Retrieves a character's current damroll for a given weapon location.
        /// </summary>
        /// <param name="weapon"></param>
        /// <returns></returns>
        public int GetDamroll( ObjTemplate.WearLocation weapon )
        {
            ObjTemplate.WearLocation otherWeapon;

            if( weapon == ObjTemplate.WearLocation.hand_one )
                otherWeapon = ObjTemplate.WearLocation.hand_two;
            else if( weapon == ObjTemplate.WearLocation.hand_two )
                otherWeapon = ObjTemplate.WearLocation.hand_one;
            else if( weapon == ObjTemplate.WearLocation.hand_three )
                otherWeapon = ObjTemplate.WearLocation.hand_one;
            else if( weapon == ObjTemplate.WearLocation.hand_four )
                otherWeapon = ObjTemplate.WearLocation.hand_one;
            else
            {
                string buf = "GetDamroll(): Invalid weapon location " + weapon + " on " + _name + ".";
                Log.Error( buf, 0 );
                return 0;
            }

            int damr = _damroll + StrengthModifier.Table[ GetCurrStr() ].DamageModifier;

            if (_charClass.ClassNumber == CharClass.Names.monk || _charClass.ClassNumber == CharClass.Names.mystic)
            {
                int count;
                damr += MonkStance.GetMonkStance(((PC)this).Stance).DamrollModifier;
                for( count = 0; count < 5; ++count )
                {
                    if (_level >= MonkStance.GetMonkStance(((PC)this).Stance).DamPlus[count])
                        damr++;
                }
            }

            Object wield = Object.GetEquipmentOnCharacter( this, weapon );

            // TODO: FIXME: BUG: Don't just cast the AttackType.DamageType.  Fix the object format so this is valid info.
            if (_charClass.ClassNumber == CharClass.Names.antipaladin && wield && wield.ItemType == ObjTemplate.ObjectType.weapon
                    && wield.HasFlag( ObjTemplate.ITEM_TWOHANDED ) && wield.Values[ 3 ] == (int)AttackType.DamageType.slash )
                damr += _level / 9;

            // TODO: FIXME: BUG: Don't just cast the AttackType.DamageType.  Fix the object format so this is valid info.
            if (_charClass.ClassNumber == CharClass.Names.paladin && wield && wield.ItemType == ObjTemplate.ObjectType.weapon
                    && wield.HasFlag( ObjTemplate.ITEM_TWOHANDED ) && wield.Values[ 3 ] == (int)AttackType.DamageType.slash )
                damr += _level / 9;

            Object otherWield = Object.GetEquipmentOnCharacter( this, otherWeapon );
            if( !otherWield )
            {
                return Math.Min( damr, _level );
            }
            if( otherWield.ItemType != ObjTemplate.ObjectType.weapon )
            {
                return Math.Min( damr, _level );
            }
            foreach( Affect aff in otherWield.ObjIndexData.Affected )
            {
                foreach (AffectApplyType apply in aff.Modifiers)
                {
                    if (apply.Location == Affect.Apply.damroll)
                        damr -= apply.Amount;
                }
            }
            foreach( Affect aff in otherWield.Affected)
            {
                foreach (AffectApplyType apply in aff.Modifiers)
                {
                    if (apply.Location == Affect.Apply.damroll)
                        damr -= apply.Amount;
                }
            }
            return Math.Min( damr, _level );
        }

        /// <summary>
        /// Retrieve a character's carry quantity capacity.
        /// </summary>
        /// <returns></returns>
        public int MaxCarryNumber()
        {
            if( !IsNPC() && _level >= Limits.LEVEL_AVATAR )
            {
                return 1000;
            }
            return GetCurrDex() / 11 + 3;
        }

        /// <summary>
        /// Retrieve a character's carry weight capacity.
        /// </summary>
        /// <returns></returns>
        public int MaxCarryWeight()
        {
            return StrengthModifier.Table[ GetCurrStr() ].CarryWeight;
        }


        /// <summary>
        /// Move a char out of a room.
        /// </summary>
        public void RemoveFromRoom()
        {
            if( !_inRoom )
            {
                Log.Error( "RemoveFromRoom(): null.", 0 );
                return;
            }

            if( !IsNPC() )
            {
                --_inRoom.Area.NumPlayers;
            }

            MoveLight( false );

            _inRoom.People.Remove( this );

            if( _riding != null )
            {
                _riding.RemoveFromRoom();
            }
            _inRoom = null;
            return;
        }

        /// <summary>
        /// Move a char into a room.
        /// </summary>
        /// <param name="targetRoom"></param>
        public void AddToRoom( Room targetRoom )
        {
            if( targetRoom == null )
            {
                Log.Error( "AddToRoom(): null target room.", 0 );
                return;
            }

            _inRoom = targetRoom;
            targetRoom.People.Insert(0,this);

            if( targetRoom.Area == null )
            {
                Log.Error( "AddToRoom(): Room " + targetRoom.IndexNumber + " not in any area!", 0 );
                return;
            }

            if( !IsNPC() )
            {
                ++targetRoom.Area.NumPlayers;
            }

            MoveLight( true );

            if( _riding )
            {
                _riding.AddToRoom( targetRoom );
            }
            return;
        }

        /// <summary>
        /// Gets the character's current armor class.
        /// </summary>
        /// <returns></returns>
        public int GetAC()
        {
            int ac = _armorPoints;

            if (!IsNPC() && _charClass.ClassNumber == CharClass.Names.monk)
            {
                ac -= ( 100 - MonkStance.GetMonkStance( ( (PC)this ).Stance ).ArmorModifier );
            }

            // Yes, people with an agility of 18 or less actually
            // have a little *better* armor class while asleep.
            // I'm not gonna worry about it.
            if( IsAwake() )
            {
                ac += AgiModifier.Table[ GetCurrAgi() ].ACModifier;
            }

            switch( _position )
            {
                case Position.sleeping:
                case Position.incapacitated:
                case Position.stunned:
                case Position.dead:
                case Position.mortally_wounded:
                    ac += 30;
                    break;
                case Position.reclining:
                    ac += 25;
                    break;
                case Position.resting:
                    ac += 20;
                    break;
                case Position.sitting:
                    ac += 15;
                    break;
                case Position.kneeling:
                    ac += 10;
                    break;
                case Position.fighting:
                case Position.standing:
                    break;
            }

            if( ac < 0 )
                ac /= 2;

            if( ac < -100 )
                ac = -100;

            return ac;
        }

        /// <summary>
        /// Sets a player's skills to minimum values when they gain a level.  Skills start at
        /// 25 and auto-increase one point per level.
        /// </summary>
        public void InitializeSkills()
        {
            if( IsNPC() )
            {
                return;
            }

            foreach (KeyValuePair<String, Skill> kvp in Skill.SkillList)
            {
                if( HasLevelForSkill(kvp.Value) )
                {
                    if (_level > 20 && !Macros.IsSet((int)Database.SystemData.ActFlags, (int)Sysdata.MudFlags.capturetheflag))
                    {
                        // Grant skill the first time.
                        if (!HasSkill(kvp.Key) || ((PC)this).SkillAptitude[kvp.Key] < 25)
                        {
                            ((PC)this).SkillAptitude[kvp.Key] = 25;
                        }
                        continue;
                    }
                    // Auto-advance skills.
                    if(((PC)this).SkillAptitude.ContainsKey(kvp.Key))
                    {
                        if (((PC)this).SkillAptitude[kvp.Key] <= 24 + _level)
                            ((PC)this).SkillAptitude[kvp.Key] = 24 + _level;
                        else if (((PC)this).SkillAptitude[kvp.Key] < Limits.MAX_SKILL_ADEPT)
                            ((PC)this).SkillAptitude[kvp.Key] += 1;
                    }
                    else
                    {
                        ((PC)this).SkillAptitude[kvp.Key] = 24 + _level;
                    }

                }
            }

            return;
        }

        /// <summary>
        /// Practice a skill by name.
        /// </summary>
        /// <param name="name"></param>
        public void PracticeSkill(string name, PracticeType practiceType)
        {
            // Can only practice a skill if it exists.
            if (Skill.SkillList.ContainsKey(name))
            {
                PracticeSkill(Skill.SkillList[name], practiceType);
            }
        }

        public void PracticeSkill(string name)
        {
            PracticeSkill(name, PracticeType.normal);
        }

        public void PraticeSkill(Skill skill)
        {
            PracticeSkill(skill, PracticeType.normal);
        }

        /// <summary>
        /// Practice a skill.  Has a random chance of improving the skill.
        /// </summary>
        /// <param name="skill"></param>
        public void PracticeSkill( Skill skill, PracticeType practiceType )
        {
            if( IsNPC() || practiceType == PracticeType.none)
            {
                return;
            }

            int aptitude = 0;
            if (((PC)this).SkillAptitude.ContainsKey(skill.Name))
            {
                aptitude = ((PC)this).SkillAptitude[skill.Name];
            }

            int chance = 5 + (GetCurrInt() / 10);
            // Modify for difficulty.  We can't do anything about 'only on success', so we assume it
            // succeeded if they made it into this function.
            switch (practiceType)
            {
                default:
                    break;
                case PracticeType.easy:
                    chance *= 2;
                    break;
                case PracticeType.difficult:
                    chance /= 2;
                    break;
            }

            // Have to be below the max and below 95% and make a successful
            // skill check and be able to have the skill.
            if (HasLevelForSkill(skill)
                    && (aptitude < Limits.MAX_SKILL_ADEPT)
                    // Cap skill gains at 26 + 2x the number of levels the char is past gaining the skill.
                    && aptitude < (26 + (2 * _level - skill.ClassAvailability[(int)_charClass.ClassNumber]))
                    && MUDMath.NumberRange( 1, 1000 ) <= ( 5 + ( GetCurrInt() / 10 ) ) )
            {
                ((PC)this).SkillAptitude[skill.Name] = aptitude + 1;
                string buf = "&+cYe feel yer skill in " + skill.Name + " improving.&n\r\n";
                SendText( buf );
            }
            return;
        }

        /// <summary>
        /// Movement function.  Handles all processing related to walking/flying/moving from one room to another.
        /// </summary>
        /// <param name="door"></param>
        public void Move( Exit.Direction door )
        {
            Exit exit;
            Room toRoom;
            Room target;
            Bitvector moved = PC.PLAYER_MOVED;
            string text;
            string text2;
            bool riding = false;
            bool disbelief = false;
            bool illusion = false;

            if (door == Exit.Direction.invalid)
            {
                return;
            }

            if( _inRoom == null )
            {
                Log.Trace( "Null from room in CharData.Move" );
                SendText("There's nowhere for you to move from - you aren't anywhere!");
                return;
            }

            // Prevents infinite move loop in maze zone when group has 2 leaders 
            if (HasActionBit(moved))
            {
                return;
            }

            if(IsAffected( Affect.AFFECT_MINOR_PARA ) || IsAffected( Affect.AFFECT_BOUND )
               || IsAffected( Affect.AFFECT_HOLD ) )
            {
                SendText( "You can't move!\r\n" );
                return;
            }

            if (_riding && (_riding.IsAffected(Affect.AFFECT_BOUND) || _riding.IsAffected(Affect.AFFECT_HOLD) ||
                _riding.IsAffected(Affect.AFFECT_MINOR_PARA)))
            {
                SendText( "Your ride can't move.\r\n" );
                return;
            }

            // More likely to go NSEW than any other dirs
            if( IsAffected( Affect.AFFECT_MISDIRECTION ) )
            {
                if (MUDMath.NumberRange(1, 2) == 1)
                {
                    door = (Exit.Direction)MUDMath.NumberRange(0, 3);
                }
                else
                {
                    door = Database.RandomDoor();
                }
            }

            Room inRoom = _inRoom;

            RemoveAffect(Affect.AFFECT_HIDE);

            if( !( exit = inRoom.GetExit(door)) || !( toRoom = Room.GetRoom(exit.IndexNumber) )
                || ( toRoom.TerrainType == TerrainType.underground_impassable )
                || ( toRoom.WorldmapTerrainType == ExtendedTerrain.EXT_ZONEMARKER )
                || ( _level < Limits.LEVEL_AVATAR
                && ( exit.HasFlag( Exit.ExitFlag.secret ) || exit.HasFlag(Exit.ExitFlag.blocked))))
            {
                SendText( "Alas, you cannot go that way.\r\n" );
                return;
            }

            if( _flyLevel != 0 && !toRoom.IsFlyable() )
            {
                SendText( "Alas, you cannot fly there.\r\n" );
                return;
            }

            // For trapped objects in the room that trigger on certain actions
            foreach( Object obj in _inRoom.Contents )
            {
                if( obj.Trap == null )
                    continue;
                if( obj.Trap.CheckTrigger( Trap.TriggerType.move ) )
                {
                    SetOffTrap( obj );
                    if (_position == Position.dead)
                    {
                        return;
                    }
                }
                else if( door != Exit.Direction.invalid )
                {
                    if (obj.Trap.CheckTrigger(Movement.TrapDirectionFlag[(int)door]))
                    {
                        SetOffTrap( obj );
                        if (_position == Position.dead)
                        {
                            return;
                        }
                    }
                }
            }

            // Being walled doesen't mean they can't go there, it just means that we
            // need to check for wall functions.
            if (!IsAffected(Affect.AFFECT_CLIMBING) && inRoom.ExitData[(int)door].HasFlag(Exit.ExitFlag.walled))
            {
                foreach( Object wall in _inRoom.Contents )
                {
                    if( wall.ItemType != ObjTemplate.ObjectType.wall || wall.Values[ 0 ] != (int)door )
                        continue;
                    if( wall.ObjIndexData.IndexNumber == StaticObjects.OBJECT_NUMBER_WALL_ILLUSION )
                    {
                        illusion = true;
                        // Test for disbelief
                        int dchance = GetCurrInt() / 2 + _level - wall.Values[ 2 ];
                        if( _fighting )
                            dchance /= 2;
                        if( GetRace() == Race.RACE_OGRE || GetRace() == Race.RACE_CENTAUR )
                            dchance /= 4;
                        if( MUDMath.NumberPercent() < dchance )
                        {
                            // Disbelief
                            SocketConnection.Act( "You disbelieve $p&n!", this, wall, null, SocketConnection.MessageTarget.character );
                            disbelief = true;
                        }
                    }

                    if( wall.Values[ 1 ] == 0 && !disbelief )
                    {
                        if( !illusion || CanSeeObj( this, wall ) )
                        {
                            SocketConnection.Act("You bump into $p&n.", this, wall, null, SocketConnection.MessageTarget.character);
                            SocketConnection.Act("$n&n bumps into $p&n.", this, wall, null, SocketConnection.MessageTarget.room);
                            if( wall.ObjIndexData.IndexNumber == StaticObjects.OBJECT_NUMBER_WALL_ICE )
                            {
                                Combat.InflictSpellDamage(this, this, MUDMath.Dice(2, (wall.Values[2] / 2)), "wall of ice", AttackType.DamageType.cold);
                                if( _position == Position.dead )
                                    return;
                            }
                            else if( wall.ObjIndexData.IndexNumber == StaticObjects.OBJECT_NUMBER_WALL_FORCE )
                            {
                                _currentMoves -= MUDMath.Dice( 2, 15 );
                                SendText( "You feel drained of energy!\r\n" );
                                SendText( "The force of the wall knocks you down!\r\n" );
                                _position = Position.sitting;
                                WaitState( MUDMath.Dice( 2, 9 ) );
                            }
                        } //end if !illusion
                        else
                        {
                            SendText( "Alas, you cannot go that way.\r\n" );
                        }
                        return;
                    }
                    if( disbelief )
                    {
                        SocketConnection.Act(StringConversion.WallDecayString(StaticObjects.OBJECT_NUMBER_WALL_ILLUSION), this, wall, null, SocketConnection.MessageTarget.all);
                        wall.RemoveFromWorld();
                        inRoom.ExitData[ (int)door ].RemoveFlag( Exit.ExitFlag.walled );
                    }
                    else if( !illusion )
                    {
                        SocketConnection.Act("You just walked through $p&n.", this, wall, null, SocketConnection.MessageTarget.character);
                        SocketConnection.Act("$n&n just walked through $p&n.", this, wall, null, SocketConnection.MessageTarget.room);
                        if( wall.ObjIndexData.IndexNumber == StaticObjects.OBJECT_NUMBER_WALL_FIRE )
                        {
                            Combat.InflictSpellDamage(this, this, MUDMath.Dice(wall.Values[2], 5), "wall of fire", AttackType.DamageType.fire);
                            if( _position == Position.dead )
                                return;
                        }
                        else if( wall.ObjIndexData.IndexNumber == StaticObjects.OBJECT_NUMBER_WALL_SPARKS )
                        {
                            Combat.InflictSpellDamage(this, this, MUDMath.Dice(3, wall.Values[2]), "wall of sparks", AttackType.DamageType.fire);
                            if( _position == Position.dead )
                                return;
                        }
                        else if( wall.ObjIndexData.IndexNumber == StaticObjects.OBJECT_NUMBER_LIGHTNING_CURTAIN )
                        {
                            Combat.InflictSpellDamage(this, this, MUDMath.Dice(wall.Values[2], 5), "lightning curtain", AttackType.DamageType.electricity);
                            if( _position == Position.dead )
                                return;
                        }
                        if( wall.ObjIndexData.IndexNumber == StaticObjects.OBJECT_NUMBER_WALL_MIST )
                        {
                            if (!Magic.SpellSavingThrow(wall.Values[2], this, AttackType.DamageType.poison))
                            {
                                Affect af = new Affect(Affect.AffectType.spell, "poison", wall.Values[2], Affect.Apply.strength, (0 - 2), Affect.AFFECT_POISON);
                                CombineAffect(af);
                                SendText( "&+GYou don't feel very well.&n\r\n" );
                            }
                            Combat.InflictSpellDamage(this, this, MUDMath.Dice(4, wall.Values[2]), "wall of mist", AttackType.DamageType.poison);
                            if( _position == Position.dead )
                                return;
                        }
                    }
                }
            }

            if( exit.HasFlag( Exit.ExitFlag.closed ) )
            {
                if( _riding )
                {
                    if( !_riding.IsAffected( Affect.AFFECT_PASS_DOOR )
                            && !HasInnate( Race.RACE_PASSDOOR )
                            && !_riding.IsImmortal() )
                    {
                        SocketConnection.Act("The $d is closed so your mount is unable to pass.", this, null, exit.Keyword, SocketConnection.MessageTarget.character);
                        return;
                    }

                    if( exit.HasFlag( Exit.ExitFlag.passproof )
                            && !_riding.IsImmortal() )
                    {
                        SocketConnection.Act( "Your mount is unable to pass through the $d.  Ouch!",
                             this, null, exit.Keyword, SocketConnection.MessageTarget.character);
                        return;
                    }
                }
                else
                {
                    if( !IsAffected( Affect.AFFECT_PASS_DOOR )
                            && !HasInnate( Race.RACE_PASSDOOR )
                            && !IsImmortal() )
                    {
                        SocketConnection.Act("The $d is closed.", this, null, exit.Keyword, SocketConnection.MessageTarget.character);
                        return;
                    }

                    if (exit.HasFlag(Exit.ExitFlag.passproof)
                            && !IsImmortal() )
                    {
                        SocketConnection.Act( "You are unable to pass through the $d.  Ouch!",
                             this, null, exit.Keyword, SocketConnection.MessageTarget.character);
                        return;
                    }
                }
            }

            if( _riding && _riding._position < Position.standing )
            {
                SendText( "Your ride doesn't want to move right now.\r\n" );
                return;
            }

            if( toRoom.IsPrivate() )
            {
                SendText( "That room is private right now.\r\n" );
                return;
            }

            // Race.RACE_SWIM means you're an aquatic race... and fish can't walk on land.
            if( _riding )
            {
                if( !toRoom.IsWater() && _riding.HasInnate( Race.RACE_SWIM ) )
                {
                    SendText( "Your mount flaps around but can't move!\r\n" );
                    return;
                }
            }
            else
            {
                if( !toRoom.IsWater() && HasInnate( Race.RACE_SWIM ) )
                {
                    SendText( "You flap around but you can't move!\r\n" );
                    return;
                }
            }

            // Allow all mobs to go wherever they want? Not allowing them to would really
            // screw up the track code, but that's probably all screwed up already anyhow.
            if( !IsNPC() )
            {

                if( ( _inRoom.IsMidair() && _inRoom.FallChance == 0 ) ||
                    ( toRoom.IsMidair() && toRoom.FallChance == 0 ) )
                {
                    if( _inRoom.FallChance != 0 )
                    {
                        Event.CheckFall(_inRoom, toRoom, this);
                    }
                    if( _riding )
                    {
                        if( !_riding.CanFly() && door != Exit.Direction.down )
                        {
                            SendText( "Your mount can't fly.\r\n" );
                            return;
                        }
                    }
                    else
                    {
                        // Changed to allow going down always in an air sector.
                        if( !CanFly() && door != Exit.Direction.down )
                        {
                            SendText( "You can't fly.\r\n" );
                            return;
                        }
                    }
                }

                // Water terrain code, check for boats and swimming.  Flyers don't need to swim
                if( ( ( _riding && !_riding.CanFly() ) || ( !_riding && !CanFly() ) )
                        && ( toRoom.IsWater() || inRoom.IsWater() ) )
                {
                    bool found = false;

                    if( _riding && !_riding.IsNPC() )
                    {
                        if( ( (PC)_riding ).SkillAptitude[ "swim" ] != 0 )
                        {
                            found = true;
                            _riding.PracticeSkill( "swim" );
                        }
                    }
                    else if( !IsNPC() )
                    {
                        if (((PC)this).SkillAptitude["swim"] != 0)
                        {
                            found = true;
                            PracticeSkill( "swim" );
                        }
                    }

                    // Check for a boat first, uses less effort than swimming.
                    foreach( Object obj in _carrying )
                    {
                        if( obj.ItemType == ObjTemplate.ObjectType.boat )
                        {
                            found = true;
                            break;
                        }
                    }

                    if( _riding != null && ( _riding.HasInnate( Race.RACE_WATERWALK )
                        || _riding.HasInnate( Race.RACE_SWIM ) ) )
                    {
                        found = true;
                    }

                    if( HasInnate( Race.RACE_WATERWALK ) || HasInnate( Race.RACE_SWIM ) )
                    {
                        found = true;
                    }

                    if( !found )
                    {
                        SendText( "You can't go there without a boat.\r\n" );
                        return;
                    }
                }

                if( _riding != null )
                {
                    if( ( inRoom.TerrainType == TerrainType.underwater_has_ground
                            || toRoom.TerrainType == TerrainType.underwater_has_ground )
                            && !_riding.HasInnate( Race.RACE_SWIM )
                            && ( !_riding.IsNPC() && ((PC)this).SkillAptitude["swim"] == 0))
                    {
                        SendText( "Your mount needs to be able to swim better to go there.\r\n" );
                        return;
                    }
                }
                else
                {
                    if( ( inRoom.TerrainType == TerrainType.underwater_has_ground
                            || toRoom.TerrainType == TerrainType.underwater_has_ground )
                            && !HasInnate( Race.RACE_SWIM ) && ((PC)this).SkillAptitude["swim"] == 0)
                    {
                        SendText( "You need to be able to swim better to go there.\r\n" );
                        PracticeSkill( "swim" );
                        return;
                    }
                    else if( (toRoom.TerrainType == TerrainType.ocean || inRoom.TerrainType == TerrainType.ocean ||
                        toRoom.TerrainType == TerrainType.underground_ocean || inRoom.TerrainType == TerrainType.underground_ocean ) &&
                        (!IsImmortal() || !HasActionBit(PC.PLAYER_GODMODE)))
                    {
                        SendText("The ocean is much too turbulent for your swimming ability.\r\n");
                        return;
                    }
                }

                int move;
                // Half of move loss comes from crossing the current terrain and half comes from crossing the destination terrain
                // since they're technically moving from midpoint to midpoint.
                if ((int)inRoom.TerrainType < RoomTemplate.TERRAIN_MAX && (int)toRoom.TerrainType < RoomTemplate.TERRAIN_MAX )
                {
                    move = ( Movement.MovementLoss[ inRoom.TerrainType ] + Movement.MovementLoss[ toRoom.TerrainType ] ) / 2;
                    int swimAbility = _level * 2 + 15;
                    if( !IsNPC() && HasSkill("swim"))
                    {
                        swimAbility = ((PC)this).SkillAptitude["swim"];
                    }
                    if (toRoom.IsWater())
                    {
                        // Movement loss can increase by up to 95% based on swim skill.
                        move += (move * ((Limits.MAX_SKILL_ADEPT - swimAbility)) / 100);
                    }
                    if (_inRoom.IsWater())
                    {
                        // Movement loss can increase by up to 95% based on swim skill.
                        move += (move * ((Limits.MAX_SKILL_ADEPT - swimAbility)) / 100);
                    }
                }
                else
                {
                    move = 4;
                }

                // Flying persons lose constant minimum movement.
                if (CanFly() || IsAffected(Affect.AFFECT_LEVITATE))
                {
                    move = 1;
                }

                if ((_riding))
                {
                    if (this != _riding._rider)
                    {
                        Log.Error(String.Format( "{0} riding {1}, who is not mounted!", _name, _riding._name ) );
                    }
                    else
                    {
                        riding = true;
                    }
                }

                if( ( _currentMoves < move ) && ( !IsImmortal() ) )
                {
                    SendText( "You are too tired to move another step.\r\n" );
                    if( ( _rider ) && riding )
                    {
                        _rider.SendText( "Your mount is too tired to carry you there.\r\n" );
                    }
                    return;
                }

                WaitState( 1 );
                _currentMoves -= move;

                if( ( _rider ) && riding )
                {
                }
            }

            if (!CheckSneak() && !HasActionBit(PC.PLAYER_WIZINVIS) && !IsAffected( Affect.AFFECT_IS_FLEEING ) )
            {
                if( ( ( inRoom.TerrainType == TerrainType.swimmable_water )
                    || ( inRoom.TerrainType == TerrainType.underwater_has_ground ) )
                    && ( ( toRoom.TerrainType == TerrainType.swimmable_water )
                    || ( toRoom.TerrainType == TerrainType.underwater_has_ground ) ) )
                {
                    SocketConnection.Act("$n&n swims $T.", this, null, door.ToString(), SocketConnection.MessageTarget.room);
                }
                else
                {
                    foreach( CharData watchCh in _inRoom.People )
                    {
                        if (watchCh == this || watchCh._flyLevel != _flyLevel)
                            continue;
                        Visibility visibility = Look.HowSee(watchCh, this);
                        switch( visibility )
                        {
                            case Visibility.visible:
                                if( !_riding )
                                {
                                    text2 = String.Format( "{0}&n {1} {2}.\r\n", ShowNameTo( watchCh, true ),
                                            ( _flyLevel != 0 ) ? "flies" :
                                            Race.RaceList[ GetRace() ].WalkMessage, door.ToString() );
                                    watchCh.SendText( text2 );
                                }
                                else
                                {
                                    text2 = String.Format( "{0}&n rides {1}&n {2}.\r\n", ShowNameTo( watchCh, true ),
                                            _riding.ShowNameTo(watchCh, false), door.ToString() );
                                    watchCh.SendText( text2 );
                                }
                                break;
                            case Visibility.sense_infravision:
                                text2 = String.Format( "&+LA &n&+rred shape &+Lleaves {0} in the &+Wd&n&+war&+Lkness.\r\n&n",
                                          door.ToString());
                                watchCh.SendText( text2 );
                                break;
                            case Visibility.invisible:
                                if( _riding )
                                {
                                    if (Look.HowSee(watchCh, _riding) == Visibility.visible)
                                    {
                                        text2 = String.Format( "{0}&n {1} {2}.\r\n",
                                                  _riding.ShowNameTo(watchCh, true),
                                                  ( _flyLevel != 0 ) ? "flies" :
                                      Race.RaceList[ GetRace() ].WalkMessage,
                                                  door.ToString() );
                                        watchCh.SendText( text2 );
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            // Check for guild golems
            if( inRoom.HasFlag( RoomTemplate.ROOM_GUILDROOM ) )
            {
                int chId = 0;
                Guild.Rank chRank = 0;
                // Find a golem
                foreach( CharData roomCharacter in _inRoom.People )
                {
                    if( !roomCharacter.IsNPC() && !MUDString.NameContainedIn( "_guildgolem_", roomCharacter._name ) )
                        continue;
                    int id = Guild.GolemGuildID( roomCharacter );
                    Exit.Direction dir = Movement.GolemGuardDirection( roomCharacter );
                    if( id > 0 && dir > Exit.Direction.invalid )
                    {
                        // We have a golem guarding an exit
                        if (!IsNPC() && ((PC)this).GuildMembership != null)
                        {
                            chId = ((PC)this).GuildMembership.ID;
                            chRank = ((PC)this).GuildRank;
                        }
                        if( dir == door && ( id != chId || chRank < Guild.Rank.parole ) && _position > Position.reclining )
                        {
                            SocketConnection.Act("$N&n roughly shoves you to the floor as you attempt to pass.", this, null, roomCharacter, SocketConnection.MessageTarget.character);
                            SocketConnection.Act("$N&n shoves $n&n to the floor.", this, null, roomCharacter, SocketConnection.MessageTarget.room);
                            _position = Position.reclining;
                            return;
                        }
                        if (dir == door && ((PC)this).GuildRank >= Guild.Rank.officer)
                        {
                            SocketConnection.Act("$N&n salutes smartly as you pass.", this, null, roomCharacter, SocketConnection.MessageTarget.character);
                            SocketConnection.Act("$N&n salutes at $n&n.", this, null, roomCharacter, SocketConnection.MessageTarget.room);
                        }
                        else if( dir == door )
                        {
                            SocketConnection.Act("$N&n nods as you pass.", this, null, roomCharacter, SocketConnection.MessageTarget.character);
                            SocketConnection.Act("$N&n nods at $n&n.", this, null, roomCharacter, SocketConnection.MessageTarget.room);
                        }
                    } //end if
                } ///end for
            } // end guild golems

            RemoveFromRoom();
            AddToRoom( toRoom );

            if( IsAffected( Affect.AFFECT_IS_FLEEING ) )
            {
                text = String.Format( "move_char: {0} has fled to room {1}.", _name, _inRoom.IndexNumber );
                ImmortalChat.SendImmortalChat( null, ImmortalChat.IMMTALK_SPAM, 0, text );
            }

            if( !CheckSneak() )
            {
                if( _riding )
                {
                    text = String.Format( "$n&n enters from {0}, riding $N&n.", Exit.ReverseDirectionName[ (int)door ] );
                    SocketConnection.Act(text, this, null, _riding, SocketConnection.MessageTarget.room);
                }
                else if( !_rider )
                {
                    // Changed: Don't show invis people entering room.
                    foreach( CharData watchCh in _inRoom.People )
                    {
                        Visibility sight;
                        if ((sight = Look.HowSee(watchCh, this)) != 0 && watchCh != this
                                && watchCh._flyLevel == _flyLevel )
                        {
                            // If visible to char (& not char), show msg.
                            if( sight == Visibility.visible )
                            {
                                // If they can see the person enter clearly..
                                text2 = String.Format( "{0}&n {1} in from {2}.\r\n", ShowNameTo( watchCh, true ),
                                        ( _flyLevel != 0 ) ? "flies" : Race.RaceList[ GetRace() ].WalkMessage,
                                        Exit.ReverseDirectionName[(int)door]);
                                watchCh.SendText( text2 );
                            }
                            else if( sight == Visibility.sense_infravision )
                            {
                                // Otherwise, show a vague message..
                                text2 = String.Format( "&+LA &n&+rred shape &+Lenters from {0}.\r\n&n",
                                          Exit.ReverseDirectionName[(int)door]);
                                watchCh.SendText( text2 );
                            }
                        }
                    }
                }
            }

            /* Because of the addition of the deleted flag, we can do this  */
            if( !IsImmortal() && GetRace() == Race.RACE_VAMPIRE
                    && toRoom.TerrainType == TerrainType.underwater_has_ground )
            {
                SendText( "Arrgh!  Large body of water!\r\n" );
                SocketConnection.Act("$n&n thrashes underwater!", this, null, null, SocketConnection.MessageTarget.room);
                Combat.InflictDamage(this, this, 20, String.Empty, ObjTemplate.WearLocation.none, AttackType.DamageType.drowning);
            }
            else if( !IsImmortal()
                      && ( toRoom.TerrainType == TerrainType.underwater_has_ground
                           && !IsAffected( Affect.AFFECT_BREATHE_UNDERWATER )
                           && !HasInnate( Race.RACE_WATERBREATH ) ) )
            {
                SendText( "You can't breathe!\r\n" );
                SocketConnection.Act("$n&n sputters and chokes!", this, null, null, SocketConnection.MessageTarget.room);
                Combat.InflictDamage(this, this, 2, String.Empty, ObjTemplate.WearLocation.none, AttackType.DamageType.drowning);
            }

            //* Why have mobiles see the room? 
            if( _socket != null )
            {
                CommandType.Interpret(this, "look auto");
            }

            SetActionBit(moved );

            // Handle following.
            for( int i = _inRoom.People.Count -1; i >= 0; i--)
            {
                CharData followChar = _inRoom.People[i];
                if (followChar._master == this && followChar._position == Position.standing && followChar._wait == 0
                        && followChar._flyLevel == _flyLevel && CanSee(followChar, this))
                {
                    SocketConnection.Act("You follow $N&n.", followChar, null, this, SocketConnection.MessageTarget.character);
                    followChar.Move(door);
                }
                else if (followChar == _rider)
                {
                    followChar.Move(door);
                }
            }

            RemoveActionBit(moved);

            // Okay, now that they're in to room, check to see if they've just invaded a
            // hometown
            Crime.CheckInvader(this);

            // Check to see if a person falls out of this room
            if( _inRoom.ExitData[ 5 ] && ( target = Room.GetRoom(_inRoom.ExitData[ 5 ].IndexNumber) ) )
            {
                if( _inRoom.TerrainType == TerrainType.air ||
                        _inRoom.TerrainType == TerrainType.plane_of_air ||
                        _inRoom.TerrainType == TerrainType.underground_no_ground ||
                        _inRoom.FallChance != 0 )
                    Event.CheckFall(_inRoom, target, this);
            }

            //    prog_entry_trigger( ch );
            //    prog_greet_trigger( ch );
            return;
        }

        /// <summary>
        /// Remove an object from a wear location.
        /// </summary>
        /// <param name="iWear"></param>
        /// <param name="replaceExisting"></param>
        /// <returns></returns>
        public bool RemoveObject(ObjTemplate.WearLocation iWear, bool replaceExisting)
        {
            Object obj;

            if( ( obj = Object.GetEquipmentOnCharacter( this, iWear ) ) == null )
                return true;

            if( !replaceExisting )
                return false;

            if( obj.HasFlag( ObjTemplate.ITEM_NODROP ) )
            {
                SocketConnection.Act( "Try as you might, you can't remove $p&n.", this, obj, null, SocketConnection.MessageTarget.character );
                return false;
            }

            UnequipObject( obj );
            SocketConnection.Act( "$n&n stops using $p&n.", this, obj, null, SocketConnection.MessageTarget.room );
            SocketConnection.Act( "You remove $p&n.", this, obj, null, SocketConnection.MessageTarget.character );
            return true;
        }

        /// <summary>
        /// Unequip an object from a char.
        /// </summary>
        /// <param name="obj"></param>
        public void UnequipObject( Object obj )
        {
            if (obj == null) return;

            int aff;

            if( obj.WearLocation == ObjTemplate.WearLocation.none )
            {
                string buf = String.Format( "Unequip_char: {0} already unequipped with {1}.",
                                            _name, obj.ObjIndexData.IndexNumber );
                Log.Error( buf, 0 );
                return;
            }

            _armorPoints += Object.GetArmorClassModifer( obj, obj.WearLocation );
            obj.WearLocation = ObjTemplate.WearLocation.none;
            _carryNumber++;

            foreach (Affect affect in obj.ObjIndexData.Affected)
            {
                ApplyAffectModifiers(affect, false);
            }
            foreach (Affect affect in obj.Affected)
            {
                ApplyAffectModifiers(affect, false);
            }

            if (obj.ItemType == ObjTemplate.ObjectType.light
                    && obj.Values[2] != 0 && _inRoom && _inRoom.Light > 0)
            {
                --_inRoom.Light;
            }

            for( aff = 0; aff < Limits.NUM_AFFECT_VECTORS; aff++ )
            {
                /* Remove all affects of object being removed. */
                _affectedBy[ aff ] = _affectedBy[ aff ] & ~obj.AffectedBy[ aff ];
                /* Add any affects that are on the object being removed and
                another object that is worn. */
                foreach( Object carryObject in _carrying )
                {
                    if (carryObject.WearLocation != ObjTemplate.WearLocation.none)
                    {
                        _affectedBy[aff] = _affectedBy[aff] | carryObject.AffectedBy[aff];
                    }
                }
            }

            if (!IsNPC() && _socket._terminalType == SocketConnection.TerminalType.TERMINAL_ENHANCED)
            {
                Command.Equipment(this, new string[] { "" });
            }

            return;
        }

        /// <summary>
        /// Initializes newbie spells to their base learned values.
        /// </summary>
        public void InitializeSpells()
        {
            if( IsNPC() )
            {
                return;
            }

            foreach (KeyValuePair<String, Spell> kvp in Spell.SpellList)
            {
                if (kvp.Value.SpellCircle[(int)_charClass.ClassNumber] == 1
                        && (!((PC)this).SpellAptitude.ContainsKey(kvp.Key) ||
                           ((PC)this).SpellAptitude[kvp.Key] < (Limits.BASE_SPELL_ADEPT + 5)))
                {
                    ((PC)this).SpellAptitude[kvp.Key] = (Limits.BASE_SPELL_ADEPT + 5);
                }
            }

            return;
        }

        /// <summary>
        /// Checks whether the skill level of the specified spell should increase (due to use).
        /// </summary>
        /// <param name="spell"></param>
        public void PracticeSpell( Spell spell )
        {
            if( IsNPC() || !((PC)this).SpellAptitude.ContainsKey(spell.Name) )
            {
                return;
            }

            // Have to be below the max and below 95% and make a successful
            // spell check and be able to have the spell.
            if (((PC)this).SpellAptitude[spell.Name] < Limits.MAX_SPELL_ADEPT && MUDMath.NumberRange(1, 1000) <= (10 + (GetCurrInt() / 5)))
            {
                ((PC)this).SpellAptitude[spell.Name] = ((PC)this).SpellAptitude[spell.Name] + 1;
                SendText( "&+cYe feel yer knowledge of " + spell.Name + " improving.&n\r\n" );
            }

            return;
        }

        /// <summary>
        /// Initializes a new bard's songs to base values.
        /// </summary>
        public void InitializeSongs()
        {
            if( IsNPC() )
            {
                return;
            }

            foreach (KeyValuePair<String, Song> kvp in Database.SongList)
            {
                if (kvp.Value._spellCircle[(int)_charClass.ClassNumber] == 1
                        && ((PC)this).SongAptitude[kvp.Key] < (Limits.BASE_SPELL_ADEPT + 5))
                    ((PC)this).SongAptitude[kvp.Key] = (Limits.BASE_SPELL_ADEPT + 5);
            }
            return;
        }

        public void PracticeSong( String song )
        {
            if( IsNPC() )
            {
                return;
            }

            // Have to be below the max and below 95% and make a successful
            // song check and be able to have the song.
            if (((PC)this).SongAptitude[song] < Limits.MAX_SPELL_ADEPT && MUDMath.NumberRange(1, 1000) <= (10 + (GetCurrInt() / 2)))
            {
                ((PC)this).SongAptitude[song] = ((PC)this).SongAptitude[song] + 1;
                string buf = "&+cYe feel yer knowledge of " + song + " improving.&n\r\n";
                SendText( buf );
            }
            return;
        }

        public void PracticeLanguage( Race.Language lang )
        {
            if( IsNPC() )
            {
                return;
            }

            // Have to be below 100 and above 0 and make a successful
            // lang check and be able to learn the lang.
            if( ( (PC)this ).LanguageAptitude[ (int)lang ] < 100 && ( (PC)this ).LanguageAptitude[ (int)lang ] > 0
                    && MUDMath.NumberRange( 1, 1000 ) <= ( 5 + GetCurrInt() ) )
            {
                ( (PC)this ).LanguageAptitude[ (int)lang ]++;
                string buf = "&+cYe feel yer knowledge of " + Race.LanguageTable[ (int)lang ] + " improving.&n\r\n";
                if( ( (PC)this ).LanguageAptitude[ (int)lang ] >= 20 && IsImmortal() )
                {
                    SendText( buf );
                }
            }

            return;
        }

        /// <summary>
        /// Checks whether a character is high enough level to have access to a particular skill.
        /// Since skills are class-based, it uses a check of the player's class to get the answer.
        /// 
        /// If any of the CharClass items used in this call become private we'll have to rewrite
        /// this _function to forward the request.
        /// </summary>
        /// <param name="skill">The skill number.</param>
        /// <returns></returns>
        private bool HasLevelForSkill(Skill skill)
        {
            if (skill == null) return false;
            return HasLevelForSkill(skill.Name);
        }

        /// <summary>
        /// A slightly simplified version of the spell-based HasLevelForSkill() _function.  This should probably
        /// not be used until we can guarantee that skill names are reliable.  It skips the reference to
        /// the skill table so is infinitesimally more efficient.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool HasLevelForSkill( string name )
        {
            if (_level >= GetLevelForSkill(name))
            {
                return true;
            }

            return false;            
        }

        /// <summary>
        /// Gets the level at which the player gets a certain skill, if any.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private int GetLevelForSkill(string name)
        {
            foreach (SkillEntry se in _charClass.Skills)
            {
                if (se.Name == name)
                {
                    return se.Level;
                }
            }

            return Limits.LEVEL_AVATAR;
        }

        /// <summary>
        /// Does the character have the necessary skill and is it trained at least to level 1?
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool HasSkill(string name)
        {
            if (!HasLevelForSkill(name))
            {
                return false;
            }
            else if (IsNPC())
            {
                return true;
            }

            if (!((PC)this).SkillAptitude.ContainsKey(name))
            {
                return false;
            }

            if (((PC)this).SkillAptitude[name] < 1)
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// Checks whether a character is high enough level to have access to a particular spell.
        /// Since spells are class-based, it uses a check of the player's class to get the answer.
        /// 
        /// If any of the CharClass items used in this call become private we'll have to rewrite
        /// this _function to forward the request.
        /// </summary>
        /// <param name="spell"></param>
        /// <returns></returns>
        private bool HasLevelForSpell( string name )
        {
            if (!Spell.SpellList.ContainsKey(name))
                return false;

            Spell spell = Spell.SpellList[name];
                return HasLevelForSpell(spell);
        }

        /// <summary>
        /// A slightly simplified version of the spell-based HasLevelForSpell() _function.  This should probably
        /// not be used until we can guarantee that spell names are reliable.  It skips the reference to
        /// the spell table so is infinitesimally more efficient.
        /// </summary>
        /// <param name="spell"></param>
        /// <returns></returns>
        private bool HasLevelForSpell(Spell spell)
        {
            foreach (SpellEntry se in _charClass.Spells)
            {
                if( se.Name == spell.Name )
                {
                    if( _level >= (se.Circle * 4 - 3) )
                    {
                        return true;
                    }
                    return false;
                }
            }

            return false;
        }

        public bool HasSpell(Spell Spell)
        {
            if (!HasLevelForSpell(Spell))
            {
                return false;
            }
            else if (IsNPC())
            {
                return true;
            }

            if (!((PC)this).SpellAptitude.ContainsKey(Spell.Name))
            {
                return false;
            }

            if (((PC)this).SpellAptitude[Spell.Name] < 1)
            {
                return false;
            }

            return true;
        }

        public bool HasSpell(string name)
        {
            if (!Spell.SpellList.ContainsKey(name))
                return false;

            Spell spell = Spell.SpellList[name];
            return HasSpell(spell);
        }

        /// <summary>
        /// Adds PC's languages.
        /// </summary>
        public void InitializeLanguages()
        {
            if( IsNPC() )
            {
                Log.Error( "InitializeLanguages() called with NPC _targetType!", 0 );
                return;
            }

            PC ch = (PC)this;

            /* Everybody knows their own language. */
            ( ch ).LanguageAptitude[ (int)Race.RaceList[ ch.GetOrigRace() ].PrimaryLanguage ] = 100;
            ( ch ).Speaking = Race.RaceList[ ch.GetOrigRace() ].PrimaryLanguage;

            if( GetRacewarSide() == Race.RacewarSide.evil )
            {
                if( Race.RaceList[ ch.GetOrigRace() ].PrimaryLanguage != Race.Language.orcish )
                    ( ch ).LanguageAptitude[ (int)Race.Language.orcish ] =
                        Macros.Range( 1, ( ch.GetCurrInt() * 3 ) / 2 + 60, 100 );
                if( Race.RaceList[ ch.GetOrigRace() ].PrimaryLanguage != Race.Language.ogre )
                    ( ch ).LanguageAptitude[ (int)Race.Language.ogre ] =
                        Macros.Range( 1, ch.GetCurrInt() / 2 - 20, 100 );
                if( Race.RaceList[ ch.GetOrigRace() ].PrimaryLanguage != Race.Language.troll )
                    ( ch ).LanguageAptitude[ (int)Race.Language.troll ] =
                        Macros.Range( 1, ch.GetCurrInt() / 3 - 10, 100 );
                if( Race.RaceList[ ch.GetOrigRace() ].PrimaryLanguage != Race.Language.neogi )
                    ( ch ).LanguageAptitude[ (int)Race.Language.neogi ] =
                        Macros.Range( 0, ch.GetCurrInt() / 10 - 5, 100 );
                if( Race.RaceList[ ch.GetOrigRace() ].PrimaryLanguage != Race.Language.goblin )
                    ( ch ).LanguageAptitude[ (int)Race.Language.goblin ] =
                        Macros.Range( 1, ch.GetCurrInt() / 4 - 5, 100 );
                if( Race.RaceList[ ch.GetOrigRace() ].PrimaryLanguage != Race.Language.githyanki )
                    ( ch ).LanguageAptitude[ (int)Race.Language.githyanki ] =
                        Macros.Range( 1, ch.GetCurrInt() / 4 - 10, 100 );
                if( Race.RaceList[ ch.GetOrigRace() ].PrimaryLanguage != Race.Language.drow )
                    ( ch ).LanguageAptitude[ (int)Race.Language.drow ] =
                        Macros.Range( 1, ch.GetCurrInt() / 5 + 5, 100 );
                if( Race.RaceList[ ch.GetOrigRace() ].PrimaryLanguage != Race.Language.kobold )
                    ( ch ).LanguageAptitude[ (int)Race.Language.kobold ] =
                        Macros.Range( 1, ch.GetCurrInt() / 2 - 40, 100 );
                if( Race.RaceList[ ch.GetOrigRace() ].PrimaryLanguage != Race.Language.duergar )
                    ( ch ).LanguageAptitude[ (int)Race.Language.duergar ] =
                        Macros.Range( 1, ch.GetCurrInt() / 4 - 5, 100 );
                if( Race.RaceList[ ch.GetOrigRace() ].PrimaryLanguage != Race.Language.rakshasa )
                    ( ch ).LanguageAptitude[ (int)Race.Language.rakshasa ] =
                        Macros.Range( 1, ch.GetCurrInt() / 4 - 10, 100 );
                if( Race.RaceList[ ch.GetOrigRace() ].PrimaryLanguage != Race.Language.gnoll )
                    ( ch ).LanguageAptitude[ (int)Race.Language.gnoll ] =
                        Macros.Range( 1, ch.GetCurrInt() / 4 - 15, 100 );
                if( Race.RaceList[ ch.GetOrigRace() ].PrimaryLanguage != Race.Language.minotaur )
                    ( ch ).LanguageAptitude[ (int)Race.Language.minotaur ] =
                        Macros.Range( 1, ch.GetCurrInt() / 2 - 40, 100 );
            }
            else if( GetRacewarSide() == Race.RacewarSide.good )
            {
                if( Race.RaceList[ ch.GetOrigRace() ].PrimaryLanguage != Race.Language.common )
                    ( ch ).LanguageAptitude[ (int)Race.Language.common ] =
                        Macros.Range( 1, ( ch.GetCurrInt() * 3 ) / 2 + 60, 100 );
                if( Race.RaceList[ ch.GetOrigRace() ].PrimaryLanguage != Race.Language.elven )
                    ( ch ).LanguageAptitude[ (int)Race.Language.elven ] =
                        Macros.Range( 1, ch.GetCurrInt() / 3 - 10, 100 );
                if( Race.RaceList[ ch.GetOrigRace() ].PrimaryLanguage != Race.Language.dwarven )
                    ( ch ).LanguageAptitude[ (int)Race.Language.dwarven ] =
                        Macros.Range( 1, ch.GetCurrInt() / 4 - 10, 100 );
                if( Race.RaceList[ ch.GetOrigRace() ].PrimaryLanguage != Race.Language.centaur )
                    ( ch ).LanguageAptitude[ (int)Race.Language.centaur ] =
                        Macros.Range( 1, ch.GetCurrInt() / 5, 100 );
                if( Race.RaceList[ ch.GetOrigRace() ].PrimaryLanguage != Race.Language.aquaticelf )
                    ( ch ).LanguageAptitude[ (int)Race.Language.aquaticelf ] =
                        Macros.Range( 0, ch.GetCurrInt() / 10 - 5, 100 );
                if( Race.RaceList[ ch.GetOrigRace() ].PrimaryLanguage != Race.Language.gnome )
                    ( ch ).LanguageAptitude[ (int)Race.Language.gnome ] =
                        Macros.Range( 1, ch.GetCurrInt() / 5 + 5, 100 );
                if( Race.RaceList[ ch.GetOrigRace() ].PrimaryLanguage != Race.Language.halfling )
                    ( ch ).LanguageAptitude[ (int)Race.Language.halfling ] =
                        Macros.Range( 1, ch.GetCurrInt() / 5 + 15, 100 );
                if( Race.RaceList[ ch.GetOrigRace() ].PrimaryLanguage != Race.Language.barbarian )
                    ( ch ).LanguageAptitude[ (int)Race.Language.barbarian ] =
                        Macros.Range( 1, ch.GetCurrInt() / 3 + 5, 100 );
                if( Race.RaceList[ ch.GetOrigRace() ].PrimaryLanguage != Race.Language.githzerai )
                    ( ch ).LanguageAptitude[ (int)Race.Language.githzerai ] =
                        Macros.Range( 1, ch.GetCurrInt() / 4 - 10, 100 );
            }

            // Added for halfelves.
            if( GetOrigRace() == Race.RACE_HALFELF )
                ( ch ).LanguageAptitude[ (int)Race.Language.elven ] = 90;

            ( ch ).LanguageAptitude[ (int)Race.Language.dragon ] = 0;
            ( ch ).LanguageAptitude[ (int)Race.Language.unknown ] = 0;
            ( ch ).LanguageAptitude[ (int)Race.Language.god ] = IsImmortal() ? 100 : 0;
            ( ch ).LanguageAptitude[ (int)Race.Language.magical ] = 0;
            ( ch ).LanguageAptitude[ (int)Race.Language.thri ] = GetOrigRace() == Race.RACE_THRIKREEN ? 100 : 0;
            ( ch ).LanguageAptitude[ (int)Race.Language.animal ] = 0;
            ( ch ).LanguageAptitude[ (int)Race.Language.illithid ] = 0;

        }

        /// <summary>
        /// Used to get the original character so that we address the correct CharData in the
        /// case of a snooping player.
        /// </summary>
        /// <returns></returns>
        public CharData GetChar()
        {
            if( IsNPC() )
            {
                return _socket.Original;
            }
            return this;
        }

        /// <summary>
        /// Check resistance type to the specified damage type.  Levels are 
        /// immune, resistant, normal, susceptible, and vulnerable.
        /// </summary>
        /// <param name="damType"></param>
        /// <returns></returns>
        public Race.ResistanceType CheckRIS(AttackType.DamageType damType)
        {
            Race.DamageType bit;
            Race.DamageType resistant = 0;
            Race.DamageType immune = 0;
            Race.DamageType susceptible = 0;
            Race.DamageType vulnerable = 0;

            Race.ResistanceType ris = Race.ResistanceType.normal;
            Race.ResistanceType raceDefault = Race.ResistanceType.normal;

            if( damType == AttackType.DamageType.none )
                return ris;

            resistant = _resistant | Race.RaceList[ GetRace() ].Resistant;
            immune = _immune | Race.RaceList[ GetRace() ].Immune;
            susceptible = _susceptible | Race.RaceList[ GetRace() ].Susceptible;
            vulnerable = _vulnerable | Race.RaceList[ GetRace() ].Vulnerable;

            if( damType < AttackType.DamageType.magic_other )
            {
                if (Macros.IsSet((int)resistant, (int)Race.DamageType.weapon))
                    raceDefault = Race.ResistanceType.resistant;
                else if (Macros.IsSet((int)immune, (int)Race.DamageType.weapon))
                    raceDefault = Race.ResistanceType.immune;
                else if (Macros.IsSet((int)susceptible, (int)Race.DamageType.weapon))
                    raceDefault = Race.ResistanceType.susceptible;
                else if (Macros.IsSet((int)vulnerable, (int)Race.DamageType.weapon))
                    raceDefault = Race.ResistanceType.vulnerable;
            }
            else
            {
                if (Macros.IsSet((int)resistant, (int)Race.DamageType.magic))
                    raceDefault = Race.ResistanceType.resistant;
                else if (Macros.IsSet((int)immune, (int)Race.DamageType.magic))
                    raceDefault = Race.ResistanceType.immune;
                else if (Macros.IsSet((int)susceptible, (int)Race.DamageType.magic))
                    raceDefault = Race.ResistanceType.susceptible;
                else if (Macros.IsSet((int)vulnerable, (int)Race.DamageType.magic))
                    raceDefault = Race.ResistanceType.vulnerable;
            }

            switch( damType )
            {
                default:
                    return raceDefault;
                case AttackType.DamageType.fire:
                    bit = Race.DamageType.fire;
                    break;
                case AttackType.DamageType.cold:
                    bit = Race.DamageType.cold;
                    break;
                case AttackType.DamageType.electricity:
                    bit = Race.DamageType.electricity;
                    break;
                case AttackType.DamageType.acid:
                    bit = Race.DamageType.acid;
                    break;
                case AttackType.DamageType.poison:
                    bit = Race.DamageType.poison;
                    break;
                case AttackType.DamageType.charm:
                    bit = Race.DamageType.charm;
                    break;
                case AttackType.DamageType.mental:
                    bit = Race.DamageType.mental;
                    break;
                case AttackType.DamageType.energy:
                    bit = Race.DamageType.energy;
                    break;
                case AttackType.DamageType.white_magic:
                    bit = Race.DamageType.whiteMana;
                    break;
                case AttackType.DamageType.black_magic:
                    bit = Race.DamageType.blackMana;
                    break;
                case AttackType.DamageType.disease:
                    bit = Race.DamageType.disease;
                    break;
                case AttackType.DamageType.drowning:
                    bit = Race.DamageType.drowning;
                    break;
                case AttackType.DamageType.light:
                    bit = Race.DamageType.light;
                    break;
                case AttackType.DamageType.sound:
                    bit = Race.DamageType.sound;
                    break;
                case AttackType.DamageType.bludgeon:
                    bit = Race.DamageType.bash;
                    break;
                case AttackType.DamageType.pierce:
                    bit = Race.DamageType.pierce;
                    break;
                case AttackType.DamageType.slash:
                    bit = Race.DamageType.slash;
                    break;
                case AttackType.DamageType.gas:
                    bit = Race.DamageType.gas;
                    break;
            }

            if (Macros.IsSet((int)immune, (int)bit))
                ris = Race.ResistanceType.immune;
            else if (Macros.IsSet((int)resistant, (int)bit) && ris != Race.ResistanceType.immune)
                ris = Race.ResistanceType.resistant;
            else if (Macros.IsSet((int)susceptible, (int)bit))
                ris = Race.ResistanceType.susceptible;
            else if (Macros.IsSet((int)vulnerable, (int)bit))
                ris = Race.ResistanceType.vulnerable;

            if (ris == Race.ResistanceType.normal)
                return raceDefault;

            return ris;
        }

        /// <summary>
        /// Checks whether the player is a member of a guild.
        /// </summary>
        /// <returns></returns>
        public bool IsGuild()
        {
            
            if( !IsNPC() && ( ( (PC)this ).GuildMembership != null ) && ( ( (PC)this ).GuildRank > Guild.Rank.exiled ) )
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Remove the character from their current guild, if any.
        /// </summary>
        public void RemoveFromGuild()
        {
            int icount;

            if( !IsGuild() )
            {
                return;
            }

            Guild guild = ( (PC)this ).GuildMembership;

            for( icount = 0; icount < Limits.MAX_GUILD_MEMBERS; ++icount )
            {
                if( !MUDString.StringsNotEqual( guild.Members[ icount ].Name, _name ) )
                {
                    guild.Members[ icount ].Filled = false;
                }
            }

            switch( ( (PC)this ).GuildRank )
            {
                default:
                    break;
                case Guild.Rank.leader:
                    guild.Overlord = String.Empty;
                    break;
            }

            guild.NumMembers--;
            Command.SetTitle( this, "&n" );

            return;
        }

        /// <summary>
        /// Advancement stuff.
        /// </summary>
        /// <param name="skills"></param>
        public void AdvanceLevel( bool skills )
        {
            CharData ch = this;
            string buf;
            int addHp;

            ch.SendText( "&+WYou gain a level!&n\r\n" );

            /* Knock down a level of exp so they don't spamlevel. */
            ch._experiencePoints -= ExperienceTable.Table[ ch._level ].LevelExperience;
            if (ch._experiencePoints < 0)
            {
                ch._experiencePoints = 0;
            }
            ch._level += 1;

            if( !ch.IsNPC() && ( (PC)ch ).LostHp != 0 )
            {
                if( MUDMath.NumberPercent() < ( ch.GetCurrLuck() + ch.GetCurrCon() ) / 3 )
                    addHp = ( (PC)ch ).LostHp;
                else
                    addHp = ( (PC)ch ).LostHp - 1;
                ( (PC)ch ).LostHp = 0;
            }
            else
            {
                addHp = MUDMath.NumberRange( ch._charClass.MinHpGain, ch._charClass.MaxHpGain );
            }

            int addMana = ch._charClass.GainsMana
                               ? MUDMath.Dice( 2, ( ( 2 * ch.GetCurrInt() + ch.GetCurrWis() ) / 30 ) )
                               : 0;

            if (!ch.IsClass(CharClass.Names.bard))
            {
                addMana = ( addMana * ch.GetCurrPow() ) / 100;
            }
            else
            {
                addMana = ( addMana * ch.GetCurrCha() ) / 100;
                if( ch.GetOrigRace() == Race.RACE_HALFELF )
                    addMana += 1; // Bonus for half-elven bards
            }

            addHp = Math.Max( 1, addHp );
            addMana = Math.Max( 0, addMana );

            // Don't touch _maxHitpoints except for PERMANENT changes.
            ch._maxHitpoints += addHp;

            ch._hitpoints += addHp;

            ch._maxMana += addMana;
            ch._currentMana += addMana;

            // People getting hits rerolled shouldn't get automatic skill increases.
            if( skills )
            {
                if( ch.IsClass(CharClass.Names.monk) || ch.IsClass(CharClass.Names.mystic ))
                {
                    ( (PC)ch ).Train += ( 1 + ch._level / 10 );
                }
                ch.InitializeSkills();
            }

            if( ( (PC)ch ).LostHp == 0 && ( ch.IsClass(CharClass.Names.monk) && ch._charClass.ClassNumber == CharClass.Names.mystic ) )
            {
                if( ch._level <= 8 )
                    ( (PC)ch ).SkillPoints += 1;
                else if( ch._level <= 16 )
                    ( (PC)ch ).SkillPoints += 2;
                else if( ch._level <= 24 )
                    ( (PC)ch ).SkillPoints += 3;
                else if( ch._level <= 32 )
                    ( (PC)ch ).SkillPoints += 4;
                else if( ch._level <= 36 )
                    ( (PC)ch ).SkillPoints += 5;
                else if( ch._level <= 40 )
                    ( (PC)ch ).SkillPoints += 6;
                else
                    ( (PC)ch ).SkillPoints += ch._level - 34;
            }
            /* Clerics and psionicists should automatically get new spells
            *  every five levels and be notified what they learned. And shamans too.
            */
            if( ch.IsClass(CharClass.Names.cleric) || ch.IsClass(CharClass.Names.psionicist)
                    || ch.IsClass(CharClass.Names.shaman) || ch.IsClass(CharClass.Names.paladin)
                    || ch.IsClass(CharClass.Names.antipaladin) || ch.IsClass(CharClass.Names.druid ))
            {
                if( ch._level % 4 == 1 )
                {
                    ch.SendText( "You learn new spells!\r\n" );
                    foreach (KeyValuePair<String, Spell> kvp in Spell.SpellList)
                    {
                        if ((kvp.Value.SpellCircle[(int)ch._charClass.ClassNumber] <=
                                ( ( ch._level + 3 ) / 4 ) )
                                && (!ch.HasSpell(kvp.Key) || ( ( (PC)ch ).SpellAptitude[ kvp.Key ] < Limits.BASE_SPELL_ADEPT + ch.GetCurrWis() / 9 ))
                                && kvp.Value.CanBeScribed )
                        {
                            buf = String.Format( "&+cYou learn {0}.&n\r\n", kvp.Value.Name );
                            ch.SendText( buf );
                            ( (PC)ch ).SpellAptitude[ kvp.Key ] = Limits.BASE_SPELL_ADEPT + ch.GetCurrWis() / 9;
                        }
                    }
                }
            }

            if( ch.IsClass(CharClass.Names.psionicist) || ch.IsClass(CharClass.Names.bard) )
            {
                buf = String.Format( "Your gain is: {0}/{1} hp, {2}/{3} mana.\r\n", ( addHp * ch.GetCurrCon() / 100 ), ch.GetMaxHit(),
                          addMana, ch._maxMana );
                ch.SendText( buf );
            }
            else
            {
                buf = String.Format( "Your gain is: {0}/{1} hp.\r\n",
                          ( addHp * ch.GetCurrCon() / 100 ), ch.GetMaxHit() );
                ch.SendText( buf );
            }

            SavePlayer( ch );

            return;
        }

        /// <summary>
        /// Checks whether a character is able to speak a particular language.
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public bool CanSpeakLanguage(Race.Language lang)
        {
            if (lang < 0 || (int)lang > Race.MAX_LANG)
                return false;
            if (lang == Race.Language.unknown && !IsImmortal() && !IsNPC())
                return false;
            if (IsNPC())
                return false;
            if (((PC)this).LanguageAptitude[(int)lang] > 19)
                return true;
            return false;
        }

        /// <summary>
        /// Demote stuff.
        /// </summary>
        public void DemoteLevel()
        {
            CharData ch = this;
            string text;

            if (ch._level == 1)
            {
                return;
            }

            int addHp = ch._charClass.MaxHpGain;

            int addMana = ch._charClass.GainsMana
                               ? MUDMath.NumberRange( 2, ( ( 2 * ch.GetCurrInt() + ch.GetCurrWis() ) / 16 ) )
                               : 0;

            if (!ch.IsClass(CharClass.Names.bard))
            {
                addMana = ( addMana * ch.GetCurrPow() ) / 100;
            }
            else
            {
                addMana = ( addMana * ch.GetCurrCha() ) / 100;
                if( ch.GetOrigRace() == Race.RACE_HALFELF )
                    addMana += 1; // Bonus for half-elven bards
            }

            addHp = Math.Max( 1, addHp );
            addMana = Math.Max( 0, addMana );

            ch._maxHitpoints -= addHp;
            ch._hitpoints -= addHp;
            ch._maxMana -= addMana;
            ch._currentMana -= addMana;

            if (!ch.IsNPC())
            {
                ((PC)ch).LostHp = addHp;
            }

            ch._level -= 1;
            if( ch.IsClass( CharClass.Names.psionicist) || ch.IsClass(CharClass.Names.bard ))
            {
                text = String.Format("Your loss is: {0}/{1} hp, {2}/{3} mana.\r\n",
                          ( addHp * ch.GetCurrCon() / 100 ), ch.GetMaxHit(), addMana, ch._maxMana );
            }
            else
            {
                text = String.Format("Your loss is: {0}/{1} hp.\r\n", ( addHp * ch.GetCurrCon() / 100 ), ch.GetMaxHit() );
            }
            ch.SendText( text );
            return;
        }

        public void GainExperience( int gain )
        {
            CharData ch = this;
            string text;

            if (ch.IsNPC())
            {
                return;
            }

            /* Cap for exp on any kill. Equivalent of ress exp.  */
            if (gain > ((50 + ch._level) * ExperienceTable.Table[ch._level].LevelExperience) / 500)
            {
                gain = ((50 + ch._level) * ExperienceTable.Table[ch._level].LevelExperience) / 500;
            }

            // Human experience bonus of 10% as a Human innate 
            if( ch.GetRace() == Race.RACE_HUMAN )
            {
                gain = ( gain * 11 ) / 10;
            }

            ch._experiencePoints += gain;

            /* Allow MAX_ADVANCE_LEVEL chars to continue gaining exp, but do not let them gain
            *  any levels.  That's the job of 51/52/etc. potions. */
            if( ( ch._experiencePoints >= ExperienceTable.Table[ ch._level ].LevelExperience ) && ( ch._level < Limits.MAX_ADVANCE_LEVEL ) )
            {
                /* Increase a level. Better than just stealing all their exp
                * past 1 level; allows someone needing 1 exp to level to get more
                * than two exp.  Also allows them to only get 1.
                */
                ch.AdvanceLevel( true );
                text = String.Format( "{0} has levelled and is now level {1}.", ch._name, ch._level );
                ImmortalChat.SendImmortalChat( ch, ImmortalChat.IMMTALK_LEVELS, ch.GetTrust(), text );
                if( Macros.IsSet( (int)Database.SystemData.ActFlags, (int)Sysdata.MudFlags.turbolevel ) )
                {
                    while( ch._level % 10 != 0 )
                    {
                        ch.GainExperience( ExperienceTable.Table[ ch._level ].LevelExperience );
                    }
                }
            }
            /* Lvl 1 chars can't lose lvl. */
            else if( ch._experiencePoints < 0 && ch._level > 1 && !ch.IsImmortal() )
            {
                ch._experiencePoints = ExperienceTable.Table[ ( ch._level - 1 ) ].LevelExperience + ch._experiencePoints;
                ch.SendText( "&+RYou lose a level!&n\r\n" );
                ch.DemoteLevel();
                text = String.Format( "{0} has de-levelled and is now level {1}.", ch._name, ch._level );
                ImmortalChat.SendImmortalChat( ch, ImmortalChat.IMMTALK_LEVELS, ch.GetTrust(), text );
            }
            return;
        }

        /// <summary>
        /// Checks whether two characters are in the same group.
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public bool IsSameGroup( CharData ch )
        {
            if (this == ch)
            {
                return true;
            }

            if (_groupLeader == null || ch._groupLeader == null)
            {
                return false;
            }

            if (ch._groupLeader)
            {
                ch = ch._groupLeader;
            }
            return _groupLeader == ch;
        }

        /// <summary>
        /// Checks whether two characters are in the same guild.
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public bool IsSameGuild( CharData ch )
        {
            if (IsGuild() && ch.IsGuild())
            {
                return ((PC)this).GuildMembership == ((PC)ch).GuildMembership;
            }
            return false;
        }

        /// <summary>
        /// True if character is grouped, false otherwise.
        /// </summary>
        /// <returns></returns>
        bool CheckGroup( )
        {
            // No char/leader => not in group
            if (_groupLeader == null)
            {
                return false;
            }

            // Must have 1 other person in group before we can return true.
            foreach (CharData groupChar in Database.CharList)
            {
                if (groupChar.IsSameGroup(this) && groupChar != this)
                {
                    return true;
                }
            }

            // Only person in group. :(
            return false;
        }

        public void DoorTrigger( string arg )
        {
            CharData ch = this;
            int door;

            if( ch._inRoom == null )
            {
                return;
            }

            for( door = 0; door < Limits.MAX_DIRECTION; door++ )
            {
                if (ch._inRoom.ExitData[door] && ch._inRoom.ExitData[door].HasFlag(Exit.ExitFlag.closed)
                        && !ch._inRoom.ExitData[door].HasFlag(Exit.ExitFlag.secret)
                        && ch._inRoom.ExitData[ door ].Key < 0 )
                {
                    /* get last argument from door keywords */
                    //            buf1 = MUDString.one_argument( ch.in_room.exit[door].keyword, buf);
                    int i = 0;
                    string text = ch._inRoom.ExitData[ door ].Keyword;
                    string buffer = text;
                    while( !String.IsNullOrEmpty(buffer) && i < 9 )
                    {
                        buffer = MUDString.OneArgument( buffer, ref text );
                        i++;
                    }
                    string lbuf = String.Format( "Comparing {0} against word said: {1}", text, arg );
                    ImmortalChat.SendImmortalChat( null, ImmortalChat.IMMTALK_SPAM, 0, lbuf );
                    if( !MUDString.StringsNotEqual( MUDString.LastArgument( ch._inRoom.ExitData[ door ].Keyword ), arg ) )
                    {
                        Exit exit = ch._inRoom.ExitData[door].TargetRoom.ExitData[(int)Exit.ReverseDirection((Exit.Direction)door)];
                        MUDString.OneArgument( ch._inRoom.ExitData[ door ].Keyword, ref text );
                        string outputText = String.Format( "The {0} hums briefly and opens.", text );
                        ch._inRoom.ExitData[door].RemoveFlag(Exit.ExitFlag.closed);
                        exit.RemoveFlag(Exit.ExitFlag.closed);
                        SocketConnection.Act( outputText, ch, null, null, SocketConnection.MessageTarget.room );
                        SocketConnection.Act( outputText, ch, null, null, SocketConnection.MessageTarget.character );
                    }
                }
            }

            return;
        }

        public void RemoveFromGroup( CharData victim )
        {
            CharData ch = this;
            if( victim == null )
            {
                Log.Error( "CharData.RemoveFromGroup: null victim", 0 );
                return;
            }

            if( ch._groupLeader == null )
            {
                Log.Error( "CharData.RemoveFromGroup: ch not grouped.", 0 );
                ch._nextInGroup = null;
                return;
            }
            if( victim._groupLeader == null )
            {
                Log.Error( "CharData.RemoveFromGroup: victim not grouped.", 0 );
                victim._nextInGroup = null;
                return;
            }

            if( ch == victim )
            {
                victim.SendText( "You leave the group.\r\n" );
                if (victim != victim._groupLeader)
                {
                    SocketConnection.Act("$N&n leaves your group.", victim._groupLeader, null, victim, SocketConnection.MessageTarget.character);
                }
            }
            else
            {
                SocketConnection.Act( "You kick $N&n out of your group.", ch, null, victim, SocketConnection.MessageTarget.character );
                SocketConnection.Act( "$n&n removes you from $s group.", ch, null, victim, SocketConnection.MessageTarget.victim );
            }

            if( victim._groupLeader != victim )
            {   
                /* Remove group member from group. */
                CharData prevGmember = victim._groupLeader;

                /* Find previous group member. */
                while (prevGmember != null && prevGmember._nextInGroup != victim)
                {
                    prevGmember = prevGmember._nextInGroup;
                }
                if( prevGmember == null )
                {
                    Log.Error( "CharData.RemoveFromGroup: ch has leader but not on group list.", 0 );
                    victim._groupLeader = null;
                    victim._nextInGroup = null;
                    return;
                }
                /* remove ch from group */
                prevGmember._nextInGroup = victim._nextInGroup;
                /* remove group from ch */
                victim._groupLeader = null;
                victim._nextInGroup = null;
                /* Member of a group of 2. */
                if( prevGmember == prevGmember._groupLeader &&
                        prevGmember._nextInGroup == null )
                {
                    prevGmember.SendText( "Your group has been disbanded.\r\n" );
                    prevGmember._groupLeader = null;
                    return;
                }
            }
            else
            {   
                /* Remove leader from group. */
                CharData newLeader = victim._nextInGroup;
                CharData gmembers;

                /* Leader of a group of 1 ?! */
                if( newLeader == null )
                {
                    Log.Error( "CharData.RemoveFromGroup: group leader has no group!", 0 );
                    return;
                }

                /* Leader of a group of 2. */
                if( ( gmembers = newLeader._nextInGroup ) == null )
                {
                    newLeader.SendText( "Your group has been disbanded.\r\n" );
                    return;
                }
                newLeader._groupLeader = newLeader;
                while( gmembers != null )
                {
                    gmembers._groupLeader = newLeader;
                    gmembers = gmembers._nextInGroup;
                }
            }
        }

        public void AddGroupMember( CharData victim )
        {
            if( victim == null )
            {
                Log.Error( "AddGroupMember: null this or null victim ({0})!", 0 );
                return;
            }

            /* Only the leader can add to group. */
            if( this != _groupLeader )
            {
                if( _groupLeader != null )
                {
                    Log.Error( "AddGroupMember: this not group leader!", 0 );
                    return;
                }
                _groupLeader = this;
            }

            SocketConnection.Act( "$N&n joins your group.", this, null, victim, SocketConnection.MessageTarget.character );
            SocketConnection.Act( "You join $n&n's group.", this, null, victim, SocketConnection.MessageTarget.victim );

            /* Move to last char in group. */
            CharData ch = this;
            while( ch._nextInGroup != null )
            {
                ch._nextInGroup._groupLeader = ch._groupLeader;
                ch = ch._nextInGroup;
            }
            /* Add char to group. */
            ch._nextInGroup = victim;
            victim._nextInGroup = null;
            /* Add group to char. */
            victim._groupLeader = ch._groupLeader;
        }

        /// <summary>
        /// Extracts a character from the world.  If delete is true, it then deletes that character.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="delete"></param>
        public static void ExtractChar( CharData ch, bool delete )
        {
            if( ch == null )
            {
                Log.Error( "ExtractChar: null ch.", 0 );
                return;
            }

            try
            {
                if (ch._fighting)
                {
                    Combat.StopFighting(ch, true);
                }

                Magic.ForgetAllSpells(ch);

                Event.DeleteAttachedEvents(ch);

                // Remove any affects we want to be gone next time they log in.
                ch.RemoveAffect(Affect.AFFECT_CASTING);
                ch.RemoveAffect(Affect.AFFECT_SINGING);

                // Meaning they're dead for good or have left the game.
                if (delete)
                {
                    string name;

                    if (ch.IsNPC())
                    {
                        name = ch._shortDescription;
                    }
                    else
                    {
                        name = ch._name;
                    }

                    ch.DieFollower(name);
                    if (ch._groupLeader || ch._nextInGroup)
                    {
                        ch.RemoveFromGroup(ch);
                    }

                    /* Get rid of weapons _first_ */

                    {
                        Object obj3 = Object.GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.hand_one);
                        Object obj2 = Object.GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.hand_two);

                        if (obj3 != null)
                        {
                            obj3.RemoveFromWorld();
                        }

                        /* Now kill obj2 if it exists no matter if on body or floor */
                        if (obj2)
                        {
                            obj2.RemoveFromWorld();
                        }
                    }

                    for (int i = ch._carrying.Count - 1; i >= 0; --i)
                    {
                        ch._carrying[i].RemoveFromWorld();
                    }
                }

                CharData worldChar;
                for (int xx = Database.CharList.Count - 1; xx >= 0; xx--)
                {
                    worldChar = Database.CharList[xx];
                    if (worldChar._replyTo == ch)
                    {
                        worldChar._replyTo = null;
                    }
                    if (worldChar.IsConsenting(ch))
                    {
                        worldChar.StopConsenting(ch);
                        SocketConnection.Act("You stop consenting $N&n.", worldChar, null, ch, SocketConnection.MessageTarget.character);
                    }
                    if (worldChar.IsIgnoring(ch))
                    {
                        worldChar.StopIgnoring(ch);
                        SocketConnection.Act("You stop ignoring $N&n.", worldChar, null, ch, SocketConnection.MessageTarget.character);
                    }
                    if (!worldChar.IsNPC() && ((PC)worldChar).Guarding == ch)
                    {
                        ((PC)worldChar).Guarding = null;
                        SocketConnection.Act("You stop guarding $N&n.", worldChar, null, ch, SocketConnection.MessageTarget.character);
                    }
                    if (worldChar.IsHating(ch))
                        worldChar.StopHating(ch);
                    if (worldChar._hunting && worldChar._hunting.Who == ch)
                        Combat.StopHunting(worldChar);
                    if (worldChar._fearing && worldChar._fearing.Who == ch)
                        Combat.StopFearing(worldChar);
                    // Remove from the active character list.
                    // BUG: TODO: FIXME: This invalidates the list for anyone iterating through
                    // a list that may kill characters, such as violence_update.
                    // it = CharList.erase( it );
                }

                if (ch._inRoom)
                {
                    // This was placed *after* the act strings to be safe.
                    for (int iwch = ch._inRoom.People.Count - 1; iwch >= 0; iwch--)
                    {
                        if (ch._inRoom.People[iwch] == ch)
                        {
                            ch.RemoveFromRoom();
                            break;
                        }
                    }
                }

                // They're not being yanked from game, probably just dead and going to the menu.
                if (!delete)
                {
                    Room location;

                    if (ch._level < 5 || Macros.IsSet((int)Database.SystemData.ActFlags, (int)Sysdata.MudFlags.alwaysequip))
                        ch.ReceiveNewbieEquipment();

                    if (!ch.IsNPC() && (ch.GetOrigRace() < Limits.MAX_PC_RACE) && ((int)ch._charClass.ClassNumber < CharClass.ClassList.Length))
                    {
                        // Get the default respawn location based on currhome, then race/class default.
                        location = Room.GetRoom(((PC)ch).CurrentHome);
                        if (location == null)
                        {
                            int place;
                            List<RepopulationPoint> repoplist = ch.GetAvailableRepops();
                            if (repoplist.Count < 1)
                            {
                                place = StaticRooms.GetRoomNumber("ROOM_NUMBER_START");
                            }
                            else
                            {
                                place = repoplist[0].RoomIndexNumber;
                            }
                            location = Room.GetRoom(place);
                            if (location == null)
                            {
                                Log.Error("Starting room does not exist for class {0} of player's race!  Calling ch.AddToRoom() for altar.", (int)ch._charClass.ClassNumber);
                                ch.AddToRoom(Room.GetRoom(StaticRooms.GetRoomNumber("ROOM_NUMBER_ALTAR")));
                            }
                        }
                        else
                        {
                            ch.AddToRoom(location);
                        }
                    }
                    else
                    {
                        location = Room.GetRoom(StaticRooms.GetRoomNumber("ROOM_NUMBER_START"));
                        if (location == null)
                        {
                            Log.Error("Starting room {0} does not exist!  Calling char_to_room for altar.", StaticRooms.GetRoomNumber("ROOM_NUMBER_START"));
                            ch.AddToRoom(Room.GetRoom(StaticRooms.GetRoomNumber("ROOM_NUMBER_ALTAR")));
                        }
                        else
                        {
                            ch.AddToRoom(Room.GetRoom(StaticRooms.GetRoomNumber("ROOM_NUMBER_START")));
                        }
                    }
                    return;
                }

                // Clear modifiers.
                if (ch.IsNPC())
                {
                    --ch._mobTemplate.NumActive;
                }
                else
                {
                    ((PC)ch).Hunger = 48;
                    ((PC)ch).Thirst = 48;
                    ((PC)ch).Drunk = 0;
                    ((PC)ch).LastRentLocation = 0;
                    ch._armorPoints = 100;
                    ch._position = Position.standing;
                    ch._hitpoints = Math.Max(1, ch._hitpoints);
                    ch._currentMana = Math.Max(1, ch._currentMana);
                    ch._currentMoves = Math.Max(1, ch._currentMoves);
                    ((PC)ch).HitpointModifier = 0;
                    ch._hitroll = 0;
                    ch._damroll = 0;
                    ch._savingThrows[0] = 0;
                    ch._savingThrows[1] = 0;
                    ch._savingThrows[2] = 0;
                    ch._savingThrows[3] = 0;
                    ch._savingThrows[4] = 0;
                    ch._modifiedStrength = 0;
                    ch._modifiedIntelligence = 0;
                    ch._modifiedWisdom = 0;
                    ch._modifiedDexterity = 0;
                    ch._modifiedConstitution = 0;
                    ch._modifiedAgility = 0;
                    ch._modifiedCharisma = 0;
                    ch._modifiedPower = 0;
                    ch._modifiedLuck = 0;
                    ((PC)ch).MaxStrMod = 0;
                    ((PC)ch).MaxIntMod = 0;
                    ((PC)ch).MaxWisMod = 0;
                    ((PC)ch).MaxDexMod = 0;
                    ((PC)ch).MaxConMod = 0;
                    ((PC)ch).MaxAgiMod = 0;
                    ((PC)ch).MaxChaMod = 0;
                    ((PC)ch).MaxPowMod = 0;
                    ((PC)ch).MaxLukMod = 0;
                }

                if (ch._socket && ch._socket.Original)
                {
                    CommandType.Interpret(ch, "return");
                }

                ch._deleteMe = true;
            }
            catch (Exception ex)
            { 
                Log.Error("Exception in ExtractChar: " + ex.ToString());
            }
            return;
        }

        /// <summary>
        /// Checks whether the character is able to concentrate well enough to start casting/singing or
        /// finish a spell.  Makes sure the character has not been bashed among other things.
        /// </summary>
        /// <returns>true if the character is able to concentrate well enough to cast a spell</returns>
        public bool CheckConcentration(Spell spell)
        {
            // Some spells can't be cast in combat.
            if( !IsClass( CharClass.Names.bard ) && _position == Position.fighting &&
                !( spell.CanCastInCombat ) )
            {
                SendText( "You can't concentrate enough.\r\n" );
                return false;
            }
            // Can't concentrate if you've been bashed.
            if( _position != Position.standing && _position != Position.fighting )
            {
                if (IsClass(CharClass.Names.bard))
                {
                    SendText("You must be standing to sing loudly enough to be heard!\r\n");
                }
                else
                {
                    SendText("You must be standing in order to cast spells!\r\n");
                }
                return false;
            }
            return true;
        }
        
        /// <summary>
        /// Formats and sends the group display, adjusting for terminal type.
        /// </summary>
        public void ShowGroup()
        {
            CharData groupChar;
            bool bastTerm = (!IsNPC() && _socket._terminalType == SocketConnection.TerminalType.TERMINAL_ENHANCED);

            // No arguments, no leader, no chance.
            if( !CheckGroup() )
            {
                if (bastTerm)
                {
                    SendText("<group></group>");
                }
                else
                {
                    SendText("But you're not in a group!\r\n");
                }
                return;
            }

            if (!bastTerm)
            {
                SendText( String.Format("&+R{0}&+L's group:&n\r\n", _groupLeader.ShowNameTo(this, true)) );
            }
            else
            {
                SendText("<group>");
            }
            String text = String.Empty;
            int num = 0;
            for( groupChar = _groupLeader; groupChar; groupChar = groupChar._nextInGroup )
            {
                // Won't show stats of a groupmember not in the room
                if( _inRoom == groupChar._inRoom )
                {
                    if (!bastTerm)
                    {
                        text = String.Format(
                            "&+L[&n{0} {1}&+L]&n {2}&n {3}&+L/&n{4}&+L hp&n {5}&+L/&n{6} &+Lmana&n {7}&+L/&n{8}&+L mv&n\r\n",
                            MUDString.PadInt(groupChar._level, 2), groupChar.IsNPC() ? "Mob         " : groupChar._charClass.WholistName,
                            MUDString.PadStr(groupChar.ShowNameTo(this, true), 16),
                            MUDString.PadInt(groupChar._hitpoints, 4), MUDString.PadInt(groupChar.GetMaxHit(), 4),
                            MUDString.PadInt(groupChar._currentMana, 4), MUDString.PadInt(groupChar._maxMana, 4),
                            MUDString.PadInt(groupChar._currentMoves, 4), MUDString.PadInt(groupChar._maxMoves, 4));
                    }
                    else
                    {
                        text = String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}:",
                            num, groupChar._level, groupChar.IsNPC() ? "Mob" : groupChar._charClass.Name, groupChar.ShowNameTo(this,true),
                              groupChar._hitpoints, groupChar.GetMaxHit(), groupChar._currentMana, groupChar._maxMana,
                              groupChar._currentMoves, groupChar._maxMoves );
                        ++num;
                    }
                    SendText( text );
                }
                else
                {
                    if (!bastTerm)
                    {
                        text = String.Format("&+L[&n{0} {1}&+L]&n {2}&n\r\n",
                                  MUDString.PadInt(groupChar._level, 2),
                                  groupChar.IsNPC() ? "Mob         " : groupChar._charClass.WholistName,
                                  MUDString.PadStr(groupChar.ShowNameTo(this, true), 16));
                    }
                    else
                    {
                        text = String.Format("{0},{1},{2},{3},,,,,,:",
                            num, groupChar._level, groupChar.IsNPC() ? "Mob" : groupChar._charClass.Name, groupChar.ShowNameTo(this, true));
                        ++num;
                    }
                    SendText( text );
                }
            }
            if (bastTerm)
            {
                SendText("</group>");
            }
        }

        /// <summary>
        /// Regeneration and natural healing.
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static int HitGain( CharData ch )
        {
            int gain;
            int percent = 0;

            if( ch == null )
            {
                Log.Error( "HitGain(): null ch", 0 );
                return 0;
            }

            // They aren't going to gain hits in a no heal room...
            if (ch._inRoom.HasFlag(RoomTemplate.ROOM_NO_HEAL))
            {
                return 0;
            }

            gain = 1;
            percent = 100;

            if (ch.CheckSkill("fast healing", PracticeType.none))
            {
                gain += 1;
            }
            if (MUDMath.NumberPercent() < 3)
            {
                ch.PracticeSkill("fast healing");
            }

            switch( ch._position )
            {
                case Position.sleeping:
                    gain += 3;
                    break;
                case Position.reclining:
                    gain += 2;
                    break;
                case Position.resting:
                    gain += 1;
                    break;
                case Position.fighting:
                    gain = 0;
                    break;
            }

            if( ch.HasInnate( Race.RACE_REGENERATE ) )
            {
                // Automatically one extra hp, two at level 30.
                gain += 1 + (ch._level / 30);
                // One percent chance of gaining another per level.
                percent += (ch._level);
            }

            // Hunger and thirst for PCs.
            if (!ch.IsNPC())
            {
                if (((PC)ch).Hunger == 0)
                {
                    gain /= 2;
                    gain -= 1;
                    percent -= 25;
                    ch.SendText("&nYou double over from &+Rhunger pains&n!\r\n");
                }

                if (((PC)ch).Thirst == 0)
                {
                    gain /= 2;
                    gain -= 1;
                    percent -= 25;
                    ch.SendText("&nYou suffer from severe &+cth&+Ci&n&+cr&+Cst&n!\r\n");
                }
            }
            
            if( ch.IsAffected( Affect.AFFECT_POISON ) )
            {
                gain /= 4;
                percent /= 2;
            }

            if( gain == 0 )
                if( MUDMath.NumberPercent() < percent )
                    gain = 1;

            // Heal rooms heal you a little quicker
            if (ch._inRoom.HasFlag(RoomTemplate.ROOM_HEAL))
            {
                gain += Math.Max(1, gain / 2);
            }

            if( ( ch._inRoom.TerrainType != TerrainType.underwater_has_ground
                    && ch._inRoom.TerrainType != TerrainType.unswimmable_water
                    && ch._inRoom.TerrainType != TerrainType.swimmable_water
                    && ch.HasInnate( Race.RACE_WATERBREATH )
                    && MUDString.StringsNotEqual( Race.RaceList[ ch.GetRace() ].Name, "Object" )
                    && ch.GetRace() != Race.RACE_GOD )
                    || ( ch._inRoom.TerrainType == TerrainType.underwater_has_ground
                         && ( !ch.IsImmortal() && !ch.IsAffected( Affect.AFFECT_BREATHE_UNDERWATER )
                              && !ch.HasInnate( Race.RACE_WATERBREATH ) ) ) )
                gain = 0;

            return Math.Min( gain, ch.GetMaxHit() - ch._hitpoints );
        }

        /// <summary>
        /// Per-tick mana recovery.
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static int ManaGain( CharData ch )
        {
            int gain;
            int percent = 0;

            if( ch == null )
            {
                Log.Error( "ManaGain(): null ch", 0 );
                return 0;
            }

            if( ch.IsNPC() )
            {
                gain = 4 + ch._level / 10;
            }
            else
            {
                /* at 17 gain == base 5, at 34 gain == base 6, at 51 gain == base 7 */
                gain = 4 + ch._level / 17;
                percent = 100;

                switch( ch._position )
                {
                    case Position.sleeping:
                        percent += 100;
                        break;
                    case Position.resting:
                        percent += 50;
                        if (ch.HasActionBit(PC.PLAYER_MEDITATING))
                        {
                            percent += 50;
                            int number = MUDMath.NumberPercent();
                            if( number < ( (PC)ch ).SkillAptitude[ "meditate" ] )
                                percent += 150;
                            ch.PracticeSkill( "meditate" );
                        }
                        break;
                }

                if( ( (PC)ch ).Hunger == 0 )
                {
                    percent -= 50;
                }
                if( ( (PC)ch ).Thirst == 0 )
                {
                    percent -= 50;
                }

            }

            if( percent < 0 )
                percent = 0;

            gain = ( gain * percent ) / 100;
            if (!ch.IsClass(CharClass.Names.bard))
                gain = ( gain * ch.GetCurrPow() ) / 100;
            else
                gain = ( gain * ch.GetCurrCha() ) / 100;

            return Math.Min( gain, ch._maxMana - ch._currentMana );
        }

        /// <summary>
        /// Per-tick movement point recovery.
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static int MoveGain( CharData ch )
        {
            int gain;
            int percent = 0;

            if( ch == null )
            {
                Log.Error( "MoveGain(): null ch", 0 );
                return 0;
            }

            if( ch.IsNPC() )
            {
                gain = 4;
            }
            else
            {
                // People like fast move regen, but 10 is too fast.
                gain = 6;

                switch( ch._position )
                {
                    case Position.sleeping:
                        gain += 4;
                        break;
                    case Position.resting:
                        gain += 2;
                        break;
                }

                if( ( (PC)ch ).Hunger == 0 )
                {
                    gain /= 2;
                    percent -= 30;
                }

                if( ( (PC)ch ).Thirst == 0 )
                {
                    gain /= 2;
                    percent -= 30;
                }
                if( ch.IsAffected( Affect.AFFECT_POISON ) )
                {
                    gain /= 3;
                    percent /= 2;
                }
                if( gain == 0 )
                    if( MUDMath.NumberPercent() < percent )
                        gain = 1;
            }
            return Math.Min( gain, ch._maxMoves - ch._currentMoves );
        }


        public Object GetKey( int key )
        {
            foreach( Object obj in _carrying )
            {
                if( obj.ObjIndexData.IndexNumber == key )
                    return obj;
            }

            return null;
        }

        /// <summary>
        /// Adjust player's hunger by a certain amount and display any related messages.
        /// </summary>
        /// <param name="value"></param>
        public void AdjustHunger(int value)
        {
            PC ch = (PC)this;
            if (ch == null || value == 0 || ch._level >= Limits.LEVEL_HERO)
                return;

            ch.Hunger = Macros.Range(0, ch.Hunger + value, 48);

            if (ch.Hunger == 0)
            {
                ch.SendText("&nYou are &+Rhungry&n.\r\n");
            }
            else if (ch.Hunger <= 3)
            {
                ch.SendText("&nYou are getting &n&+rhungry&n.\r\n");
            }
            else if (ch.Hunger <= 10)
            {
                if (MUDMath.NumberBits(2) == 0)
                    ch.SendText("&nYour stomach rumbles.\r\n");
            }

        }

        /// <summary>
        /// Adjust player's thirst by a certain amount and display any related messages.
        /// </summary>
        /// <param name="value"></param>
        public void AdjustThirst(int value)
        {
            PC ch = (PC)this;
            if (ch == null || value == 0 || ch._level >= Limits.LEVEL_HERO)
            {
                return;
            }

            ch.Thirst = Macros.Range(0, ch.Thirst + value, 48);

            if (ch.Thirst == 0)
            {
                ch.SendText("&nYou are &+Cthirsty&n.\r\n");
            }
            else if (ch.Thirst <= 3)
            {
                ch.SendText("&nYou are getting &n&+cthirsty&n.\r\n");
            }
            else if (ch.Thirst <= 10)
            {
                if (MUDMath.NumberBits(2) == 0)
                {
                    ch.SendText("&nYour mouth is feeling parched.\r\n");
                }
            }

        }

        /// <summary>
        /// Adjust player's drunkenness by a certain amount and display any related messages.
        /// 
        /// Cause bards to gain mana when drinking alcohol.
        /// </summary>
        /// <param name="iCond"></param>
        /// <param name="value"></param>
        public void AdjustDrunk( int value )
        {
            PC ch = (PC)this;
            if (value == 0 || ch._level >= Limits.LEVEL_HERO)
            {
                return;
            }

            // Giving bards 3 mana per drunken unit
            if( ch.IsClass(CharClass.Names.bard ))
                ch._currentMana += value * 3;

            if( ch.HasSkill( "alcohol tolerance" ) )
            {
                if( MUDMath.NumberPercent() < ch.SkillAptitude[ "alcohol tolerance" ] )
                {
                    value /= 2;
                    ch.PracticeSkill( "alcohol tolerance" );
                    ch.SendText( "That drink wasn't nearly as strong as you had hoped.\r\n" );
                }
            }

            bool wasDrunk = (ch.Drunk > 0);
            ch.Drunk = Macros.Range(0, ch.Drunk + value, 48);

            if (wasDrunk)
            {
                if (ch.Drunk == 0 )
                {
                    ch.SendText("You are sober.\r\n");
                }
                else if (ch.Drunk <= 3)
                {
                    ch.SendText("You are nearly sober.\r\n");
                }
                else if (ch.Drunk <= 10)
                {
                    ch.SendText("You feel yourself sobering up a little.\r\n");
                }
            }

        }

        public void AttackCharacter( CharData victim )
        {
            if (!victim)
            {
                return;
            }
            victim = Combat.CheckGuarding(this, victim);
            if (Combat.IsSafe(this, victim))
            {
                return;
            }

            if( _position == Position.fighting || _fighting )
            {
                if( victim == _fighting )
                {
                    SendText( "You're doing the best you can!\r\n" );
                    return;
                }
                int chance;
                if( IsNPC() )
                {
                    chance = ( _level * 3 / 2 + 15 );
                }
                else if( HasSkill( "switch opponents" ) )
                {
                    chance = ((PC)this).SkillAptitude["switch opponents"];
                    PracticeSkill("switch opponents");
                }
                else
                {
                    chance = _level / 2;
                }
                if( MUDMath.NumberPercent() < chance )
                {
                    SendText( "You switch opponents!\r\n" );
                    SocketConnection.Act( "$n&n switches targets...", this, null, victim, SocketConnection.MessageTarget.character );
                    SocketConnection.Act( "$n&n switches targets...", this, null, victim, SocketConnection.MessageTarget.room_vict );
                    _fighting = victim;
                    WaitState(Skill.SkillList["switch opponents"].Delay);
                    return;
                }
                SendText( "You can't seem to break away from your current opponent.\r\n" );
                Combat.StopFighting( this, false );
                WaitState(Skill.SkillList["switch opponents"].Delay);
                return;
            }

            WaitState( 1 * Event.TICK_COMBAT );
            Combat.SingleAttack(this, victim, String.Empty, ObjTemplate.WearLocation.hand_one);
            return;
        }

        /// <summary>
        /// Gets the unmodified chance of performing a skill.
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        public int GetSkillChance(String skill)
        {
            if (!HasSkill(skill))
            {
                return 0;
            }

            if (IsNPC())
            {
                int chance = (((GetLevelForSkill(skill) + 1 - _level) * 2) + 15);
                if (chance > 95) chance = 95;
                return chance;
            }
            else if( ((PC)this).SkillAptitude.ContainsKey(skill))
            {
                int chance = ((PC)this).SkillAptitude[skill];
                if (chance > 95) chance = 95;
                return chance;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Equip a char with an object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="iWear">The equipment slot to place the object on.</param>
        public void EquipObject( ref Object obj, ObjTemplate.WearLocation iWear )
        {
            int aff;

            if( obj == null )
                return;

            if( Object.GetEquipmentOnCharacter( this, iWear ) )
            {
                string buf = String.Format( "CharData.EquipObject(): {0} already equipped at slot {1}.", _name, iWear );
                Log.Error( buf, 0 );
                return;
            }

            if( ( obj.HasFlag( ObjTemplate.ITEM_ANTI_EVIL ) && IsEvil() )
                    || ( obj.HasFlag( ObjTemplate.ITEM_ANTI_GOOD ) && IsGood() )
                    || ( obj.HasFlag( ObjTemplate.ITEM_ANTI_NEUTRAL ) && IsNeutral() ) )
            {
                SocketConnection.Act( "You are zapped by $p&n and drop it.", this, obj, null, SocketConnection.MessageTarget.character );
                SocketConnection.Act( "$n&n is zapped by $p&n and drops it.", this, obj, null, SocketConnection.MessageTarget.room );
                obj.RemoveFromChar();
                obj.AddToRoom( _inRoom );
                return;
            }

            _armorPoints -= Object.GetArmorClassModifer( obj, iWear );
            obj.WearLocation = iWear;
            _carryNumber--;

            foreach (Affect affect in obj.ObjIndexData.Affected)
            {
                ApplyAffectModifiers(affect, true);
            }
            foreach (Affect affect in obj.Affected)
            {
                ApplyAffectModifiers(affect, true);
            }

            if (obj.ItemType == ObjTemplate.ObjectType.light
                    && obj.Values[2] != 0 && _inRoom)
            {
                ++_inRoom.Light;
            }

            for( aff = 0; aff < Limits.NUM_AFFECT_VECTORS; aff++ )
                _affectedBy[ aff ] = _affectedBy[ aff ] | obj.AffectedBy[ aff ];

            if (!IsNPC() && _socket._terminalType == SocketConnection.TerminalType.TERMINAL_ENHANCED)
            {
                Command.Equipment(this, new string[] { "" } );
            }

            return;
        }

        /// <summary>
        /// Gives a character a full set of newbie gear.
        /// </summary>
        public void ReceiveNewbieEquipment( )
        {
            int count;

            int indexNumber = MUDMath.NumberRange( StaticObjects.OBJECT_NUMBER_NEWBIE_VEST,
                                            ( StaticObjects.OBJECT_NUMBER_NEWBIE_VEST + StaticObjects.NUM_NEWBIE_VEST - 1 ) );
            NewbieObjToChar( indexNumber, ObjTemplate.WearLocation.body );

            indexNumber = MUDMath.NumberRange( StaticObjects.OBJECT_NUMBER_NEWBIE_HELM,
                                ( StaticObjects.OBJECT_NUMBER_NEWBIE_HELM + StaticObjects.NUM_NEWBIE_HELM - 1 ) );
            NewbieObjToChar( indexNumber, ObjTemplate.WearLocation.head  );

            indexNumber = MUDMath.NumberRange( StaticObjects.OBJECT_NUMBER_NEWBIE_SLEEVES,
                                ( StaticObjects.OBJECT_NUMBER_NEWBIE_SLEEVES + StaticObjects.NUM_NEWBIE_SLEEVES - 1 ) );
            NewbieObjToChar(indexNumber, ObjTemplate.WearLocation.arms );

            if( GetOrigRace() != Race.RACE_CENTAUR )
            {
                indexNumber = MUDMath.NumberRange( StaticObjects.OBJECT_NUMBER_NEWBIE_PANTS,
                                    ( StaticObjects.OBJECT_NUMBER_NEWBIE_PANTS + StaticObjects.NUM_NEWBIE_PANTS - 1 ) );
                NewbieObjToChar( indexNumber, ObjTemplate.WearLocation.legs );
            }

            if( GetOrigRace() != Race.RACE_CENTAUR )
            {
                indexNumber = MUDMath.NumberRange( StaticObjects.OBJECT_NUMBER_NEWBIE_BOOTS,
                                    ( StaticObjects.OBJECT_NUMBER_NEWBIE_BOOTS + StaticObjects.NUM_NEWBIE_BOOTS - 1 ) );
                NewbieObjToChar( indexNumber, ObjTemplate.WearLocation.feet );
            }

            indexNumber = MUDMath.NumberRange( StaticObjects.OBJECT_NUMBER_NEWBIE_CLOAK,
                                ( StaticObjects.OBJECT_NUMBER_NEWBIE_CLOAK + StaticObjects.NUM_NEWBIE_CLOAK - 1 ) );
            NewbieObjToChar( indexNumber, ObjTemplate.WearLocation.about_body );

            NewbieObjToChar(_charClass.FirstWeapon, ObjTemplate.WearLocation.hand_one );

            for( count = 0; count < 5; ++count )
            {
                NewbieObjToChar( StaticObjects.OBJECT_NUMBER_IRON_RATION, ObjTemplate.WearLocation.none );
            }

            NewbieObjToChar( StaticObjects.OBJECT_NUMBER_WATERSKIN, ObjTemplate.WearLocation.none  );

            if( IsClass(CharClass.Names.sorcerer ))
            {
                NewbieObjToChar( StaticObjects.OBJECT_NUMBER_QUILL, ObjTemplate.WearLocation.none  );
            }

            if( IsClass(CharClass.Names.shaman) )
            {
                NewbieObjToChar( StaticObjects.OBJECT_NUMBER_NEWBIE_TOTEM_A, ObjTemplate.WearLocation.none );
                NewbieObjToChar( StaticObjects.OBJECT_NUMBER_NEWBIE_TOTEM_E, ObjTemplate.WearLocation.none );
                NewbieObjToChar( StaticObjects.OBJECT_NUMBER_NEWBIE_TOTEM_S, ObjTemplate.WearLocation.none );
            }

            // Objects for Humans
            if( GetOrigRace() == Race.RACE_HUMAN )
            {
                NewbieObjToChar( 60038, ObjTemplate.WearLocation.none );
                NewbieObjToChar( 60053, ObjTemplate.WearLocation.none );
            }

            if( !HasInnate( Race.RACE_ULTRAVISION ) )
            {
                NewbieObjToChar( StaticObjects.OBJECT_NUMBER_TORCH, ObjTemplate.WearLocation.none );
                NewbieObjToChar( StaticObjects.OBJECT_NUMBER_TORCH, ObjTemplate.WearLocation.none );
                NewbieObjToChar( StaticObjects.OBJECT_NUMBER_TORCH, ObjTemplate.WearLocation.none );
            }

            return;
        }

        /// <summary>
        /// This function is used to keep stats in line in case they get buggy.
        /// It should be called when a player enters the game or when a player is "reset"
        /// by an immortal.
        /// </summary>
        public void ResetStats()
        {
            _groupLeader = null;
            _nextInGroup = null;
            _hitroll = 0;
            _damroll = 0;
            _armorPoints = 100;
            _savingThrows[0] = 0;
            _savingThrows[1] = 0;
            _savingThrows[2] = 0;
            _savingThrows[3] = 0;
            _savingThrows[4] = 0;
            _modifiedStrength = 0;
            _modifiedIntelligence = 0;
            _modifiedWisdom = 0;
            _modifiedDexterity = 0;
            _modifiedConstitution = 0;
            _modifiedAgility = 0;
            _modifiedCharisma = 0;
            _modifiedPower = 0;
            _modifiedLuck = 0;
            _resistant = Race.DamageType.none;
            _immune = Race.DamageType.none;
            _susceptible = Race.DamageType.none;
            _vulnerable = Race.DamageType.none;
            if (!IsNPC())
            {
                ((PC)this).MaxStrMod = 0;
                ((PC)this).MaxIntMod = 0;
                ((PC)this).MaxWisMod = 0;
                ((PC)this).MaxDexMod = 0;
                ((PC)this).MaxConMod = 0;
                ((PC)this).MaxAgiMod = 0;
                ((PC)this).MaxChaMod = 0;
                ((PC)this).MaxPowMod = 0;
                ((PC)this).MaxLukMod = 0;
                ((PC)this).HitpointModifier = 0;
            }
            _affectedBy = new int[Limits.NUM_AFFECT_VECTORS];
            _carryNumber = 0;
            foreach (Object obj3 in _carrying)
            {
                if (obj3.WearLocation == ObjTemplate.WearLocation.none)
                    _carryNumber++;
            }
            foreach (ObjTemplate.WearLocation pos in Enum.GetValues(typeof(ObjTemplate.WearLocation)))
            {
                Object obj = Object.GetEquipmentOnCharacter(this, pos);
                if (!obj)
                    continue;
                _armorPoints -= Object.GetArmorClassModifer(obj, pos);
                int count; 
                for (count = 0; count < Limits.NUM_AFFECT_VECTORS; ++count)
                {
                    Macros.SetBit(ref _affectedBy[count], obj.AffectedBy[count]);
                }
                if (obj.ObjIndexData)
                {
                    foreach (Affect affect in obj.ObjIndexData.Affected)
                    {
                        for (count = 0; count < Limits.NUM_AFFECT_VECTORS; ++count)
                        {
                            Macros.SetBit(ref _affectedBy[count], affect.BitVectors[count]);
                        }
                        ApplyAffectModifiers(affect);
                    }
                }
                else
                {
                    Log.Trace("Object " + obj.Name + " has no index data.  Origin zone may not be loaded.");
                }
                foreach (Affect affect in obj.Affected)
                {
                    for (count = 0; count < Limits.NUM_AFFECT_VECTORS; ++count)
                    {
                        Macros.SetBit(ref _affectedBy[count], affect.BitVectors[count]);
                    }
                    ApplyAffectModifiers(affect);
                }
            }
            foreach (Affect affect in _affected)
            {
                ApplyAffectModifiers(affect);
            } //end for
        }

        public void ApplyAffectModifiers(Affect affect)
        {
            foreach (AffectApplyType apply in affect.Modifiers)
            {
                int mod = apply.Amount;
                switch (apply.Location)
                {
                    default:
                        break;
                    case Affect.Apply.none:
                        break;
                    case Affect.Apply.strength:
                        _modifiedStrength += mod;
                        break;
                    case Affect.Apply.dexterity:
                        _modifiedDexterity += mod;
                        break;
                    case Affect.Apply.intelligence:
                        _modifiedIntelligence += mod;
                        break;
                    case Affect.Apply.wisdom:
                        _modifiedWisdom += mod;
                        break;
                    case Affect.Apply.constitution:
                        {
                            _modifiedConstitution += mod;
                            break;
                        }
                    case Affect.Apply.agility:
                        _modifiedAgility += mod;
                        break;
                    case Affect.Apply.charisma:
                        _modifiedCharisma += mod;
                        break;
                    case Affect.Apply.power:
                        _modifiedPower += mod;
                        break;
                    case Affect.Apply.luck:
                        _modifiedLuck += mod;
                        break;
                    case Affect.Apply.age:
                        break;
                    case Affect.Apply.height:
                        break;
                    case Affect.Apply.weight:
                        break;
                    case Affect.Apply.hitpoints:
                        if (!IsNPC())
                        {
                            ((PC)this).HitpointModifier += mod;
                        }
                        else
                        {
                            _maxHitpoints += mod;
                        }
                        break;
                    case Affect.Apply.ac:
                        _armorPoints += mod;
                        break;
                    case Affect.Apply.hitroll:
                        _hitroll += mod;
                        break;
                    case Affect.Apply.damroll:
                        _damroll += mod;
                        break;
                    case Affect.Apply.save_paralysis:
                        _savingThrows[0] += mod;
                        break;
                    case Affect.Apply.save_poison:
                        _savingThrows[1] += mod;
                        break;
                    case Affect.Apply.save_petrification:
                        _savingThrows[2] += mod;
                        break;
                    case Affect.Apply.save_breath:
                        _savingThrows[3] += mod;
                        break;
                    case Affect.Apply.save_spell:
                        _savingThrows[4] += mod;
                        break;
                    case Affect.Apply.fire_protection:
                        break;
                    case Affect.Apply.max_strength:
                        if (!IsNPC())
                            ((PC)this).MaxStrMod += mod;
                        else
                            _modifiedStrength += mod;
                        break;
                    case Affect.Apply.max_dexterity:
                        if (!IsNPC())
                            ((PC)this).MaxDexMod += mod;
                        else
                            _modifiedDexterity += mod;
                        break;
                    case Affect.Apply.max_intelligence:
                        if (!IsNPC())
                            ((PC)this).MaxIntMod += mod;
                        else
                            _modifiedIntelligence += mod;
                        break;
                    case Affect.Apply.max_wisdom:
                        if (!IsNPC())
                            ((PC)this).MaxWisMod += mod;
                        else
                            _modifiedWisdom += mod;
                        break;
                    case Affect.Apply.max_constitution:
                        if (!IsNPC())
                        {
                            ((PC)this).MaxConMod += mod;
                        }
                        else
                        {
                            _modifiedConstitution += mod;
                            break;
                        }
                        break;
                    case Affect.Apply.max_agility:
                        if (!IsNPC())
                            ((PC)this).MaxAgiMod += mod;
                        else
                            _modifiedAgility += mod;
                        break;
                    case Affect.Apply.max_power:
                        if (!IsNPC())
                            ((PC)this).MaxPowMod += mod;
                        else
                            _modifiedPower += mod;
                        break;
                    case Affect.Apply.max_charisma:
                        if (!IsNPC())
                            ((PC)this).MaxChaMod += mod;
                        else
                            _modifiedCharisma += mod;
                        break;
                    case Affect.Apply.max_luck:
                        if (!IsNPC())
                            ((PC)this).MaxLukMod += mod;
                        else
                            _modifiedLuck += mod;
                        break;
                    case Affect.Apply.race_strength:
                    case Affect.Apply.race_dexterity:
                    case Affect.Apply.race_intelligence:
                    case Affect.Apply.race_wisdom:
                    case Affect.Apply.race_constitution:
                    case Affect.Apply.race_agility:
                    case Affect.Apply.race_power:
                    case Affect.Apply.race_charisma:
                    case Affect.Apply.race_luck:
                    case Affect.Apply.curse:
                        break;
                    case Affect.Apply.resistant:
                        {
                            int val = (int)_resistant;
                            Macros.SetBit(ref val, mod);
                            if (val < 0)
                                break;
                            _resistant = (Race.DamageType)val;
                            break;
                        }
                    case Affect.Apply.immune:
                        {
                            int val = (int)_immune;
                            Macros.SetBit(ref val, mod);
                            if (val < 0)
                                break;
                            _immune = (Race.DamageType)val;
                            break;
                        }
                    case Affect.Apply.susceptible:
                        {
                            int val = (int)_susceptible;
                            Macros.SetBit(ref val, mod);
                            if (val < 0)
                                break;
                            _susceptible = (Race.DamageType)val;
                            break;
                        }
                    case Affect.Apply.vulnerable:
                        {
                            int val = (int)_vulnerable;
                            Macros.SetBit(ref val, mod);
                            if (val < 0)
                                break;
                            _vulnerable = (Race.DamageType)val;
                            break;
                        }
                    case Affect.Apply.race:
                        // Added size adjustment so when you change races higher or lower
                        // you are adjusted proportionally.  We don't just set the race to default
                        // size because the person may be enlarged or reduced and will be hosed when
                        // the enlarge or reduce affect wears off.
                        _size -= (Race.RaceList[GetOrigRace()].DefaultSize - Race.RaceList[(GetOrigRace() + mod)].DefaultSize);
                        SetPermRace(GetRace() + mod);
                        break;
                } // End switch.
            } // End modifier loop.
            return;
        }

        /// <summary>
        /// Handles showing contents when you look inside a container.
        /// </summary>
        /// <param name="arg2"></param>
        /// <param name="checkCanLook"></param>
        public void LookInContainer(string arg2, bool checkCanLook)
        {
            CharData ch = this;
            string text;

            /* 'look in' */
            if (String.IsNullOrEmpty(arg2))
            {
                ch.SendText("Look in what?\r\n");
                return;
            }

            Object obj = ch.GetObjHere(arg2);
            if (!obj)
            {
                ch.SendText("You do not see that here.\r\n");
                return;
            }

            switch (obj.ItemType)
            {
                default:
                    ch.SendText("That is not a container.\r\n");
                    break;

                case ObjTemplate.ObjectType.drink_container:
                    if (obj.Values[1] == -1)
                    {
                        ch.SendText("It is full.\r\n");
                        break;
                    }

                    if (obj.Values[1] <= 0)
                    {
                        ch.SendText("It is empty.\r\n");
                        break;
                    }

                    // TODO: Fix this, it's obviously been written by an idiot.
                    text = String.Format("It's {0} full of a {1} liquid.\r\n", obj.Values[1] < obj.Values[0] / 4
                             ? "less than half" : obj.Values[1] < 3 * obj.Values[0] / 4 ? "about half" : obj.Values[1] < obj.Values[0]
                             ? "more than half" : "completely", Liquid.Table[obj.Values[2]].Color);

                    ch.SendText(text);
                    break;

                case ObjTemplate.ObjectType.quiver:
                case ObjTemplate.ObjectType.container:
                case ObjTemplate.ObjectType.npc_corpse:
                case ObjTemplate.ObjectType.pc_corpse:
                case ObjTemplate.ObjectType.storage_chest:
                    if (Macros.IsSet(obj.Values[1], ObjTemplate.CONTAINER_CLOSED.Vector))
                    {
                        ch.SendText("It is closed.\r\n");
                        break;
                    }

                    SocketConnection.Act("$p&n contains:", ch, obj, null, SocketConnection.MessageTarget.character, true);
                    Look.ShowListToCharacter(obj.Contains, ch, true, true);
                    break;
                case ObjTemplate.ObjectType.portal:
                    SocketConnection.Act("A $p&n leads to:", ch, obj, null, SocketConnection.MessageTarget.character);
                    text = String.Format("{0}\r\n{1}\r\n", Room.GetRoom(obj.Values[0]).Title, Room.GetRoom(obj.Values[0]).Description);
                    ch.SendText(text);
                    break;
            }
        }

        /// <summary>
        /// Adds an innate timer to prevent immediate reuse of an innate ability.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="timer"></param>
        public void AddInnateTimer(InnateTimerData.Type type, int timer)
        {
            if (IsNPC())
                return;
            if (HasInnateTimer(type))
                return;
            InnateTimerData itd = new InnateTimerData();

            itd.AbilityType = type;
            itd.Timer = timer;
            itd.Who = this;
            ((PC)this).InnateTimers.Add(itd);
        }

        /// <summary>
        /// Creates a level 1 object, flags it nosell, and gives it to the character.  Used
        /// for newbie equipment.
        /// </summary>
        /// <param name="indexNumber"></param>
        /// <param name="wearLocation"></param>
        public void NewbieObjToChar( int indexNumber, ObjTemplate.WearLocation wearLocation )
        {
            if( indexNumber == 0 )
            {
                return;
            }
            ObjTemplate index = Database.GetObjTemplate( indexNumber );
            if( index == null )
            {
                return;
            }
            Object obj = Database.CreateObject( index, 1 );
            if( obj != null )
            {
                obj.AddFlag( ObjTemplate.ITEM_NOSELL );
                obj.ObjToChar( this );
            }
            if( wearLocation != ObjTemplate.WearLocation.none )
            {
                EquipObject( ref obj, wearLocation );
            }
        }

        public static void AddFollower( CharData ch, CharData master )
        {
            if( !master )
            {
                Log.Error( "AddFollower: Called with no argument for master.", 0 );
                return;
            }

            if( ch._master != null )
            {
                Log.Error( "AddFollower: Follower character " + ch._name + " already has non-null master.", 0 );
                return;
            }

            CharData follower = new CharData();

            ch._master = master;

            // Put the follower at the top of the list and set the
            // previous of the old one to point to it
            if (master._followers == null)
            {
                master._followers = new List<CharData>();
            }
            master._followers.Add( follower );

            if( CanSee( master, ch ) )
                SocketConnection.Act( "$n&n now follows you.", ch, null, master, SocketConnection.MessageTarget.victim );

            SocketConnection.Act( "You now follow $N&n.", ch, null, master, SocketConnection.MessageTarget.character );

            return;
        }

        /// <summary>
        /// Adds a follower without displaying a message to anyone.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="master"></param>
        public static void AddFollowerWithoutMessage( CharData ch, CharData master )
        {
            if( !master )
            {
                Log.Error("AddFollowerWithoutMessage method called with no argument for master.\r\n", 0);
                return;
            }

            if( ch._master )
            {
                Log.Error("AddFollowerWithoutMessage: non-null master.", 0);
                return;
            }

            CharData follower = new CharData();

            ch._master = master;
            ch._groupLeader = null;

            // Put the follower at the top of the list and set the
            // previous of the old one to point to it
            if (master._followers == null)
                master._followers = new List<CharData>();
            master._followers.Add( follower );

            return;
        }

        static public void StopFollower( CharData ch )
        {
            if( !ch )
            {
                Log.Error("StopFollower called with no CH argument.\r\n", 0);
                return;
            }

            if( !ch._master )
            {
                Log.Error("StopFollower: null master.", 0);
                return;
            }

            if( ch.IsAffected( Affect.AFFECT_CHARM ) )
            {
                ch.RemoveAffect(Affect.AFFECT_CHARM);
                ch.AffectStrip( Affect.AffectType.spell, "domination");
            }

            if( ch._master != ch && CanSee( ch._master, ch ) && ch._master._inRoom )
                SocketConnection.Act( "$n&n stops following you.", ch, null, ch._master, SocketConnection.MessageTarget.victim );
            if( ch._inRoom )
                SocketConnection.Act( "You stop following $N&n.", ch, null, ch._master, SocketConnection.MessageTarget.character );

            // Remove the follower from the list of followers
            foreach( CharData follower in ch._master._followers )
            {
                if( follower == ch )
                {
                    ch._master._followers.Remove( follower );
                }
            }

            ch._master = null;

            return;
        }

        /// <summary>
        /// Check whether a character is affected by a particular affect vector.
        /// </summary>
        /// <param name="bvect"></param>
        /// <returns></returns>
        public bool IsAffected(Bitvector bvect)
        {
            if (Macros.IsSet(_affectedBy[bvect.Group], bvect.Vector))
            {
                return true;
            }
            foreach (Affect affect in _affected)
            {
                if (affect.HasBitvector(bvect))
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckSkill(String skillName)
        {
            return CheckSkill(skillName, 0, PracticeType.normal);
        }

        public bool CheckSkill(String skillName, int modifier)
        {
            return CheckSkill(skillName, modifier, PracticeType.normal);
        }

        public bool CheckSkill(String skillName, PracticeType practiceType)
        {
            return CheckSkill(skillName, 0, practiceType);
        }

        /// <summary>
        /// Makes a skill check with the specified modifier and automatically practices the skill in the process.
        /// </summary>
        /// <param name="skillName"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public bool CheckSkill(String skillName, int modifier, PracticeType practiceType)
        {
            if (!IsNPC())
            {
                PC pc = (PC)this;
                if (!HasSkill(skillName))
                {
                    return false;
                }
                bool success = (MUDMath.NumberPercent() < pc.SkillAptitude[skillName]);
                if (practiceType != PracticeType.only_on_success || success)
                {
                    PracticeSkill(skillName, practiceType);
                }
                return success;
            }
            else
            {
                if (!HasSkill(skillName))
                {
                    return false;
                }
                int chance = GetSkillChance(skillName);
                if (MUDMath.NumberPercent() < chance)
                {
                    return true;
                }
                return false;

            }
        }

        public void DieFollower( string name )
        {
            CharData ch = this;

            if (ch._master)
            {
                Combat.StopFighting(ch, true);
            }

            if( ch._groupLeader )
            {
                ch.RemoveFromGroup( ch );
            }

            foreach (CharData listChar in Database.CharList)
            {
                if( listChar._master == ch )
                    Combat.StopFighting( listChar, true );
                if( listChar._groupLeader == ch )
                    listChar._groupLeader = null;
            }

            return;
        }

        /// <summary>
        /// Find a character in the game world based on a _name/keyword.
        /// </summary>
        /// <param name="argument"></param>
        /// <returns></returns>
        public CharData GetCharWorld(string argument)
        {
            string arg = String.Empty;

            CharData roomChar = GetCharRoom(argument);
            if (roomChar != null)
            {
                return roomChar;
            }

            int number = MUDString.NumberArgument(argument, ref arg);
            int count = 0;
            foreach (CharData worldChar in Database.CharList)
            {
                // look only at PCs first
                if (worldChar.IsNPC())
                    continue;
                if (IsRacewar(worldChar))
                    continue;
                if (!CanSee(this, worldChar))
                    continue;

                if (!worldChar.IsNPC() && (IsRacewar(worldChar)) && !IsImmortal()
                        && !worldChar.IsImmortal())
                {
                    if (!MUDString.NameContainedIn(arg, Race.RaceList[worldChar.GetRace()].Name))
                        continue;
                }
                else
                {
                    if (!MUDString.NameContainedIn(arg, worldChar._name))
                        continue;
                }

                if (++count == number)
                    return worldChar;
            }

            // Now loop for all the NPCs.
            foreach (CharData worldChar in Database.CharList)
            {
                if (!worldChar.IsNPC())
                    continue;
                if (!CanSee(this, worldChar))
                    continue;
                if (MUDString.NameContainedIn(arg, worldChar._name))
                    return worldChar;
            }

            return null;
        }

        /// <summary>
        /// Find a char in the same room as the calling CharData, based on a _name/keyworrd.
        /// "me" and "self" are aliases for targeting one's self.
        /// </summary>
        /// <param name="argument"></param>
        /// <returns></returns>
        public CharData GetCharRoom( string argument )
        {
            if (argument.Equals("self") || argument.Equals("me"))
            {
                return this;
            }

            if( _inRoom == null )
            {
                return null;
            }

            string arg = String.Empty;
            int number = MUDString.NumberArgument(argument, ref arg);
            int count = 0;

            foreach( CharData roomChar in _inRoom.People )
            {
                if( !CanSee( this, roomChar ) || _flyLevel != roomChar._flyLevel )
                    continue;

                if( !roomChar.IsNPC() && IsRacewar( roomChar ) && !IsImmortal()
                        && !roomChar.IsImmortal() && !IsNPC() )
                {
                    if( !MUDString.NameContainedIn( arg, Race.RaceList[ roomChar.GetRace() ].Name ) )
                        continue;
                }
                else
                {
                    if( !MUDString.NameContainedIn( arg, roomChar._name ) )
                        continue;
                }

                if( ++count == number )
                    return roomChar;
            }

            return null;
        }

        /// <summary>
        /// Find a char that is in the same area as the calling CharData, based on a _name/keyword string.
        /// </summary>
        /// <param name="argument"></param>
        /// <returns></returns>
        public CharData GetCharInArea( string argument )
        {
            CharData roomChar = GetCharRoom( argument );
            if( roomChar != null )
            {
                return roomChar;
            }

            if (!_inRoom || _inRoom.Area == null)
            {
                return null;
            }

            string arg = String.Empty;
            int number = MUDString.NumberArgument(argument, ref arg);
            int count = 0;
            foreach( CharData ach in Database.CharList )
            {
                if( !ach._inRoom || ach._inRoom.Area != _inRoom.Area || !CanSee( this, ach ) )
                    continue;

                if( !ach.IsNPC() && ( IsRacewar( ach ) ) && !IsImmortal() )
                {
                    if( !MUDString.NameContainedIn( arg, Race.RaceList[ ach.GetRace() ].Name ) &&
                            !IsRacewar( ach ) )
                        continue;
                }
                else
                {
                    if( !MUDString.NameContainedIn( arg, ach._name ) )
                        continue;
                }
                if( ++count == number )
                    return ach;
            }

            return null;
        }

        /// <summary>
        /// Finds an object in the char's inventory based on a _name/keyword.
        /// </summary>
        /// <param name="argument"></param>
        /// <returns></returns>
        public Object GetObjCarrying( string argument )
        {
            string arg = String.Empty;

            if( argument.Length == 0 )
            {
                return null;
            }

            int number = MUDString.NumberArgument( argument, ref arg );
            int count = 0;
            foreach( Object obj in _carrying )
            {
                if( obj.WearLocation == ObjTemplate.WearLocation.none && CanSeeObj( this, obj ) && ( MUDString.NameContainedIn( arg, obj.Name )
                   || !String.IsNullOrEmpty(Database.GetExtraDescription( arg, obj.ExtraDescription ))
                   || !String.IsNullOrEmpty(Database.GetExtraDescription(arg, obj.ObjIndexData.ExtraDescriptions))))
                {
                    if( ++count == number )
                    {
                        return obj;
                    }
                }
            }

            count = 0;
            foreach( Object obj in _carrying )
            {
                if( obj.WearLocation == ObjTemplate.WearLocation.none && CanSeeObj( this, obj ) && MUDString.NameIsPrefixOfContents( arg, obj.Name ) )
                {
                    if( ++count == number )
                    {
                        return obj;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Lets us use boolean operator to check for null.
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static bool operator !( CharData ch )
        {
            if (ch == null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Lets us use boolean operator to check for null.
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static implicit operator bool( CharData ch )
        {
            if (ch == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Updates a character's position.
        /// </summary>
        public void UpdatePosition()
        {
            if( _hitpoints < -10 && _inRoom != null )
            {
                if( _riding != null && _inRoom == _riding._inRoom )
                {
                    SocketConnection.Act( "$n&n unceremoniously falls from $N&n.", this, null, _riding, SocketConnection.MessageTarget.room );
                    _riding._rider = null;
                    _riding = null;
                }

                _position = Position.dead;
                
                return;
            }

            if( _hitpoints > 0 )
            {
                if( _position <= Position.stunned )
                    _position = Position.resting;
            }
            else
            {
                if( _hitpoints <= -6 )
                    _position = Position.mortally_wounded;
                else if( _hitpoints <= -3 )
                    _position = Position.incapacitated;
                else
                    _position = Position.stunned;

                if( _riding != null )
                {
                    SocketConnection.Act( "$n&n falls unconscious from $N&n.", this, null, this._riding, SocketConnection.MessageTarget.room );
                    _riding._rider = null;
                    _riding = null;
                }
            }
            // If is an NPC just return now.
            if( IsNPC() )
                return;

            // Passing out.
            if( !IsNPC() && !IsImmortal()
                    && ( GetCurrCon() - ( (PC)this ).Drunk < MUDMath.NumberPercent() )
                    && ((!IsClass(CharClass.Names.bard) && ((PC)this).Drunk > 22)
                         || ( IsClass(CharClass.Names.bard) && ( (PC)this ).Drunk > 32 ) ) )
            {
                WaitState( 5 * Event.TICK_PER_SECOND );
                ( (PC)this ).Drunk -= 3;
                if( _position > Position.sleeping )
                {
                    SendText( "&+WOh, you &n&+wfeel &+Lreally&N&+l bad...&n\r\n" );
                    _position = Position.sleeping;
                    SocketConnection.Act( "$n&n falls flat on $s face, immobile&n.",
                         this, null, null, SocketConnection.MessageTarget.room );
                }
            }
            if( !IsNPC() && !IsImmortal() && _currentMana < 0 )
            {
                WaitState( 5 * Event.TICK_PER_SECOND );
                if( _position > Position.sleeping )
                {
                    SendText( "&+WYou &n&+wfeel &+Lexhau&n&+lsted...&n\r\n" );
                    _position = Position.standing;
                    _fighting = null;
                    SocketConnection.Act( "$n&n collapses from exhaustion&n.",
                         this, null, null, SocketConnection.MessageTarget.room );
                    Command.Sleep( this, null );
                }
            }

            return;
        }

        /// <summary>
        /// Displays this object as a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _name;
        }

        public bool IsClass(CharClass.Names name)
        {
            if (_charClass.ClassNumber == name)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// True if char can see victim.
        ///
        /// This is only a straightford all-or-none vision checker.
        ///
        /// If you need more granularity, use Command.HowSee which returns an enum
        /// based on the level of visibility but otherwise functions similarly.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="victim"></param>
        /// <returns></returns>
        public static bool CanSee( CharData ch, CharData victim )
        {
            if( ch == null )
            {
                Log.Error( "CharData.CanSee: called with null ch.", 0 );
                return false;
            }

            if( victim == null )
            {
                Log.Error( "CharData.CanSee: called with null victim.", 0 );
                return false;
            }

            if( ch == victim )
            {
                return true;
            }

            /* All mobiles cannot see wizinvised immorts */
            if (ch.IsNPC() && !ch.IsNPC() && ch.HasActionBit(PC.PLAYER_WIZINVIS))
            {
                return false;
            }

            if (!ch.IsNPC() && ch.HasActionBit(PC.PLAYER_WIZINVIS) && ch.GetTrust() < ch._level)
            {
                return false;
            }

            if (!ch.IsNPC() && ch.HasActionBit(PC.PLAYER_GODMODE))
            {
                return true;
            }

            if( ch.IsAffected( Affect.AFFECT_BLIND ) )
            {
                return false;
            }

            if (ch._inRoom == null)
            {
                Log.Error("CanSee called by player " + ch._name + " with null room.");
                return false;
            }

            if( ch._inRoom.IsDark() && !ch.HasInnate( Race.RACE_ULTRAVISION )
                    && !ch.IsAffected( Affect.AFFECT_ULTRAVISION ) && !ch.HasInnate( Race.RACE_INFRAVISION )
                    && !ch.IsAffected(Affect.AFFECT_INFRAVISION ) )
            {
                return false;
            }

            if( ch._position == Position.dead )
            {
                return true;
            }

            if ((victim.IsAffected(Affect.AFFECT_INVISIBLE) || victim.IsAffected(Affect.AFFECT_MINOR_INVIS))
                    && !ch.HasInnate( Race.RACE_DETECT_INVIS ) && !ch.IsAffected(Affect.AFFECT_DETECT_INVIS )
                    && !(ch.IsAffected(Affect.AFFECT_ELEM_SIGHT) &&
                          ( ch.GetRace() == Race.RACE_AIR_ELE || ch.GetRace() == Race.RACE_WATER_ELE
                            || ch.GetRace() == Race.RACE_FIRE_ELE || ch.GetRace() == Race.RACE_EARTH_ELE ) ) )
            {
                return false;
            }

            if( victim.IsAffected( Affect.AFFECT_HIDE ) && !ch.HasInnate( Race.RACE_DETECT_HIDDEN )
                    && !ch.IsAffected( Affect.AFFECT_DETECT_HIDDEN ) && !ch._fighting )
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks whether a character can see an objects.  Returns true if yes, false if no.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool CanSeeObj( CharData ch, Object obj )
        {
            if( obj == null )
            {
                Log.Error("Calling CanSeeObj with no obj data!", 0);
                return false;
            }

            if( ch == null )
            {
                Log.Error("Calling CanSeeObj with no CharData!", 0);
                return false;
            }

            if (!ch.IsNPC() && ch.HasActionBit(PC.PLAYER_GODMODE))
                return true;

            if( ch.IsAffected( Affect.AFFECT_BLIND ) || ch._position <= Position.sleeping )
                return false;

            if( obj.ItemType == ObjTemplate.ObjectType.light && obj.Values[ 2 ] != 0 )
                return true;

            if( obj.HasFlag( ObjTemplate.ITEM_SECRET )
                    && !( ch.HasInnate( Race.RACE_DETECT_HIDDEN )
                         || ch.IsAffected( Affect.AFFECT_DETECT_HIDDEN ) ) )
                return false;

            if( obj.HasFlag( ObjTemplate.ITEM_INVIS )
                    && !ch.HasInnate( Race.RACE_DETECT_INVIS )
                    && !ch.IsAffected( Affect.AFFECT_DETECT_INVIS ) )
                return false;

            if( obj.HasFlag( ObjTemplate.ITEM_LIT ) )
                return true;

            if( ch._inRoom && ch._inRoom.IsDark() && !( ch.HasInnate( Race.RACE_ULTRAVISION )
                         || ch.IsAffected( Affect.AFFECT_ULTRAVISION ) ) )
                return false;

            return true;
        }

        /// <summary>
        /// Checks whether a character can drop an object.  Returns true if it can be dropped, false if not.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool CanDropObject( Object obj )
        {
            
            if( !obj.HasFlag( ObjTemplate.ITEM_NODROP ) )
                return true;

            if( !IsNPC() && _level >= Limits.LEVEL_AVATAR )
                return true;

            return false;
        }

        public bool StringTooLong( string argument )
        {
            if( argument.Length > 60 )
            {
                SendText( "No more than 60 characters in this field.\r\n" );
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks whether the character is authorized to use a particular immortal skill.
        /// </summary>
        /// <param name="skllnm"></param>
        /// <returns></returns>
        public bool Authorized( string skllnm )
        {
            if( ( !IsNPC() && ((PC)this).ImmortalData != null ))
            {
                bool result = ((PC)this).ImmortalData.Authorized(skllnm);
                if (!result)
                {
                    SendText(String.Format("Sorry, you are not authorized to use {0}.\r\n", skllnm));
                }
                return result;
            }

            return false;
        }

        public bool HasInnate( Bitvector bit )
        {
            int race = GetRace();

            if( Macros.IsSet( Race.RaceList[ race ].InnateAbilities[ bit.Group ], bit.Vector ) )
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the maximum number of pets a player can have, influenced by charisma.
        /// </summary>
        /// <returns></returns>
        public bool MaxPets( )
        {
            CharData fol;

            int numPets = 0;
            foreach( CharData it in Database.CharList )
            {
                fol = it;
                if( fol._master == this && fol.IsNPC() )
                {
                    numPets++;
                }
            }
            int maxNum = 3;
            if( GetCurrCha() > 75 )
                maxNum += ( GetCurrCha() - 75 ) / 10 + 1;
            if( numPets >= maxNum )
                return true;
            return false;
        }

        /// <summary>
        /// Finds a character in an arbitrary room.
        /// </summary>
        /// <param name="room"></param>
        /// <param name="ch"></param>
        /// <param name="argument"></param>
        /// <returns></returns>
        public static CharData GetCharAtRoom( Room room, CharData ch, string argument )
        {
            string arg = String.Empty;

            int number = MUDString.NumberArgument( argument, ref arg );
            int count = 0;
            foreach( CharData roomChar in ch._inRoom.People )
            {
                if( ch._flyLevel != roomChar._flyLevel )
                    continue;

                if( !roomChar.IsNPC() && ( ch.IsRacewar( roomChar ) ) && !ch.IsImmortal() )
                {
                    if( !MUDString.NameContainedIn( arg, Race.RaceList[ roomChar.GetRace() ].Name ) )
                        continue;
                }
                else
                {
                    if( !MUDString.NameContainedIn( arg, roomChar._name ) )
                        continue;
                }
                if (++count == number)
                {
                    return roomChar;
                }
            }

            return null;
        }

        /// <summary>
        /// Decrements light as necessary if a player leaves a room or increments it as necessary if
        /// a player enters a room.
        /// </summary>
        /// <param name="entering">True if the player is entering the room, false if they are leaving it.</param>
        void MoveLight( bool entering )
        {
            if( !entering && _inRoom == null )
            {
                // Not a problem - there's just no need to subtract the char's light from a room
                // if they're not leaving a room.
                return;
            }

            foreach( Object carryObj in _carrying )
            {
                if( carryObj.HasFlag( ObjTemplate.ITEM_LIT ) )
                {
                    if( entering )
                        _inRoom.Light++;
                    else
                        _inRoom.Light--;
                }
            }

            /* If they're holding a light source. */
            Object obj = Object.GetEquipmentOnCharacter( this, ObjTemplate.WearLocation.hand_one );
            if( obj && obj.ItemType == ObjTemplate.ObjectType.light && obj.Values[ 2 ] != 0 )
            {
                if( entering )
                {
                    _inRoom.Light++;
                }
                else
                {
                    if( _inRoom.Light > 0 )
                    {
                        --_inRoom.Light;
                    }
                    else
                    {
                        string error = "Trying to decrement 0-value light in room " + _inRoom.IndexNumber + ".";
                        Log.Error( error, 0 );
                    }
                }
            }

            /* If they're holding a light source in 2ndary hand. */
            Object obj4 = Object.GetEquipmentOnCharacter( this, ObjTemplate.WearLocation.hand_two );
            if( obj4 && obj4.ItemType == ObjTemplate.ObjectType.light && obj4.Values[ 2 ] != 0 )
            {
                if( entering )
                    _inRoom.Light++;
                else
                {
                    if( _inRoom.Light > 0 )
                        --_inRoom.Light;
                    else
                    {
                        string error = "Trying to decrement 0-value light in room " + _inRoom.IndexNumber + ".";
                        Log.Error( error, 0 );
                    }
                }
            }
        }

        /// <summary>
        /// Utility function to remove all affects from a char based on type.
        /// </summary>
        /// <param name="bvect"></param>
        public void RemoveAffect( Bitvector bvect )
        {
            for (int i = (_affected.Count - 1); i >= 0; i--)
            {
                if( _affected[i].HasBitvector( bvect ) )
                {
                    RemoveAffect( _affected[i] );
                }
            }
            RemoveAffectBit(bvect);
        }

        /// <summary>
        /// Sends a sound to a player, formatted as an MSP string.  If the player does not
        /// have MSP turned on, this is a NOOP.  If MUD has MSP disabled system-wide this is
        /// also a NOOP.
        /// </summary>
        /// <param name="txt"></param>
        public void SendSound(string txt)
        {
            if (IsNPC() || !HasActionBit(PC.PLAYER_MSP) ||
                Macros.IsSet((int)Database.SystemData.ActFlags, (int)Sysdata.MudFlags.disablemsp))
            {
                return;
            }

            if (String.IsNullOrEmpty(txt))
            {
                Log.Error("CharData.SendSound: called with empty string.", 0);
            }

            string output = "!!SOUND(" + txt + ")\r\n";

            _socket.WriteToBuffer(output);

            return;
        }

        /// <summary>
        /// Sends a music to a player, formatted as an MSP string.  If the player does not
        /// have MSP turned on, this is a NOOP.
        /// </summary>
        /// <param name="txt"></param>
        public void SendMusic(string txt)
        {
            if (IsNPC() || !HasActionBit(PC.PLAYER_MSP))
            {
                return;
            }

            if (String.IsNullOrEmpty(txt))
            {
                Log.Error("CharData.SendMusic: called with empty string.", 0);
            }

            string output = "!!MUSIC(" + txt + ")\r\n";

            _socket.WriteToBuffer(output);

            return;
        }

        /// <summary>
        /// Sends text to the character, processing color codes as necessary.
        /// </summary>
        /// <param name="txt"></param>
        public virtual void SendText( string txt )
        {
            int point;
            char[] input = txt.ToCharArray();
            string output = String.Empty;

            if( txt.Length < 1 )
            {
                Log.Error( "CharData.SendText: called with empty string.", 0 );
            }

            // Not sending to descriptorless mobs.
            if( _socket == null )
                return;

            for( point = 0; point < input.Length; point++ )
            {
                if( input[ point ] == '&' )
                {
                    point++;
                    // only colorize stuff that needs to be colorized... otherwise
                    // don't worry about it
                    if( input[ point ] != 'N' && input[ point ] != 'n' && input[ point ] != '+' && input[ point ] != '-' )
                    {
                        point--;
                    }
                    else
                    {
                        if (HasActionBit(PC.PLAYER_COLOR))
                        {
                            output += SocketConnection.GetColorCode( txt.Substring(point) );
                            if( input[ point ] != 'n' && input[ point ] != 'N' )
                            {
                                point++;
                            }
                            continue;
                        }
                        if( input[ point ] == '&' )        /* if !Macros.IS_SET( ch.Descriptor._actFlags, PC.PLAYER_COLOR ) */
                        {
                            output += input[ point ];
                        }

                        // was just point++, but since we added another character to the
                        // color _function, we needed to take that into account.
                        // so if we don't find an "N", we skip the next two characters
                        // ( "+r" or "+R" )  if we do find the N we just skip it
                        // should fix the character gobbling for non-color folks
                        // -- Xangis
                        if( input[ point ] != 'n' && input[ point ] != 'N' )
                        {
                            point++;
                        }
                        continue;
                    }
                }
                output += input[ point ];
            }

            // Bypass the paging procedure if the text output is small
            // Saves process time.
            if (output.Length < 500 || !HasActionBit(PC.PLAYER_PAGER))
            {
                _socket.WriteToBuffer( output );
            }
            else
            {
                // TODO: FIX THIS SO WE CAN USE THE PAGER AGAIN.
                _socket.WriteToBuffer(output);
            }

            return;
        }

        /// <summary>
        /// Gets the repop point for a mob.
        /// </summary>
        /// <returns>The RoomIndex if valid, or null if not.</returns>
        virtual public Room GetRepopPoint()
        {
            int place;
            List<RepopulationPoint> repoplist = GetAvailableRepops();
            if (repoplist.Count < 1)
            {
                place = StaticRooms.GetRoomNumber("ROOM_NUMBER_START");
            }
            else
            {
                place = repoplist[0].RoomIndexNumber;
            }
            return Room.GetRoom(place);
        }

        /// <summary>
        /// Find an obj in the room or in inventory.
        /// </summary>
        /// <param name="argument"></param>
        /// <returns></returns>
        public Object GetObjHere( string argument )
        {
            if( argument.Length == 0 )
            {
                return null;
            }

            Object obj = GetObjCarrying( argument );
            if( obj != null )
            {
                return obj;
            }

            obj = GetObjWear( argument );
            if( obj != null )
            {
                return obj;
            }

            obj = Object.GetObjFromList( _inRoom.Contents, this, argument );

            if( obj != null )
            {
                return obj;
            }

            return null;
        }

        /// <summary>
        /// Checks whether char is resistant to the supplied damage type.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="bit"></param>
        /// <returns></returns>
        public static bool CheckResistant( CharData ch, Race.DamageType bit )
        {
            return ( Macros.IsSet( (int)ch._resistant | (int)Race.RaceList[ ch.GetRace() ].Resistant, (int)bit ) );
        }

        /// <summary>
        /// Checks whether char is immune to the supplied damage type.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="bit"></param>
        /// <returns></returns>
        public static bool CheckImmune( CharData ch, Race.DamageType bit )
        {
            return ( Macros.IsSet( (int)ch._immune | (int)Race.RaceList[ ch.GetRace() ].Immune, (int)bit ) );
        }

        /// <summary>
        /// Checks whether char is susceptible to the supplied damage type.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="bit"></param>
        /// <returns></returns>
        public static bool CheckSusceptible( CharData ch, Race.DamageType bit )
        {
            return ( Macros.IsSet( (int)ch._susceptible | (int)Race.RaceList[ ch.GetRace() ].Susceptible, (int)bit ) );
        }

        /// <summary>
        /// Checks whether char is vulnerable to the supplied damage type.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="bit"></param>
        /// <returns></returns>
        public static bool CheckVulnerable(CharData ch, Race.DamageType bit)
        {
            return (Macros.IsSet((int)ch._vulnerable | (int)Race.RaceList[ch.GetRace()].Vulnerable, (int)bit));
        }

        public void WaitState( int pulse )
        {
            _wait += pulse;
        }

        public bool IsSwitched { get; set; }

        /// <summary>
        /// Mobs can't consent anyone.
        /// </summary>
        /// <param name="victim"></param>
        /// <returns>false</returns>
        public virtual bool IsConsenting( CharData victim )
        {
            return false;
        }

        void RemoveInnateTimer( InnateTimerData itd )
        {
            if( IsNPC() || ( (PC)this ).InnateTimers.Count == 0 )
                return;
            if( itd == null )
                return;
            if( ( (PC)this ).InnateTimers.Contains( itd ) )
            {
                ( (PC)this ).InnateTimers.Remove( itd );
            }
            return;
        }

        public bool HasInnateTimer( InnateTimerData.Type type )
        {
            if( IsNPC() )
                return false;
            foreach( InnateTimerData itd in ((PC)this).InnateTimers )
            {
                if( itd.AbilityType == type )
                    return true;
            }
            return false;
        }

        public void UpdateInnateTimers()
        {
            if( IsNPC() )
                return;
            PC pc = (PC)this;
            for(int i = pc.InnateTimers.Count - 1; i >- 0; i-- )
            {
                pc.InnateTimers[i].Timer--;
                if (pc.InnateTimers[i].Timer <= 0)
                {
                    RemoveInnateTimer(pc.InnateTimers[i]);
                }
            }
            return;
        }

        public void PurgeInnateTimers( )
        {
            if( IsNPC() )
                return;
            ( (PC)this ).InnateTimers.Clear();
            return;
        }

        public bool CheckMemorized( Spell spell )
        {
            // Immortals do not need to memorize.
            if (IsImmortal())
            {
                return true;
            }

            bool found = false;
            if( !IsNPC() && !IsClass( CharClass.Names.bard ) && !IsClass( CharClass.Names.psionicist ) && !IsImmortal() )
            {
                foreach( MemorizeData mem in ( (PC)this ).Memorized )
                {
                    if( !mem.Memmed )
                        continue;
                    if( mem.Name == spell.Name )
                    {
                        found = true;
                        break;
                    }
                }

                if( !found )
                {
                    SendText( "You do not have that spell memorized!\r\n" );
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Stop hating the target character.
        /// </summary>
        /// <param name="victim"></param>
        public void StopHating( CharData victim )
        {
            for (int i = (_hating.Count - 1); i >= 0; i-- )
            {
                EnemyData enemy = _hating[i];
                if (enemy.Who == victim)
                {
                    String text = String.Format("{0}&n has stopped hating {1}.", victim._shortDescription, victim._name);
                    ImmortalChat.SendImmortalChat(null, ImmortalChat.IMMTALK_HATING, 0, text);
                    _hating.Remove(enemy);
                }
            }
            return;
        }

        /// <summary>
        /// Tests for a successful sneak attempt by the character.
        /// </summary>
        /// <returns></returns>
        public bool CheckSneak()
        {
            // If not actively sneaking, then no sneak.
            if( !IsAffected(Affect.AFFECT_SNEAK))
                return false;

            // Invisible immortals auto-sneak.
            if (HasActionBit(PC.PLAYER_WIZINVIS))
                return true;

            // If PCs pass their sneak check, then yes.
            if( !IsNPC() && ((PC)this).SkillAptitude.ContainsKey("sneak"))
            {
                if (MUDMath.NumberPercent() < ((PC)this).SkillAptitude["sneak"])
                {
                    return true;
                }
                return false;
            }

            // People with the sneak affect but without the skill are tested as mobs.
            //
            // Chance = 1.33 x level + 20.  At level 1 it's 24%, at level 21 it's 51%,
            // and at level 39 it's 75%.
            int chance = Math.Min(95, ((_level * 4 / 3) + 23));
            if (!IsNPC() && MUDMath.NumberPercent() < chance)
            {
                return true;
            }

            return false;
        }

        public void StopHatingAll()
        {
            _hating.Clear();
            return;
        }

        public CharData GetRandomHateTarget(bool restrictToRoom)
        {
            if( _hating.Count == 0 )
                return null;

            if( !restrictToRoom )
            {
                // Get a random _targetType from the hate list.
                int num = MUDMath.NumberRange( 0, ( _hating.Count - 1 ) );
                return _hating[ num ].Who;
            }
            else
            {
                List<CharData> selections = new List<CharData>();
                // Build a list of who's in the room and on the hate list.
                foreach( EnemyData enemy in _hating )
                {
                    if( enemy.Who._inRoom == _inRoom )
                    {
                        selections.Add( enemy.Who );
                    }
                }
                if( selections.Count == 0 )
                    return null;
                // Pick a random _targetType from the list of people in the room.
                int num = MUDMath.NumberRange( 0, selections.Count );
                return selections[ num ];
            }
        }

        /// <summary>
        /// Format's this character's name to whoever is viewing it.
        /// </summary>
        /// <param name="looker"></param>
        /// <returns></returns>
        public string ShowNameTo(CharData looker, bool capitalize)
        {
            if (looker == null)
            {
                return String.Empty;
            }

            string retstr = ((CanSee(looker, (this)) ? (IsNPC() ? (this)._shortDescription :
                ((!IsRacewar(looker) || ((IsImmortal()) || (looker.IsImmortal()))) ? _name :
                RaceName())) : "someone"));

            if (capitalize)
            {
                return MUDString.CapitalizeANSIString(retstr);
            }

            return retstr;
        }

        /// <summary>
        /// Gets the caller's faction standing with the victim.
        /// </summary>
        /// <param name="victim"></param>
        /// <returns></returns>
        public virtual double GetFaction(CharData victim)
        {
            int race = victim.GetOrigRace();
            return GetFaction(race);
        }

        /// <summary>
        /// Checks whether the target is in the same room as the calling character.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool IsInSameRoom(CharData target)
        {
            if (_inRoom == target._inRoom && _flyLevel == target._flyLevel)
                return true;
            return false;
        }

        /// <summary>
        /// Checks whether the caller is aggressive toward the victim.
        /// </summary>
        /// <param name="victim"></param>
        /// <returns></returns>
        public bool IsAggressive(CharData victim)
        {
            CharData ch = this;
            Guild guild = null;

            if (victim == null)
            {
                Log.Error("IsAggressive: called with null ch or victim.", 0);
                return false;
            }

            if (ch == victim)
            {
                return false;
            }

            if (MUDString.NameContainedIn("_guildgolem_", ch._name))
            {
                foreach (Guild it in Database.GuildList)
                {
                    guild = it;
                    if (guild.ID == Guild.GolemGuildID(ch))
                        break;
                }
                if (guild != null && guild.Ostracized.Length != 0)
                {
                    if (MUDString.NameContainedIn(victim._name, guild.Ostracized))
                        return true;
                }
            }
            if (MUDString.NameContainedIn(Race.RaceList[victim.GetRace()].Name, Race.RaceList[ch.GetOrigRace()].Hate))
            {
                Log.Trace("Returning true for IsAggressive due to race hatred.");
                return true;
            }
            if (ch.HasActionBit(MobTemplate.ACT_AGGROGOOD) && victim.IsGood())
            {
                Log.Trace("Returning true for IsAggressive due to aggro good and good victim.");
                return true;
            }
            if (ch.HasActionBit(MobTemplate.ACT_AGGROEVIL) && victim.IsEvil())
            {
                Log.Trace("Returning true for IsAggressive due to aggro evil and evil victim.");
                return true;
            }
            if (ch.HasActionBit(MobTemplate.ACT_AGGRONEUT) && victim.IsNeutral())
            {
                Log.Trace("Returning true for IsAggressive due to aggro neutral and neutral victim.");
                return true;
            }
            if (ch.HasActionBit(MobTemplate.ACT_AGGROEVILRACE) && victim.GetRacewarSide() == Race.RacewarSide.evil)
            {
                Log.Trace("Returning true for IsAggressive due to aggro evil race and victim evil racewar.");
                return true;
            }
            if (ch.HasActionBit(MobTemplate.ACT_AGGROGOODRACE) && victim.GetRacewarSide() == Race.RacewarSide.good)
            {
                Log.Trace("Returning true for IsAggressive due to aggro good race and victim good racewar.");
                return true;
            }
            if (ch.HasActionBit(MobTemplate.ACT_AGGRESSIVE))
            {
                Log.Trace("Returning true for IsAggressive due to aggressive flag on ch.");
                return true;
            }

            return ch.IsHating(victim);
        }

        public void Restore(CharData victim)
        {
            if (victim == null) return;

            if (victim._hitpoints < victim.GetMaxHit())
                victim._hitpoints = victim.GetMaxHit();
            victim._currentMana = victim._maxMana;
            victim._currentMoves = victim._maxMoves;
            if (!victim.IsNPC())
            {
                ((PC)victim).Hunger = 48;
                ((PC)victim).Thirst = 48;
                ((PC)victim).Drunk = 0;
                victim.PurgeInnateTimers();
            }
            if (victim.IsAffected(Affect.AFFECT_BLIND))
                victim.RemoveBlindness();
            if (victim.IsAffected(Affect.AFFECT_POISON))
                victim.RemovePoison();
            victim.UpdatePosition();
            SocketConnection.Act("$n has restored you.", this, null, victim, SocketConnection.MessageTarget.victim);
            return;
        }

        public virtual double GetFaction(int race)
        {
            return Race.RaceList[GetOrigRace()].RaceFaction[race];
        }

        /// <summary>
        /// Backs up a player file by saving them to the backup directory.
        /// </summary>
        /// <param name="ch">The character to be backed up.</param>
        public static void BackupPlayer(CharData ch)
        {
            string strsave = FileLocation.BackupDirectory + ch._name.ToUpper();

            if (!ch.SaveFile(strsave))
            {
                Log.Error("CharData.BackupPlayer(): Unable to backup character file for " + ch._name + ".");
            }

            return;
        }

        /// <summary>
        /// Delete a character's file.
        /// Used for retire & delete commands.
        /// </summary>
        /// <param name="ch">The character to delete.</param>
        public static void DeletePlayer(CharData ch)
        {
            if (ch.IsNPC() || ch._level < 1)
                return;

            string strsave = FileLocation.PlayerDirectory + (ch._name[0]).ToString().ToLower() + Path.DirectorySeparatorChar + ch._name.ToUpper() + ".xml";

            if (File.Exists(strsave))
            {
                File.Delete(strsave);
            }
            else
            {
                Log.Error("CharData.DeletePlayer(): File not found for character " + strsave + ".");
            }

            return;
        }

        /// <summary>
        /// Save a player to disk.
        /// </summary>
        /// <param name="ch">The player to CharData.</param>
        /// <returns>Boolean value representing success/failure.</returns>
        public static bool SavePlayer(CharData ch)
        {
            if (ch == null)
            {
                Log.Error("CharData.SavePlayer(): Can't save null CharData.");
                return false;
            }

            if (ch.IsNPC() || ch._level < 1)
                return false;

            if (ch._socket && ch._socket.Original)
                ch = ch._socket.Original;

            ch._charClassNum = (int)ch._charClass.ClassNumber;
            ch._saveTime = Database.SystemData.CurrentTime;

            //Log.Trace("Saving character: " + ch._name.ToUpper() + ".");

            string strsave = FileLocation.PlayerDirectory + MUDString.LowercaseInitial(ch._name) + "/" + ch._name.ToUpper() + ".xml";

            if (!ch.SaveFile(strsave))
            {
                Log.Error("CharData.SavePlayer(): Unable to save player file: " + strsave + ".");
                return false;
            }

            return true;
        }

        public void ImprintSpell(Spell spell, int level, Target target)
        {
            int[] sucessRate = new[] 
            {
                80, 30, 25, 10
            };

            string text;
            Object obj = (Object)target;
            int freeSlots;
            int i;

            if (spell == null)
            {
                SendText("That is not a spell.\r\n");
                return;
            }

            for (freeSlots = i = 1; i < 5; i++)
                if (obj.Values[i] != -1)
                    freeSlots++;

            if (freeSlots > 4)
            {
                SocketConnection.Act("$p&n cannot contain any more spells.", this, obj, null, SocketConnection.MessageTarget.character);
                return;
            }

            int mana = 4 * Macros.ManaCost(this, spell);

            if (!IsNPC() && _currentMana < mana)
            {
                SendText("You don't have enough mana.\r\n");
                return;
            }

            if (MUDMath.NumberPercent() > ((PC)this).SpellAptitude[spell.Name]
                    && (_level <= (spell.SpellCircle[(int)this._charClass.ClassNumber] * 4 + 1)))
            {
                SendText("You lost your concentration.\r\n");
                SocketConnection.Act("&+r$n&n&+r stops chanting abruptly.&n", this, null, null, SocketConnection.MessageTarget.room);
                _currentMana -= mana / 2;
                return;
            }

            _currentMana -= mana;
            // TODO: FIXME: BUG: Can't cram a string into a integer value inside an object.
            // This is a problem because it makes it impossible for objects to contain spells.
            //obj._values[free_slots] = spell;

            if (MUDMath.NumberPercent() > sucessRate[freeSlots - 1])
            {
                text = String.Format("The magic enchantment has failed: the {0} vanishes.\r\n",
                          StringConversion.ItemTypeString(obj));
                SendText(text);
                obj.RemoveFromWorld();
                ;
                return;
            }

            obj.ShortDescription = String.Empty;
            text = String.Format("a {0} of ", StringConversion.ItemTypeString(obj));
            for (i = 1; i <= freeSlots; i++)
            {
                if (obj.Values[i] != -1)
                {
                    text += SpellNumberToTextMap.GetSpellNameFromNumber(obj.Values[i]);
                    if (i != freeSlots)
                    {
                        text += ", ";
                    }
                    else
                    {
                        text += String.Empty;
                    }
                }
            }
            obj.ShortDescription = text;

            text = String.Format("{0} {1}", obj.Name, StringConversion.ItemTypeString(obj));
            obj.Name = text;

            text = String.Format("You have imbued a new spell to the {0}.\r\n",
                      StringConversion.ItemTypeString(obj));
            SendText(text);

            return;
        }


        /// <summary>
        /// Locate a shopkeeper in the current room.
        /// </summary>
        /// <param name="argument"></param>
        /// <returns></returns>
        public CharData FindShopkeeper(string argument)
        {
            CharData keeper = null;
            Shop pShop = null;

            foreach (CharData ikeeper in _inRoom.People)
            {
                if (ikeeper.IsNPC() && (pShop = ikeeper._mobTemplate.ShopData)
                        && (String.IsNullOrEmpty(argument) || MUDString.NameContainedIn(argument, ikeeper._name)))
                {
                    keeper = ikeeper;
                    break;
                }
            }

            if (keeper == null || pShop == null || (keeper && keeper.IsAffected(Affect.AFFECT_CHARM)))
            {
                SendText("You can't do that here.\r\n");
                return null;
            }

            /*
            * Shop hours.
            */
            if (Database.SystemData.GameHour < pShop.OpenHour)
            {
                CommandType.Interpret(keeper, "say Sorry, come back later.");
                return null;
            }

            if (Database.SystemData.GameHour > pShop.CloseHour)
            {
                CommandType.Interpret(keeper, "say Sorry, come back tomorrow.");
                return null;
            }

            /*
            * Invisible or hidden people.
            */
            if (!CanSee(keeper, this))
            {
                CommandType.Interpret(keeper, "say I don't trade with folks I can't see.");
                return null;
            }

            return keeper;
        }

        /// <summary>
        /// Gets chance of second, third, and fourth attack.  Exists to keep combat functions
        /// from getting too large.
        /// </summary>
        /// <param name="attackNumber"></param>
        /// <returns></returns>
        public int GetAttackChance(int attackNumber)
        {
            int chance = 0;
            if (IsNPC())
            {
                switch (attackNumber)
                {
                    case 2:
                        if (!HasSkill("second attack"))
                        {
                            chance = 0;
                        }
                        else
                        {
                            chance = ((_level - Skill.SkillList["second attack"].ClassAvailability[(int)_charClass.ClassNumber]) * 2 + 25) * 3 / 4;
                        }
                        break;
                    case 3:
                        if (!HasSkill("third attack"))
                        {
                            chance = 0;
                        }
                        else
                        {
                            chance = ((_level - Skill.SkillList["third attack"].ClassAvailability[(int)_charClass.ClassNumber]) * 2 + 25) * 3 / 8;
                        }
                        break;
                    case 4:
                        if (!HasSkill("fourth attack"))
                        {
                            chance = 0;
                        }
                        else
                        {
                            chance = ((_level - Skill.SkillList["fourth attack"].ClassAvailability[(int)_charClass.ClassNumber]) * 2 + 25) / 4;
                        }
                        break;
                }
            }
            else
            {
                switch (attackNumber)
                { 
                    case 2:
                        if (((PC)this).SkillAptitude.ContainsKey("second attack"))
                        {
                            chance = ((PC)this).SkillAptitude["second attack"] * 3 / 4;
                        }
                        else
                        {
                            chance = 0;
                        }
                        break;
                    case 3:
                        if (((PC)this).SkillAptitude.ContainsKey("third attack"))
                        {
                            chance = ((PC)this).SkillAptitude["third attack"] * 3 / 8;
                        }
                        else
                        {
                            chance = 0;
                        }
                        break;
                    case 4:
                        if (((PC)this).SkillAptitude.ContainsKey("fourth attack"))
                        {
                            chance = ((PC)this).SkillAptitude["fourth attack"] / 4;
                        }
                        else
                        {
                            chance = 0;
                        }
                        break;
                }
            }

            if (chance > 95)
                chance = 95;

            return chance;
        }

        /// <summary>
        /// Loads a player from disk.
        /// </summary>
        /// <param name="socket">The player's socket connection.</param>
        /// <param name="name">The character _name.</param>
        /// <returns>Boolean value representing success/failure.</returns>
        public static bool LoadPlayer(SocketConnection socket, string name)
        {
            name = name.ToUpper();
            Log.Trace("Loading character: " + name + ".");

            string strsave = FileLocation.PlayerDirectory + MUDString.LowercaseInitial(name) + "/" + name + ".xml";

            PC ch = PC.LoadFile(strsave);
            if (ch == null)
            {
                Log.Trace("CharData.LoadPlayer(): " + name + " not loaded.");
                return false;
            }

            if (socket)
            {
                socket.Character = ch;
                ch._socket = socket;
            }
            ch._castingTime = 0;
            ch._castingSpell = 0;
            ch._charClass = CharClass.ClassList[ch._charClassNum];

            Log.Trace("CharData.LoadPlayer(): Successfully loaded " + name + ".");
            return true;
        }

        /// <summary>
        /// Checks whether the character is listening to a talk channel.
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        public bool IsListening(TalkChannel channel)
        {
            if (Macros.IsSet((int)_deaf, (int)channel))
            {
                // If they're deaf to the channel, listening = false.
                return false;
            }
            return true;
        }
        
        /// <summary>
        /// Gets the available repops based on a player's race and class.
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public List<RepopulationPoint> GetAvailableRepops()
        {
            List<RepopulationPoint> list = new List<RepopulationPoint>();

            foreach (RepopulationPoint repop in RepopulationPoint.RepopList)
            {
                if (repop.CharacterClass == _charClass && repop.Race == Race.RaceList[GetOrigRace()])
                {
                    list.Add(repop);
                }
            }
            return list;
        }


        /// <summary>
        /// Checks whether an item given to a mob is part of a quest.  This function
        /// should be passed an object or a money amount, but not both.
        /// </summary>
        /// <param name="victim"></param>
        /// <param name="obj"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        public bool CheckQuest(CharData victim, Object obj, int money)
        {
            CharData ch = this;
            QuestTemplate quest;
            QuestData resetQuest = null;
            int qcount = 0;
            bool value = false;
            bool needsReset = false;
            Object newobj = null;

            if (!victim.IsNPC())
                return value;

            if (money == 0 && obj == null)
            {
                Log.Trace("Check_quest _function called with no data");
                return value;
            }

            if (money != 0 && obj != null)
            {
                Log.Trace("Check_quest _function called with too much data");
                return value;
            }

            /* Quest Stuff */
            foreach (QuestTemplate it in QuestTemplate.QuestList)
            {
                quest = it;
                if (quest.IndexNumber == victim._mobTemplate.IndexNumber)
                {
                    string buf = String.Format("{0}&n has quest data.", victim._shortDescription);
                    ImmortalChat.SendImmortalChat(null, ImmortalChat.IMMTALK_QUESTS, 0, buf);

                    /* let's just check all the quests first, then test for completion afterward */
                    foreach (QuestData quests in quest.Quests)
                    {
                        ++qcount;
                        buf = String.Format("Give items: ");
                        foreach (QuestItem questitem in quests.Give)
                        {
                            string buf2 = String.Format("{0} ", questitem.Value);
                            buf += buf2;
                        }
                        ImmortalChat.SendImmortalChat(null, ImmortalChat.IMMTALK_QUESTS, 0, buf);
                        foreach (QuestItem questitem in quests.Receive)
                        {
                            buf = String.Format("  {0}&n receives item type {1}, value {2}.",
                                      victim._shortDescription, questitem.Type, questitem.Value);
                            ImmortalChat.SendImmortalChat(null, ImmortalChat.IMMTALK_QUESTS, 0, buf);
                            if (questitem.Completed)
                            {
                                continue;
                            }

                            if (obj)
                            {
                                if (questitem.Type == QuestItem.QuestType.item && questitem.Value == obj.ObjIndexData.IndexNumber)
                                {
                                    questitem.Completed = true;
                                    value = true;
                                    buf = String.Format("{0} has given quest item {1} to {2}&n.",
                                              ch._name, questitem.Value, victim._shortDescription);
                                    ImmortalChat.SendImmortalChat(null, ImmortalChat.IMMTALK_QUESTS, 0, buf);
                                }
                                /* //this type is money, dont need to process this here
                                else if( questitem.type == 1 && questitem.value == obj.item_type )
                                {
                                questitem.complete = true;
                                value = true;
                                }
                                //
                                */
                                //obj doesnt match this particular questitem
                            }
                            else if (money != 0)
                            {
                                if (questitem.Type == QuestItem.QuestType.money && questitem.Value <= money)
                                {
                                    questitem.Completed = true;
                                    value = true;
                                    buf = String.Format("  &n{0}&n has given {1} copper to {2}&n.",
                                              ch._name, questitem.Value, victim._shortDescription);
                                    ImmortalChat.SendImmortalChat(null, ImmortalChat.IMMTALK_QUESTS, 0, buf);
                                }
                            }
                            else
                            { // can't imgaine how we would get here
                                Log.Trace("Check_quest _function way buggy!");
                                buf = String.Format("check_quest _function way buggy!");
                                ImmortalChat.SendImmortalChat(null, ImmortalChat.IMMTALK_QUESTS, 0, buf);
                                return value;
                            }
                        }
                    } // end of quests loop

                    foreach (QuestData quests in quest.Quests)
                    {
                        if (needsReset)
                            break;
                        bool qcomplete = true;
                        foreach (QuestItem questitem in quests.Receive)
                        {
                            if (!questitem.Completed)
                            {
                                qcomplete = false;
                                break;
                            }
                        }
                        if (qcomplete)
                        {
                            buf = String.Format("{0}&n completed a quest for {1}&n.", ch._name, victim._shortDescription);
                            ImmortalChat.SendImmortalChat(null, ImmortalChat.IMMTALK_QUESTS, 0, buf);
                            Log.Trace(buf);
                            if (quests.Complete.Length > 0)
                            {
                                ch.SendText(quests.Complete);
                            }
                            foreach (QuestItem item1 in quests.Give)
                            {
                                if (item1.Completed)
                                    continue;
                                switch (item1.Type)
                                {
                                    default:
                                        break;
                                    case QuestItem.QuestType.item:
                                        newobj = Database.CreateObject(Database.GetObjTemplate(item1.Value), 0);
                                        SocketConnection.Act("$n&n hands $p&n to $N&n.", victim, newobj, ch, SocketConnection.MessageTarget.room);
                                        SocketConnection.Act("$n&n gives you $p&n.", ch, newobj, victim, SocketConnection.MessageTarget.victim);
                                        newobj.ObjToChar(ch);
                                        item1.Completed = true;
                                        break;
                                    case QuestItem.QuestType.money:
                                        SocketConnection.Act("$N&n gives some &n&+wcoins&n to $n&n.", victim, newobj, ch, SocketConnection.MessageTarget.everyone_but_victim);
                                        ch.ReceiveCash(item1.Value);
                                        item1.Completed = true;
                                        break;
                                    case QuestItem.QuestType.skill:
                                        ch.SendText("Questing for skills is not yet enabled.\r\n");
                                        item1.Completed = true;
                                        break;
                                    case QuestItem.QuestType.experience:
                                        ch.SendText("You gain experience!\r\n");
                                        ch.GainExperience(item1.Value);
                                        item1.Completed = true;
                                        break;
                                    case QuestItem.QuestType.spell:
                                        ch.SendText("Questing for spells is not yet enabled.\r\n");
                                        break;
                                    case QuestItem.QuestType.spellcast:
                                        ch.SendText("Questing for cast spells is not yet enabled.\r\n");
                                        break;
                                    case QuestItem.QuestType.song:
                                        ch.SendText("Questing for bard songs is not yet enabled.\r\n");
                                        break;
                                }
                                SavePlayer(ch);
                            }
                            if (quests.Disappear.Length > 0)
                            {
                                ch.SendText(quests.Disappear);
                                ExtractChar(victim, true);

                            }
                            needsReset = true;
                            resetQuest = quests;
                        } //end if(qcomplete)
                    } //end of quests loop
                    if (needsReset)
                    {
                        foreach (QuestData quests in quest.Quests)
                        {
                            foreach (QuestItem questitem in quests.Receive)
                            {
                                foreach (QuestItem item2 in resetQuest.Receive)
                                    if (item2.Value == questitem.Value && item2.Type == questitem.Type)
                                        questitem.Completed = false;
                            }
                            foreach (QuestItem questitem in quests.Give)
                            {
                                foreach (QuestItem item2 in resetQuest.Give)
                                    if (item2.Value == questitem.Value && item2.Type == questitem.Type)
                                        questitem.Completed = false;
                            }
                        }
                    }
                    buf = String.Format("Done processing {0} quests for {1}&n.", qcount, victim._shortDescription);
                    ImmortalChat.SendImmortalChat(null, ImmortalChat.IMMTALK_QUESTS, 0, buf);
                    return value; //we matched the one mob, why cycle through the rest?
                } // end if(index number) statement
            }
            return value;
        }

        /// <summary>
        /// Sets whether the character is listening to a talk channel.
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="state"></param>
        public void SetListening(TalkChannel channel, bool state)
        {
            if (!state)
            {
                // Set Listening = False: Turn the deaf channel on.
                int value = (int)_deaf | (int)channel;
                _deaf = (TalkChannel)value;
            }
            else
            {
                // Set Listening = True: Turn the deaf channel off.
                int value = (int)_deaf & ~((int)channel);
                _deaf = (TalkChannel)value;
            }
        }

        public void SetOffTrap(Object obj)
        {
            if (obj == null || obj.Trap == null)
            {
                Log.Error("SetOffTrap called with no trap on object, or no object", 0);
                return;
            }

            if (obj.Trap.Charges == 0)
            {
                return;
            }

            if (MUDMath.NumberPercent() < obj.Trap.Percent)
            {
                SendText("You set off a trap!\r\n");
            }
            else
            {
                return;
            }

            if (obj.Trap.Charges > -1)
            {
                obj.Trap.Charges--;
            }

            string text = String.Format("{0} set off trap {1} in room {2}.", _name, obj.Name, obj.InRoom.IndexNumber);
            Log.Trace(text);

            Spell spell = null;
            switch (obj.Trap.Damage)
            {
                default:
                    break;
                case Trap.TrapType.sleep:
                    spell = StringLookup.SpellLookup("trap-sleep");
                    break;
                case Trap.TrapType.teleport:
                    spell = StringLookup.SpellLookup("trap-teleport");
                    break;
                case Trap.TrapType.fire:
                    spell = StringLookup.SpellLookup("trap-fire");
                    break;
                case Trap.TrapType.cold:
                    spell = StringLookup.SpellLookup("trap-cold");
                    break;
                case Trap.TrapType.acid:
                    spell = StringLookup.SpellLookup("trap-acid");
                    break;
                case Trap.TrapType.energy:
                    spell = StringLookup.SpellLookup("trap-energy");
                    break;
                case Trap.TrapType.blunt:
                    spell = StringLookup.SpellLookup("trap-blunt");
                    break;
                case Trap.TrapType.piercing:
                    spell = StringLookup.SpellLookup("trap-piercing");
                    break;
                case Trap.TrapType.slashing:
                    spell = StringLookup.SpellLookup("trap-slashing");
                    break;
                case Trap.TrapType.dispel:
                    spell = StringLookup.SpellLookup("trap-dispel");
                    break;
                case Trap.TrapType.gate:
                    spell = StringLookup.SpellLookup("trap-gate");
                    break;
                case Trap.TrapType.summon:
                    spell = StringLookup.SpellLookup("trap-summon");
                    break;
                case Trap.TrapType.wither:
                    spell = StringLookup.SpellLookup("trap-wither");
                    break;
                case Trap.TrapType.harm:
                    spell = StringLookup.SpellLookup("trap-harm");
                    break;
                case Trap.TrapType.poison:
                    spell = StringLookup.SpellLookup("trap-poison");
                    break;
                case Trap.TrapType.paralysis:
                    spell = StringLookup.SpellLookup("trap-paralysis");
                    break;
                case Trap.TrapType.stun:
                    spell = StringLookup.SpellLookup("trap-stun");
                    break;
                case Trap.TrapType.disease:
                    spell = StringLookup.SpellLookup("trap-disease");
                    break;
            }

            if (!spell)
            {
                SendText("Lucky for you the trap malfunctioned!\r\n");
                Log.Error("Trap type " + obj.Trap.Damage.ToString() + " not found. Check that it exists in the spells file.");
            }
            else
            {
                spell.Invoke(this, obj.Trap.Level, new Target(obj));
            }

            return;
        }

        /// <summary>
        /// Forwards the spell's _name to the main AffectRefresh method.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="spell"></param>
        /// <param name="duration"></param>
        public void AffectRefresh(Affect.AffectType type, Spell spell, int duration)
        {
            AffectRefresh(type, spell.Name, duration);
        }

        /// <summary>
        /// Applies modifiers of an affect to a character.  Called when adding and removing affects
        /// to/from a char.
        /// </summary>
        /// <param name="af">The affect to apply.</param>
        /// <param name="addingAffect">True if the affect is being added, false if it is being removed.</param>
        public void ApplyAffectModifiers(Affect af, bool addingAffect)
        {
            if (af == null)
            {
                Log.Error("ApplyAffectModifiers called with NULL affect data.");
                return;
            }

            Object wield2;

            foreach (AffectApplyType apply in af.Modifiers)
            {
                int mod = apply.Amount;

                if (!addingAffect)
                    mod = 0 - mod;

                switch (apply.Location)
                {
                    default:
                        string buf = "ApplyAffectModifiers: unknown location " + apply.Location + " on " + _name;
                        Log.Error(buf, 0);
                        /* Changed this to break from return. */
                        break;
                    case Affect.Apply.none:
                        break;
                    case Affect.Apply.strength:
                        _modifiedStrength += mod;
                        break;
                    case Affect.Apply.dexterity:
                        _modifiedDexterity += mod;
                        break;
                    case Affect.Apply.intelligence:
                        _modifiedIntelligence += mod;
                        break;
                    case Affect.Apply.wisdom:
                        _modifiedWisdom += mod;
                        break;
                    case Affect.Apply.constitution:
                        {
                            int hit = GetMaxHit();
                            _modifiedConstitution += mod;
                            _hitpoints += GetMaxHit() - hit;
                            if (_hitpoints > GetMaxHit())
                                _hitpoints = GetMaxHit();
                            break;
                        }
                    case Affect.Apply.agility:
                        _modifiedAgility += mod;
                        break;
                    case Affect.Apply.charisma:
                        _modifiedCharisma += mod;
                        break;
                    case Affect.Apply.power:
                        _modifiedPower += mod;
                        break;
                    case Affect.Apply.luck:
                        _modifiedLuck += mod;
                        break;
                    case Affect.Apply.size:
                        _size += mod;
                        break;
                    case Affect.Apply.sex:
                        _sex += mod;
                        break;
                    case Affect.Apply.age:
                        break;
                    case Affect.Apply.height:
                        break;
                    case Affect.Apply.weight:
                        break;
                    case Affect.Apply.mana:
                        _maxMana += mod;
                        break;
                    case Affect.Apply.hitpoints:
                        if (!IsNPC())
                        {
                            ((PC)this).HitpointModifier += mod;
                        }
                        else
                        {
                            _maxHitpoints += mod;
                        }
                        _hitpoints += mod;
                        break;
                    case Affect.Apply.move:
                        _maxMoves += mod;
                        break;
                    case Affect.Apply.ac:
                        _armorPoints += mod;
                        break;
                    case Affect.Apply.hitroll:
                        _hitroll += mod;
                        break;
                    case Affect.Apply.damroll:
                        _damroll += mod;
                        break;
                    case Affect.Apply.save_paralysis:
                        _savingThrows[0] += mod;
                        break;
                    case Affect.Apply.save_poison:
                        _savingThrows[1] += mod;
                        break;
                    case Affect.Apply.save_petrification:
                        _savingThrows[2] += mod;
                        break;
                    case Affect.Apply.save_breath:
                        _savingThrows[3] += mod;
                        break;
                    case Affect.Apply.save_spell:
                        _savingThrows[4] += mod;
                        break;
                    case Affect.Apply.fire_protection:
                        break;
                    case Affect.Apply.max_strength:
                        if (!IsNPC())
                            ((PC)this).MaxStrMod += mod;
                        else
                            _modifiedStrength += mod;
                        break;
                    case Affect.Apply.max_dexterity:
                        if (!IsNPC())
                            ((PC)this).MaxDexMod += mod;
                        else
                            _modifiedDexterity += mod;
                        break;
                    case Affect.Apply.max_intelligence:
                        if (!IsNPC())
                            ((PC)this).MaxIntMod += mod;
                        else
                            _modifiedIntelligence += mod;
                        break;
                    case Affect.Apply.max_wisdom:
                        if (!IsNPC())
                            ((PC)this).MaxWisMod += mod;
                        else
                            _modifiedWisdom += mod;
                        break;
                    case Affect.Apply.max_constitution:
                        if (!IsNPC())
                        {
                            int hit = GetMaxHit();
                            ((PC)this).MaxConMod += mod;
                            _hitpoints += GetMaxHit() - hit;
                            if (_hitpoints > GetMaxHit())
                                _hitpoints = GetMaxHit();
                        }
                        else
                        {
                            int hit = GetMaxHit();
                            _modifiedConstitution += mod;
                            _hitpoints += GetMaxHit() - hit;
                            if (_hitpoints > GetMaxHit())
                                _hitpoints = GetMaxHit();
                            break;
                        }
                        break;
                    case Affect.Apply.max_agility:
                        if (!IsNPC())
                            ((PC)this).MaxAgiMod += mod;
                        else
                            _modifiedAgility += mod;
                        break;
                    case Affect.Apply.max_power:
                        if (!IsNPC())
                            ((PC)this).MaxPowMod += mod;
                        else
                            _modifiedPower += mod;
                        break;
                    case Affect.Apply.max_charisma:
                        if (!IsNPC())
                            ((PC)this).MaxChaMod += mod;
                        else
                            _modifiedCharisma += mod;
                        break;
                    case Affect.Apply.max_luck:
                        if (!IsNPC())
                            ((PC)this).MaxLukMod += mod;
                        else
                            _modifiedLuck += mod;
                        break;
                    case Affect.Apply.race_strength:
                    case Affect.Apply.race_dexterity:
                    case Affect.Apply.race_intelligence:
                    case Affect.Apply.race_wisdom:
                    case Affect.Apply.race_constitution:
                    case Affect.Apply.race_agility:
                    case Affect.Apply.race_power:
                    case Affect.Apply.race_charisma:
                    case Affect.Apply.race_luck:
                    case Affect.Apply.curse:
                        break;
                    case Affect.Apply.resistant:
                        {
                            int val = (int)_resistant;
                            Macros.SetBit(ref val, mod);
                            if (val < 0)
                                break;
                            _resistant = (Race.DamageType)val;
                            break;
                        }
                    case Affect.Apply.immune:
                        {
                            int val = (int)_immune;
                            Macros.SetBit(ref val, mod);
                            if (val < 0)
                                break;
                            _immune = (Race.DamageType)val;
                            break;
                        }
                    case Affect.Apply.susceptible:
                        {
                            int val = (int)_susceptible;
                            Macros.SetBit(ref val, mod);
                            if (val < 0)
                                break;
                            _susceptible = (Race.DamageType)val;
                            break;
                        }
                    case Affect.Apply.vulnerable:
                        {
                            int val = (int)_vulnerable;
                            Macros.SetBit(ref val, mod);
                            if (val < 0)
                                break;
                            _vulnerable = (Race.DamageType)val;
                            break;
                        }
                    case Affect.Apply.race:
                        // Added size adjustment so when you change races higher or lower
                        // you are adjusted proportionally.  We don't just set the race to default
                        // size because the person may be enlarged or reduced and will be hosed when
                        // the enlarge or reduce affect wears off.
                        _size -= (Race.RaceList[GetOrigRace()].DefaultSize - Race.RaceList[(GetOrigRace() + mod)].DefaultSize);
                        SetPermRace(GetRace() + mod);
                        break;
                }

                /* Remove the excess general stats */
                // took out class check, because evryone can go over max with Holy Sacrifice
                _hitpoints = Math.Min(_hitpoints, (GetMaxHit() * 3 / 2));
                _currentMana = Math.Min(_currentMana, _maxMana);
                _currentMoves = Math.Min(_currentMoves, _maxMoves);

                if (IsNPC())
                    return;

                Object wield = Object.GetEquipmentOnCharacter(this, ObjTemplate.WearLocation.hand_one);
                if (wield != null)
                {
                    wield2 = Object.GetEquipmentOnCharacter(this, ObjTemplate.WearLocation.hand_two);
                    if (wield2 != null)
                    {
                        if (((wield.GetWeight() + wield2.GetWeight()) > StrengthModifier.Table[GetCurrStr()].WieldWeight)
                                || !HasInnate(Race.RACE_WEAPON_WIELD))
                        {
                            SocketConnection.Act("You drop $p&n because it is too heavy.", this, wield2, null, SocketConnection.MessageTarget.character);
                            SocketConnection.Act("$n&n drops $p&n.", this, wield2, null, SocketConnection.MessageTarget.room);
                            wield2.RemoveFromChar();
                            wield2.AddToRoom(_inRoom);
                        }
                    }
                    else
                        if ((wield.GetWeight() > StrengthModifier.Table[GetCurrStr()].WieldWeight)
                                || !HasInnate(Race.RACE_WEAPON_WIELD))
                        {
                            SocketConnection.Act("You drop $p&n because it is too heavy.", this, wield, null, SocketConnection.MessageTarget.character);
                            SocketConnection.Act("$n&N drops $p&n.", this, wield, null, SocketConnection.MessageTarget.room);
                            wield.RemoveFromChar();
                            wield.AddToRoom(_inRoom);
                        }
                }
                else if ((wield2 = Object.GetEquipmentOnCharacter(this, ObjTemplate.WearLocation.hand_two)) && (wield2.GetWeight() > StrengthModifier.Table[GetCurrStr()].WieldWeight
                               || !HasInnate(Race.RACE_WEAPON_WIELD)))
                {
                    SocketConnection.Act("You drop $p&n because it is too heavy.", this, wield2, null, SocketConnection.MessageTarget.character);
                    SocketConnection.Act("$n&n drops $p&n.", this, wield2, null, SocketConnection.MessageTarget.room);
                    wield2.RemoveFromChar();
                    wield2.AddToRoom(_inRoom);
                }
            }
            return;
        }


        /// <summary>
        /// Adds an affect to a char, combining durations if the same affect is already on the char.
        /// If the matching affects have different modifiers, this keeps the larger modifier.  This is
        /// an "add or replace" operation.
        /// </summary>
        /// <param name="ch">The char to combine the affect with.</param>
        public void CombineAffect(Affect af)
        {
            if (af == null)
            {
                Log.Error("CombineAffect: null affect", 0);
                return;
            }

            for (int i = (_affected.Count - 1); i >= 0; i--)
            {
                if (_affected[i].Type == af.Type && _affected[i].Value == af.Value)
                {
                    _affected[i].Duration += _affected[i].Duration;
                    _affected[i].Level = Math.Max(af.Level, _affected[i].Level);
                    for(int j = 0; j < _affected[i].Modifiers.Count; j++ )
                    {
                        foreach(AffectApplyType newapply in af.Modifiers )
                        {
                            if (newapply.Location == _affected[i].Modifiers[j].Location)
                            {
                                _affected[i].Modifiers[j].Amount = Math.Max(newapply.Amount, _affected[i].Modifiers[j].Amount);
                            }
                        }
                    }
                    return;
                }
            }

            AddAffect(af);
            return;
        }

        /// <summary>
        /// Removes an affect from a char.
        /// </summary>
        public void RemoveAffect(Affect af)
        {
            if (_affected == null)
            {
                Log.Error("RemoveAffect: Trying to remove NULL affect.", 0);
                return;
            }

            ApplyAffectModifiers(af, false);
            _affected.Remove(af);
            return;
        }

        /// <summary>
        /// Searches a players affects for an affect of the specified type and value and
        /// sets that affect's duration to the new specified time without adding or applying
        /// any new affects.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <param name="duration"></param>
        public void AffectRefresh(Affect.AffectType type, string value, int duration)
        {
            if (String.IsNullOrEmpty(value))
                return;

            foreach (Affect aff in _affected)
            {
                if (aff.Type == type && aff.Value == value)
                {
                    if (aff.Duration < duration)
                        aff.Duration = duration;
                }
            }
            return;
        }

        /// <summary>
        /// Forwards to the string-based AffectStrip.  Since this only strips
        /// specific spells, in most cases you will want to use
        /// CharData.RemoveAffect() instead.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        public void AffectStrip(Affect.AffectType type, Spell value)
        {
            AffectStrip(type, value.Name);
        }

        /// <summary>
        /// Strips all instances of a particular affect from a player.  Since this only
        /// catches the exact spell/skill _name being checked for, in most cases you will
        /// want to call CharData.RemoveAffect() instead.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        public void AffectStrip(Affect.AffectType type, string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                Log.Error("Invalid call to Affect.AffectStrip!  Must pass a non-null value!", 0);
                return;
            }

            for (int i = (_affected.Count - 1); i >= 0; i--)
            {
                if (_affected[i].Type == type && _affected[i].Value == value)
                {
                    RemoveAffect(_affected[i]);
                }
            }

            return;
        }


        /// <summary>
        /// Adds an affect to a character.  Does not combine durations.
        /// </summary>
        /// <param name="ch">The character to add the affect to.</param>
        public void AddAffect(Affect af)
        {
            _affected.Add(af);
            ApplyAffectModifiers(af, true);
            return;
        }

        /// <summary>
        /// Forwards to the string-based HasAffect.  If checking whether a character is
        /// affected by a specific type of affect rather than an exact spell, use
        /// CharData.IsAffected() instead.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool HasAffect(Affect.AffectType type, Spell value)
        {
            return HasAffect(type, value.Name);
        }

        /// <summary>
        /// New version of HasAffect.  Much more efficient than the legacy version
        /// because it only iterates through ch._affected once.  In most cases you
        /// will want to use CharData.IsAffected() instead so that you can check
        /// against all spells or skills that give a specific affect.
        /// </summary>
        /// <param name="type">The Affect.AffectType (skill, spell, song, etc.)</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool HasAffect(Affect.AffectType type, string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                Log.Error("Invalid call to Affect.HasAffect!  Must pass a positive non-null value!", 0);
                return false;
            }

            foreach (Affect aff in _affected)
            {
                if (aff.Type == type && aff.Value == value)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the maximum number of items the character can carry, modified by dexterity.
        /// </summary>
        /// <returns></returns>
        internal int GetMaxItemsCarried()
        {
            if (IsNPC())
            {
                return Limits.MAX_CARRY;
            }
            else
            {
                // Assume a dexterity of 80 gives you full carry ability.
                return GetCurrDex() / 20 + (Limits.MAX_CARRY - 4);
            }
        }
    }
}

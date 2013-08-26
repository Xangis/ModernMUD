using System;
using System.Reflection;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using ModernMUD;

namespace MUDEngine
{
    /// <summary>
    /// Represents a spell.
    /// 
    /// This is also used for other spell-like game behavior, such as traps.
    /// </summary>
    [Serializable]
    public class Spell
    {
        public static readonly Dictionary<String, Spell> SpellList = new Dictionary<String, Spell>();

        /// <summary>
        /// Name of spell.
        /// </summary>
        public string Name { get; set; }
        private int[] _spellCircle = new int[ Limits.MAX_CLASS ];
        public delegate void SpellFun( CharData ch, Spell spell, int level, Target target );
        /// <summary>
        /// The target type for this spell.
        /// </summary>
        public TargetType ValidTargets { get; set; }
        /// <summary>
        /// Can you cast it in a fight?
        /// </summary>
        public bool CanCastInCombat { get; set; }
        /// <summary>
        /// Minimum mana used
        /// </summary>
        public int MinimumMana { get; set; }
        /// <summary>
        /// Amount of time before spell goes off.
        /// </summary>
        public int CastingTime { get; set; }
        /// <summary>
        /// Damage message
        /// </summary>
        public string MessageDamage { get; set; }
        /// <summary>
        /// Damage message to victim
        /// </summary>
        public string MessageDamageToVictim { get; set; }
        /// <summary>
        /// Damage message to room
        /// </summary>
        public string MessageDamageToRoom { get; set; }
        /// <summary>
        /// Damage message when target is self
        /// </summary>
        public string MessageDamageToSelf { get; set; }
        /// <summary>
        /// Damage message displayed to room when target is self
        /// </summary>
        public string MessageDamageSelfToRoom { get; set; }
        /// <summary>
        /// Kill Message
        /// </summary>
        public string MessageKill { get; set; }
        /// <summary>
        /// Wear off message
        /// </summary>
        public string MessageWearOff { get; set; }
        /// <summary>
        /// Spell completion message.  Shown to caster when spell completes.
        /// </summary>
        public string MessageCompleted { get; set; }
        /// <summary>
        /// Spell completion message.  Shown to target when spell completes.
        /// </summary>
        public string MessageCompletedToTarget { get; set; }
        /// <summary>
        /// Spell completion message.  Shown to room when spell completes.
        /// </summary>
        public string MessageCompletedToRoom { get; set; }
        /// <summary>
        /// Skill realm requirements (conjuration, evocation, etc.)
        /// </summary>
        public int School { get; set; }
        /// <summary>
        /// Elemental Mana type (for spells)
        /// </summary>
        public int ManaType { get; set; }
        /// <summary>
        /// AI power: order in which spell is checked for AI actions.
        /// Mobs try to cast more powerful spells first.
        /// This typically ranges from 1 to 100, with about 10 points
        /// per spell level, modified by usefulness.
        /// </summary>
        public int AIPower { get; set; }
        /// <summary>
        /// Chance of casting when this spell comes up in the AI list.
        /// </summary>
        public int AIChance { get; set; }
        /// <summary>
        /// Category for AI use.  Is it a spellup, vigorize, heal, offensive, etc.
        /// </summary>
        public AICategory AICategoryType { get; set; }
        /// <summary>
        /// Affect(s) that it gives to a character.  Used primarily for AI.
        /// </summary>
        public int[] Provides { get; set; }
        /// <summary>
        /// Affect(s) that it negates on a character.  Used primarily for AI.
        /// </summary>
        public int[] Negates { get; set; }
        /// <summary>
        /// Modifiers that it sets on a character.
        /// </summary>
        public List<AffectApplyType> Modifies { get; set; }
        /// <summary>
        /// Duration of any effects caused by the spell.
        /// </summary>
        public SpellDurationType Duration { get; set; }
        /// <summary>
        /// Is it a detrimental effect?  Used primarily for AI to determine
        /// whether an effect should be dispelled or cured.
        /// </summary>
        public bool Detrimental { get; set; }
        /// <summary>
        /// Can the spell be scribed?
        /// </summary>
        public bool CanBeScribed { get; set; }
        public StackType StackingType { get; set; }
        public string Code { get; set; }
        /// <summary>
        /// Name of the file where the spell is stored.
        /// </summary>
        [XmlIgnore]
        public string FileName { get; set; }
        /// <summary>
        /// Compiled, executable code for the spell.
        /// </summary>
        [XmlIgnore]
        public Assembly CompiledCode { get; set; }
        /// <summary>
        /// Type of damage that the spell inflicts.
        /// </summary>
        public AttackType.DamageType DamageInflicted { get; set; }
        /// <summary>
        /// Determines what happens if a victim makes a saving throw.
        /// </summary>
        public SavingThrowResult SavingThrowEffect { get; set; }
        /// <summary>
        /// Max level cap for power of spell.  i.e. if spell is 1d6 per level and
        /// cap is 15, max damage is 15d6 at level 15.
        /// </summary>
        public int LevelCap { get; set; }
        public int BaseDamage { get; set; }
        /// <summary>
        /// Total damage is level * damage per level + base damage.
        /// Spell cast at level 10 with 4 damage per level and 5 base damage
        /// would do 10d4+5 damage.
        /// </summary>
        public int DamageDicePerLevel { get; set; }

        /// <summary>
        /// Circle needed by class in order to cast.  This is not stored -- it is set after classes are loaded.
        /// </summary>
        [XmlIgnore]
        public int[] SpellCircle
        {
            get { return _spellCircle; }
            set { _spellCircle = value; }
        }

        /// <summary>
        /// Categories that spell resides in, used for AI logic checks.
        /// </summary>
        [Flags]
        public enum AICategory
        {
            none = 0,
            offensive,
            area_offensive,
            heal,
            vigorize,
            self_spellup,
            targeted_spellup,
            area_spellup,
            cure_blindness,
            remove_poison,
            cure_disease,
            debuff
        }

        /// <summary>
        /// Defines how the spell reacts when cast on someone who already has it.
        /// </summary>
        public enum StackType
        {
            addDuration,
            takeMaxDuration,
            replaceDuration,
            noRefresh,
            addModifierMaxDuration,
            addModifierAddDuration,
            takeMaxModifier,
            takeMaxModifierAndDuration,
            addModifier,
            alwaysReplace
        }

        /// <summary>
        /// Determines what happens if the victim makes a successful saving throw.
        /// </summary>
        public enum SavingThrowResult
        {
            none = 0,
            negates,
            halfDamage,
            halfDamageNoAffects,  // Halves damage and negates any detrimental effects that would have happened.
            fullDamageNoAffects   // Full damage, but negates any detrimental effects that would have happened.
        }

        /// <summary>
        /// Parameterless constructor.  Required for spell serialization.
        /// </summary>
        public Spell()
        {
            Name = String.Empty;
            ValidTargets = TargetType.none;
            CanCastInCombat = true;
            MinimumMana = 5;
            CastingTime = 12;
            MessageDamage = String.Empty;
            MessageDamageToVictim = String.Empty;
            MessageDamageToRoom = String.Empty;
            MessageDamageToSelf = String.Empty;
            MessageDamageSelfToRoom = String.Empty;
            MessageKill = String.Empty;
            MessageWearOff = String.Empty;
            School = Magic.SCHOOL_NONE;
            ManaType = Magic.MANA_NONE;
            CanBeScribed = true;
            Code = String.Empty;
            FileName = "None.xml";
            CompiledCode = null;
            AIPower = 10;
            AICategoryType = AICategory.none;
            AIChance = 10;
            Provides = new int[Limits.NUM_AFFECT_VECTORS];
            Negates = new int[Limits.NUM_AFFECT_VECTORS];
            Detrimental = false;
            DamageInflicted = AttackType.DamageType.none;
            SavingThrowEffect = SavingThrowResult.halfDamageNoAffects;
            LevelCap = 41;
            BaseDamage = 1;
            DamageDicePerLevel = 6;
        }

        /// <summary>
        /// Multi-parameter constructor.  Obsoleted by spell saving/loading.
        /// </summary>
        /// <param name="nam"></param>
        /// <param name="func"></param>
        /// <param name="targ"></param>
        /// <param name="combat"></param>
        /// <param name="ignoredValue"></param>
        /// <param name="minmana"></param>
        /// <param name="time"></param>
        /// <param name="dmg"></param>
        /// <param name="dmgvict"></param>
        /// <param name="dmgroom"></param>
        /// <param name="dmgself"></param>
        /// <param name="dmgselfrm"></param>
        /// <param name="kill"></param>
        /// <param name="wearoff"></param>
        /// <param name="schools"></param>
        /// <param name="manatype"></param>
        /// <param name="scribe"></param>
        public Spell( string nam, SpellFun func, TargetType targ, bool combat, string ignoredValue, int minmana, int time, string dmg, string dmgvict, string dmgroom, string dmgself, string dmgselfrm, string kill, string wearoff, int schools, int manatype, bool scribe )
        {
            Name = nam;
            ValidTargets = targ;
            CanCastInCombat = combat;
            MinimumMana = minmana;
            CastingTime = time;
            MessageDamage = dmg;
            MessageDamageToVictim = dmgvict;
            MessageDamageToRoom = dmgroom;
            MessageDamageToSelf = dmgself;
            MessageDamageSelfToRoom = dmgselfrm;
            MessageKill = kill;
            MessageWearOff = wearoff;
            School = schools;
            ManaType = manatype;
            CanBeScribed = scribe;
            Code = String.Empty;
            FileName = "None.xml";
            CompiledCode = null;
            AIPower = 10;
            AICategoryType = AICategory.none;
            AIChance = 10;
            Provides = new int[Limits.NUM_AFFECT_VECTORS];
            Negates = new int[Limits.NUM_AFFECT_VECTORS];
            Detrimental = false;
            DamageInflicted = AttackType.DamageType.none;
            SavingThrowEffect = SavingThrowResult.halfDamageNoAffects;
            LevelCap = 41;
            BaseDamage = 1;
            DamageDicePerLevel = 6;
            if (!String.IsNullOrEmpty(wearoff) && wearoff.StartsWith("!"))
            {
                MessageWearOff = String.Empty;
            }
        }

        /// <summary>
        /// Gets the total number of spells in existence
        /// </summary>
        public static int Count
        {
            get
            {
                return SpellList.Count;
            }
        }

        /// <summary>
        /// Lets us use boolean operator to check for null.
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static implicit operator bool(Spell ch)
        {
            if (ch == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Loads all spells from disk.
        /// </summary>
        /// <returns></returns>
        public static bool LoadSpells()
        {
            string spellList = String.Format("{0}{1}", FileLocation.SpellDirectory, FileLocation.SpellLoadList);
            try
            {
                FileStream fpList = File.OpenRead(spellList);
                StreamReader sr = new StreamReader(fpList);

                while (!sr.EndOfStream)
                {
                    string filename = sr.ReadLine();

                    if (filename[0] == '$')
                    {
                        break;
                    }

                    if (!Load(FileLocation.SpellDirectory + filename))
                    {
                        string bugbuf = "Cannot load spell file: " + filename;
                        Log.Error(bugbuf, 0);
                    }
                }
                sr.Close();
            }
            catch (Exception ex)
            {
                Log.Error("Exception in Spell.LoadSpells(): " + ex);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Saves all spells to disk.
        /// </summary>
        /// <returns></returns>
        public static bool SaveSpells()
        {
            string spellList = String.Empty;
            foreach (KeyValuePair<String, Spell> kvp in Spell.SpellList)
            {
                kvp.Value.Save();
                spellList += kvp.Value.FileName + "\n";
            }
            spellList += "$";
            FileStream fpList = File.OpenWrite(FileLocation.SpellDirectory + FileLocation.SpellLoadList);
            StreamWriter sw = new StreamWriter(fpList);
            sw.Write(spellList);
            sw.Flush();
            sw.Close();
            return true;
        }

        /// <summary>
        /// Loads a spell from disk.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        static public bool Load(string filename)
        {
            Stream stream = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Spell));
                stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.None);
                Spell spell = (Spell)serializer.Deserialize(stream);
                spell.FileName = Path.GetFileName(filename);
                
                stream.Close();

                // Do any necessary data correction or association.
                if (spell.Provides.Length == 0)
                {
                    spell.Provides = new int[Limits.NUM_AFFECT_VECTORS];
                }

                if (spell.Negates.Length == 0)
                {
                    spell.Negates = new int[Limits.NUM_AFFECT_VECTORS];
                }

                Spell.SpellList.Add(spell.Name, spell);
                return true;
            }
            catch (ArgumentException)
            {
                Log.Error("Attempted to load a spell with a duplicate name from file " + filename + ".  Please check " + FileLocation.SpellLoadList + " to make sure that the same spell file isn't listed twice.");
                if (stream != null)
                {
                    stream.Close();
                }
                return false;
            }
            catch (Exception ex)
            {
                Log.Error("Exception loading spell file: " + filename + ex);
                if (stream != null)
                {
                    stream.Close();
                }
                return false;
            }
        }

        /// <summary>
        /// Saves a spell to disk.
        /// </summary>
        public void Save()
        {
            XmlSerializer serializer = new XmlSerializer(GetType());
            Stream stream = new FileStream(FileLocation.SpellDirectory + FileName, FileMode.Create,
                FileAccess.Write, FileShare.None);
            serializer.Serialize(stream, this);
            stream.Close();
        }

        /// <summary>
        /// Invokes a spell by calling either that spell's compiled code or the generic spell processing
        /// function, whichever is appropriate.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="level"></param>
        /// <param name="target"></param>
        public void Invoke(CharData ch, int level, Target target)
        {
            if (CompiledCode == null)
            {
                GenericSpellFunction(ch, level, target);
            }
            else
            {
                object o = CompiledCode.CreateInstance("ModernMUD.SpellScript");
                Type type = o.GetType();
                MethodInfo[] methods = type.GetMethods();
                try
                {
                    type.InvokeMember("Execute", BindingFlags.Default|BindingFlags.InvokeMethod, null, o, new object[] { ch, this, level, target });
                }
                catch (Exception ex)
                {
                    Log.Error("Error executing spell code for " + Name + ":" + ex.ToString());
                }
            }
        }

        /// <summary>
        /// Generic spell processing function.  Handles basic spells based on values set in the spell file.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="level"></param>
        /// <param name="target"></param>
        public void GenericSpellFunction(CharData ch, int level, Target target)
        {
            switch (ValidTargets)
            {
                case TargetType.singleCharacterOffensive:
                    {
                        CharData opponent = (CharData)target;

                        if (level > LevelCap)
                        {
                            level = LevelCap;
                        }

                        int damage = MUDMath.Dice( level, DamageDicePerLevel ) + BaseDamage;

                        bool saved = Magic.SpellSavingThrow(level, opponent, DamageInflicted);
                        bool affects = true;

                        if (saved)
                        {
                            switch (SavingThrowEffect)
                            {
                                case SavingThrowResult.negates:
                                    damage = 0;
                                    affects = false;
                                    ch.SendText("Nothing happens.\r\n");
                                    return;
                                case SavingThrowResult.halfDamage:
                                    damage /= 2;
                                    affects = true;
                                    break;
                                case SavingThrowResult.halfDamageNoAffects:
                                    damage /= 2;
                                    affects = true;
                                    break;
                                case SavingThrowResult.fullDamageNoAffects:
                                    affects = false;
                                    break;
                                case SavingThrowResult.none:
                                    affects = true;
                                    break;
                            }
                        }

                        if (damage > 0)
                        {
                            Combat.InflictSpellDamage(ch, opponent, damage, this, DamageInflicted);
                        }

                        if (affects)
                        {
                            // Apply "negates" to character.
                            for (int n = 0; n < Negates.Length; n++)
                            {
                                opponent.RemoveAffect(new Bitvector(n, Negates[n]));
                            }
                            Affect af = new Affect();
                            af.Level = ch._level;
                            af.BitVectors = Provides;
                            af.Value = Name;
                            af.Type = Affect.AffectType.spell;
                            for (int p = 0; p < Provides.Length; p++)
                            {
                                if (Provides[p] != 0)
                                {
                                    if (!ch.IsAffected(new Bitvector(p, Provides[p])))
                                    {
                                        opponent.AddAffect(af);
                                        if (!String.IsNullOrEmpty(MessageCompleted))
                                        {
                                            SocketConnection.Act(MessageCompleted, ch, null, null, SocketConnection.MessageTarget.character);
                                        }
                                        else
                                        {
                                            ch.SendText("Ok.\r\n");
                                        }
                                        if (!String.IsNullOrEmpty(MessageCompletedToTarget))
                                        {
                                            SocketConnection.Act(MessageCompletedToTarget, ch, opponent, null, SocketConnection.MessageTarget.victim);
                                        }
                                        if (!String.IsNullOrEmpty(MessageCompletedToRoom))
                                        {
                                            SocketConnection.Act(this.MessageCompletedToRoom, ch, opponent, null, SocketConnection.MessageTarget.room);
                                        }
                                    }
                                    else
                                    {
                                        switch (StackingType)
                                        {
                                            case StackType.addDuration:
                                                ch.SendText("Your spell has no effect.\r\n");
                                                throw new NotSupportedException("Spell addDuration stacking types are not yet supported.");
                                            case StackType.addModifier:
                                                ch.SendText("Your spell has no effect.\r\n");
                                                throw new NotSupportedException("Spell addModifier stacking types are not yet supported.");
                                            case StackType.addModifierAddDuration:
                                                ch.SendText("Your spell has no effect.\r\n");
                                                throw new NotSupportedException("Spell addModifierAddDuration stacking types are not yet supported.");
                                            case StackType.addModifierMaxDuration:
                                                ch.SendText("Your spell has no effect.\r\n");
                                                throw new NotSupportedException("Spell addModifierMaxDuration stacking types are not yet supported.");
                                            case StackType.alwaysReplace:
                                                ch.SendText("Your spell has no effect.\r\n");
                                                throw new NotSupportedException("Spell alwaysReplace stacking types are not yet supported.");
                                            case StackType.noRefresh:
                                                ch.SendText("Your spell has no effect.\r\n");
                                                break;
                                            case StackType.replaceDuration:
                                                ch.SendText("Your spell has no effect.\r\n");
                                                throw new NotSupportedException("Spell replaceDuration stacking types are not yet supported.");
                                            case StackType.takeMaxDuration:
                                                ch.SendText("Your spell has no effect.\r\n");
                                                throw new NotSupportedException("Spell takeMaxDuration stacking types are not yet supported.");
                                            case StackType.takeMaxModifier:
                                                ch.SendText("Your spell has no effect.\r\n");
                                                throw new NotSupportedException("Spell takeMaxModifier stacking types are not yet supported.");
                                            case StackType.takeMaxModifierAndDuration:
                                                ch.SendText("Your spell has no effect.\r\n");
                                                throw new NotSupportedException("Spell takeMaxModifierAndDuration stacking types are not yet supported.");
                                        }
                                    }
                                }
                            }
                        }

                        return;
                    }
                case TargetType.singleCharacterDefensive:
                case TargetType.self:
                    {
                        CharData victim;
                        if (ValidTargets == TargetType.self)
                        {
                            victim = ch;
                        }
                        else
                        {
                            victim = (CharData)target;
                            if (victim == null)
                            {
                                victim = ch;
                            }
                        }

                        for (int n = 0; n < Negates.Length; n++)
                        {
                            victim.RemoveAffect(new Bitvector(n, Negates[n]));
                        }

                        Affect af = new Affect();
                        af.Level = ch._level;
                        af.BitVectors = Provides;
                        af.Value = Name;
                        af.Type = Affect.AffectType.spell;
                        switch (this.Duration)
                        {
                            case SpellDurationType.oneHourPerlevel:
                                af.Duration = level;
                                break;
                            case SpellDurationType.quarterHourPerLevel:
                                af.Duration = level / 4;
                                break;
                            case SpellDurationType.halfHourPerLevel:
                                af.Duration = level / 2;
                                break;
                            case SpellDurationType.oneDay:
                                af.Duration = 24;
                                break;
                            case SpellDurationType.threeHoursPerLevel:
                                af.Duration = level * 3;
                                break;
                            case SpellDurationType.twoHoursPerLevel:
                                af.Duration = level * 2;
                                break;
                            case SpellDurationType.fourHoursPerLevel:
                                af.Duration = level * 4;
                                break;
                            case SpellDurationType.oneHour:
                                af.Duration = 1;
                                break;
                            case SpellDurationType.permanent:
                                af.Duration = -1;
                                break;
                            case SpellDurationType.sixHours:
                                af.Duration = 6;
                                break;
                            case SpellDurationType.threeHours:
                                af.Duration = 3;
                                break;
                            case SpellDurationType.twoHours:
                                af.Duration = 2;
                                break;
                            default:
                                throw new NotSupportedException("Spells with duration type " + Duration + " are not implemented yet.");
                        }
                        foreach (AffectApplyType apply in Modifies)
                        {
                            af.AddModifier(apply.Location, apply.Amount);
                        }

                        for( int p = 0; p < Provides.Length; p++ )
                        {
                            if( Provides[p] != 0 )
                            {
                                if (!ch.IsAffected(new Bitvector(p, Provides[p])))
                                {
                                    victim.AddAffect(af);
                                    if (!String.IsNullOrEmpty(MessageCompleted))
                                    {
                                        SocketConnection.Act(MessageCompleted, ch, null, null, SocketConnection.MessageTarget.character);
                                    }
                                    else
                                    {
                                        ch.SendText("Ok.\r\n");
                                    }
                                    if (!String.IsNullOrEmpty(MessageCompletedToTarget))
                                    {
                                        SocketConnection.Act(MessageCompletedToTarget, ch, victim, null, SocketConnection.MessageTarget.victim);
                                    }
                                    if (!String.IsNullOrEmpty(MessageCompletedToRoom))
                                    {
                                        SocketConnection.Act(MessageCompletedToRoom, ch, victim, null, SocketConnection.MessageTarget.room);
                                    }
                                }
                                else
                                {
                                    switch (StackingType)
                                    {
                                        case StackType.addDuration:
                                            ch.SendText("Your spell has no effect.\r\n");
                                            throw new NotSupportedException("Spell addDuration stacking types are not yet supported.");
                                        case StackType.addModifier:
                                            ch.SendText("Your spell has no effect.\r\n");
                                            throw new NotSupportedException("Spell addModifier stacking types are not yet supported.");
                                        case StackType.addModifierAddDuration:
                                            ch.SendText("Your spell has no effect.\r\n");
                                            throw new NotSupportedException("Spell addModifierAddDuration stacking types are not yet supported.");
                                        case StackType.addModifierMaxDuration:
                                            ch.SendText("Your spell has no effect.\r\n");
                                            throw new NotSupportedException("Spell addModifierMaxDuration stacking types are not yet supported.");
                                        case StackType.alwaysReplace:
                                            ch.SendText("Your spell has no effect.\r\n");
                                            throw new NotSupportedException("Spell alwaysReplace stacking types are not yet supported.");
                                        case StackType.noRefresh:
                                            ch.SendText("Your spell has no effect.\r\n");
                                            break;
                                        case StackType.replaceDuration:
                                            ch.SendText("Your spell has no effect.\r\n");
                                            throw new NotSupportedException("Spell replaceDuration stacking types are not yet supported.");
                                        case StackType.takeMaxDuration:
                                            ch.SendText("Your spell has no effect.\r\n");
                                            throw new NotSupportedException("Spell takeMaxDuration stacking types are not yet supported.");
                                        case StackType.takeMaxModifier:
                                            ch.SendText("Your spell has no effect.\r\n");
                                            throw new NotSupportedException("Spell takeMaxModifier stacking types are not yet supported.");
                                        case StackType.takeMaxModifierAndDuration:
                                            ch.SendText("Your spell has no effect.\r\n");
                                            throw new NotSupportedException("Spell takeMaxModifierAndDuration stacking types are not yet supported.");
                                    }
                                }
                            }
                        }
                    }
                    break;
                case TargetType.multipleCharacterOffensive:
                    {
                    }
                    break;
                case TargetType.objectCorpse:
                    {
                    }
                    break;
                case TargetType.objectInInventory:
                    {
                    }
                    break;
                case TargetType.objectInRoom:
                    {
                    }
                    break;
                case TargetType.objectOrCharacter:
                    {
                    }
                    break;
                case TargetType.ritual:
                    {
                    }
                    break;
                case TargetType.singleCharacterRanged:
                    {
                        CharData opponent = (CharData)target;

                        if (level > LevelCap)
                            level = LevelCap;

                        int damage = MUDMath.Dice(level, DamageDicePerLevel) + BaseDamage;

                        bool saved = Magic.SpellSavingThrow(level, opponent, DamageInflicted);
                        bool affects = true;

                        if (saved)
                        {
                            switch (SavingThrowEffect)
                            {
                                case SavingThrowResult.negates:
                                    damage = 0;
                                    affects = false;
                                    break;
                                case SavingThrowResult.halfDamage:
                                    damage /= 2;
                                    affects = true;
                                    break;
                                case SavingThrowResult.halfDamageNoAffects:
                                    damage /= 2;
                                    affects = false;
                                    break;
                                case SavingThrowResult.fullDamageNoAffects:
                                    affects = false;
                                    break;
                                case SavingThrowResult.none:
                                    affects = true;
                                    break;
                            }
                        }

                        if (damage > 0)
                        {
                            Combat.InflictSpellDamage(ch, opponent, damage, this, DamageInflicted);
                        }

                        if (affects)
                        {
                            // TODO: Apply "provides" to character.
                            // TODO: Apply "negates" to character.
                        }

                        return;
                    }
                case TargetType.singleCharacterWorld:
                    {
                    }
                    break;
                case TargetType.trap:
                    {
                    }
                    break;
                case TargetType.none:
                    {
                        Log.Error("Magic spell '" + Name + "' is flagged as TargetType.none.  This should never happen.  Fix it.");
                    }
                    break;
            }
        }

        /// <summary>
        /// Formats the spell information for a terminal window.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string text = String.Format("\r\nSpell: '{0}' ", Name);
            text += String.Format("Minimum mana: {0} Castable in battle: {1}\r\n",
                    MinimumMana,
                    CanCastInCombat ? "Yes" : "No");
            text += String.Format("Targets: {0}  Mana Type: {1}  School: {2}\r\n",
                    ValidTargets, ManaType, StringConversion.SpellSchoolString(School));
            text += String.Format("Duration: {0}  Save: {1}  Stack: {2}\r\n",
                    Duration, SavingThrowEffect, StackingType);
            text += String.Format("Dmg Type: {0}  Cast Time: {1}  Dmg: 1d{2}/lvl+{3} Cap: Lv {4}\r\n",
                    DamageInflicted.ToString(), CastingTime, DamageDicePerLevel, BaseDamage, LevelCap);
            text += String.Format("AI Pwr: {0}  AI %: {1}  AI Type: {2}  Detriment: {3}  Scribe: {4}\r\n",
                    AIPower, AIChance, AICategoryType, Detrimental, CanBeScribed);
            text += String.Format("Cast Msg: {0}\r\nCast To Vict: {1}\r\n",
                    MessageCompleted, MessageCompletedToTarget);
            text += String.Format("Cast To Room: {0}\r\n",
                    MessageCompletedToRoom);
            text += String.Format("Dmg To Room: {0}\r\nDmg To Vict: {1}\r\n",
                    MessageDamageToRoom,MessageDamageToVictim);
            text += String.Format("Dmg To Room: {0}\r\nDmg To Self: {1}\r\n",
                    MessageDamageSelfToRoom, MessageDamageToSelf);
            text += String.Format("Damage Msg: {0}\r\nKill Msg: {1}\r\n",
                    MessageDamage, MessageKill);
            text += String.Format("Wear Off Msg: {0}\r\n",
                    MessageWearOff);
            text += String.Format("Provides: {0}\r\n",
                    BitvectorFlagType.AffectString(Provides, false));
            text += String.Format("Negates: {0}\r\n",
                    BitvectorFlagType.AffectString(Negates, false));
            text += String.Format("Modifies: {0}\r\n",
                    "");
            text += "\r\nClass Availability:\r\n";
            for (int count = 0; count < CharClass.ClassList.Length; ++count)
            {
                text += String.Format("  {0}: {1:00}", MUDString.PadStr(CharClass.ClassList[count].Name, 18),
                       _spellCircle[count]);
                if (count % 3 == 2)
                    text += "\r\n";
            }
            text += "\r\n";
            return text;           
        }

        /// <summary>
        /// Summoning an elemental.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="spell"></param>
        /// <param name="level"></param>
        /// <param name="indexNumber"></param>
        public static void SummonElem(CharData ch, Spell spell, int level, int indexNumber)
        {
            int numpets = 0;
            foreach (CharData petChar in Database.CharList)
            {
                if (petChar != ch && petChar._master == ch &&
                        petChar.IsNPC() && petChar.HasActionBit(MobTemplate.ACT_PET))
                    numpets++;
            }

            //just a WAG as far as number...check for some consistency with necro
            int maxpets = ch._level / 20 + ch.GetCurrCha() / 35;
            if (ch._level >= Limits.LEVEL_AVATAR)
            {
                string text = String.Format("You can summon at most {0} pets.\r\n", maxpets);
                ch.SendText(text);
            }
            if (numpets >= maxpets)
            {
                ch.SendText("You cannot handle any more pets!\r\n");
                return;
            }
            CharData elemental = Database.CreateMobile(Database.GetMobTemplate(indexNumber));
            elemental.AddToRoom(ch._inRoom);

            elemental.SetCoins(0, 0, 0, 0);
            elemental._level = 27 + MUDMath.Dice(2, 5);
            elemental._maxHitpoints = 150 + MUDMath.Dice(16, level / 2);
            elemental._hitpoints = elemental._maxHitpoints;
            elemental._armorPoints -= (ch._level * 3 / 2);
            elemental.SetActionBit(MobTemplate.ACT_NOEXP);

            switch (indexNumber)
            {
                case StaticMobs.MOB_NUMBER_EARTH_PECH:
                    SocketConnection.Act("$n&n rises from the ground before your eyes.", elemental,
                         null, null, SocketConnection.MessageTarget.room);
                    break;
                case StaticMobs.MOB_NUMBER_AIR_SLYPH:
                    elemental._armorPoints = -130;
                    elemental._permAgility = 100;
                    SocketConnection.Act("$n&n appears out of thin air.", elemental, null, null, SocketConnection.MessageTarget.room);
                    break;
                case StaticMobs.MOB_NUMBER_FIRE_SERPENT:
                    SocketConnection.Act("$n&n bursts into existence in a roaring ball of flame.",
                         elemental, null, null, SocketConnection.MessageTarget.room);
                    break;
                case StaticMobs.MOB_NUMBER_WATER_NEREID:
                    SocketConnection.Act("$n&n coalesces into existence.", elemental, null, null, SocketConnection.MessageTarget.room);
                    elemental._maxHitpoints += 100;
                    elemental._hitpoints = elemental._maxHitpoints;
                    break;
                default:
                    Log.Error("SummonElem: bad indexNumber in switch: " + indexNumber);
                    ch.SendText("You managed to summon a bad indexNumber! Shame on you.\r\n");
                    break;
            }
            SocketConnection.Act("$N&n says 'Your wish is my command $n&n.'", ch, null, elemental, SocketConnection.MessageTarget.room);
            SocketConnection.Act("$N&n tells you 'Your wish is my command.'", ch, null, elemental, SocketConnection.MessageTarget.character);
            CharData.AddFollower(elemental, ch);
            elemental._master = ch;
            Affect af = new Affect(Affect.AffectType.spell, spell.Name, level / 2 + MUDMath.Dice(4, level / 2), Affect.Apply.none, 0, Affect.AFFECT_CHARM);
            elemental.AddAffect(af);
            // Set the MobIndex.ACT_PET bit as well
            elemental.SetActionBit(MobTemplate.ACT_PET);
            elemental._flyLevel = ch._flyLevel;
            if (ch._fighting)
            {
                Combat.SetFighting(elemental, ch._fighting);
            }

            return;
        }

        /// <summary>
        /// Creating a portal.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="spell"></param>
        /// <param name="level"></param>
        /// <param name="target"></param>
        /// <param name="indexNumber"></param>
        public static void MakePortal(CharData ch, Spell spell, int level, Target target, int indexNumber)
        {
            Room location;
            CharData victim = (CharData)target;
            Room original = ch._inRoom;

            if (ch.IsNPC())
                return;
            if (!victim)
            {
                ch.SendText("Who exactly is your target?\r\n");
                return;
            }
            if (victim.IsNPC() && !ch.IsImmortal())
                return;

            if (!Magic.HasSpellConsent(ch, victim))
            {
                return;
            }

            if (victim == ch || ch._inRoom == victim._inRoom)
            {
                ch.SendText("Seems like a waste of time.\r\n");
                return;
            }

            if (!(location = victim._inRoom)
                    || victim._inRoom.HasFlag(RoomTemplate.ROOM_SAFE)
                    || victim._inRoom.HasFlag(RoomTemplate.ROOM_PRIVATE)
                    || victim._inRoom.HasFlag(RoomTemplate.ROOM_SOLITARY))
            {
                ch.SendText("You can't seem to get a fix their location.\r\n");
                return;
            }
            if (!victim.IsNPC() && (ch.IsRacewar(victim)) && !ch.IsImmortal())
            {
                ch.SendText("Don't you wish it was that easy!\r\n");
                return;
            }

            Object portal = Database.CreateObject(Database.GetObjTemplate(indexNumber), 0);

            if (victim._inRoom.HasFlag(RoomTemplate.ROOM_NO_GATE)
                    || ch._inRoom.HasFlag(RoomTemplate.ROOM_NO_GATE))
            {
                SocketConnection.Act("$p opens for a brief instant and then collapses.&n", ch, portal, null, SocketConnection.MessageTarget.character);
                SocketConnection.Act("$p opens for a brief instant and then collapses.&n", ch, portal, null, SocketConnection.MessageTarget.room);
                SocketConnection.Act("$p opens for a brief instant and then collapses.&n", victim, portal, null, SocketConnection.MessageTarget.character);
                SocketConnection.Act("$p opens for a brief instant and then collapses.&n", victim, portal, null, SocketConnection.MessageTarget.room);
                portal.RemoveFromWorld();
                return;
            }

            portal.Timer = level / 15;
            portal.Values[2] = level / 7;
            portal.Values[0] = location.IndexNumber;

            portal.AddToRoom(original);

            portal = Database.CreateObject(Database.GetObjTemplate(indexNumber), 0);
            portal.Timer = level / 15;
            portal.Values[2] = level / 7;
            portal.Values[0] = original.IndexNumber;

            portal.AddToRoom(location);

            switch (indexNumber)
            {
                case StaticObjects.OBJECT_NUMBER_PORTAL:
                    SocketConnection.Act("$p&+Y rises up from the ground.&n", ch, portal, null, SocketConnection.MessageTarget.room);
                    SocketConnection.Act("$p&+Y rises up before you.&n", ch, portal, null, SocketConnection.MessageTarget.character);

                    if (location.People.Count > 0)
                    {
                        SocketConnection.Act("$p&+Y rises up from the ground.&n", location.People[0], portal, null,
                             SocketConnection.MessageTarget.room);
                        SocketConnection.Act("$p&+Y rises up from the ground.&n", location.People[0], portal, null,
                             SocketConnection.MessageTarget.character);
                    }
                    break;
                case StaticObjects.OBJECT_NUMBER_MOONWELL:
                    SocketConnection.Act("&+WSilver mists swirl and form into a $p.&n", ch, portal, null, SocketConnection.MessageTarget.room);
                    SocketConnection.Act("&+WSilver mists swirl and form into a $p.&n", ch, portal, null, SocketConnection.MessageTarget.character);

                    if (location.People.Count > 0)
                    {
                        SocketConnection.Act("&+WSilver mists swirl and form into a $p.&n", location.People[0], portal, null,
                             SocketConnection.MessageTarget.room);
                        SocketConnection.Act("&+WSilver mists swirl and form into a $p.&n", location.People[0], portal, null,
                             SocketConnection.MessageTarget.character);
                    }
                    break;
                case StaticObjects.OBJECT_NUMBER_WORMHOLE:
                    SocketConnection.Act("$p&+L appears from a warping of space and time.&n", ch, portal, null, SocketConnection.MessageTarget.room);
                    SocketConnection.Act("$p&+L appears from a warping of space and time.&n", ch, portal, null, SocketConnection.MessageTarget.character);

                    if (location.People.Count > 0)
                    {
                        SocketConnection.Act("$p&+L appears from a warping of space and time.&n", location.People[0], portal, null,
                             SocketConnection.MessageTarget.room);
                        SocketConnection.Act("$p&+L appears from a warping of space and time.&n", location.People[0], portal, null,
                             SocketConnection.MessageTarget.character);
                    }
                    break;
            }

            ch.WaitState(8);

            return;
        }
    }
}
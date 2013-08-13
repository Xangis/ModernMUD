using System;
using System.Collections.Generic;
using System.Text;

namespace ModernMUD
{
    /// <summary>
    /// Represents a custom action that can be used by a mob, object, race, or class.
    /// 
    /// Used for special procs and triggers.
    /// </summary>
    [Serializable]
    public class CustomAction
    {
        public ActionTriggerType Trigger { get; set; }
        public List<Action> Actions { get; set; }
        public String TriggerData { get; set; }
        public int Chance { get; set; }

        public CustomAction()
        {
            Actions = new List<Action>();
            Chance = 100;
            TriggerData = String.Empty;
        }

        public struct Action
        {
            public String ActionData;
            public ActionType Type;
        }

        public enum ActionTriggerType
        {
            Speech,
            Random,
            Entry,
            Exit,
            Engage,
            Combat,
            Death,
            ReceiveItem,
            ReceiveMoney,
            Action,
            PlayerEntry,
            PlayerExit,
            ObjectInRoom,
            Hour,
            Weekday,
            Month,
        }

        public enum ActionType
        {
            InterpretCommand,
            CastSpell,
            UseSkill,
            Say,
            Move,
            PreventAction,
            Attack,
            Teleport,
            TeleportPlayer,
            TeleportAll,
            CreateObject,
            CreateMobile,
            GiveObject,
            Delay,
            Echo,
            DestroyObject,
            ZoneEcho,
            Destroy,
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(WhenString(Trigger));
            if( CustomAction.UsesTriggerData(Trigger))
            {
                sb.Append(" " + TriggerData);
            }
            sb.Append(", ");
            foreach (Action action in Actions)
            {
                sb.Append(ActionString(action.Type));
                if( CustomAction.UsesActionData(action.Type))
                {
                    sb.Append(" " + action.ActionData);
                }
                sb.Append(". ");
            }
            sb.Append("(" + this.Chance.ToString() + "%)");
            return sb.ToString();
        }

        public static String WhenString(ActionTriggerType type)
        {
            switch( type )
            {
                case ActionTriggerType.Action:
                    return "When someone performs the action";
                case ActionTriggerType.Combat:
                    return "When in combat";
                case ActionTriggerType.Death:
                    return "When killled or destroyed";
                case ActionTriggerType.Engage:
                    return "When combat is initiated";
                case ActionTriggerType.Entry:
                    return "When entering a room";
                case ActionTriggerType.Exit:
                    return "When exiting a room";
                case ActionTriggerType.ObjectInRoom:
                    return "When room contains object";
                case ActionTriggerType.PlayerEntry:
                    return "When a player enters the room";
                case ActionTriggerType.PlayerExit:
                    return "When a player leaves the room";
                case ActionTriggerType.Random:
                    return "A random chance";
                case ActionTriggerType.ReceiveItem:
                    return "When receiving an item";
                case ActionTriggerType.ReceiveMoney:
                    return "When receiving money";
                case ActionTriggerType.Speech:
                    return "When someone says";
                case ActionTriggerType.Weekday:
                    return "When the day of the week is";
                case ActionTriggerType.Hour:
                    return "When the hour is";
                case ActionTriggerType.Month:
                    return "When the month is";
                default:
                    return "(invalid)";
            }
        }

        public static bool UsesTriggerData(ActionTriggerType type)
        {
            switch (type)
            {
                case ActionTriggerType.Action:
                    return true;
                case ActionTriggerType.Combat:
                    return false;
                case ActionTriggerType.Death:
                    return false;
                case ActionTriggerType.Engage:
                    return false;
                case ActionTriggerType.Entry:
                    return false;
                case ActionTriggerType.Exit:
                    return false;
                case ActionTriggerType.ObjectInRoom:
                    return true;
                case ActionTriggerType.PlayerEntry:
                    return false;
                case ActionTriggerType.PlayerExit:
                    return true;
                case ActionTriggerType.Random:
                    return false;
                case ActionTriggerType.ReceiveItem:
                    return true;
                case ActionTriggerType.ReceiveMoney:
                    return true;
                case ActionTriggerType.Speech:
                    return true;
                case ActionTriggerType.Weekday:
                    return true;
                case ActionTriggerType.Hour:
                    return true;
                case ActionTriggerType.Month:
                    return true;
                default:
                    return false;
            }
        }

        public static string ActionString(ActionType action)
        {
            switch (action)
            {
                case ActionType.Attack:
                    return "Attack";
                case ActionType.CastSpell:
                    return "Cast the spell";
                case ActionType.CreateMobile:
                    return "Create the mobile";
                case ActionType.CreateObject:
                    return "Create the object";
                case ActionType.DestroyObject:
                    return "Destroy the object";
                case ActionType.Delay:
                    return "Wait for this many seconds";
                case ActionType.Echo:
                    return "Echo text to the room";
                case ActionType.GiveObject:
                    return "Give player the object";
                case ActionType.InterpretCommand:
                    return "Interpret the command";
                case ActionType.Move:
                    return "Move in direction";
                case ActionType.PreventAction:
                    return "Prevent the action";
                case ActionType.Say:
                    return "Say";
                case ActionType.Teleport:
                    return "Teleport to";
                case ActionType.TeleportPlayer:
                    return "Teleport the player to";
                case ActionType.TeleportAll:
                    return "Teleport all players in the room to";
                case ActionType.UseSkill:
                    return "Use the skill";
                case ActionType.ZoneEcho:
                    return "Echo text to the zone";
                case ActionType.Destroy:
                    return "Destroy self";
                default:
                    return "(invalid)";
            }
        }

        public static bool UsesActionData(ActionType action)
        {
            switch (action)
            {
                case ActionType.Attack:
                    return false;
                case ActionType.CastSpell:
                    return true;
                case ActionType.CreateMobile:
                    return true;
                case ActionType.CreateObject:
                    return true;
                case ActionType.DestroyObject:
                    return true;
                case ActionType.Delay:
                    return true;
                case ActionType.Echo:
                    return true;
                case ActionType.GiveObject:
                    return true;
                case ActionType.InterpretCommand:
                    return true;
                case ActionType.Move:
                    return true;
                case ActionType.PreventAction:
                    return false;
                case ActionType.Say:
                    return true;
                case ActionType.Teleport:
                    return true;
                case ActionType.TeleportPlayer:
                    return true;
                case ActionType.TeleportAll:
                    return true;
                case ActionType.UseSkill:
                    return true;
                case ActionType.ZoneEcho:
                    return true;
                case ActionType.Destroy:
                    return false;
                default:
                    return false;
            }
        }
    }
}

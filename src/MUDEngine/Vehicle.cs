using System;
using System.IO;
namespace MUDEngine
{
    [Serializable]
    // We can cover much of this just be creating a pointer to a ship or vehicle object.
    public class Vehicle
    {
        private VehicleType _type; // Type of vehicle
        private string _ownerName; // _name of owner
        // we can't use CharData because some ships belong to characters not in the game.
        private Object _parentObject; // IndexNumber of object for ship
        private int _entryRoomTemplateNumber; // room in which people start when they
        // board the ship
        private int _controlPanelRoomTemplateNumber;
        private int _hullPoints;
        private int _flyLevel;
        private int _occupants;
        private int _direction; // direction currently moving in
        private int _speed; // current speed
        private int _movementTimer;
        private int _movementDelay;
        private int _movementPointer;
        private string _movementScript;
        private static int _numVehicles;

        // Vehicle/Ship code
        //
        // The control panel will be handled by the index number of the room which contains the ship's
        // control panel.  This may or may not create an object in the control panel room,
        // this remains to be determined.
        //
        // We need a method of determining who is able to fly the ship, possibly a bool flag called
        // locked, which allows only the keyholder to fly the ship.  Can also have a lower level
        // of lock that allows keyholder's group members to fly the ship, and open which allows
        // anyone to fly the ship.  Can also have maximum lock that means only the MUD can move the
        // ship.
        //
        // Automated ships:
        // Ship movement patterns should be a public string of which way to go.. I.E. "NESSENEENEESEENNNW...ESSSWW"
        // These can include up and down for flying ships and the "." means sit there and wait for
        // a bit.
        //
        // Ships are essentially mobile zones and we need a way of representing them on the map or
        // in a room.
        //
        // The disembark command should just scan through all the ship data and see if the room
        // is flagged as an entry room.  If it is, they can disembark.
        //
        // How to accurately keep track of the people entering and leaving a ship?  Occupancy limits?
        // Well, I don't know.
        //
        // Descriptions have to be entered in manually.
        //
        // After creating a ship object that will load successfully, we need to give this to builders
        // and/or include it in the DE distribution and even the creation routines.
        public Vehicle()
        {
            ++_numVehicles;

            _type = 0;
            _parentObject = null;
            _entryRoomTemplateNumber = 0;
            _controlPanelRoomTemplateNumber = 0;
            _hullPoints = 0;
            _flyLevel = 0;
            _occupants = 0;
            _direction = 0;
            _speed = 0;
            _movementTimer = 0;
            _movementDelay = 0;
            _movementPointer = 0;
            _movementScript = String.Empty;
        }

        ~Vehicle()
        {
            --_numVehicles;
        }

        public static int Count
        {
            get
            {
                return _numVehicles;
            }
        }

        public string MovementScript
        {
            get { return _movementScript; }
            set { _movementScript = value; }
        }

        public int MovementPointer
        {
            get { return _movementPointer; }
            set { _movementPointer = value; }
        }

        public int MovementDelay
        {
            get { return _movementDelay; }
            set { _movementDelay = value; }
        }

        public int MovementTimer
        {
            get { return _movementTimer; }
            set { _movementTimer = value; }
        }

        public int Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public int Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        public int Occupants
        {
            get { return _occupants; }
            set { _occupants = value; }
        }

        public int FlyLevel
        {
            get { return _flyLevel; }
            set { _flyLevel = value; }
        }

        public int HullPoints
        {
            get { return _hullPoints; }
            set { _hullPoints = value; }
        }

        public int ControlPanelRoomTemplateNumber
        {
            get { return _controlPanelRoomTemplateNumber; }
            set { _controlPanelRoomTemplateNumber = value; }
        }

        public int EntryRoomTemplateNumber
        {
            get { return _entryRoomTemplateNumber; }
            set { _entryRoomTemplateNumber = value; }
        }

        public Object ParentObject
        {
            get { return _parentObject; }
            set { _parentObject = value; }
        }

        public string OwnerName
        {
            get { return _ownerName; }
            set { _ownerName = value; }
        }

        public VehicleType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public static int NumVehicles
        {
            get { return _numVehicles; }
            set { _numVehicles = value; }
        }

        /*
         * Vehicle Data
         */
        // Vehicle types - Xangis
        public enum VehicleType
        {
            none = 0,               // Invalid, uninitialized, or broken.
            ship_any_water,         // Multi-purpose boats, typically medium sized.
            ship_shallows,          // Rowboats, canoes, and rafts.
            ship_deep_water,        // Giant boats, submarines, other non-beachable ships.
            ship_any_terrain,       // All-terrain ships such as hovercraft, flying carpets, airships.
            no_water,               // All-terrain land vehicles.
            flat_land,              // Flat-land vehicles like carts, buggies, and wagons.
            spelljammer,            // All-terrain spelljammer.
            spelljammer_land,       // Spelljammer, can only land on flat land.
            spelljammer_water,      // Spelljammer, can only land on water.
            spelljammer_sky_only,   // Spelljammer, incapable of landing.
            creature                // Giant creature.
        }

        static void MoveVehicle()
        {
            return;
        }

        static void LoadVehicles( FileStream fp )
        {
            return;
        }
    };
}
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using ModernMUD;

namespace MUDEngine
{
    /// <summary>
    /// Represents "hard-coded" rooms, but handles them in a file rather than in the
    /// source code.  Makes it easy to switch rooms used for various purposes such 
    /// as universal respawn or planar areas without recompiling.
    /// </summary>
    [Serializable]
    public class StaticRooms
    {
        static SerializableDictionary<string, int> _staticRooms = new SerializableDictionary<string, int>();

        static StaticRooms()
        {
        }

        /// <summary>
        /// Gets the number of static rooms that have been defined.
        /// </summary>
        public static int Count
        {
            get
            {
                return _staticRooms.Count;
            }
        }

        /// <summary>
        /// Gets a room from the static room list, or -1 if that room is not in the list.
        /// </summary>
        /// <param name="roomName">The name of the room to look for.</param>
        /// <returns>The room number if found, otherwise -1. Be sure to check for room
        /// existence - it's very possible the room was not found.</returns>
        public static int GetRoomNumber(string roomName)
        {
            if (_staticRooms.ContainsKey(roomName))
            {
                return _staticRooms[roomName];
            }

            return -1;
        }

        /// <summary>
        /// Loads all of the static rooms from disk.
        /// </summary>
        /// <returns></returns>
        public static bool Load()
        {
            string filename = FileLocation.SystemDirectory + FileLocation.StaticRoomFile;
            string blankFilename = FileLocation.BlankSystemFileDirectory + FileLocation.StaticRoomFile;
            Stream stream = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(SerializableDictionary<string,int>));
                try
                {
                    stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.None);
                }
                catch (FileNotFoundException)
                {
                    Log.Info("Could not load static room file. Using blank version.");
                    File.Copy(blankFilename, filename);
                    stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.None);
                }
                _staticRooms = (SerializableDictionary<string, int>)serializer.Deserialize(stream);
                stream.Close();
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Exception in StaticRooms.Load(): " + ex);
                _staticRooms = new SerializableDictionary<string, int>();
                return false;
            }
        }

        /// <summary>
        /// Saves all of the static room mappings.
        /// </summary>
        /// <returns></returns>
        public static bool Save()
        {
            string filename = FileLocation.SystemDirectory + FileLocation.StaticRoomFile;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(SerializableDictionary<string, int>));
                Stream stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
                serializer.Serialize(stream, _staticRooms);
                stream.Close();
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Unable to save StaticRooms: " + ex);
                return false;
            }
        }
    }
}
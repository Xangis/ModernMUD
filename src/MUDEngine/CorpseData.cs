using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using ModernMUD;

namespace MUDEngine
{
    /// <summary>
    /// Represents in-game player corpses.
    /// </summary>
    [Serializable]
    public class CorpseData
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public CorpseData() {}

        /// <summary>
        /// The full list of in-game player corpses.
        /// </summary>
        public static List<Object> CorpseList = new List<Object>();

        /// <summary>
        /// Save player corpses for later loading.
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            string filename = FileLocation.SystemDirectory + FileLocation.CorpseFile;
            try
            {
                XmlSerializer serializer = new XmlSerializer( GetType() );
                Stream stream = new FileStream( filename, FileMode.Create, FileAccess.Write, FileShare.None );
                serializer.Serialize( stream, this );
                stream.Close();
                return true;
            }
            catch( Exception ex )
            {
                Log.Error( "Exception saving corpse file: " + ex );
                return false;
            }
        }

        /// <summary>
        /// Load player corpses and add them to the corpse list.
        /// </summary>
        /// <returns></returns>
        public static CorpseData Load()
        {
            string filename = FileLocation.SystemDirectory + FileLocation.CorpseFile;
            string blankFilename = FileLocation.BlankSystemFileDirectory + FileLocation.CorpseFile;
            XmlSerializer serializer = new XmlSerializer(typeof(CorpseData));
            Stream stream = null;
            try
            {
                try
                {
                    stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.None);
                }
                catch (FileNotFoundException)
                {
                    Log.Info("Corpse file not found, using blank file.");
                    File.Copy(blankFilename, filename);
                    stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.None);
                }
                CorpseData data = (CorpseData)serializer.Deserialize(stream);
                stream.Close();
                return data;
            }
            catch (Exception ex)
            {
                Log.Error("Exception in CorpseData.Load(): " + ex);
                return new CorpseData();
            }
        }

        /// <summary>
        /// Get the number of corpses in game.
        /// </summary>
        public static int Count
        {
            get
            {
                return CorpseList.Count;
            }
        }
    }
}

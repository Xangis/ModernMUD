using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using ModernMUD;

namespace MUDEngine
{
    [Serializable]
    public class CorpseData
    {
        public CorpseData() {}

        public static List<Object> CorpseList = new List<Object>();
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

        public static CorpseData Load()
        {
            string filename = FileLocation.SystemDirectory + FileLocation.CorpseFile;
            try
            {
                XmlSerializer serializer = new XmlSerializer( typeof( CorpseData ) );
                Stream stream = new FileStream( filename, FileMode.Open, FileAccess.Read, FileShare.None );
                CorpseData data = (CorpseData)serializer.Deserialize( stream );
                stream.Close();
                return data;
            }
            catch( Exception ex )
            {
                Log.Error( "Exception in CorpseData.Load(): " + ex );
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

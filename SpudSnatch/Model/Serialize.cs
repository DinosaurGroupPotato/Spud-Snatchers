using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpudSnatch.Model
{
    interface Serialized
    {
        void AddToObjects();
        string Serialize();
        string Deserialize();
        
    }

    class SerializeData
    {
        static List<Serialized> objects = new List<Serialized>();

        static string csv;

        public static string SerializeInfo()
        {
            foreach (Serialized obj in objects)
            {
               csv += obj.Serialize() + "\n";
            }
            return csv;
            //will do research and rework
        }

        public static void DeserializeInfo()
        {
            ///implementation of choosing wich type here
            ///recommend that each type start with its own 2 character identifier

            ///object specific data
            ///x,y,health
        }
    }
}

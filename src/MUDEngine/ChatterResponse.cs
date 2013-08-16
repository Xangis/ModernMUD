using System;

namespace MUDEngine
{
    [Serializable]
    public class ChatterResponse
    {
        public string Keyphrase { get; set; }
        public string Response { get; set; }
        public string Topic { get; set; }
        public int RageModifier { get; set; }
    }
}

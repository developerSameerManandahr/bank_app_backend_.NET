using System.Collections.Generic;

namespace worksheet2.Model.Response
{
    public class Exchange
    {
        public bool Success { get; set; }
        public string Base { get; set; }
        public string Date { get; set; }
        public Dictionary<string, float> Rates { get; set; }
    }
}
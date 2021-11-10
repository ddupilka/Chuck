using System;
using System.Collections.Generic;
using System.Text;

namespace Chuck
{
    public class Class1
    {
        public string userID = "647dad188";
        public string ChuckQuote { get; set; }

        public string EnteredBy = "DD";
        public string id { get; set; }

        public string QuoteDate = DateTime.Now.ToString("MM/dd/yyyy h:m:s");

        public Class1()
        {
        }

        public override string ToString()
        {
            String s = "id: " + this.id + " ChuckFact: " + this.ChuckQuote + " EnteredBy: " + this.EnteredBy + " FactDate: " + this.QuoteDate + " userId: " + this.userID;
            return s;
        }

    }
}


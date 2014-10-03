﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ru.org.openam.sdk.naming
{
//<RequestSet vers="1.0" svcid="com.iplanet.am.naming" reqid="2">
//<Request><![CDATA[<NamingRequest vers="3.0" reqid="2">
//<GetNamingProfile>
//</GetNamingProfile>
//</NamingRequest>]]></Request>
//</RequestSet>
    public class Request: pll.Request
    {
        static int reqid = 1;

        public String SessionID;

        public Request()
            : base()
        {
            svcid = pll.type.naming;
        }

        public Request(String SessionID)
            : this()
        {
            this.SessionID = SessionID;
        }

        override public String ToString()
        {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.Encoding = new UTF8Encoding();
            XmlWriter writer = XmlWriter.Create(sb, settings);
            writer.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"");
            writer.WriteStartElement("NamingRequest");
            writer.WriteAttributeString("vers", "3.0");
            writer.WriteAttributeString("reqid", (reqid++).ToString());
            if (SessionID!=null)
                writer.WriteAttributeString("sessid", SessionID);
            writer.WriteStartElement("GetNamingProfile");
            writer.WriteValue("");
            writer.WriteEndElement();
            writer.Close();
            return sb.ToString();
        }
    }
}

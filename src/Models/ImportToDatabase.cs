using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using OFX.Entities;

namespace OFX.Models
{
    public class ImportToDatabase
    {
        public static List<Transacoes> Ofx(List<string> paths)
        {
            List<Transacoes> result = new List<Transacoes>();

            foreach (var path in paths)
            {
                XElement doc = ToXElement.Convert(path);

                var query = (from c in doc.Descendants("STMTTRN")
                                 //where c.Element("TRNTYPE").Value == "DEBIT"
                             select new Transacoes
                             {
                                 Operation = c.Element("TRNTYPE").Value,
                                 Amount = decimal.Parse(c.Element("TRNAMT").Value.Replace("-", ""),
                                                        NumberFormatInfo.InvariantInfo),
                                 Data = Convert.ToDateTime(DateTime.ParseExact(c.Element("DTPOSTED").Value,
                                                            "yyyyMMdd", null).ToString("dd/MM/yyyy")),
                                 Description = c.Element("MEMO").Value,
                             });

                if (query.Count() > 0)
                {
                    result.AddRange(query);
                }
            }
            return result;
        }
    }
}
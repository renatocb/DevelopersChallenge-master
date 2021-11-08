using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace OFX.Models
{
    public class ToXElement
    {
        public static XElement Convert(string pathToOfxFile)
        {
            if (!System.IO.File.Exists(pathToOfxFile))
            {
                throw new FileNotFoundException();
            }

            var tags = from line in File.ReadAllLines(pathToOfxFile)
                       where line.Contains("<STMTTRN>") ||
                       line.Contains("<TRNTYPE>") ||
                       line.Contains("<DTPOSTED>") ||
                       line.Contains("<TRNAMT>") ||
                       line.Contains("<FITID>") ||
                       line.Contains("<CHECKNUM>") ||
                       line.Contains("<MEMO>")
                       select line;

            XElement el = new XElement("root");
            XElement son = null;

            foreach (var l in tags)
            {
                if (l.IndexOf("<STMTTRN>") != -1)
                {
                    son = new XElement("STMTTRN");
                    el.Add(son);
                    continue;
                }
                
                var tagName = getTagName(l);
                var elSon = new XElement(tagName);
                elSon.Value = getTagValue(l);
                son.Add(elSon);
            }
            return el;

        }
        /// <summary>
        /// Get the Tag name to create an Xelement
        /// </summary>
        /// <param name="line">One line from the file</param>
        /// <returns></returns>
        private static string getTagName(string line)
        {
            int pos_init = line.IndexOf("<") + 1;
            int pos_end = line.IndexOf(">");
            pos_end = pos_end - pos_init;
            return line.Substring(pos_init, pos_end);
        }
        private static string getTagValue(string line)
        {
            int pos_init = line.IndexOf(">") + 1;
            string retValue = line.Substring(pos_init).Trim();
            if (retValue.IndexOf("[") != -1)
            {
                retValue = retValue.Substring(0, 8);
            }
            return retValue;
        }
    }
}
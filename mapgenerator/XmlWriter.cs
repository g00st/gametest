using System;
using System.Xml.Linq;

namespace mapgenerator;

public class XmlWriter
{
    public void WriteXmlToFile(uint[] ind, float[] vert, float[]text, float[] tileMap, string fileName)
    {
        // Create a new XML document
        XDocument xmlDocument = new XDocument(
            new XElement("Root",
                new XElement("Ind",
                    new XElement("Array", string.Join(" ", ind)),
                    new XElement("Count", ind.Length)
                ),
                new XElement("Vert",
                    new XElement("Array", string.Join(" ", vert)),
                    new XElement("Count", vert.Length)
                ),
                new XElement("Text",
                    new XElement("Array", string.Join(" ", text)),
                    new XElement("Count", vert.Length)
                ),new XElement("Tile",
                    new XElement("Array", string.Join(" ", tileMap)),
                    new XElement("Count", vert.Length)
                )
            
            )
        );

        // Save the XML document to a file
        xmlDocument.Save(fileName);
    }
}

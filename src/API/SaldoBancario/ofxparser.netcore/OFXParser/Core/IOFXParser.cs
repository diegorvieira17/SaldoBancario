using OFXParser.Entities;

namespace OFXParser
{
    public interface IOFXParser
    {
        Extract GenerateExtract(string ofxSourceFile);
    }
}
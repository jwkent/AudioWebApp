using System.Text;

namespace AudioWebApp.Server.Utilities
{
    public class EncodingStringWriter : StringWriter
    {
        private readonly Encoding _encoding;

        public EncodingStringWriter(StringBuilder builder, Encoding encoding) : base(builder)
        {
            _encoding = encoding;
        }

        public override Encoding Encoding
        {
            get { return _encoding; }
        }
    }
}

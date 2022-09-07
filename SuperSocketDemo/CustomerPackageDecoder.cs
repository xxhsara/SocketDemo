using SuperSocket.ProtoBase;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSocketDemo
{
    public class CustomerPackageDecoder : IPackageDecoder<StringPackageInfo>
    {
        public StringPackageInfo Decode(ref ReadOnlySequence<byte> buffer,object context)
        {
            var text = buffer.GetString(new UTF8Encoding(false));
            var parts = text.Split(':', 2);

            return new StringPackageInfo
            {
                Key = parts[0],
                Body = parts[1],
                Parameters = parts[1].Split(',')
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COPsyncPresenceMap.Helpers
{
    public class TemporalFileHelper : IDisposable
    {
        public string TemporalFileName { get; private set; }

        public TemporalFileHelper()
        {
            TemporalFileName = Path.GetTempFileName();
        }

        public TemporalFileHelper(string copyFrom)
            : this()
        {
            File.Copy(copyFrom, TemporalFileName, true);
        }

        public void Dispose()
        {
            File.Delete(TemporalFileName);
        }
    }
}

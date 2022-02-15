using System;
using System.Collections.Generic;
using System.Text;

namespace LoadImage.db
{
    public interface ISqlite
    {
        string GetDatabasePath(string filename);
    }
}

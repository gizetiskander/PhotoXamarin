using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoXamarin.db
{
    public interface ISqlite
    {
        string GetDatabase(string filename);
    }
}

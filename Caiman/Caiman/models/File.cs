using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caiman.models
{
    public class FileModel
    {
        public int id;
        public string filename;
            public string date;
        public FileModel(int idp, string flienamep, string datep)
        {
            id = idp;
            filename = flienamep;
            date = datep;
        }
        public FileModel()
        {

        }
    }
}

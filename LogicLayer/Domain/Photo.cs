using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Domain
{
    public class Photo
    {
        public int PhotoNumber { get;  }
        public string URL { get; }
        public int PhotoId { get; }
        public Photo(int photoNumber, string url, int photoId)
        {
            
            PhotoNumber = photoNumber;
            URL = url;
            PhotoId = photoId;
        }
    }
}

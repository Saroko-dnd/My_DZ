using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example5DataTemplate
{
  public  class Message
    {
      public long ID { get; set; }
      public string Topic { get; set; }
      public string Content { get; set; }
      public string Author { get; set; }
      public DateTime GetDate { get; set; }

      public int Category { get; set; }
    }
}

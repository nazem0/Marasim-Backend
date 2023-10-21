using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Models
{
    public class CheckListItem
    {
        public int ID { get; set; }
        public int ChecklistID { get; set; }
        public string Text { get; set; }
        public bool Status { get; set; }
        public virtual CheckList CheckList { get; set; }
    }


}
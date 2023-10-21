using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Models
{
    public class CheckList
    {
        public virtual int ID { get; set; }
        public virtual string UserID { get; set; }
        public virtual DateTime WeddingDate { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<CheckListItem> CheckListItems { get; set; }
    }

}

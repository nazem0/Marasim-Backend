using System;
using System.Collections.Generic;


namespace Models
{
    public class CheckList : BaseModel
    {
        public string UserID { get; set; }
        public DateTime WeddingDate { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<CheckListItem> CheckListItems { get; set; }
    }

}

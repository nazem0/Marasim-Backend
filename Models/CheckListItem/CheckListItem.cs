namespace Models
{
    public class CheckListItem : BaseModel
    {
        public int ChecklistID { get; set; }
        public string Text { get; set; }
        public bool Status { get; set; }
        public virtual CheckList CheckList { get; set; }
    }


}
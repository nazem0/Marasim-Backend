namespace Domain.Entities
{
    public class Category : BaseModel
    {
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<Vendor> Vendors { set; get; }
    }

}
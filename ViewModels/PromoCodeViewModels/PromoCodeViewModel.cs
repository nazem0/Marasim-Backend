namespace ViewModels.PromoCodeViewModel
{
    public class PromoCodeViewModel
    {
        public string Code { get; set; }
        public float Discount { get; set; }
        //public int Limit { get; set; }
        public int Count { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }

    }
}


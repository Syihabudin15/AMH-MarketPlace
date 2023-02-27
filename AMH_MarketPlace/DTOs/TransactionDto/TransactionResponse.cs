namespace AMH_MarketPlace.DTOs.TransactionDto
{
    public class TransactionResponse
    {
        public string? status_message { get; set; }
        public string? transaction_id { get; set; }
        public string? order_id { get; set; }
        public string? gross_amount { get; set; }
        public string? currency { get; set; }
        public string? payment_type { get; set; }
        public string? transaction_time { get; set; }
        public string? transaction_status { get; set; }
        public string? merchant_id { get; set; }
        public string? userId { get; set; }
    }
}

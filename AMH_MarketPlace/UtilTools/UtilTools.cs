namespace AMH_MarketPlace.UtilTools
{
    public static class UtilTools
    {
        public static string GenerateOrderId()
        {
            Random random = new Random();
            string orderId = "order" + random.Next(0, 100000).ToString("D6");
            return orderId;
        }
    }
}

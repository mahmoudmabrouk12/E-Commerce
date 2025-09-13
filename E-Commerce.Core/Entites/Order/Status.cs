namespace E_Commerce.Core.Entites.Order
{
    public enum Status
    {
        Pending,     
        PaymentReceived, 
        PaymentFailed,   
        Shipped,     // الطلب خرج من المخزن
        Delivered,   // الطلب اتسلم للعميل
        Cancelled    
    }

}
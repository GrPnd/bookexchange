namespace Domain;

public class Trade
{
    public int Id { get; set; }
    public ETradeStatus TradeStatus { get; set; }
    public ETradeStatus BookAStatus { get; set; }
    public ETradeStatus BookBStatus { get; set; }
    
    public int BookAId { get; set; }
    public UserBook? BookA { get; set; }
    
    public int BookBId { get; set; }
    public UserBook? BookB { get; set; }
}
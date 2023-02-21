namespace WinFormsApp1;

public class Products
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string? Category { get; set; }
    public float Rating { get; set; }

    public override string ToString()
    {
        return $@"ID : {Id}
Name : {Name}
Price : {Price}
Quantity : {Quantity}
Category : {Category}
Rating : {Rating}";
    }

}

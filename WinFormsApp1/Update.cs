using Dapper;
using System.Data.SqlClient;

namespace WinFormsApp1;

public partial class Update : Form
{
    Products? product;
    SqlConnection? conn;
    bool flag = true;
    public Update()
    {
        InitializeComponent();

        textBox1.Text = Form1.p!.SubItems[2].Text; 
        textBox2.Text = Form1.p!.SubItems[3].Text; 
        textBox4.Text = Form1.p!.SubItems[4].Text; 
        textBox5.Text = Form1.p!.SubItems[5].Text; 
        textBox3.Text = Form1.p!.SubItems[6].Text; 
    }

    private void button1_Click(object sender, EventArgs e)
    {
        if(Checker())
        {
            using (conn = new(Form1.connection))
            {
                string text = @"Update Products
SET Name = @Name, Price = @Price, Quantity = @Quantity, Category = @Category, Rating = @Rating
WHERE Products.Id = @Id";
                int a = conn.Execute(text, new
                {
                    Id = Form1.id,
                    Name = product!.Name,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    Category = product.Category,
                    Rating = product.Rating
                });
                if (a == 1)
                {
                    MessageBox.Show("Successfully saved");
                    Close();
                }
                else
                    MessageBox.Show("Error");
            }
        }
    }

    private bool Checker()
    {
        if (textBox1.Text != string.Empty && textBox2.Text != string.Empty && textBox4.Text != string.Empty)
        {
            flag = true;

            product = new Products();
            product.Name = textBox1.Text;
            product.Category = textBox5.Text;

            if (decimal.TryParse(textBox2.Text, out decimal price))
                product.Price = price;
            else
            {
                textBox_Click(textBox2);
                flag = false;
            }

            if (int.TryParse(textBox4.Text, out int quantity))
                product.Quantity = quantity;
            else
            {
                textBox_Click(textBox4);
                flag = false;
            }

            if (float.TryParse(textBox3.Text, out float rating) && rating >= 0 && rating <= 5)
                product.Rating = rating;
            else
            {
                textBox_Click(textBox3);
                flag = false;
            }

            if (flag)
                return flag;
            else
                return flag;
        }
        else
            return false;
    }

    private void textBox_Click(object sender)
    {
        var txtBox = sender as TextBox;

        txtBox!.Clear();
    }
}

using Dapper;
using System.Data.SqlClient;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public static string connection = "Data Source=R0Y4L;Initial Catalog=Market;Integrated Security=True;";
        public static int id;
        public string query = @"SELECT * FROM Products";
        public static ListViewItem? p;
        SqlConnection? conn = null;
        List<Products>? products = null;
        public Form1()
        {
            InitializeComponent();

            listView1.MultiSelect = false;
            listView1.View = View.Details;
            listView1.Columns.Add("Number ");
            listView1.Columns.Add("Id ");
            listView1.Columns.Add("Name ");
            listView1.Columns.Add("Price ");
            listView1.Columns.Add("Quantity ");
            listView1.Columns.Add("Category ");
            listView1.Columns.Add("Rating ");
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            conn = new SqlConnection(connection);
        }

        private void GetProducts()
        {
            conn.Open();

            products = conn.Query<Products>(query).ToList();

            int count = 1;
            foreach (Products product in products)
            {
                ListViewItem item = new((count++).ToString() + '.');
                item.SubItems.Add(product.Id.ToString());
                item.SubItems.Add(product.Name);
                item.SubItems.Add(product.Price.ToString());
                item.SubItems.Add(product.Quantity.ToString());
                item.SubItems.Add(product.Category);
                item.SubItems.Add(product.Rating.ToString());

                listView1.Items.Add(item);
            }

            conn.Close();
        }

        private void button_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;

            if (btn == button5)
            {
                listView1.Items.Clear();
                GetProducts();
            }
            else if (btn == button1)
            {
                Add Add = new Add();
                Add.Show();
            }
            else if (btn == button2)
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    id = int.Parse(listView1.SelectedItems[0].SubItems[1].Text);
                    p = listView1.SelectedItems[0];
                    Update update = new Update();
                    update.Show();
                }
            }
            else if (btn == button3)
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    id = int.Parse(listView1.SelectedItems[0].SubItems[1].Text);
                    conn.Execute("DELETE Products WHERE Id = @Id", new { Id = id });

                    products!.RemoveAll(p => p.Id == id);

                    listView1.Items.Clear();
                    GetProducts();
                }
            }
            else if (btn == button4)
            {
                listView1.Items.Clear();
            }
        }
    }
}
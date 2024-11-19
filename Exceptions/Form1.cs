using Exceptions;
using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Exceptions
{
    public partial class frmAddProduct : Form
    {
        private string _ProductName;
        private string _Category;
        private string _MfgDate;
        private string _ExpDate;
        private string _Description;
        private int _Quantity;
        private double _SellPrice;

        // Use a BindingList to hold the products
        private BindingList<ProductClass> productList;

        public string Product_Name(string name)
        {
            if (!Regex.IsMatch(name, @"^[a-zA-Z ]+$")) // Allow spaces in product names
                throw new ArgumentException("Invalid product name. Only letters and spaces are allowed.");
            return name;
        }

        public int Quantity(string qty)
        {
            if (!int.TryParse(qty, out int quantity) || quantity < 0)
                throw new ArgumentException("Invalid quantity. Please enter a non-negative integer.");
            return quantity;
        }

        public double SellingPrice(string price)
        {
            if (!double.TryParse(price, out double sellPrice) || sellPrice < 0)
                throw new ArgumentException("Invalid selling price. Please enter a non-negative number.");
            return sellPrice;
        }

        class NumberFormattException : Exception
        {
            public NumberFormattException(string Quantity) : base(Quantity) { }
        }

        class StringFormattException : Exception
        {
            public StringFormattException(string Product_Name) : base(Product_Name) { }
        }
        class CurrencyFormatException : Exception
        {
            public CurrencyFormatException(string SellingPrice) : base(SellingPrice) { }
        }

        public frmAddProduct()
        {
            InitializeComponent();
            productList = new BindingList<ProductClass>();
            gridViewProductList.DataSource = productList;
            gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void frmAddProduct_Load(object sender, EventArgs e)
        {
            string[] ListofProductCategory = new string[]
            {
                "Beverages",
                "Bread/Bakery",
                "Canned/Jarred Goods",
                "Dairy",
                "Frozen Goods",
                "Meat",
                "Personal Care",
                "Other"
            };
            foreach (string Category in ListofProductCategory)
            {
                cbCategory.Items.Add(Category);
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                _ProductName = Product_Name(txtProductName.Text);
                _Category = cbCategory.Text;
                _MfgDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
                _ExpDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd");
                _Description = richTxtDescription.Text;
                _Quantity = Quantity(txtQuantity.Text);
                _SellPrice = SellingPrice(txtSellPrice.Text);

                // Add the new product to the BindingList
                productList.Add(new ProductClass(_ProductName, _Category, _MfgDate, _ExpDate, _SellPrice, _Quantity, _Description));
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
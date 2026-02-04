/*
Eric Cato Jr CIS199-01 Due: 11/7/2024
This program calculates the final price for a catering contract with a discount
Discount is based on the caterer, business, and years of contract.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Program3
{
    public partial class Form1 : Form
    {
        // Arrays to hold caterer names, their discount rates, business names, and contract prices
        string[] catererNames = { "Hill Catering Co.", "Food in a Flash", "Sally's Sandwiches", "Perry's Pierogis" }; //Caterer names
        double[] catererDiscounts = { 0.30, 0.20, 0.12, 0.05 }; // Discounts for each caterer (in the same order as caterer names)
        string[] businessNames = { "John's Books", "Office Supplies", "J.B. Car Parts", "Gevalia Coffee", "Ceylon Tea", "My Footwear" }; //Business names
        double[] contractPrices = { 500, 489, 412, 350, 325, 279 }; // Contract prices for each business

        // Arrays for contract years and corresponding additional discounts
        int[] yearRanges = { 1, 2, 3, 4, 5 }; // Placeholder array for contract years
        int[] yearDiscountRanges = { 0, 30, 40, 50 }; // Additional discount for contract years (based on ranges)

        public Form1()
        {
            InitializeComponent();
            this.Load += new EventHandler(Form1_Load); // Link Form Load event
            btnCalculate.Click += new EventHandler(btnCalculate_Click); // Link Calculate button Click event
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Clear existing items first to avoid duplicates
            cBoxCaterer.Items.Clear();
            cBoxBusiness.Items.Clear();

            // Add items to caterer combo box and check if it succeeds
            cBoxCaterer.Items.AddRange(catererNames);
            if (cBoxCaterer.Items.Count == 0)
            {
                MessageBox.Show("Failed to add items to cBoxCaterer. Please check ComboBox name and initialization."); // Display error if no items were added
            }

            // Add items to business combo box and check if it succeeds
            cBoxBusiness.Items.AddRange(businessNames);
            if (cBoxBusiness.Items.Count == 0)
            {
                MessageBox.Show("Failed to add items to cBoxBusiness. Please check ComboBox name and initialization."); // Display error if no items were added
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            lblPriceOutput.Text = string.Empty; // Clear previous output

            // Check if a caterer is selected
            if (cBoxCaterer.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a caterer."); // Prompt if no caterer is selected
                return; // Stop here if no selection
            }

            // Check if a business is selected
            if (cBoxBusiness.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a business."); // Prompt if no business is selected
                return; // Stop here if no selection
            }

            // Validate contract years input
            if (!int.TryParse(tBoxContract.Text, out int years) || years < 1) // Check if input is valid
            {
                MessageBox.Show("Please enter a valid number of years."); // Prompt if input is invalid
                return; // Stop here if input is invalid
            }

            // Get the discount rate based on the selected caterer
            double discountRate = catererDiscounts[cBoxCaterer.SelectedIndex];

            // Get the contract price based on the selected business
            double basePrice = contractPrices[cBoxBusiness.SelectedIndex];

            // Determine additional discount based on the number of contract years
            double additionalDiscount = 0;
            if (years >= 8) // Apply highest discount for 8+ years
            {
                additionalDiscount = yearDiscountRanges[3];
            }
            else if (years >= 5) // Apply moderate discount for 5-7 years
            {
                additionalDiscount = yearDiscountRanges[2];
            }
            else if (years >= 2) // Apply smaller discount for 2-4 years
            {
                additionalDiscount = yearDiscountRanges[1];
            }

            // Calculate the final price
            double discountAmount = basePrice * discountRate; // Calculate discount based on base price
            double finalPrice = basePrice - discountAmount - additionalDiscount; // Subtract discounts to get final price

            // Show the final price in lblPriceOutput
            lblPriceOutput.Text = "$" + finalPrice.ToString("F2"); // Format and display final price as currency
            lblPriceOutput.Refresh(); // Ensure output updates visually
        }
    }
}

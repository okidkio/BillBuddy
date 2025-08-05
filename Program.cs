using System;
using System.Linq;

namespace BillBuddy
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set console to support Unicode (important for ₹ symbol)
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            using (var db = new BillBuddyContext())
            {
                while (true)
                {
                    Console.WriteLine("\nBill Buddy - Menu");
                    Console.WriteLine("1. Add Bill");
                    Console.WriteLine("2. View All Bills");
                    Console.WriteLine("3. Update Bill");
                    Console.WriteLine("4. Delete Bill");
                    Console.WriteLine("5. View Total Amount");
                    Console.WriteLine("6. Exit");
                    Console.Write("Select an option: ");

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            AddBill(db);
                            break;
                        case "2":
                            ViewBills(db);
                            break;
                        case "3":
                            UpdateBill(db);
                            break;
                        case "4":
                            DeleteBill(db);
                            break;
                        case "5":
                            ViewTotalAmount(db);
                            break;
                        case "6":
                            return;
                        default:
                            Console.WriteLine("Invalid option. Try again.");
                            break;
                    }
                }
            }
        }

        static void AddBill(BillBuddyContext db)
        {
            Console.Write("Enter bill name: ");
            string name = Console.ReadLine();
            Console.Write("Enter amount: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Invalid amount.");
                return;
            }
            Console.Write("Enter due date (yyyy-MM-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime dueDate))
            {
                Console.WriteLine("Invalid date.");
                return;
            }
            Console.Write("Enter category: ");
            string category = Console.ReadLine();

            var bill = new Bill { Name = name, Amount = amount, DueDate = dueDate, Category = category };
            db.Bills.Add(bill);
            db.SaveChanges();
            Console.WriteLine("Bill added successfully!");
        }

        static void ViewBills(BillBuddyContext db)
        {
            var bills = db.Bills.ToList();
            if (!bills.Any())
            {
                Console.WriteLine("No bills found.");
                return;
            }

            foreach (var bill in bills)
            {
                Console.WriteLine($"ID: {bill.Id}, Name: {bill.Name}, Amount: ₹{bill.Amount:0.00}, Due: {bill.DueDate:yyyy-MM-dd}, Category: {bill.Category}");
            }
        }

        static void UpdateBill(BillBuddyContext db)
        {
            Console.Write("Enter bill ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            var bill = db.Bills.Find(id);
            if (bill == null)
            {
                Console.WriteLine("Bill not found.");
                return;
            }

            Console.Write($"Enter new name (current: {bill.Name}): ");
            string name = Console.ReadLine();
            if (!string.IsNullOrEmpty(name)) bill.Name = name;

            Console.Write($"Enter new amount (current: ₹{bill.Amount:0.00}): ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount)) bill.Amount = amount;

            Console.Write($"Enter new due date (current: {bill.DueDate:yyyy-MM-dd}): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime dueDate)) bill.DueDate = dueDate;

            Console.Write($"Enter new category (current: {bill.Category}): ");
            string category = Console.ReadLine();
            if (!string.IsNullOrEmpty(category)) bill.Category = category;

            db.SaveChanges();
            Console.WriteLine("Bill updated successfully!");
        }

        static void DeleteBill(BillBuddyContext db)
        {
            Console.Write("Enter bill ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            var bill = db.Bills.Find(id);
            if (bill == null)
            {
                Console.WriteLine("Bill not found.");
                return;
            }

            db.Bills.Remove(bill);
            db.SaveChanges();
            Console.WriteLine("Bill deleted successfully!");
        }

        static void ViewTotalAmount(BillBuddyContext db)
        {
            decimal total = db.Bills.Sum(b => b.Amount);
            Console.WriteLine($"Total amount of all bills: ₹{total:0.00}");
        }
    }
}

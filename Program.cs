using Eto.Drawing;
using Eto.Forms;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace GridTest.Wpf
{
    class MainClass
    {
        [STAThread]
        public static void Main()
        {
            new Application(Eto.Platforms.Wpf).Run(new MainForm());
        }
    }


    class Item
    {
        public int Index { get; set; }

        public string Name { get; set; }
    }

    public partial class MainForm : Form
    {
        private readonly ObservableCollection<Item> data = new ObservableCollection<Item>();

        public MainForm()
        {
            var grid = new GridView();

            grid.Columns.Add(
                new GridColumn() {
                    HeaderText = "Index",
                    DataCell = new TextBoxCell() { Binding = Binding.Property((Item item) => item.Index.ToString()) },
                    AutoSize = false,
                    Resizable = true
                });
            grid.Columns.Add(
                new GridColumn() {
                    HeaderText = "Name",
                    DataCell = new TextBoxCell() { Binding = Binding.Property((Item item) => item.Name) },
                    AutoSize = false,
                    Resizable = true
                });

            grid.DataStore = data;

            Title = "Eto grid test";
            MinimumSize = new Size(500, 500);

            var rowsNumTxt = new NumericMaskedTextBox<int>();
            var addbtn = new Button() { Text = "Add rows" };
            addbtn.Click += (o, ev) => {
                AddRows(rowsNumTxt.Value);
            };

            var toolbar = new StackLayout() { Orientation = Orientation.Horizontal, VerticalContentAlignment = VerticalAlignment.Top };
            toolbar.Items.Add(rowsNumTxt);
            toolbar.Items.Add(addbtn);

            var content = new StackLayout() { Orientation = Orientation.Vertical, HorizontalContentAlignment = HorizontalAlignment.Stretch };
            content.Items.Add(toolbar);
            content.Items.Add(new StackLayoutItem(grid, true));

            Content = content;
        }

        private void AddRows(int n)
        {
            foreach (var item in Enumerable.Range(0, n).Select(i => new Item { Index = i, Name = $"model: {data.Count + i}" }))
            {
                data.Add(item);
            }
        }
    }
}

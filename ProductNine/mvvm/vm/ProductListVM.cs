using Microsoft.EntityFrameworkCore;
using ProductNine.DB;
using ProductNine.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ProductNine.Pages;

namespace ProductNine.mvvm.vm
{
    internal class ProductListVM : BaseVM
    {
        private string searchInfo;
        private List<Product> products;
        private string searchText = "";
        private Manufacturer selectedManufacturer;
        private string selectedSortValue;

        public List<string> SortValues { get; set; }
        public List<Manufacturer> Manufacturers { get; set; }

        public Visibility AdminVisibility { get; set; }

        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                Search();
            }
        }

        public Manufacturer SelectedManufacturer
        {
            get => selectedManufacturer;
            set
            {
                selectedManufacturer = value;
                Search();
            }
        }
        public string SelectedSortValue
        {
            get => selectedSortValue;
            set
            {
                selectedSortValue = value;
                Search();
            }
        }

        public VmCommand CreateProduct { get; set; }
        public VmCommand EditProduct { get; set; }
        public VmCommand DeleteProduct { get; set; }

        public string SearchInfo
        {
            get => searchInfo;
            set
            {
                searchInfo = value;
                Signal();
            }
        }
        public List<Product> Products
        {
            get => products;
            set
            {
                products = value;
                Signal();
            }
        }
        public Product SelectedProduct { get; set; }

        byte[] defaultImage;

        public ProductListVM()
        {
            LoadDefaultImage();

            AdminVisibility = Static.User.Logged.IdRole == 1 ? Visibility.Visible : Visibility.Collapsed;

            SortValues = new List<string>(new string[] { "По возрастанию", "По убыванию" });
            try
            {
                Manufacturers = Static.DataBase.Instance().Manufacturers.ToList();
                Manufacturers.Insert(0, new Manufacturer { Name = "Все производители" });
                SelectedManufacturer = Manufacturers.First();
            }
            catch { }

            /*EditProduct = new VmCommand(() => {
                PageNavigator.CurrentPage = new EditProduct(SelectedProduct);
            }, () => SelectedProduct != null);
            CreateProduct = new VmCommand(() => {
                PageNavigator.CurrentPage = new EditProduct();
            }, () => true);*/

            DeleteProduct = new VmCommand(() =>
            {
                if (SelectedProduct.Baskets.Count > 0)
                {
                    MessageBox.Show("Данный товар удалить нельзя. Он присутствует в заказах.");
                }
                else
                {
                    try
                    {
                        Static.DataBase.Instance().Products.Remove(SelectedProduct);
                        Static.DataBase.Instance().SaveChanges();
                        Search();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Произошла ошибка при удалении товара");
                    }
                    Search();
                }
            }, () => SelectedProduct != null);
        }

        private void LoadDefaultImage()
        {
            var stream = Application.GetResourceStream(new Uri("Images/picture.png", UriKind.Relative));
            defaultImage = new byte[stream.Stream.Length];
            stream.Stream.Read(defaultImage, 0, defaultImage.Length);
        }

        private void Search()
        {
            try
            {
                var db = Static.DataBase.Instance();
                int maxCountProducts = db.Products.Count();

                var products = db.Products.
                    Include(s => s.IdManufacturer).
                    Include(s => s.IdCategory).
                    Include(s => s.IdProvider).
                    Include(s => s.Unit).
                    Include(s => s.Baskets);
                IQueryable<Product> filterProduct;
                if (SelectedManufacturer.Id != 0)
                    filterProduct = products.Where(s => s.IdManufacturer == SelectedManufacturer.Id);
                else
                    filterProduct = products;
                filterProduct = filterProduct.Where(s => s.IdManufacturerNavigation.Name.Contains(searchText) ||
                    s.IdProviderNavigation.Name.Contains(searchText) ||
                    s.Article.Contains(searchText) ||
                    s.Description.Contains(searchText) ||
                    s.Name.Contains(searchText)
                    );

                if (SelectedSortValue == "По возрастанию")
                    Products = filterProduct.OrderBy(s => s.Price).ToList();
                else
                    Products = filterProduct.OrderByDescending(s => s.Price).ToList();
                foreach (var item in Products)
                {
                    if (item.Image == null)
                        item.Image = defaultImage;
                }

                if (Products.Count == 0)
                    MessageBox.Show("По запросу ничего не найдено");

                SearchInfo = $"Отображено записей: {Products.Count} из {maxCountProducts}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        internal void DoubleClick()
        {
            EditProduct?.Execute(null);
        }
    }
}


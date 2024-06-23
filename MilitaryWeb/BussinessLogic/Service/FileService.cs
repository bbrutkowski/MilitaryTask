using CSharpFunctionalExtensions;
using MilitaryWeb.BussinessLogic.Model;
using MilitaryWeb.BussinessLogic.Service.Interface;
using System.Xml.Linq;

namespace MilitaryWeb.BussinessLogic.Service
{
    public class FileService : IFileService
    {
        private readonly string _xmlFileType = "*.xml";
        private readonly string _documentsFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

        public FileService() { }

        private string[] GetXmlFiles() => Directory.GetFiles(_documentsFolderPath, _xmlFileType);

        public async Task<Result<ProductCatalog>> ProcessXmlFiles()
        {
            var mainProducts = new ProductCatalog();
            if (!Directory.Exists(_documentsFolderPath)) return Result.Failure<ProductCatalog>("Folder path does not exist");

            try
            {
                var xmlFiles = GetXmlFiles();
                if (!xmlFiles.Any()) return Result.Failure<ProductCatalog>("No files downloaded");

                foreach (var xmlFile in xmlFiles)
                {
                    var document = XDocument.Load(xmlFile);
                    if (document is null) return Result.Failure<ProductCatalog>("File has not been loaded");

                    switch (Path.GetFileName(xmlFile))
                    {
                        case "dostawca1plik1.xml":
                            mainProducts.Offerts = DeserializeOfferts(document);
                            break;
                        case "dostawca2plik1.xml":
                            mainProducts.ProductDetails = DeserializeProductDetails(document);
                            break;
                        case "dostawca2plik2.xml":
                            mainProducts.SimpleProductOfferts = DeserializeSimpleProducts(document);
                            break;
                        case "dostawca3plik1.xml":
                            mainProducts.InternationatProducts = DeserializeInternationalProducts(document);
                            break;
                        default:
                            Console.WriteLine($"Nieobsługiwany plik: {Path.GetFileName(xmlFile)}");
                            break;
                    }
                }

                await Task.Delay(3000); // long process simulation

                return Result.Success(mainProducts);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while processing files");
                return Result.Failure<ProductCatalog>(e.Message);
            }
        }

        private List<InternationalProduct> DeserializeInternationalProducts(XDocument doc)
        {
            try
            {
                return doc.Descendants("produkt")
                    .Select(p => new InternationalProduct
                    {
                        ID = (int)p.Element("id"),
                        Name = (string)p.Element("nazwa"),
                        NamePl = (string)p.Element("nazwa_pl"),
                        NameEn = (string)p.Element("nazwa_en"),
                        Description = (string)p.Element("dlugi_opis"),
                        DescriptionPl = (string)p.Element("dlugi_opis_pl"),
                        DescriptionEn = (string)p.Element("dlugi_opis_en"),
                        Code = (string)p.Element("kod"),
                        EAN = (string)p.Element("ean"),
                        Status = (int)p.Element("status"),
                        WholesalePrice = (decimal)p.Element("cena_zewnetrzna_hurt"),
                        SuggestedRetailPrice = (decimal)p.Element("cena_sugerowana"),
                        SupplierCode = (string)p.Element("kod_dostawcy"),
                        VAT = (decimal)p.Element("vat"),
                        Size = (string)p.Element("rozmiar"),
                        Color = (string)p.Element("kolor"),
                        Category = (string)p.Element("cat"),
                        CategoryPl = (string)p.Element("cat_pl"),
                        CategoryEn = (string)p.Element("cat_en"),
                        Photo = p.Descendants("zdjecie").Select(z => (string)z.Attribute("url")).FirstOrDefault()
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deserializing the file: {ex.Message}." +
                    $" Method: {nameof(DeserializeInternationalProducts)}");
                return [];
            }
        }

        private List<SimpleProduct> DeserializeSimpleProducts(XDocument doc)
        {
            try
            {
                return doc.Descendants("product")
                    .Select(sp => new SimpleProduct
                    {
                        EAN = (string)sp.Element("ean"),
                        ID = (int)sp.Element("id"),
                        SKU = (string)sp.Element("sku"),
                        Name = (string)sp.Element("name"),
                        Description = (string)sp.Element("desc"),
                        URL = (string)sp.Element("url"),
                        Categories = sp.Descendants("category")
                           .Select(c => new Category
                           {
                               Id = (string)c.Attribute("id"),
                               Description = (string)c
                           })
                           .ToList(),
                        Unit = (string)sp.Element("unit"),
                        Weight = (string)sp.Element("weight"),
                        PKWiU = (string)sp.Element("PKWiU"),
                        InStock = (bool)sp.Element("inStock"),
                        Quantity = (int)sp.Element("qty"),
                        PriceAfterDiscountNet = (string)sp.Element("priceAfterDiscountNet"),
                        RetailPriceGross = (decimal)sp.Element("retailPriceGross"),
                        Photo = sp.Descendants("photo").Select(x => (string)x).FirstOrDefault()                    
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deserializing the file: {ex.Message}." +
                    $"Method: {nameof(DeserializeSimpleProducts)}");
                return [];
            };
        }

        private List<ProductDetails> DeserializeProductDetails(XDocument doc)
        {
            try
            {
                return doc.Descendants("product")
                    .Select(pd => new ProductDetails
                    {
                        EAN = (string)pd.Element("ean"),
                        ID = (int)pd.Element("id"),
                        SKU = (string)pd.Element("sku"),
                        InStock = (bool)pd.Element("inStock"),
                        Quantity = (int)pd.Element("qty")
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deserializing the file {ex.Message}." +
                    $" Method: {nameof(DeserializeProductDetails)}");
                return [];
            }
        }

        private List<Offer> DeserializeOfferts(XDocument doc)
        {
            try
            {
                return doc.Descendants("product")
                    .Select(o => new Offer
                    {
                        Id = (int)o.Attribute("id"),
                        PriceGross = (decimal)o.Element("price").Attribute("gross"),
                        PriceNet = (decimal)o.Element("price").Attribute("net"),
                        Vat = (decimal)o.Element("price").Attribute("vat"),
                        SRPGross = (decimal)o.Element("srp").Attribute("gross"),
                        SRPNet = (decimal)o.Element("srp").Attribute("net"),
                        SRPVat = (decimal)o.Element("srp").Attribute("vat"),
                        SizeId = (int)o.Descendants("sizes").FirstOrDefault().Element("size").Attribute("id"),
                        SizeCodeProducer = (string)o.Descendants("sizes").FirstOrDefault().Element("size").Attribute("code_producer"),
                        SizeCode = (string)o.Descendants("sizes").FirstOrDefault().Element("size").Attribute("code"),
                        Weight = (int)o.Descendants("sizes").FirstOrDefault().Element("size").Attribute("weight"),
                        StockId = (int)o.Descendants("stock").FirstOrDefault().Attribute("id"),
                        StockQuantity = (int)o.Descendants("stock").FirstOrDefault().Attribute("quantity")
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deserializing the file {ex.Message}." +
                    $" Method: {nameof(DeserializeOfferts)}");
                return [];
            }
        }
    }
}

using CSharpFunctionalExtensions;
using MilitaryASPWeb.BussinessLogic.Model;
using MilitaryASPWeb.BussinessLogic.Services.Interfaces;
using MilitaryASPWeb.Models.Model;
using MilitaryASPWeb.Models.Model.Exceptions;
using MilitaryASPWeb.Models.Services.Interfaces;
using System.Xml.Linq;

namespace MilitaryASPWeb.BussinessLogic.Services
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
            catch (ProcessXmlFileException ex)
            {
                Console.WriteLine(ex.Message);
                return Result.Failure<ProductCatalog>(ex.Message);
            }
        }

        private List<InternationalProduct> DeserializeInternationalProducts(XDocument doc)
        {
            try
            {
                return doc.Descendants("produkt")
                    .Select(p => new InternationalProduct
                    {
                        Id = (int)p.Element("id"),
                        Name = (string)p.Element("nazwa"),                      
                        Description = (string)p.Element("dlugi_opis"),                     
                        Status = (int)p.Element("status"),                      
                        Photo = p.Descendants("zdjecie").Select(z => (string)z.Attribute("url")).FirstOrDefault()
                    })
                    .ToList();
            }
            catch (Exception)
            {
                throw new ProcessXmlFileException($"An error occurred while deserializing file in method {nameof(DeserializeInternationalProducts)}");      
            }
        }

        private List<SimpleProduct> DeserializeSimpleProducts(XDocument doc)
        {
            try
            {
                return doc.Descendants("product")
                    .Select(sp => new SimpleProduct
                    {
                        Id = (int)sp.Element("id"),                      
                        Name = (string)sp.Element("name"),
                        Description = (string)sp.Element("desc"),                      
                        Quantity = (int)sp.Element("qty"),                 
                        Photo = sp.Descendants("photo").Select(x => (string)x).FirstOrDefault()
                    })
                    .ToList();
            }
            catch (Exception)
            {
                throw new ProcessXmlFileException($"An error occurred while deserializing file in method {nameof(DeserializeSimpleProducts)}");
            };
        }

        private List<ProductDetails> DeserializeProductDetails(XDocument doc)
        {
            try
            {
                return doc.Descendants("product")
                    .Select(pd => new ProductDetails
                    {                      
                        Id = (int)pd.Element("id"),                     
                        Quantity = (int)pd.Element("qty")
                    })
                    .ToList();
            }
            catch (Exception)
            {
                throw new ProcessXmlFileException($"An error occurred while deserializing file in method {nameof(DeserializeProductDetails)}");
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
                        StockQuantity = (int)o.Descendants("stock").FirstOrDefault().Attribute("quantity")
                    })
                    .ToList();
            }
            catch (Exception)
            {
                throw new ProcessXmlFileException($"An error occurred while deserializing file in method {nameof(DeserializeOfferts)}");
            }
        }
    }
}

using CSharpFunctionalExtensions;
using MilitaryASPWeb.BussinessLogic.Model;
using MilitaryASPWeb.Models.Model.Exceptions;
using MilitaryASPWeb.Models.Services.Interfaces;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace MilitaryASPWeb.BussinessLogic.Services
{
    public class FileService : IFileService
    {
        private readonly string _xmlFileType = "*.xml";
        private readonly string _documentsFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

        private string[] GetXmlFiles() => Directory.GetFiles(_documentsFolderPath, _xmlFileType);

        public async Task<Result<List<Product>>> CreateProductListFromDeliveredXmlFilesAsync()
        {
            var products = new List<Product>();
            if (!Directory.Exists(_documentsFolderPath)) return Result.Failure<List<Product>>("Folder path does not exist");

            try
            {
                var xmlFiles = GetXmlFiles();
                if (!xmlFiles.Any()) return Result.Failure<List<Product>>("No files downloaded");

                foreach (var xmlFile in xmlFiles)
                {
                    var document = XDocument.Load(xmlFile);
                    if (document is null) return Result.Failure<List<Product>>("File has not been loaded");   

                    switch (Path.GetFileName(xmlFile))
                    {
                        case "dostawca1plik1.xml":
                            products.AddRange(DeserializeFileOneFromFirstSupplier(document));
                            break;
                        case "dostawca1plik2.xml":
                            products.AddRange(DeserializeFileTwoFromFirstSupplier(document));
                            break;
                        case "dostawca2plik1.xml":
                            products.AddRange(DeserializeFileOneFromSecondSupplier(document));
                            break;
                        case "dostawca2plik2.xml":
                            products.AddRange(DeserializeFileTwoFromSecondSupplier(document));
                            break;
                        case "dostawca3plik1.xml":
                            products.AddRange(DeserializeInternationalProducts(document));
                            break;
                        default:
                            Console.WriteLine($"Unsupported file: {Path.GetFileName(xmlFile)}");
                            break;
                    }
                }

                await Task.Delay(3000); // long process simulation

                return Result.Success(products);
            }
            catch (ProcessXmlFileException ex)
            {
                return Result.Failure<List<Product>>(ex.Message);
            }
        }

        private List<Product> DeserializeFileTwoFromFirstSupplier(XDocument doc)
        {
            try
            {
                return doc.Descendants("product")
                    .Select(p => new Product
                    {
                        Id = (int)p.Attribute("id"),
                        Name = p.Descendants("name")
                                    .FirstOrDefault(z => (string)z.Attribute("{http://www.w3.org/XML/1998/namespace}lang") == "pol").Value,
                        Description = RemoveHtmlTagsAndDashes(p.Descendants("long_desc")
                                           .FirstOrDefault(z => (string)z.Attribute("{http://www.w3.org/XML/1998/namespace}lang") == "pol").Value),
                        Photo = (string)p.Descendants("image")
                                     .FirstOrDefault().Attribute("url")
                    })
                    .ToList();
            }
            catch (Exception)
            {
                throw new ProcessXmlFileException($"An error occurred while deserializing file in method" +
                    $" {nameof(DeserializeFileTwoFromFirstSupplier)}");
            }
        }

        private List<Product> DeserializeInternationalProducts(XDocument doc)
        {
            try
            {
                return doc.Descendants("produkt")
                    .Select(p => new Product
                    {
                        Id = (int)p.Element("id"),
                        Name = (string)p.Element("nazwa"),                      
                        Description = RemoveHtmlTagsAndDashes((string)p.Element("dlugi_opis")),                     
                        Quantity = (int)p.Element("status"),                      
                        Photo = p.Descendants("zdjecie").Select(z => (string)z.Attribute("url")).FirstOrDefault()
                    })
                    .ToList();
            }
            catch (Exception)
            {
                throw new ProcessXmlFileException($"An error occurred while deserializing file in method" +
                    $" {nameof(DeserializeInternationalProducts)}");      
            }
        }

        private List<Product> DeserializeFileTwoFromSecondSupplier(XDocument doc)
        {
            try
            {
                return doc.Descendants("product")
                    .Select(sp => new Product
                    {
                        Id = (int)sp.Element("id"),                      
                        Name = (string)sp.Element("name"),
                        Description = RemoveHtmlTagsAndDashes((string)sp.Element("desc")),                      
                        Quantity = (int)sp.Element("qty"),                 
                        Photo = sp.Descendants("photo").Select(x => (string)x).FirstOrDefault()
                    })
                    .ToList();
            }
            catch (Exception)
            {
                throw new ProcessXmlFileException($"An error occurred while deserializing file in method" +
                    $" {nameof(DeserializeFileTwoFromSecondSupplier)}");
            };
        }

        private List<Product> DeserializeFileOneFromSecondSupplier(XDocument doc)
        {
            try
            {
                return doc.Descendants("product")
                    .Select(pd => new Product
                    {                      
                        Id = (int)pd.Element("id"),                     
                        Quantity = (int)pd.Element("qty")
                    })
                    .ToList();
            }
            catch (Exception)
            {
                throw new ProcessXmlFileException($"An error occurred while deserializing file in method" +
                    $" {nameof(DeserializeFileOneFromSecondSupplier)}");
            }
        }

        private List<Product> DeserializeFileOneFromFirstSupplier(XDocument doc)
        {
            try
            {
                return doc.Descendants("product")
                    .Select(o => new Product
                    {
                        Id = (int)o.Attribute("id"),                       
                        Quantity = (int)o.Descendants("stock").FirstOrDefault().Attribute("quantity")
                    })
                    .ToList();
            }
            catch (Exception)
            {
                throw new ProcessXmlFileException($"An error occurred while deserializing file in method" +
                    $" {nameof(DeserializeFileOneFromFirstSupplier)}");
            }
        }

        private string RemoveHtmlTagsAndDashes(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var withoutTags = Regex.Replace(input, "<.*?>", string.Empty);
            return withoutTags.Replace("-", string.Empty);
        }
    }
}

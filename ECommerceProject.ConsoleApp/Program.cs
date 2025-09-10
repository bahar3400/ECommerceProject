using ECommerceProject.Core.Base;
using ECommerceProject.Core.Interfaces;
using ECommerceProject.Core.Models.Enums;
using ECommerceProject.Core.Models.Products;
using ECommerceProject.Core.Models.Users;
using ECommerceProject.Service.ElectronicServices;
using ECommerceProject.Service.EmployeeServices;
using ECommerceProject.Service.FurnutireServices;
using ECommerceProject.Service.Helpers;
using ECommerceProject.Service.Logins;
using ECommerceProject.Service.ManagerServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceProject.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            #region GenelBilgiler
           
            bool control = true;
            Console.WriteLine("****Mağza Yönetim Sayfasına Hoşgeldiniz****");
            Console.WriteLine("Giriş Yapabilmek için gözüken bilgileri giriniz: ");
            #endregion
            #region InstanceService
            ManagerService managerService = new ManagerService();
            EmployeeService employeeService = new EmployeeService();
            ElectronicService electronicService = new ElectronicService();
            FurnitureService furnitureService = new FurnitureService();
            #endregion
            #region LoginGirişi
            //Ben tüm sınıfları IUserLogin tipine çeviripi kontrol oradan yapıcam şuan hafızada listem bulunuyor 
            List<IUserLogin> userLogins = new List<IUserLogin>();
            userLogins.AddRange(managerService.GetAll().OfType<IUserLogin>());
            userLogins.AddRange(employeeService.GetAll().OfType<IUserLogin>());
            LoginManager loginManager = new LoginManager(userLogins);
            #endregion
            while (control)
            {
                #region KullanıcıGirişi
                Console.WriteLine("Kullanıcı Adınız: ");
                string userName = Console.ReadLine();
                Console.WriteLine("Şifre Giriniz: ");
                string password = Console.ReadLine();
                IUserLogin authentiatedUser = loginManager.Authenticate(userName, password);
                #endregion
                if (authentiatedUser != null)
                {
                    if (authentiatedUser.GetRole() == Role.Manager)
                    {
                        //Giriş yapan kullanıcıdan gelen nesneyi doğru tipe dönüştürmelisin:
                        if (authentiatedUser is Manager manager)
                        {
                            #region AdminGirişi
                            if (manager.IsRoot)
                            {
                                Console.WriteLine("Hoşgeldiniz Yönetim Paneline: ");
                                Console.WriteLine("Yapıcağınız İşlemleri Seçiniz: ");
                                Console.WriteLine("1-Çalışan Yönetimi\n" +
                                    "2-Ürün Yönetimi\n" +
                                    "3-Yönetici İşlemleri\n" +
                                    "4-Çıkış 4 basınız");
                                int selection = Convert.ToInt32(Console.ReadLine());
                                if (selection == 4) break;
                                if (selection == 1)
                                {
                                    Employee employee = new Employee(DateTime.Now);
                                    Console.WriteLine("1-Ekleme\n" +
                                        "2-Silme\n" +
                                        "3-Güncelleme\n" +
                                        "4-Listeleme\n" +
                                        "5-Çıkış için 5 basınız");
                                    int election = Convert.ToInt32(Console.ReadLine());
                                    if (election == 5) break;

                                    if (election == 1)
                                    {

                                        employeeService.GetNextId(employee);
                                        Console.WriteLine("Eklemek istediğiniz çalışanın İsmini giriniz: ");
                                        Helper.IsValidName(employee.UserName = Console.ReadLine());
                                        Console.WriteLine("Eklemek istediğiniz çalışanın şifresi: ");
                                        Helper.IsValidPassword(employee.Password = Console.ReadLine());
                                        Console.WriteLine("Eklemek istediğiniz çalışanın yaşını giriniz:  ");
                                        employee.Year = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("Eklemek istediğiniz kullanıcın telefon numarasın:");
                                        Helper.IsValidPhoneNumber(employee.PhoneNumber = Console.ReadLine());
                                        Console.WriteLine("Kullanıcın çalışma deneyeimini yazınız: ");
                                        employee.Seniority = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("Çalışanın maaşını yazınız: ");
                                        employee.Salary = Convert.ToDecimal(Console.ReadLine());
                                        employeeService.CalculateBonus(employee.Salary);
                                        employeeService.GetTotalEarnings(employee.Salary);

                                        if (employeeService.Add(employee) == true)
                                        {
                                            Console.WriteLine("Kullanıcı eklendi..");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Kullanıcı eklenemedi yanlış bilgiler...");
                                        }

                                    }
                                    else if (election == 2)
                                    {
                                        int count = 1;
                                        foreach (var item in employeeService.GetAll())
                                        {
                                            Console.WriteLine($"Sıra Numarası: {count} , Ürün ismi {item.UserName}, Numara: {item.PhoneNumber}");
                                        }
                                        Console.WriteLine("Silmek istediğiniz ürünün sıra numarasını giriniz: ");
                                        int election3 = Convert.ToInt16(Console.ReadLine());

                                        var employeeDelete = employeeService.GetAll()[election3 - 1];

                                        if (employeeService.Delete(employeeDelete.Id))
                                        {
                                            Console.WriteLine("Silme işlemi gerçekleşti...");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Silme işlemi yapılamadı"); break;
                                        }
                                    }
                                    else if (election == 3)
                                    {
                                        //count sadece göstürüp ekleme amacıyla yapıldı 
                                        int count = 1;
                                        Console.WriteLine("Kullanıcılar listeleniyor...");
                                        foreach (var list in employeeService.GetAll())
                                        {
                                            Console.WriteLine($"{count} İsimi: {list.UserName}  , Telefon Numarası : {list.PhoneNumber}");
                                            count++;
                                        }
                                        //Kullanıcaya seçtirdim 
                                        Console.WriteLine("Güncellemek istediğiniz kişinin sıra numarasın seçiniz: ");
                                        int election2 = Convert.ToInt32(Console.ReadLine());

                                        //Buradadaki amaç belirleyeceğim parametre listemde seçtiğin mnumaradan gelecek.
                                        var employeeUpdate = employeeService.GetAll()[election2 - 1];// -1 olma sebebi listeler -1 den başlar index alınan sayı 1den başladı 
                                        if (employeeService.Update(employeeUpdate.Id, employee))
                                        {
                                            Console.WriteLine("Güncellemek istediğiniz işlemi seçiniz: ");
                                            Console.WriteLine("1-İsim\n" +
                                                "2-Telefon Numarası\n" +
                                                "3-Şifreyi Güncelle\n" +
                                                "4-Maaşı güncelle");
                                            int election3 = Convert.ToInt16(Console.ReadLine());
                                            switch (election3)
                                            {
                                                case 1:
                                                    Console.WriteLine("İsimi Giriniz: ");
                                                    employee.UserName = Console.ReadLine();
                                                    break;
                                                case 2:
                                                    Console.WriteLine("Telefon Nmarası giriniz: ");
                                                    employee.PhoneNumber = Console.ReadLine();
                                                    break;
                                                case 3:
                                                    Console.WriteLine("Kullanıcın şifresin güncelle");
                                                    employee.Password = Console.ReadLine();
                                                    break;
                                                case 4:
                                                    Console.WriteLine("Güncel Maaş tutarını yazınız: ");
                                                    employee.Salary = Convert.ToInt16(Console.ReadLine());
                                                    break;
                                                default:
                                                    Console.WriteLine("Yanlış işlem yaptınız...");
                                                    break;
                                            }
                                        }
                                    }
                                    else if (election == 4)
                                    {
                                        Console.WriteLine("Listeleme işlemi gerçekleşiyor");
                                        foreach (var list in employeeService.GetAll())
                                        {
                                            Console.WriteLine($"İsmi: {list.UserName} , Şifre: {list.Password} , Telefon Numarası: {list.PhoneNumber} , Yaşı: {list.Year} , Maaşı: {list.Salary}");
                                        }
                                    }
                                }
                                else if (selection == 2)
                                {

                                    Console.WriteLine("Yapmak istediğiniz işlemi seçiniz: ");
                                    Console.WriteLine("Elektironik ürün işlemleri: \n" +
                                        "2-Mobilya Ürün işlemleri");
                                    int election = Convert.ToInt32(Console.ReadLine());
                                    if (election == 1)
                                    {
                                        Electronic electronic = new Electronic(DateTime.Now);
                                        Console.WriteLine("Yapmak istediğiniz işlemi seçiniz: ");
                                        Console.WriteLine("1-Ekleme\n" +
                                       "2-Silme\n" +
                                       "3-Güncelleme\n" +
                                       "4-Listeleme\n" +
                                       "5-Çıkış için 5 basınız");
                                        int election2 = Convert.ToInt32(Console.ReadLine());

                                        if (election2 == 1)
                                        {
                                            electronicService.GetNextId(electronic);
                                            Console.WriteLine("Eklemek istediğiniz ürünün ismini giriniz: ");
                                            electronic.Name = Console.ReadLine();
                                            Console.WriteLine("Eklemek istediğiniz ürün rengini giriniz: ");
                                            electronic.Color = Console.ReadLine();
                                            Console.WriteLine("Eklemek istediğiniz ürünün boyutunu giriniz (örneğin 30.48): ");
                                            electronic.Dimension = double.Parse(Console.ReadLine());
                                            Console.WriteLine("Eklemek istediğiniz ürünün garanti süresini yıl olarak giriniz: ");
                                            electronic.WarrantyPeriod = int.Parse(Console.ReadLine());
                                            Console.WriteLine("Eklemek istediğiniz ürünün fiyatını giriniz: ");
                                            electronic.Price = decimal.Parse(Console.ReadLine());
                                            Console.WriteLine("Eklemek istediğiniz ürünün modelini giriniz: ");
                                            electronic.Model = Console.ReadLine();
                                            Console.WriteLine("Eklemek istediğiniz ürünün kategorisini giriniz (Electronic, Furniture vb.): ");
                                            string CategoryInput = Console.ReadLine();
                                            if (Enum.TryParse<ProductCategory>(CategoryInput, out var category))
                                            {
                                                electronic.Category = category;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Yanlış katagori seçilci");
                                                electronic.Category = ProductCategory.Electronic;
                                            }

                                            Console.WriteLine("Eklemek istediğiniz ürünün hafıza miktarını (GB) giriniz: ");
                                            electronic.Memory = int.Parse(Console.ReadLine());

                                            Console.WriteLine("Eklemek istediğiniz ürünün işlemcisini giriniz (örn. i5): ");
                                            electronic.Processor = Console.ReadLine();

                                            Console.WriteLine("Eklemek istediğiniz ürünün işlemci hızını giriniz (örn. 65hz): ");
                                            electronic.ProcessorSpeed = Console.ReadLine();

                                            Console.WriteLine("Eklemek istediğiniz ürünün ekran kartı bilgisi giriniz (örn. Var veya Yok): ");
                                            electronic.GraphicsCard = Console.ReadLine();

                                            Console.WriteLine("Eklemek istediğiniz ürünün marka bilgisini giriniz (Mac, Samsung, Lenovo vb.): ");
                                            string brandInput = Console.ReadLine();
                                            if (Enum.TryParse<ElectronicBrand>(brandInput, out var brand))
                                            {
                                                electronic.ElectronicBrand = brand;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Yanlış bilgi girdiniz..");
                                                electronic.ElectronicBrand = ElectronicBrand.Huawei;
                                            }
                                            if (electronicService.Add(electronic) == true)
                                            {
                                                Console.WriteLine("Ekleme işlemi yapıldı...");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Ekleme işlemi yapılamadı");
                                            }

                                        }
                                        else if (election2 == 2)
                                        {
                                            int count = 1;
                                            foreach (var item in electronicService.GetAll())
                                            {
                                                Console.WriteLine($"Sıra Numarası: {count} , Ürün ismi {item.Name}, Modeli: {item.Model}");
                                            }
                                            Console.WriteLine("Silmek istediğiniz ürünün sıra numarasını giriniz: ");
                                            int election3 = Convert.ToInt16(Console.ReadLine());

                                            var electronicDelete = electronicService.GetAll()[election3 - 1];

                                            if (electronicService.Delete(electronicDelete.Id))
                                            {
                                                Console.WriteLine("Silme işlemi gerçekleşti...");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Silme işlemi yapılamadı"); break;
                                            }
                                        }
                                        else if (election2 == 3)
                                        {
                                            //count sadece göstürüp ekleme amacıyla yapıldı 
                                            int count = 1;
                                            Console.WriteLine("Kullanıcılar listeleniyor...");
                                            foreach (var list in electronicService.GetAll())
                                            {
                                                Console.WriteLine($"{count} İsimi: {list.Name}  , Telefon Numarası : {list.Model}");
                                                count++;
                                            }

                                            Console.WriteLine("Güncellemek istediğiniz ürünün  sıra numarasın seçiniz: ");
                                            int election3 = Convert.ToInt32(Console.ReadLine());


                                            var electronicUpdate = electronicService.GetAll()[election2 - 1];
                                            if (electronicService.Update(electronicUpdate.Id, electronic))
                                            {
                                                Console.WriteLine("Güncellemek istediğiniz ürünün hangi bilgisin güncelemek istersiniz: ");
                                                Console.WriteLine("Güncellemek istediğiniz alanı seçiniz:");
                                                Console.WriteLine("1 - Renk");
                                                Console.WriteLine("2 - Marka");
                                                Console.WriteLine("3 - Kategori");
                                                Console.WriteLine("4 - Fiyat");
                                                Console.WriteLine("5 - Ekran Kartı");
                                                Console.WriteLine("6 - Garanti Süresi");
                                                Console.WriteLine("7 - Boyut");
                                                int updateChoice = Convert.ToInt32(Console.ReadLine());

                                                switch (updateChoice)
                                                {
                                                    case 1:
                                                        Console.Write("Yeni Renk: ");
                                                        electronic.Color = Console.ReadLine();
                                                        break;
                                                    case 2:
                                                        Console.Write("Yeni Marka (sayı olarak): ");
                                                        electronic.ElectronicBrand = (ElectronicBrand)Convert.ToInt32(Console.ReadLine());
                                                        break;
                                                    case 3:
                                                        Console.Write("Yeni Kategori (sayı olarak): ");
                                                        electronic.Category = (ProductCategory)Convert.ToInt32(Console.ReadLine());
                                                        break;
                                                    case 4:
                                                        Console.Write("Yeni Fiyat: ");
                                                        electronic.Price = Convert.ToDecimal(Console.ReadLine());
                                                        break;
                                                    case 5:
                                                        Console.Write("Yeni Ekran Kartı Bilgisi: ");
                                                        electronic.GraphicsCard = Console.ReadLine();
                                                        break;
                                                    case 6:
                                                        Console.Write("Yeni Garanti Süresi: ");
                                                        electronic.WarrantyPeriod = Convert.ToInt32(Console.ReadLine());
                                                        break;
                                                    case 7:
                                                        Console.Write("Yeni Boyut: ");
                                                        electronic.Dimension = Convert.ToDouble(Console.ReadLine());
                                                        break;
                                                    default:
                                                        Console.WriteLine("Geçersiz seçim.");
                                                        break;
                                                }


                                            }

                                        }
                                        else if (election2 == 4)
                                        {
                                            Console.WriteLine("Listeleme işlemi yapılıyor...");
                                            int count = 1;
                                            foreach (var item in electronicService.GetAll())
                                            {
                                                Console.WriteLine($"--- Ürün {count} ---");
                                                Console.WriteLine($"İsim: {item.Name}");
                                                Console.WriteLine($"Renk: {item.Color}");
                                                Console.WriteLine($"Boyut: {item.Dimension} cm");
                                                Console.WriteLine($"Garanti Süresi: {item.WarrantyPeriod} yıl");
                                                Console.WriteLine($"Fiyat: {item.Price} TL");
                                                Console.WriteLine($"Model: {item.Model}");
                                                Console.WriteLine($"Kategori: {item.Category}");
                                                Console.WriteLine($"Bellek (RAM): {item.Memory} GB");
                                                Console.WriteLine($"İşlemci: {item.Processor}");
                                                Console.WriteLine($"İşlemci Hızı: {item.ProcessorSpeed}");
                                                Console.WriteLine($"Ekran Kartı: {item.GraphicsCard}");
                                                Console.WriteLine($"Marka: {item.ElectronicBrand}");
                                                Console.WriteLine("-----------------------------\n");
                                                count++;
                                            }
                                        }

                                    }
                                    else if (election == 2)
                                    {
                                        Furniture furniture = new Furniture(DateTime.Now);
                                        Console.WriteLine("Yapmak istediğiniz işlemi seçiniz: ");
                                        Console.WriteLine("1-Ekleme\n" +
                                       "2-Silme\n" +
                                       "3-Güncelleme\n" +
                                       "4-Listeleme\n" +
                                       "5-Çıkış için 5 basınız");
                                        int election2 = Convert.ToInt32(Console.ReadLine());

                                        if (election2 == 1)
                                        {
                                            furnitureService.GetNextId(furniture);
                                            Console.WriteLine("Eklemek istediğiniz ürünün ismini giriniz: ");
                                            furniture.Name = Console.ReadLine();
                                            Console.WriteLine("Eklemek istediğiniz ürün rengini giriniz: ");
                                            furniture.Color = Console.ReadLine();
                                            Console.WriteLine("Eklemek istediğiniz ürünün boyutunu giriniz (örneğin 30.48): ");
                                            furniture.Dimension = double.Parse(Console.ReadLine());
                                            Console.WriteLine("Eklemek istediğiniz ürünün garanti süresini yıl olarak giriniz: ");
                                            furniture.WarrantyPeriod = int.Parse(Console.ReadLine());
                                            Console.WriteLine("Eklemek istediğiniz ürünün fiyatını giriniz: ");
                                            furniture.Price = decimal.Parse(Console.ReadLine());
                                            Console.WriteLine("Eklemek istediğiniz ürünün modelini giriniz: ");
                                            furniture.Model = Console.ReadLine();
                                            Console.WriteLine("Eklemek istediğiniz ürünün kategorisini giriniz (Electronic, Furniture vb.): ");
                                            string CategoryInput = Console.ReadLine();
                                            if (Enum.TryParse<ProductCategory>(CategoryInput, out var category))
                                            {
                                                furniture.Category = category;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Yanlış katagori seçilci");
                                                furniture.Category = ProductCategory.Electronic;
                                            }
                                            Console.WriteLine("Eklemek istediğiniz ürünün marka bilgisini giriniz  ");
                                            string brandInput = Console.ReadLine();
                                            if (Enum.TryParse<FurnitureBrand>(brandInput, out var brand))
                                            {
                                                furniture.FurnitureBrand = brand;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Yanlış bilgi girdiniz..");
                                                furniture.FurnitureBrand = FurnitureBrand.Hepsiburada;
                                            }
                                            if (furnitureService.Add(furniture) == true)
                                            {
                                                Console.WriteLine("Ekleme işlemi yapıldı...");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Ekleme işlemi yapılamadı");
                                            }

                                        }
                                        else if (election2 == 2)
                                        {
                                            int count = 1;
                                            foreach (var item in furnitureService.GetAll())
                                            {
                                                Console.WriteLine($"Sıra Numarası: {count} , Ürün ismi {item.Name}, Modeli: {item.Model}");
                                            }
                                            Console.WriteLine("Silmek istediğiniz ürünün sıra numarasını giriniz: ");
                                            int election3 = Convert.ToInt16(Console.ReadLine());

                                            var furnitureDelete = furnitureService.GetAll()[election3 - 1];

                                            if (furnitureService.Delete(furnitureDelete.Id))
                                            {
                                                Console.WriteLine("Silme işlemi gerçekleşti...");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Silme işlemi yapılamadı"); break;
                                            }
                                        }
                                        else if (election2 == 3)
                                        {
                                            //count sadece göstürüp ekleme amacıyla yapıldı 
                                            int count = 1;
                                            Console.WriteLine("Kullanıcılar listeleniyor...");
                                            foreach (var list in furnitureService.GetAll())
                                            {
                                                Console.WriteLine($"{count} İsimi: {list.Name}  , Model: {list.Model}");
                                                count++;
                                            }

                                            Console.WriteLine("Güncellemek istediğiniz ürünün  sıra numarasın seçiniz: ");
                                            int election3 = Convert.ToInt32(Console.ReadLine());


                                            var furnitureUpdate = furnitureService.GetAll()[election2 - 1];
                                            if (furnitureService.Update(furnitureUpdate.Id, furniture))
                                            {
                                                Console.WriteLine("Güncellemek istediğiniz ürünün hangi bilgisin güncelemek istersiniz: ");
                                                Console.WriteLine("Güncellemek istediğiniz alanı seçiniz:");
                                                Console.WriteLine("1 - Renk");
                                                Console.WriteLine("2 - Marka");
                                                Console.WriteLine("3 - Kategori");
                                                Console.WriteLine("4 - Fiyat");
                                                int updateChoice = Convert.ToInt32(Console.ReadLine());

                                                switch (updateChoice)
                                                {
                                                    case 1:
                                                        Console.Write("Yeni Renk: ");
                                                        furniture.Color = Console.ReadLine();
                                                        break;
                                                    case 2:
                                                        Console.Write("Yeni Marka (sayı olarak): ");
                                                        furniture.FurnitureBrand = (FurnitureBrand)Convert.ToInt32(Console.ReadLine());
                                                        break;
                                                    case 3:
                                                        Console.Write("Yeni Kategori (sayı olarak): ");
                                                        furniture.Category = (ProductCategory)Convert.ToInt32(Console.ReadLine());
                                                        break;
                                                    case 4:
                                                        Console.Write("Yeni Fiyat: ");
                                                        furniture.Price = Convert.ToDecimal(Console.ReadLine());
                                                        break;
                                                    default:
                                                        Console.WriteLine("Geçersiz seçim.");
                                                        break;
                                                }


                                            }

                                        }
                                        else if (election2 == 4)
                                        {
                                            Console.WriteLine("Listeleme işlemi yapılıyor...");
                                            int count = 1;
                                            foreach (var item in furnitureService.GetAll())
                                            {
                                                Console.WriteLine($"--- Ürün {count} ---");
                                                Console.WriteLine($"İsim: {item.Name}");
                                                Console.WriteLine($"Renk: {item.Color}");
                                                Console.WriteLine($"Boyut: {item.Dimension} cm");
                                                Console.WriteLine($"Garanti Süresi: {item.WarrantyPeriod} yıl");
                                                Console.WriteLine($"Fiyat: {item.Price} TL");
                                                Console.WriteLine($"Model: {item.Model}");
                                                Console.WriteLine($"Kategori: {item.Category}");
                                                count++;
                                            }
                                        }
                                    }


                                }
                                else if (selection == 3)
                                {

                                    Console.WriteLine("1-Ekleme\n" +
                                        "2-Silme\n" +
                                        "3-Güncelleme\n" +
                                        "4-Listeleme\n" +
                                        "5-Çıkış için 5 basınız");
                                    int election = Convert.ToInt32(Console.ReadLine());
                                    if (election == 5) break;

                                    if (election == 1)
                                    {

                                        employeeService.GetNextId(manager);
                                        Console.WriteLine("Eklemek istediğiniz yöneticinin İsmini giriniz: ");
                                        Helper.IsValidName(manager.UserName = Console.ReadLine());
                                        Console.WriteLine("Eklemek istediğiniz yöneticinin şifresi: ");
                                        Helper.IsValidPassword(manager.Password = Console.ReadLine());
                                        Console.WriteLine("Eklemek istediğiniz yöneticinin yaşını giriniz:  ");
                                        manager.Year = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("Eklemek istediğiniz kullanıcın telefon numarasın:");
                                        Helper.IsValidPhoneNumber(manager.PhoneNumber = Console.ReadLine());
                                        Console.WriteLine("Yöneticinin maaşını yazınız: ");
                                        manager.Salary = Convert.ToInt16(Console.ReadLine());

                                        if (managerService.Add(manager) == true)
                                        {
                                            Console.WriteLine("Kullanıcı eklendi..");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Kullanıcı eklenemedi yanlış bilgiler...");
                                        }

                                    }
                                    else if (election == 2)
                                    {
                                        int count = 1;
                                        foreach (var item in managerService.GetAll())
                                        {
                                            Console.WriteLine($"Sıra Numarası: {count} , Ürün ismi {item.UserName}, Numara: {item.PhoneNumber}");
                                        }
                                        Console.WriteLine("Silmek istediğiniz ürünün sıra numarasını giriniz: ");
                                        int election3 = Convert.ToInt16(Console.ReadLine());

                                        var managerDelete = managerService.GetAll()[election3 - 1];

                                        if (managerService.Delete(managerDelete.Id))
                                        {
                                            Console.WriteLine("Silme işlemi gerçekleşti...");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Silme işlemi yapılamadı"); break;
                                        }

                                    }
                                    else if (election == 3)
                                    {
                                        //count sadece göstürüp ekleme amacıyla yapıldı 
                                        int count = 1;
                                        Console.WriteLine("Kullanıcılar listeleniyor...");
                                        foreach (var list in managerService.GetAll())
                                        {
                                            Console.WriteLine($"{count} İsimi: {list.UserName}  , Telefon Numarası : {list.PhoneNumber}");
                                            count++;
                                        }
                                        //Kullanıcaya seçtirdim 
                                        Console.WriteLine("Güncellemek istediğiniz kişinin sıra numarasın seçiniz: ");
                                        int election2 = Convert.ToInt32(Console.ReadLine());

                                        //Buradadaki amaç belirleyeceğim parametre listemde seçtiğin mnumaradan gelecek.
                                        var managerUpdate = managerService.GetAll()[election2 - 1];// -1 olma sebebi listeler -1 den başlar index alınan sayı 1den başladı 
                                        if (managerService.Update(managerUpdate.Id, manager))
                                        {
                                            Console.WriteLine("Güncellemek istediğiniz işlemi seçiniz: ");
                                            Console.WriteLine("1-İsim\n" +
                                                "2-Telefon Numarası\n" +
                                                "3-Şifreyi Güncelle\n" +
                                                "4-Maaşı güncelle");
                                            int election3 = Convert.ToInt16(Console.ReadLine());
                                            switch (election3)
                                            {
                                                case 1:
                                                    Console.WriteLine("İsimi Giriniz: ");
                                                    manager.UserName = Console.ReadLine();
                                                    break;
                                                case 2:
                                                    Console.WriteLine("Telefon Nmarası giriniz: ");
                                                    manager.PhoneNumber = Console.ReadLine();
                                                    break;
                                                case 3:
                                                    Console.WriteLine("Kullanıcın şifresin güncelle");
                                                    manager.Password = Console.ReadLine();
                                                    break;
                                                case 4:
                                                    Console.WriteLine("Güncel Maaş tutarını yazınız: ");
                                                    manager.Salary = Convert.ToInt16(Console.ReadLine());
                                                    break;
                                                default:
                                                    Console.WriteLine("Yanlış işlem yaptınız...");
                                                    break;
                                            }
                                        }
                                    }
                                    else if (election == 4)
                                    {
                                        Console.WriteLine("Listeleme işlemi gerçekleşiyor");
                                        foreach (var list in managerService.GetAll())
                                        {
                                            Console.WriteLine($"İsmi: {list.UserName} , Şifre: {list.Password} , Telefon Numarası: {list.PhoneNumber} , Yaşı: {list.Year} , Maaşı: {list.Salary}");
                                        }
                                    }

                                }
                            }
                            #endregion
                            #region YöneticiGirişi
                            else
                            {
                                Console.WriteLine("Hoşgeldiniz Yönetici sayfasına ");
                                Console.WriteLine("Yapıcağınız İşlemleri Seçiniz: ");
                                Console.WriteLine("1-Çalışan Yönetimi\n" +
                                    "2-Ürün Yönetimi\n" +
                                    "3-Çıkış Yapınız");
                                int selection = Convert.ToInt32(Console.ReadLine());

                                if (selection == 3) break;
                                else if (selection == 1)
                                {
                                    Employee employee = new Employee(DateTime.Now);
                                    Console.WriteLine("1-Ekleme\n" +
                                        "2-Silme\n" +
                                        "3-Güncelleme\n" +
                                        "4-Listeleme\n" +
                                        "5-Çıkış için 5 basınız");
                                    int election = Convert.ToInt32(Console.ReadLine());
                                    if (election == 5) break;

                                    if (election == 1)
                                    {

                                        employeeService.GetNextId(employee);
                                        Console.WriteLine("Eklemek istediğiniz çalışanın İsmini giriniz: ");
                                        Helper.IsValidName(employee.UserName = Console.ReadLine());
                                        Console.WriteLine("Eklemek istediğiniz çalışanın şifresi: ");
                                        Helper.IsValidPassword(employee.Password = Console.ReadLine());
                                        Console.WriteLine("Eklemek istediğiniz çalışanın yaşını giriniz:  ");
                                        employee.Year = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("Eklemek istediğiniz kullanıcın telefon numarasın:");
                                        Helper.IsValidPhoneNumber(employee.PhoneNumber = Console.ReadLine());
                                        Console.WriteLine("Kullanıcın çalışma deneyeimini yazınız: ");
                                        employee.Seniority = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("Çalışanın maaşını yazınız: ");
                                        employee.Salary = Convert.ToInt16(Console.ReadLine());

                                        if (employeeService.Add(employee) == true)
                                        {
                                            Console.WriteLine("Kullanıcı eklendi..");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Kullanıcı eklenemedi yanlış bilgiler...");
                                        }

                                    }
                                    else if (election == 2)
                                    {
                                        int count = 1;
                                        foreach (var item in employeeService.GetAll())
                                        {
                                            Console.WriteLine($"Sıra Numarası: {count} , Ürün ismi {item.UserName}, Telefon:  {item.PhoneNumber}");
                                        }
                                        Console.WriteLine("Silmek istediğiniz ürünün sıra numarasını giriniz: ");
                                        int election3 = Convert.ToInt16(Console.ReadLine());

                                        var employeeDelete = employeeService.GetAll()[election3 - 1];

                                        if (employeeService.Delete(employeeDelete.Id))
                                        {
                                            Console.WriteLine("Silme işlemi gerçekleşti...");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Silme işlemi yapılamadı"); break;
                                        }

                                    }
                                    else if (election == 3)
                                    {
                                        //count sadece göstürüp ekleme amacıyla yapıldı 
                                        int count = 1;
                                        Console.WriteLine("Kullanıcılar listeleniyor...");
                                        foreach (var list in employeeService.GetAll())
                                        {
                                            Console.WriteLine($"{count} İsimi: {list.UserName}  , Telefon Numarası : {list.PhoneNumber}");
                                            count++;
                                        }
                                        //Kullanıcaya seçtirdim 
                                        Console.WriteLine("Güncellemek istediğiniz kişinin sıra numarasın seçiniz: ");
                                        int election2 = Convert.ToInt32(Console.ReadLine());

                                        //Buradadaki amaç belirleyeceğim parametre listemde seçtiğin mnumaradan gelecek.
                                        var employeeUpdate = employeeService.GetAll()[election2 - 1];// -1 olma sebebi listeler -1 den başlar index alınan sayı 1den başladı 
                                        if (employeeService.Update(employeeUpdate.Id, employee))
                                        {
                                            Console.WriteLine("Güncellemek istediğiniz işlemi seçiniz: ");
                                            Console.WriteLine("1-İsim\n" +
                                                "2-Telefon Numarası\n" +
                                                "3-Şifreyi Güncelle\n" +
                                                "4-Maaşı güncelle");
                                            int election3 = Convert.ToInt16(Console.ReadLine());
                                            switch (election3)
                                            {
                                                case 1:
                                                    Console.WriteLine("İsimi Giriniz: ");
                                                    employee.UserName = Console.ReadLine();
                                                    break;
                                                case 2:
                                                    Console.WriteLine("Telefon Nmarası giriniz: ");
                                                    employee.PhoneNumber = Console.ReadLine();
                                                    break;
                                                case 3:
                                                    Console.WriteLine("Kullanıcın şifresin güncelle");
                                                    employee.Password = Console.ReadLine();
                                                    break;
                                                case 4:
                                                    Console.WriteLine("Güncel Maaş tutarını yazınız: ");
                                                    employee.Salary = Convert.ToInt16(Console.ReadLine());
                                                    break;
                                                default:
                                                    Console.WriteLine("Yanlış işlem yaptınız...");
                                                    break;
                                            }
                                        }
                                    }
                                    else if (election == 4)
                                    {
                                        Console.WriteLine("Listeleme işlemi gerçekleşiyor");
                                        foreach (var list in employeeService.GetAll())
                                        {
                                            Console.WriteLine($"İsmi: {list.UserName} , Şifre: {list.Password} , Telefon Numarası: {list.PhoneNumber} , Yaşı: {list.Year} , Maaşı: {list.Salary}");
                                        }
                                    }


                                }
                                else if (selection == 2)
                                {
                                    Console.WriteLine("Yapmak istediğiniz işlemi seçiniz: ");
                                    Console.WriteLine("Elektironik ürün işlemleri: \n" +
                                        "2-Mobilya Ürün işlemleri");
                                    int election = Convert.ToInt32(Console.ReadLine());
                                    if (election == 1)
                                    {
                                        Electronic electronic = new Electronic(DateTime.Now);
                                        Console.WriteLine("Yapmak istediğiniz işlemi seçiniz: ");
                                        Console.WriteLine("1-Ekleme\n" +
                                       "2-Silme\n" +
                                       "3-Güncelleme\n" +
                                       "4-Listeleme\n" +
                                       "5-Çıkış için 5 basınız");
                                        int election2 = Convert.ToInt32(Console.ReadLine());

                                        if (election2 == 1)
                                        {
                                            electronicService.GetNextId(electronic);
                                            Console.WriteLine("Eklemek istediğiniz ürünün ismini giriniz: ");
                                            electronic.Name = Console.ReadLine();
                                            Console.WriteLine("Eklemek istediğiniz ürün rengini giriniz: ");
                                            electronic.Color = Console.ReadLine();
                                            Console.WriteLine("Eklemek istediğiniz ürünün boyutunu giriniz (örneğin 30.48): ");
                                            electronic.Dimension = double.Parse(Console.ReadLine());
                                            Console.WriteLine("Eklemek istediğiniz ürünün garanti süresini yıl olarak giriniz: ");
                                            electronic.WarrantyPeriod = int.Parse(Console.ReadLine());
                                            Console.WriteLine("Eklemek istediğiniz ürünün fiyatını giriniz: ");
                                            electronic.Price = decimal.Parse(Console.ReadLine());
                                            Console.WriteLine("Eklemek istediğiniz ürünün modelini giriniz: ");
                                            electronic.Model = Console.ReadLine();
                                            Console.WriteLine("Eklemek istediğiniz ürünün kategorisini giriniz (Electronic, Furniture vb.): ");
                                            string CategoryInput = Console.ReadLine();
                                            if (Enum.TryParse<ProductCategory>(CategoryInput, out var category))
                                            {
                                                electronic.Category = category;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Yanlış katagori seçilci");
                                                electronic.Category = ProductCategory.Electronic;
                                            }

                                            Console.WriteLine("Eklemek istediğiniz ürünün hafıza miktarını (GB) giriniz: ");
                                            electronic.Memory = int.Parse(Console.ReadLine());

                                            Console.WriteLine("Eklemek istediğiniz ürünün işlemcisini giriniz (örn. i5): ");
                                            electronic.Processor = Console.ReadLine();

                                            Console.WriteLine("Eklemek istediğiniz ürünün işlemci hızını giriniz (örn. 65hz): ");
                                            electronic.ProcessorSpeed = Console.ReadLine();

                                            Console.WriteLine("Eklemek istediğiniz ürünün ekran kartı bilgisi giriniz (örn. Var veya Yok): ");
                                            electronic.GraphicsCard = Console.ReadLine();

                                            Console.WriteLine("Eklemek istediğiniz ürünün marka bilgisini giriniz (Mac, Samsung, Lenovo vb.): ");
                                            string brandInput = Console.ReadLine();
                                            if (Enum.TryParse<ElectronicBrand>(brandInput, out var brand))
                                            {
                                                electronic.ElectronicBrand = brand;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Yanlış bilgi girdiniz..");
                                                electronic.ElectronicBrand = ElectronicBrand.Huawei;
                                            }
                                            if (electronicService.Add(electronic) == true)
                                            {
                                                Console.WriteLine("Ekleme işlemi yapıldı...");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Ekleme işlemi yapılamadı");
                                            }

                                        }
                                        else if (election2 == 2)
                                        {
                                            int count = 1;
                                            foreach (var item in electronicService.GetAll())
                                            {
                                                Console.WriteLine($"Sıra Numarası: {count} , Ürün ismi {item.Name}, Modeli: {item.Model}");
                                            }
                                            Console.WriteLine("Silmek istediğiniz ürünün sıra numarasını giriniz: ");
                                            int election3 = Convert.ToInt16(Console.ReadLine());

                                            var electronicDelete = electronicService.GetAll()[election3 - 1];

                                            if (electronicService.Delete(electronicDelete.Id))
                                            {
                                                Console.WriteLine("Silme işlemi gerçekleşti...");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Silme işlemi yapılamadı"); break;
                                            }
                                        }
                                        else if (election2 == 3)
                                        {
                                            //count sadece göstürüp ekleme amacıyla yapıldı 
                                            int count = 1;
                                            Console.WriteLine("Kullanıcılar listeleniyor...");
                                            foreach (var list in electronicService.GetAll())
                                            {
                                                Console.WriteLine($"{count} İsimi: {list.Name}  , Telefon Numarası : {list.Model}");
                                                count++;
                                            }

                                            Console.WriteLine("Güncellemek istediğiniz ürünün  sıra numarasın seçiniz: ");
                                            int election3 = Convert.ToInt32(Console.ReadLine());


                                            var electronicUpdate = electronicService.GetAll()[election2 - 1];
                                            if (electronicService.Update(electronicUpdate.Id, electronic))
                                            {
                                                Console.WriteLine("Güncellemek istediğiniz ürünün hangi bilgisin güncelemek istersiniz: ");
                                                Console.WriteLine("Güncellemek istediğiniz alanı seçiniz:");
                                                Console.WriteLine("1 - Renk");
                                                Console.WriteLine("2 - Marka");
                                                Console.WriteLine("3 - Kategori");
                                                Console.WriteLine("4 - Fiyat");
                                                Console.WriteLine("5 - Ekran Kartı");
                                                Console.WriteLine("6 - Garanti Süresi");
                                                Console.WriteLine("7 - Boyut");
                                                int updateChoice = Convert.ToInt32(Console.ReadLine());

                                                switch (updateChoice)
                                                {
                                                    case 1:
                                                        Console.Write("Yeni Renk: ");
                                                        electronic.Color = Console.ReadLine();
                                                        break;
                                                    case 2:
                                                        Console.Write("Yeni Marka (sayı olarak): ");
                                                        electronic.ElectronicBrand = (ElectronicBrand)Convert.ToInt32(Console.ReadLine());
                                                        break;
                                                    case 3:
                                                        Console.Write("Yeni Kategori (sayı olarak): ");
                                                        electronic.Category = (ProductCategory)Convert.ToInt32(Console.ReadLine());
                                                        break;
                                                    case 4:
                                                        Console.Write("Yeni Fiyat: ");
                                                        electronic.Price = Convert.ToDecimal(Console.ReadLine());
                                                        break;
                                                    case 5:
                                                        Console.Write("Yeni Ekran Kartı Bilgisi: ");
                                                        electronic.GraphicsCard = Console.ReadLine();
                                                        break;
                                                    case 6:
                                                        Console.Write("Yeni Garanti Süresi: ");
                                                        electronic.WarrantyPeriod = Convert.ToInt32(Console.ReadLine());
                                                        break;
                                                    case 7:
                                                        Console.Write("Yeni Boyut: ");
                                                        electronic.Dimension = Convert.ToDouble(Console.ReadLine());
                                                        break;
                                                    default:
                                                        Console.WriteLine("Geçersiz seçim.");
                                                        break;
                                                }


                                            }
                                        }
                                        else if (election2 == 4)
                                        {
                                            Console.WriteLine("Listeleme işlemi yapılıyor...");
                                            int count = 1;
                                            foreach (var item in electronicService.GetAll())
                                            {
                                                Console.WriteLine($"--- Ürün {count} ---");
                                                Console.WriteLine($"İsim: {item.Name}");
                                                Console.WriteLine($"Renk: {item.Color}");
                                                Console.WriteLine($"Boyut: {item.Dimension} cm");
                                                Console.WriteLine($"Garanti Süresi: {item.WarrantyPeriod} yıl");
                                                Console.WriteLine($"Fiyat: {item.Price} TL");
                                                Console.WriteLine($"Model: {item.Model}");
                                                Console.WriteLine($"Kategori: {item.Category}");
                                                Console.WriteLine($"Bellek (RAM): {item.Memory} GB");
                                                Console.WriteLine($"İşlemci: {item.Processor}");
                                                Console.WriteLine($"İşlemci Hızı: {item.ProcessorSpeed}");
                                                Console.WriteLine($"Ekran Kartı: {item.GraphicsCard}");
                                                Console.WriteLine($"Marka: {item.ElectronicBrand}");
                                                Console.WriteLine("-----------------------------\n");
                                                count++;
                                            }
                                        }
                                    }
                                    else if (election == 2)
                                    {
                                        Furniture furniture = new Furniture(DateTime.Now);
                                        Console.WriteLine("Yapmak istediğiniz işlemi seçiniz: ");
                                        Console.WriteLine("1-Ekleme\n" +
                                       "2-Silme\n" +
                                       "3-Güncelleme\n" +
                                       "4-Listeleme\n" +
                                       "5-Çıkış için 5 basınız");
                                        int election2 = Convert.ToInt32(Console.ReadLine());

                                        if (election2 == 1)
                                        {
                                            furnitureService.GetNextId(furniture);
                                            Console.WriteLine("Eklemek istediğiniz ürünün ismini giriniz: ");
                                            furniture.Name = Console.ReadLine();
                                            Console.WriteLine("Eklemek istediğiniz ürün rengini giriniz: ");
                                            furniture.Color = Console.ReadLine();
                                            Console.WriteLine("Eklemek istediğiniz ürünün boyutunu giriniz (örneğin 30.48): ");
                                            furniture.Dimension = double.Parse(Console.ReadLine());
                                            Console.WriteLine("Eklemek istediğiniz ürünün garanti süresini yıl olarak giriniz: ");
                                            furniture.WarrantyPeriod = int.Parse(Console.ReadLine());
                                            Console.WriteLine("Eklemek istediğiniz ürünün fiyatını giriniz: ");
                                            furniture.Price = decimal.Parse(Console.ReadLine());
                                            Console.WriteLine("Eklemek istediğiniz ürünün modelini giriniz: ");
                                            furniture.Model = Console.ReadLine();
                                            Console.WriteLine("Eklemek istediğiniz ürünün kategorisini giriniz (Electronic, Furniture vb.): ");
                                            string CategoryInput = Console.ReadLine();
                                            if (Enum.TryParse<ProductCategory>(CategoryInput, out var category))
                                            {
                                                furniture.Category = category;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Yanlış katagori seçilci");
                                                furniture.Category = ProductCategory.Electronic;
                                            }
                                            Console.WriteLine("Eklemek istediğiniz ürünün marka bilgisini giriniz  ");
                                            string brandInput = Console.ReadLine();
                                            if (Enum.TryParse<FurnitureBrand>(brandInput, out var brand))
                                            {
                                                furniture.FurnitureBrand = brand;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Yanlış bilgi girdiniz..");
                                                furniture.FurnitureBrand = FurnitureBrand.Hepsiburada;
                                            }
                                            if (furnitureService.Add(furniture) == true)
                                            {
                                                Console.WriteLine("Ekleme işlemi yapıldı...");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Ekleme işlemi yapılamadı");
                                            }

                                        }
                                        else if (election2 == 2)
                                        {
                                            int count = 1;
                                            foreach (var item in furnitureService.GetAll())
                                            {
                                                Console.WriteLine($"Sıra Numarası: {count} , Ürün ismi {item.Name}, Modeli: {item.Model}");
                                            }
                                            Console.WriteLine("Silmek istediğiniz ürünün sıra numarasını giriniz: ");
                                            int election3 = Convert.ToInt16(Console.ReadLine());

                                            var furnitureDelete = furnitureService.GetAll()[election3 - 1];

                                            if (furnitureService.Delete(furnitureDelete.Id))
                                            {
                                                Console.WriteLine("Silme işlemi gerçekleşti...");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Silme işlemi yapılamadı"); break;
                                            }
                                        }
                                        else if (election2 == 3)
                                        {
                                            //count sadece göstürüp ekleme amacıyla yapıldı 
                                            int count = 1;
                                            Console.WriteLine("Kullanıcılar listeleniyor...");
                                            foreach (var list in furnitureService.GetAll())
                                            {
                                                Console.WriteLine($"{count} İsimi: {list.Name}  , Model: {list.Model}");
                                                count++;
                                            }

                                            Console.WriteLine("Güncellemek istediğiniz ürünün  sıra numarasın seçiniz: ");
                                            int election3 = Convert.ToInt32(Console.ReadLine());


                                            var furnitureUpdate = furnitureService.GetAll()[election2 - 1];
                                            if (furnitureService.Update(furnitureUpdate.Id, furniture))
                                            {
                                                Console.WriteLine("Güncellemek istediğiniz ürünün hangi bilgisin güncelemek istersiniz: ");
                                                Console.WriteLine("Güncellemek istediğiniz alanı seçiniz:");
                                                Console.WriteLine("1 - Renk");
                                                Console.WriteLine("2 - Marka");
                                                Console.WriteLine("3 - Kategori");
                                                Console.WriteLine("4 - Fiyat");
                                                int updateChoice = Convert.ToInt32(Console.ReadLine());

                                                switch (updateChoice)
                                                {
                                                    case 1:
                                                        Console.Write("Yeni Renk: ");
                                                        furniture.Color = Console.ReadLine();
                                                        break;
                                                    case 2:
                                                        Console.Write("Yeni Marka (sayı olarak): ");
                                                        furniture.FurnitureBrand = (FurnitureBrand)Convert.ToInt32(Console.ReadLine());
                                                        break;
                                                    case 3:
                                                        Console.Write("Yeni Kategori (sayı olarak): ");
                                                        furniture.Category = (ProductCategory)Convert.ToInt32(Console.ReadLine());
                                                        break;
                                                    case 4:
                                                        Console.Write("Yeni Fiyat: ");
                                                        furniture.Price = Convert.ToDecimal(Console.ReadLine());
                                                        break;
                                                    default:
                                                        Console.WriteLine("Geçersiz seçim.");
                                                        break;
                                                }


                                            }

                                        }
                                    }
                                }

                            }
                            #endregion

                        }
                    }
                    #region ÇalışanGirişi
                    else if (authentiatedUser.GetRole() == Role.Employee)
                    {
                        Console.WriteLine("Yapmak istediğiniz işlemi seçiniz: ");
                        Console.WriteLine("Elektironik ürün işlemleri: \n" +
                            "2-Mobilya Ürün işlemleri");
                        int election = Convert.ToInt32(Console.ReadLine());
                        if (election == 1)
                        {
                            Electronic electronic = new Electronic(DateTime.Now);
                            Console.WriteLine("Yapmak istediğiniz işlemi seçiniz: ");
                            Console.WriteLine("1-Ekleme\n" +
                           "2-Silme\n" +
                           "3-Güncelleme\n" +
                           "4-Listeleme\n" +
                           "5-Çıkış için 5 basınız");
                            int election2 = Convert.ToInt32(Console.ReadLine());

                            if (election2 == 1)
                            {
                                electronicService.GetNextId(electronic);
                                Console.WriteLine("Eklemek istediğiniz ürünün ismini giriniz: ");
                                electronic.Name = Console.ReadLine();
                                Console.WriteLine("Eklemek istediğiniz ürün rengini giriniz: ");
                                electronic.Color = Console.ReadLine();
                                Console.WriteLine("Eklemek istediğiniz ürünün boyutunu giriniz (örneğin 30.48): ");
                                electronic.Dimension = double.Parse(Console.ReadLine());
                                Console.WriteLine("Eklemek istediğiniz ürünün garanti süresini yıl olarak giriniz: ");
                                electronic.WarrantyPeriod = int.Parse(Console.ReadLine());
                                Console.WriteLine("Eklemek istediğiniz ürünün fiyatını giriniz: ");
                                electronic.Price = decimal.Parse(Console.ReadLine());
                                Console.WriteLine("Eklemek istediğiniz ürünün modelini giriniz: ");
                                electronic.Model = Console.ReadLine();
                                Console.WriteLine("Eklemek istediğiniz ürünün kategorisini giriniz (Electronic, Furniture vb.): ");
                                string CategoryInput = Console.ReadLine();
                                if (Enum.TryParse<ProductCategory>(CategoryInput, out var category))
                                {
                                    electronic.Category = category;
                                }
                                else
                                {
                                    Console.WriteLine("Yanlış katagori seçilci");
                                    electronic.Category = ProductCategory.Electronic;
                                }

                                Console.WriteLine("Eklemek istediğiniz ürünün hafıza miktarını (GB) giriniz: ");
                                electronic.Memory = int.Parse(Console.ReadLine());

                                Console.WriteLine("Eklemek istediğiniz ürünün işlemcisini giriniz (örn. i5): ");
                                electronic.Processor = Console.ReadLine();

                                Console.WriteLine("Eklemek istediğiniz ürünün işlemci hızını giriniz (örn. 65hz): ");
                                electronic.ProcessorSpeed = Console.ReadLine();

                                Console.WriteLine("Eklemek istediğiniz ürünün ekran kartı bilgisi giriniz (örn. Var veya Yok): ");
                                electronic.GraphicsCard = Console.ReadLine();

                                Console.WriteLine("Eklemek istediğiniz ürünün marka bilgisini giriniz (Mac, Samsung, Lenovo vb.): ");
                                string brandInput = Console.ReadLine();
                                if (Enum.TryParse<ElectronicBrand>(brandInput, out var brand))
                                {
                                    electronic.ElectronicBrand = brand;
                                }
                                else
                                {
                                    Console.WriteLine("Yanlış bilgi girdiniz..");
                                    electronic.ElectronicBrand = ElectronicBrand.Huawei;
                                }
                                if (electronicService.Add(electronic) == true)
                                {
                                    Console.WriteLine("Ekleme işlemi yapıldı...");
                                }
                                else
                                {
                                    Console.WriteLine("Ekleme işlemi yapılamadı");
                                }

                            }
                            else if (election2 == 2)
                            {
                                int count = 1;
                                foreach (var item in electronicService.GetAll())
                                {
                                    Console.WriteLine($"Sıra Numarası: {count} , Ürün ismi {item.Name}, Modeli: {item.Model}");
                                }
                                Console.WriteLine("Silmek istediğiniz ürünün sıra numarasını giriniz: ");
                                int election3 = Convert.ToInt16(Console.ReadLine());

                                var electronicDelete = electronicService.GetAll()[election3 - 1];

                                if (electronicService.Delete(electronicDelete.Id))
                                {
                                    Console.WriteLine("Silme işlemi gerçekleşti...");
                                }
                                else
                                {
                                    Console.WriteLine("Silme işlemi yapılamadı"); break;
                                }
                            }
                            else if (election2 == 3)
                            {
                                //count sadece göstürüp ekleme amacıyla yapıldı 
                                int count = 1;
                                Console.WriteLine("Kullanıcılar listeleniyor...");
                                foreach (var list in electronicService.GetAll())
                                {
                                    Console.WriteLine($"{count} İsimi: {list.Name}  , Telefon Numarası : {list.Model}");
                                    count++;
                                }

                                Console.WriteLine("Güncellemek istediğiniz ürünün  sıra numarasın seçiniz: ");
                                int election3 = Convert.ToInt32(Console.ReadLine());


                                var electronicUpdate = electronicService.GetAll()[election2 - 1];
                                if (electronicService.Update(electronicUpdate.Id, electronic))
                                {
                                    Console.WriteLine("Güncellemek istediğiniz ürünün hangi bilgisin güncelemek istersiniz: ");
                                    Console.WriteLine("Güncellemek istediğiniz alanı seçiniz:");
                                    Console.WriteLine("1 - Renk");
                                    Console.WriteLine("2 - Marka");
                                    Console.WriteLine("3 - Kategori");
                                    Console.WriteLine("4 - Fiyat");
                                    Console.WriteLine("5 - Ekran Kartı");
                                    Console.WriteLine("6 - Garanti Süresi");
                                    Console.WriteLine("7 - Boyut");
                                    int updateChoice = Convert.ToInt32(Console.ReadLine());

                                    switch (updateChoice)
                                    {
                                        case 1:
                                            Console.Write("Yeni Renk: ");
                                            electronic.Color = Console.ReadLine();
                                            break;
                                        case 2:
                                            Console.Write("Yeni Marka (sayı olarak): ");
                                            electronic.ElectronicBrand = (ElectronicBrand)Convert.ToInt32(Console.ReadLine());
                                            break;
                                        case 3:
                                            Console.Write("Yeni Kategori (sayı olarak): ");
                                            electronic.Category = (ProductCategory)Convert.ToInt32(Console.ReadLine());
                                            break;
                                        case 4:
                                            Console.Write("Yeni Fiyat: ");
                                            electronic.Price = Convert.ToDecimal(Console.ReadLine());
                                            break;
                                        case 5:
                                            Console.Write("Yeni Ekran Kartı Bilgisi: ");
                                            electronic.GraphicsCard = Console.ReadLine();
                                            break;
                                        case 6:
                                            Console.Write("Yeni Garanti Süresi: ");
                                            electronic.WarrantyPeriod = Convert.ToInt32(Console.ReadLine());
                                            break;
                                        case 7:
                                            Console.Write("Yeni Boyut: ");
                                            electronic.Dimension = Convert.ToDouble(Console.ReadLine());
                                            break;
                                        default:
                                            Console.WriteLine("Geçersiz seçim.");
                                            break;
                                    }


                                }
                            }
                            else if (election2 == 4)
                            {
                                Console.WriteLine("Listeleme işlemi yapılıyor...");
                                int count = 1;
                                foreach (var item in electronicService.GetAll())
                                {
                                    Console.WriteLine($"--- Ürün {count} ---");
                                    Console.WriteLine($"İsim: {item.Name}");
                                    Console.WriteLine($"Renk: {item.Color}");
                                    Console.WriteLine($"Boyut: {item.Dimension} cm");
                                    Console.WriteLine($"Garanti Süresi: {item.WarrantyPeriod} yıl");
                                    Console.WriteLine($"Fiyat: {item.Price} TL");
                                    Console.WriteLine($"Model: {item.Model}");
                                    Console.WriteLine($"Kategori: {item.Category}");
                                    Console.WriteLine($"Bellek (RAM): {item.Memory} GB");
                                    Console.WriteLine($"İşlemci: {item.Processor}");
                                    Console.WriteLine($"İşlemci Hızı: {item.ProcessorSpeed}");
                                    Console.WriteLine($"Ekran Kartı: {item.GraphicsCard}");
                                    Console.WriteLine($"Marka: {item.ElectronicBrand}");
                                    Console.WriteLine("-----------------------------\n");
                                    count++;
                                }
                            }

                        }
                        else if (election == 2)
                        {
                            Furniture furniture = new Furniture(DateTime.Now);
                            Console.WriteLine("Yapmak istediğiniz işlemi seçiniz: ");
                            Console.WriteLine("1-Ekleme\n" +
                           "2-Silme\n" +
                           "3-Güncelleme\n" +
                           "4-Listeleme\n" +
                           "5-Çıkış için 5 basınız");
                            int election2 = Convert.ToInt32(Console.ReadLine());

                            if (election2 == 1)
                            {
                                furnitureService.GetNextId(furniture);
                                Console.WriteLine("Eklemek istediğiniz ürünün ismini giriniz: ");
                                furniture.Name = Console.ReadLine();
                                Console.WriteLine("Eklemek istediğiniz ürün rengini giriniz: ");
                                furniture.Color = Console.ReadLine();
                                Console.WriteLine("Eklemek istediğiniz ürünün boyutunu giriniz (örneğin 30.48): ");
                                furniture.Dimension = double.Parse(Console.ReadLine());
                                Console.WriteLine("Eklemek istediğiniz ürünün garanti süresini yıl olarak giriniz: ");
                                furniture.WarrantyPeriod = int.Parse(Console.ReadLine());
                                Console.WriteLine("Eklemek istediğiniz ürünün fiyatını giriniz: ");
                                furniture.Price = decimal.Parse(Console.ReadLine());
                                Console.WriteLine("Eklemek istediğiniz ürünün modelini giriniz: ");
                                furniture.Model = Console.ReadLine();
                                Console.WriteLine("Eklemek istediğiniz ürünün kategorisini giriniz (Electronic, Furniture vb.): ");
                                string CategoryInput = Console.ReadLine();
                                if (Enum.TryParse<ProductCategory>(CategoryInput, out var category))
                                {
                                    furniture.Category = category;
                                }
                                else
                                {
                                    Console.WriteLine("Yanlış katagori seçilci");
                                    furniture.Category = ProductCategory.Electronic;
                                }
                                Console.WriteLine("Eklemek istediğiniz ürünün marka bilgisini giriniz  ");
                                string brandInput = Console.ReadLine();
                                if (Enum.TryParse<FurnitureBrand>(brandInput, out var brand))
                                {
                                    furniture.FurnitureBrand = brand;
                                }
                                else
                                {
                                    Console.WriteLine("Yanlış bilgi girdiniz..");
                                    furniture.FurnitureBrand = FurnitureBrand.Hepsiburada;
                                }
                                if (furnitureService.Add(furniture) == true)
                                {
                                    Console.WriteLine("Ekleme işlemi yapıldı...");
                                }
                                else
                                {
                                    Console.WriteLine("Ekleme işlemi yapılamadı");
                                }

                            }
                            else if (election2 == 2)
                            {
                                int count = 1;
                                foreach (var item in furnitureService.GetAll())
                                {
                                    Console.WriteLine($"Sıra Numarası: {count} , Ürün ismi {item.Name}, Modeli: {item.Model}");
                                }
                                Console.WriteLine("Silmek istediğiniz ürünün sıra numarasını giriniz: ");
                                int election3 = Convert.ToInt16(Console.ReadLine());

                                var furnitureDelete = furnitureService.GetAll()[election3 - 1];

                                if (furnitureService.Delete(furnitureDelete.Id))
                                {
                                    Console.WriteLine("Silme işlemi gerçekleşti...");
                                }
                                else
                                {
                                    Console.WriteLine("Silme işlemi yapılamadı"); break;
                                }
                            }
                            else if (election2 == 3)
                            {
                                //count sadece göstürüp ekleme amacıyla yapıldı 
                                int count = 1;
                                Console.WriteLine("Kullanıcılar listeleniyor...");
                                foreach (var list in furnitureService.GetAll())
                                {
                                    Console.WriteLine($"{count} İsimi: {list.Name}  , Model: {list.Model}");
                                    count++;
                                }

                                Console.WriteLine("Güncellemek istediğiniz ürünün  sıra numarasın seçiniz: ");
                                int election3 = Convert.ToInt32(Console.ReadLine());


                                var furnitureUpdate = furnitureService.GetAll()[election2 - 1];
                                if (furnitureService.Update(furnitureUpdate.Id, furniture))
                                {
                                    Console.WriteLine("Güncellemek istediğiniz ürünün hangi bilgisin güncelemek istersiniz: ");
                                    Console.WriteLine("Güncellemek istediğiniz alanı seçiniz:");
                                    Console.WriteLine("1 - Renk");
                                    Console.WriteLine("2 - Marka");
                                    Console.WriteLine("3 - Kategori");
                                    Console.WriteLine("4 - Fiyat");
                                    int updateChoice = Convert.ToInt32(Console.ReadLine());

                                    switch (updateChoice)
                                    {
                                        case 1:
                                            Console.Write("Yeni Renk: ");
                                            furniture.Color = Console.ReadLine();
                                            break;
                                        case 2:
                                            Console.Write("Yeni Marka (sayı olarak): ");
                                            furniture.FurnitureBrand = (FurnitureBrand)Convert.ToInt32(Console.ReadLine());
                                            break;
                                        case 3:
                                            Console.Write("Yeni Kategori (sayı olarak): ");
                                            furniture.Category = (ProductCategory)Convert.ToInt32(Console.ReadLine());
                                            break;
                                        case 4:
                                            Console.Write("Yeni Fiyat: ");
                                            furniture.Price = Convert.ToDecimal(Console.ReadLine());
                                            break;
                                        default:
                                            Console.WriteLine("Geçersiz seçim.");
                                            break;
                                    }


                                }

                            }
                        }

                    }
                    #endregion
                }
                #region YanlışGiriş
                else
                {
                    Console.WriteLine("Yanlış Girdiniz....");
                    UserBase.AttempCount++;
                    if (UserBase.AttempCount == 3)
                    {
                        Console.WriteLine("Giriş hakkınız bitti"); break;
                    }

                }
                #endregion


            }


        }
    }
}

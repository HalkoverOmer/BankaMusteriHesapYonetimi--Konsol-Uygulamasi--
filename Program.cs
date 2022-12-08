using System;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;

namespace BankaMusteriHesapYonetimi
{
    internal class Program
    {
        static int basamak = 0;
        static char[] ayrac = { ';', '-' };
        static string musteri;
        static int musteriIndex = 0;
        static string[] musteriInceleme = new string[0];


        static long BasamakHesapla(long sayi)
        {
            basamak = 0;
            do
            {
                sayi /= 10;
                basamak++;

            } while (sayi != 0);

            return basamak;
        }

        static void SonSatirSil()
        {
            Console.SetCursorPosition(9, Console.CursorTop - 1);
            Console.Write("                                                                                  ");
            Console.SetCursorPosition(9, Console.CursorTop - 0);
        }

        static void MusteriYazdir(string[] musteriler, int index)
        {
            string[] musteriParcalar = new string[0];

            for (int j = 0; j < musteriler.Length; j++)
            {
                musteri = musteriler[j];
                musteriParcalar = musteri.Split(ayrac);

                if (index == 4)
                {
                    Console.Write($"         [{musteriParcalar[3]}-{musteriParcalar[4]}] -- {musteriParcalar[0]} {musteriParcalar[1]}" +
                                  $" TC: {musteriParcalar[2]} B: {musteriParcalar[5]} TL\n");
                }

                else if (index == 2)
                {
                    Console.Write($"         [{musteriParcalar[3]}-{musteriParcalar[4]}] -- {musteriParcalar[0]} {musteriParcalar[1]}\n");
                }
            }
        }

        static bool MusteriVarMi(int hesapNo, int hesapNo2, string[] dizi)
        {
            
            int sayac, i;
            musteriIndex = 0;
            sayac = 0;
            i = 0;

            for (i = 0; i < dizi.Length; i++)
            {
                musteriIndex++;
                musteri = dizi[i];
                musteriInceleme = musteri.Split(ayrac);

                if (musteriInceleme.Length <= 4)
                {
                    break;
                }

                else
                {
                    if ((hesapNo == int.Parse(musteriInceleme[3])) && (hesapNo2 == int.Parse(musteriInceleme[4])))
                    {
                        sayac++;
                        break;
                    }

                }
            }

            if (sayac != 0) return true;
            
            else
            {
                return false;
            }
        }

        static string[] musteriBilgileri (int hesapNo, int hesapNo2, string[] dizi)
        {
            if (MusteriVarMi(hesapNo, hesapNo2, dizi) == true)
            {
                return musteriInceleme;
            }

            else
            {
                return musteriInceleme;
            }
        }

        static void Main(string[] args)
        {

            int cikis = 1, islem, i = 0;
            int kontrol = 0, kontrol2 = 0, kontrol3 = 0;
            long tc = 0;
            string[] musteriler = new string[0];
            string[] logKayit = new string[0];
            DateTime date = new DateTime();
            string tarih = date.ToShortDateString();
            int hesapNo, hesapNo2, bakiye, kayitIndex = 0;

            while (cikis == 1)
            {
                Console.Clear();
                Console.Write("         ---- Banka Hesap Yönetimi Uygulamasına Hoşgeldiniz ----\n\n" +
                              "         [1] - Yeni müşteri hesabı tanımlama \n" +
                              "         [2] - Havale işlemi\n" +
                              "         [3] - İşlem geçmişi\n" +
                              "         [4] - Müşterileri Listele\n" +
                              "         [0] - Çıkış\n\n" +
                              "         -> Yapmak istediğiniz işlemi girin : ");

                islem = int.Parse(Console.ReadLine());

                if (islem == 0)
                {
                    Environment.Exit(0);
                }

                else if (islem == 1)
                {
                    Array.Resize(ref musteriler, i + 1);
                    kontrol = 0;

                    Console.Clear();
                    Console.Write("         ---- Yani Müşteri Kayıt Paneli ----\n\n" +
                                  "         Müşteri Adı : ");
                    musteriler[i] += Console.ReadLine() + ";";

                    Console.Write("         Müşteri Soyadı : ");
                    musteriler[i] += Console.ReadLine() + ";";

                    Console.Write("         Müşteri TC : ");
                    tc = Convert.ToInt64(Console.ReadLine());
                    
                    while (kontrol == 0)
                    {
                        basamak = (int)BasamakHesapla(tc);


                        if (basamak != 11 || ((tc % 10) % 2 != 0))
                        {

                            SonSatirSil();
                            Console.Write("Hatalı TC tekrar giriniz: ");

                            tc = Convert.ToInt64(Console.ReadLine());

                            basamak = (int)BasamakHesapla(tc);

                            if (basamak == 11 && ((tc % 10) % 2 == 0))
                            {
                                
                                kontrol = 1;
                                musteriler[i] += Convert.ToString(tc) + ";";

                                SonSatirSil();
                                Console.Write($"Müşteri TC: {tc}\n");
                            }

                        }

                        else
                        {
                            
                            kontrol = 1;
                            musteriler[i] += Convert.ToString(tc) + ";";
                        }
                    }

                    Console.Write($"         Hesap Numarası :     -");
                    Console.SetCursorPosition(26, Console.CursorTop - 0);
                    hesapNo = int.Parse(Console.ReadLine());

                    do
                    {
                        if (BasamakHesapla(hesapNo) == 4)
                        {
                            SonSatirSil();
                            Console.Write($"Hesap Numarası : {hesapNo}-");

                            Console.SetCursorPosition(31, Console.CursorTop - 0);
                            hesapNo2 = int.Parse(Console.ReadLine());

                            do
                            {
                                if (BasamakHesapla(hesapNo2) == 6)
                                {
                                    if (MusteriVarMi(hesapNo, hesapNo2, musteriler) == false)
                                    {
                                        SonSatirSil();
                                        Console.Write($"Hesap Numarası : {hesapNo}-{hesapNo2}\n");
                                        musteriler[i] += Convert.ToString(hesapNo + "-" + hesapNo2);
                                        kontrol2 = 1;
                                        kontrol = 1;
                                    }

                                    else
                                    {
                                        Console.WriteLine("\n         ! Bu hesap numarasına sahip bir müşteri mevcut,\n" +
                                                          "         ! Müşteri Ekleme işlemi iptal edildi, Lütfen bekleyin...");
                                        Thread.Sleep(1000);
                                        musteriler[i] = null;
                                        kontrol2 = 1;
                                        kontrol = 0;
                                        Array.Resize(ref musteriler, musteriler.Length - 1);
                                    }
                                    
                                }

                                else
                                {
                                    SonSatirSil();
                                    Console.Write($"Hatalı Hesap Numarası (İkinci 6 haneyi tekrar giriniz) : {hesapNo}-");
                                    Console.SetCursorPosition(71, Console.CursorTop - 0);
                                    hesapNo2 = int.Parse(Console.ReadLine());
                                    kontrol2 = 0;
                                }
                            } while (kontrol2 != 1);

                        }

                        else
                        {
                            SonSatirSil();
                            Console.Write($"Hatalı Hesap Numarası (İlk 4 haneyi tekrar giriniz) :     -");
                            Console.SetCursorPosition(63, Console.CursorTop - 0);
                            hesapNo = int.Parse(Console.ReadLine());
                            kontrol2 = 0;
                        }
                    } while (kontrol2 != 1);

                    if (kontrol == 1)
                    {
                        Console.Write($"         Müşteri Bakiyesi : ");
                        Console.SetCursorPosition(28, Console.CursorTop - 0);
                        bakiye = int.Parse(Console.ReadLine());
                        kontrol2 = 0;

                        do
                        {
                            if (bakiye >= 0)
                            {
                                SonSatirSil();
                                Console.Write($"Müşteri Bakiyesi : {bakiye}");
                                musteriler[i] += Convert.ToString($";{bakiye}");
                                kontrol2 = 1;
                                i++;
                                Console.Write($"\n\n         -> Müşteri Ekleme işlemi tamamlandı...\n" +
                                              $"         -> Lütfen bekleyiniz");

                                Array.Resize(ref logKayit, i + 1);
                                logKayit[kayitIndex] = $"Tarih[{tarih}]-YeniMüşteriHesabıTanımlama-Müşteri[{musteriler[i-1]}]";
                                kayitIndex++;
                                Thread.Sleep(1000);

                            }

                            else
                            {
                                SonSatirSil();
                                Console.Write($"Hatalı bakiye miktarı, tekrar giriniz : ");
                                Console.SetCursorPosition(49, Console.CursorTop - 0);
                                bakiye = int.Parse(Console.ReadLine());
                                kontrol2 = 0;
                            }

                        } while (kontrol2 != 1);
                    }
                }

                else if (islem == 2)
                {
                    Console.Clear();
                    Console.Write("                   ---- Müşteri Görüntüleme Paneli ----\n" +
                                  "    (Hesap numarasını 4hane-6hane şeklinde, arasında tire ile yazınız)\n\n");
                    MusteriYazdir(musteriler, 2);

                    Console.Write($"\n         Gönderen Hesap No : ");
                    string hesapNoMetin = Console.ReadLine();
                    string[] hesapNoParcalari = hesapNoMetin.Split("-");

                    do
                    {
                        if (MusteriVarMi(int.Parse(hesapNoParcalari[0]), int.Parse(hesapNoParcalari[1]), musteriler) == true)
                        {
                            string gondericiHesapMetin = musteriler[musteriIndex - 1];
                            int gondericiIndex = musteriIndex - 1;
                            string[] gondericiHesap = gondericiHesapMetin.Split(ayrac);

                            SonSatirSil();
                            Console.Write($"Gönderen Hesap No : [{gondericiHesap[3]}-{gondericiHesap[4]}] Bakiye : {gondericiHesap[5]} TL" +
                                          $"\n         Alıcı    Hesap No : ");

                            hesapNoMetin = Console.ReadLine();
                            hesapNoParcalari = hesapNoMetin.Split("-");

                            if (MusteriVarMi(int.Parse(hesapNoParcalari[0]), int.Parse(hesapNoParcalari[1]), musteriler) == true)
                            {
                                string aliciHesapMetin = musteriler[musteriIndex - 1];
                                int aliciIndex = musteriIndex - 1;
                                string[] aliciHesap = aliciHesapMetin.Split(ayrac);

                                SonSatirSil();
                                Console.Write($"Alıcı    Hesap No : [{aliciHesap[3]}-{aliciHesap[4]}]");

                                Console.Write($"\n         Havale edilecek TL tutarı : ");
                                int havaleMiktari = int.Parse(Console.ReadLine());

                                if (havaleMiktari <= int.Parse(gondericiHesap[5]))
                                {
                                    int gondericiBakiye = int.Parse(gondericiHesap[5]);
                                    gondericiBakiye -= havaleMiktari;
                                    gondericiHesap[5] = Convert.ToString(gondericiBakiye);
                                    musteriler[gondericiIndex] = $"{gondericiHesap[0]};{gondericiHesap[1]};{gondericiHesap[2]};{gondericiHesap[3]}-{gondericiHesap[4]};{gondericiHesap[5]}";

                                    int aliciBakiye = int.Parse(aliciHesap[5]);
                                    aliciBakiye += havaleMiktari;
                                    aliciHesap[5] = Convert.ToString(aliciBakiye);
                                    musteriler[aliciIndex] = $"{aliciHesap[0]};{aliciHesap[1]};{aliciHesap[2]};{aliciHesap[3]}-{aliciHesap[4]};{aliciHesap[5]}";

                                    Console.WriteLine("\n         Havale işlemi başarıyla tamamlandı..." +
                                                      "\n         Ana menüye dönülüyor...");
                                    Thread.Sleep(1000);
                                    kontrol3 = 1;
                                    logKayit[kayitIndex] = $"Tarih[{tarih}]-Havaleİşlemi-Miktar[{havaleMiktari}TL]-Gönderen[{musteriler[gondericiIndex]}]-Alıcı[{musteriler[aliciIndex]}]";
                                }

                                else
                                {
                                    Console.WriteLine("\n         Havale işlemi başarısız, yeterli bakiyeniz yok..." +
                                                      "\n         Ana menüye dönülüyor...");
                                    Thread.Sleep(1000);
                                    kontrol3 = 1;
                                }
                            }

                            else
                            {
                                Console.Write($"\n         Girilen alıcı hesabı bulunamadı..." +
                                              $"\n         Ana menüye dönülüyor...");
                                Thread.Sleep(1000);
                                kontrol3 = 1;
                            }
                        }

                        else
                        {
                            Console.Write($"\n         Girilen gönderici hesabı bulunamadı..." +
                                              $"\n         Ana menüye dönülüyor...");
                            Thread.Sleep(1000);
                            kontrol3 = 1;
                        }

                    } while (kontrol3 == 0);

                }

                else if (islem == 3)
                {
                    Console.Clear();
                    Console.Write($"\n         --- Log Kayıtları --- \n\n");
                    foreach (string log in logKayit)
                    {
                        Console.WriteLine("         [-] " + log);
                    }
                    Console.Write("\n         Menüye dönmek için herhangi bir tuşa basın...");
                    Console.ReadKey();
                }

                else if (islem == 4)
                {
                    Console.Clear();
                    Console.Write("             ---- Müşteri Görüntüleme Paneli ----\n\n");
                    MusteriYazdir(musteriler, 4);
                    Console.Write("\n         Menüye dönmek için herhangi bir tuşa basın...");
                    Console.ReadKey();

                }
            }
        }
    }
}
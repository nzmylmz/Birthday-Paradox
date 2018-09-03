using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NazimYilmaz
{
    class Program
    {
        static Random rnd = new Random();
        static void Main(string[] args)
        {
            int[][] birthDay = new int[12][];
            int[,] cakismaSayilari = new int[3, 10];
            Console.WriteLine("\t\tTüm Aylar için Doğum Günü Paradoksu\n\t\t   10 Bağımsız Deney Olasılığı\n\n \tDeney\t\t|\t100\t500\t1000\n \tSayısı    \t|\tKişi\tKişi\tKişi");
            Console.WriteLine("------------------------------------------------------------");

            for (int i = 0; i < 12; ++i)
            {//28,30,31 çeken aylara göre jagged array'in düzenlenmesi
                if ((i % 2 == 0) || (i == 7) || (i == 11))
                    birthDay[i] = new int[30];
                else if (i == 1)
                    birthDay[i] = new int[28];
                else
                    birthDay[i] = new int[31];
            }
            sonuc(0, birthDay, cakismaSayilari);
            yazdir(cakismaSayilari);
            Console.WriteLine("\n------------------------------------------------------------");
            Console.WriteLine("\n\n------------------------------------------------------------");
            Console.WriteLine("\n\n\t\tYaz Aylar için Doğum Günü Paradoksu\n\t\t   10 Bağımsız Deney Olasılığı\n\n \tDeney\t\t|\t100\t500\t1000\n \tSayısı    \t|\tKişi\tKişi\tKişi");
            Console.WriteLine("------------------------------------------------------------");
            sonuc(1, birthDay, cakismaSayilari);
            yazdir(cakismaSayilari);
            Console.Write("\n\n Çıkmak için herhangi bir tuşa basınız...");
            Console.ReadKey();
        }
        static void sonuc(int sart, int[][] birthDay, int[,] cakismaSayilari)
        {
            int kisiSayisi = 0;
            for (int grup = 0; grup <= 2; grup++) //kişi sayısısını belirlemek için bir döngü ve if-else yapısı kullanıldı
            {
                for (int i = 0; i < 10; i++)//deneyi 10 kere gerçekleştirmek için
                {
                    if (grup == 0) //grubun sayısını belirliyoruz
                        kisiSayisi = 100;
                    else if (grup == 1)
                        kisiSayisi = 500;
                    else
                        kisiSayisi = 1000;
                    deney(birthDay, kisiSayisi, sart);//kişi sayılarına ve şarta göre deneyi gerçekleştiriyoruz
                    cakismaSayilari[grup, i] = cakismaSayisi(birthDay);//grup çakışmalarını matrise yüklüyoruz
                    diziyiSıfırla(birthDay);//bağımsız deney sonuçları elde etmek için bir method kullanıldı
                }
            }
        }//Sonuçların elde edilmesi
        static void yazdir(int[,] cakismaSayilari)
        {
            for (int i = 0; i < 10; i++)
            {
                Console.Write("\tDeney {0,0} \t|", i + 1);

                for (int a = 0; a <= 2; a++)
                {

                    Console.Write(" \t{0,1}", cakismaSayilari[a, i]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("-------------------------------------------------------------");
            Console.Write("Ortalama Çakışma\t|\n   Olasılığı\t\t|");
            for (int i = 0; i < 3; i++)
            {
                double ortalama = 0.0;
                for (int a = 0; a < 10; a++)
                    ortalama = ortalama + cakismaSayilari[i, a];
                Console.Write("\t{0,1}", ortalama / 10);
            }
            Console.Write("\n\n");
        }//Deney sonuçlarının ve olasılığın ekrana yazdırılması
        static void deney(int[][] birthDay, int kisiSayisi, int sart)//Rastgele günler ve aylar oluşturulur
        {
            int rndGun = 0, rndAy = 0;
            for (int x = 0; x < kisiSayisi; x++)//deneyin gerçekleştirileceği kişi sayısı n=100,500,1000
            {
                if (sart == 0)
                    rndAy = rnd.Next(12); //Rastgele ay üretilmesi
                else
                    rndAy = rnd.Next(5, 8);
                if ((rndAy % 2 == 0) || (rndAy == 7) || (rndAy == 11)) // 28,30,31 çeken ayların tespiti
                    rndGun = rnd.Next(30); //Rastgele üretilmiş aya göre rastgele gün üretilmesi
                else if (rndAy == 1)
                    rndGun = rnd.Next(28);
                else
                    rndGun = rnd.Next(31);

                birthDay[rndAy][rndGun] += 1; //aynı doğum günü sayılarının arttırılması
            }
        }
        static int cakismaSayisi(int[][] birthDay)
        {
            int cakisma = 0;
            for (int ay = 0; ay < birthDay.GetLength(0); ay++)
            {//Çakışmaların tespiti için dizinin her elemanını döndüren iç içe iki for döngüsü oluşturuldu
                for (int gun = 0; gun < birthDay[ay].GetLength(0); gun++)
                {
                    if (birthDay[ay][gun] > 1)
                    {
                        cakisma = cakisma + birthDay[ay][gun] - 1;
                    }
                }
            }
            return cakisma;
        }//Aynı günlerin çakışma sayısı  tespit edilir
        static void diziyiSıfırla(int[][] birthDay)//Deney değerlerinin bağımsız olması için dizi elemanları sıfırlanıyor
        {
            for (int ay = 0; ay < birthDay.GetLength(0); ay++)
            {
                for (int gun = 0; gun < birthDay[ay].GetLength(0); gun++)
                {
                    birthDay[ay][gun] = 0;
                }
            }
        }

    }
}
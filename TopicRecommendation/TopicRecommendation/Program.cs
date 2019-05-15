using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopicRecommendation
{
    class Program
    {
        public static double MIN_SUPPORT = 0.1 ;
        public static double MIN_CONFIDENCE = 0.5;

        static void Main(string[] args)
        {
            int[,] array2D = new int[,] {
                {5,11,12,13,14,15,-1,-1},
                {2,5,7,8,9,11,15,17},
                {2,3,4,5,6,8,14,15},
                {1,2,4,5,6,8,12,14},
                {1,2,3,4,6,10,13,14},
                {2,3,6,7,8,11,12,14},
                {2,3,4,8,15,17,-1,-1},
                {2,4,6,8,9,13,14,15},
                {1,2,3,4,6,9,14,-1},
                {2,3,4,5,14,-1,-1,-1},
                {3,4,6,9,11,12,14,17},
                {1,2,3,9,14,-1,-1,-1},
                {2,3,4,5,11,12,13,14},
                {2,3,7,8,11,13,14,16},
                {1,2,3,4,5,6,11,15},
                {1,2,3,4,6,11,15,-1},
                {1,3,4,6,8,9,11,15},
                {1,3,5,8,9,11,14,17},
                {2,3,4,8,9,11,13,14},
                {1,2,3,4,13,14,15,17},
                {1,4,7,8,9,11,10,17},
                {1,2,3,5,6,7,14,17},
                {1,4,5,6,8,10,15,17}
            };
            Console.WriteLine("1   Sport 	 			Spor  \n2   Love / marriage / Relationships     İnsanlar Arası İlişkiler\n3   Education / School                  Eğitim / Okul\n4   Computer Games                      Bilgisayar Oyunları\n5   Entertainment / Events              Eğlence / Düğün / Davet / Parti\n6   Arts / Hobbies                      Sanat / Hobi\n7   Politics                            Politika\n8   Work / Job                          İş / Meslek\n9   Travel                              Seyahat\n10  Religion                            İnanç / Din\n11  Career                              Mesleki Kariyer\n12  Gastronomi / Food                   Yemek\n13  News                                Güncel Haberler\n14  Daily Talk                          Günlük Sohbet\n15  Family                              Aile\n16  Celebrity Gossip                    Ünlü Dedikodusu\n17  Economy / Financial                 Ekonomi");
            Console.WriteLine("ilk konuyu girin (1-17)");
            int ürün1 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("ikinci konuyu girin (1-17)");
            int ürün2 = Convert.ToInt32(Console.ReadLine());

            if (ürün2 < ürün1)
            {
                int ürünSwap = ürün1;
                ürün1 = ürün2;
                ürün2 = ürünSwap;
            }

            Console.WriteLine();
            Console.WriteLine("Seçilen iki konu için;");
            Console.WriteLine();
            Console.WriteLine("Support => " + FindSupport(ürün1, ürün2, array2D));
            Console.WriteLine("Confidence => "+ FindConfidence(ürün1, ürün2, array2D));
            Console.WriteLine();


            //List<int> listee = ikisiDeVarMi(ürün1, ürün2, array2D);
            //Console.WriteLine(listee[1]);
            Recommend(ürün1, ürün2, array2D);

            Console.ReadKey();

        }

        static public double FindSupport(int value1, int value2, int[,] array)
        {
            int valBoth = 0;

            for (int y = 0; y < 23; y++)
            {
                bool flag = false;

                for (int i = 0; i < 8; i++)
                {
                    if (array[y, i] == value1)
                    {
                        flag = true;
                    }
                    if (array[y, i] == value2 && flag == true)
                    {
                        valBoth++;
                    }
                }
            }
            double support = Convert.ToDouble(valBoth)  / Convert.ToDouble(23);
            //Console.WriteLine("Support = " + valBoth + "/" + "23       => "+ support);
            return support;
        }

        static public double FindConfidence(int value1, int value2, int[,] array)
        {
            
            int val1 = 0;
            int valBoth = 0;

            for(int y = 0; y < 23; y++)
            {
                bool flag = false;

                for (int i = 0; i < 8; i++)
                {
                    if (array[y, i] == value1)
                    {
                        flag = true;
                        val1++;
                    }
                    if (array[y, i] == value2 && flag==true)
                    {
                        valBoth++;
                    }
                }
            }
            double confidence = Convert.ToDouble(valBoth) / Convert.ToDouble(val1);
            //Console.WriteLine("Confidence = " + valBoth + "/" + val1 + "      => " + confidence);
            return confidence;
        }

        static public void Recommend(int value1, int value2, int[,] array)
        {
            int[] ucunculerListesi = new int[] { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 };
            List<int> listee = ikisiDeVarMi(value1, value2, array);

            for(int i = 0; i < listee.Count; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    if (array[listee[i],j]!=value1 && array[listee[i], j] != value2 && array[listee[i], j] !=-1)
                    {
                        ucunculerListesi[(array[listee[i], j]-1)]++;
                    }
                }
            }
            //Console.WriteLine("konuşulan konuların sayıları = ");
            int enBuyugunIndexi = -1;
            int enBuyugunDatasi = 0;
            //int ikincininIndexi = -1;
            //int ikincininDatasi = 0;

            for(int i = 0; i < 17; i++)
            {
                if(ucunculerListesi[i]>0 && ucunculerListesi[i] > enBuyugunDatasi)
                {
                    enBuyugunIndexi = i;
                    enBuyugunDatasi = ucunculerListesi[i];
                }

                //Console.WriteLine(ucunculerListesi[i]);
                
            }
            //Console.WriteLine("En buyuk indexi = " + enBuyugunIndexi + " En buyugun datası = " + enBuyugunDatasi);
            
            double support = Convert.ToDouble(enBuyugunDatasi) / Convert.ToDouble(23);
            double confidence = Convert.ToDouble(enBuyugunDatasi) / Convert.ToDouble(listee.Count);
            if(support>= MIN_SUPPORT)
            {
                if (confidence>=MIN_CONFIDENCE)
                {
                    Console.WriteLine("Önerilecek konunun indexi =======>  " + ++enBuyugunIndexi);
                    Console.WriteLine();
                    Console.WriteLine("Önerilecek konu için support değeri = " + enBuyugunDatasi + " / " + 23 + " => " + support);
                    Console.WriteLine("Önerilecek konu için confidence değeri = " + enBuyugunDatasi + " / " + listee.Count + " => " + confidence);

                }
                else
                {
                    Console.WriteLine("Seçilen konular için minimum confidence değerinden büyük confidence değerine sahip bir association rule bulunamamıştır.");
                }
            }
            else
            {
                Console.WriteLine("Seçilen konular için minimum support değerinden büyük support değerine sahip bir association rule bulunamamıştır.");
            }


        }

        static public void Recommend(int value1, int value2, int value3, int[,] array)
        {

        }

        static public List<int> ikisiDeVarMi(int value1, int value2, int[,] array)
        {
            List<int> ylerListesi = new List<int>();
            for (int y = 0; y < 23; y++)
            {
                bool flag = false;

                for (int i = 0; i < 8; i++)
                {
                    if (array[y, i] == value1)
                    {
                        flag = true;
                    }
                    if (array[y, i] == value2 && flag == true)
                    {
                        ylerListesi.Add(y);
                        //Console.WriteLine(y);
                    }
                }
            }
            //Console.WriteLine("List teki ilk eleman = "+ ylerListesi[1]);
            return ylerListesi;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP.DataLayer.Entites
{
    public sealed class TypeMaterial
    {
        public const string SPIRITUS = "cпирт";
        public const string BOR_ALMAZN = "бор алмазный";
        public const string TEMP_PLOMB_MATERIAL = "временный пломб. материал";
        public const string TOOL_KANAL = "инструменты  для прохождения каналов";
        public const string LUMBRIKANT = "люмбриканты";
        public const string MATERIAL_PLOMB = "материал пломбировочный";
        public const string MATERIAL_PLOMB_INSIDEKANAL = "материал пломбировочный внутриканальный";
        public const string MATRIZA_METALL = "матрица металлическая";
        public const string MEZHZUBNIE_KLINIYA = "межзубные клинья";
        public const string PREPARAT_FLUOKAL = "препарат фторсодержащий";
        public const string GOLOVKA_POLIROVOCHNAYA = "головка полировочная";
        public const string RASTVOR_ANTISEPTIK = "раствор антисептика";
        public const string RASTVOR_ANTISEPTIKSPECIFIC = "раствор антисептика специальный";
        public const string RETRAKCION_NIT = "ретракционная нить";
        public const string CEMENT_STEKLOIONOMERNYI = "цемент стеклоиномерный";
        public const string SHTRIPSA = "штрипса";
        public const string DISC_POLIROVOCHNYI = "диск полировочный";
        public const string VALIKI = "валики";       
        public const string ANESTETIK = "анестетик";      
        public const string SHTIFT_PAPER = "штифт бумажный";
        public const string SCHETKI_POLIROVOCHNYE = "щетки полировочные";
        public const string PASTA_POLIROVOCHNAYA = "паста полировочная";
        public const string PULPOEXTRACTOR = "пульпоэкстрактор";
        public const string KANALONAPOLNITEL = "каналонаполнитель";
        public const string SHTIFT_GUTAPERCHIV = "гуттаперчевый штифт";
        public const string SPREDER = "спредер";

        static string[] typeMaterials = new string[]
   {
         "cпирт",
         "бор алмазный",
         "временный пломб. материал",
         "инструменты  для прохождения каналов",
         "люмбриканты",
         "материал пломбировочный",
         "материал пломбировочный внутриканальный",
         "матрица металлическая",
         "межзубные клинья",
         "препарат фторсодержащий",
         "головка полировочная",
         "раствор антисептика",
         "ретракционная нить",
         "цемент стеклоиономерный",
         "штрипса",
         "диск полировочный",
         "валики",         
         "анестетик",           
         "штифт бумажный",
         "щетки полировочные",
         "паста полировочная",
          "пульпоэкстрактор",
          "каналонаполнитель",
         "гуттаперчевый штифт",
         "спредер"
   };
        public static string[] TypeMaterials
        {
            get
            {
                return typeMaterials;
            }

        }
    }
}

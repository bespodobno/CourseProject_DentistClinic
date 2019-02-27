using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using CP.DataLayer.Entites;

namespace CP.DataLayer.EFContext
{
    class CourseProjectInitializer : CreateDatabaseIfNotExists<CourseProjectContext>
    {
        protected override void Seed(CourseProjectContext context)
        {
            context.Doctors.AddRange(new Doctor[]
            {
                new Doctor{ DoctorName="Соломевич А.С.", Category=Category.высшая, Speciality="стоматолог-терапевт", Room=15},
                new Doctor{ DoctorName="Даревский В.И.", Category=Category.высшая, Speciality="стоматолог-терапевт", Room=16}
            });
            context.Patients.AddRange(new Patient[]
            {
                new Patient{ Name="Тронь Н.А.", Sex=Sex.жен, DateOfBirth=new DateTime(1986,07,04),Address="Есенина 139-8",
                    Telephone ="225-22-85",Contract="Договор б/н от 18.01.2018"
                },
                new Patient{ Name="Попок С.А.", Sex=Sex.муж, DateOfBirth=new DateTime(1956,04,22),Address="Октябрьская 10-1",
                    Telephone ="444-24-81",Contract="Договор б/н от 05.01.2018"
                },
                new Patient{ Name="Иванов С.С.", Sex=Sex.муж, DateOfBirth=new DateTime(1976,08,22),Address="Столетова 28-2",
                    Telephone ="320-21-01",Contract="Договор б/н от 05.08.2018"
                },
                new Patient{ Name="Петрова А.Л.", Sex=Sex.жен, DateOfBirth=new DateTime(1977,12,02),Address="Первомайская 11-256",
                    Telephone ="253-21-80",Contract="Договор б/н от 01.08.2018"
                }
            });

            context.SaveChanges();
            context.Appointments.AddRange(new Appointment[]
           {
                new Appointment{  Date=new DateTime(2018,08,01), Time = new TimeSpan(14,00,00), DoctorId=1,PatientId=1},
                new Appointment{  Date=new DateTime(2018,08,02), Time = new TimeSpan(10,00,00), DoctorId=1,PatientId=3},
                 new Appointment{  Date=new DateTime(2018,08,03), Time = new TimeSpan(13,00,00), DoctorId=2,PatientId=2},
                 new Appointment{  Date=new DateTime(2018,08,04), Time = new TimeSpan(16,00,00), DoctorId=2,PatientId=2},
                 new Appointment{  Date=new DateTime(2018,08,05), Time = new TimeSpan(10,00,00), DoctorId=1,PatientId=4},
                 new Appointment{  Date=new DateTime(2018,08,05), Time = new TimeSpan(12,00,00), DoctorId=2,PatientId=4},
                 new Appointment{  Date=new DateTime(2018,08,05), Time = new TimeSpan(14,00,00), DoctorId=1,PatientId=2}
           });
            context.SaveChanges();
            context.PriceLists.AddRange(new PriceList[]
            {
                new PriceList{ ServiceCode="1.01.",ServiceName="Стоматологическое обследование при первичном обращении",Price= 10590},
                new PriceList{ ServiceCode="1.02.",ServiceName="Динамическое наблюдение в процессе лечения",Price= 5300},
                new PriceList{ ServiceCode="1.04.",ServiceName="Анализ дентальных снимков",Price= 2650},
                new PriceList{ ServiceCode="1.06.",ServiceName="Анализ результатов дополнительных методов исследования",Price= 2650},
                new PriceList{ ServiceCode="1.07.",ServiceName="Обучение пациента чистке зубов",Price= 5300},
                new PriceList{ ServiceCode="1.08.",ServiceName="Покрытие одного зуба фторсодержащим препаратом",Price= 2120,
                    NormConsumptionRates =new List<NormConsumptionRate>
                {
                        new NormConsumptionRate{ TypeMaterial=TypeMaterial.PREPARAT_FLUOKAL, Norm=0.1,AutoSelection=true,PriceMaterial=2003.469} }
                },
                new PriceList{ ServiceCode="1.14.",ServiceName="Удаление зубного налета с одного зуба, очистка зуба",Price= 1590,
                NormConsumptionRates = new List<NormConsumptionRate>
                {
                    new NormConsumptionRate{TypeMaterial=TypeMaterial.SCHETKI_POLIROVOCHNYE, Norm=0.1,AutoSelection=true,PriceMaterial=1635.4},
                    new NormConsumptionRate{TypeMaterial=TypeMaterial.PASTA_POLIROVOCHNAYA, Norm=0.1,AutoSelection=false,PriceMaterial=915.68, NameMaterial="Препараты для гигиены полости рта Detartrine"},
                      new NormConsumptionRate{TypeMaterial=TypeMaterial.GOLOVKA_POLIROVOCHNAYA, Norm=0.2,AutoSelection=true,PriceMaterial=6431.1},
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.RASTVOR_ANTISEPTIK, Norm=1,AutoSelection=true,PriceMaterial=9.72}
                }
                },
                new PriceList{ ServiceCode="1.17.",ServiceName="Ультразвуковое удаление зубных отложений с одного зуба",Price= 2120,
                NormConsumptionRates = new List<NormConsumptionRate>
                {
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.RASTVOR_ANTISEPTIK, Norm=1,AutoSelection=true,PriceMaterial=9.72}
                }
                },
                new PriceList{ ServiceCode="1.21.",ServiceName="Полирование одного зуба после снятия зубных отложений",Price= 2120,
                 NormConsumptionRates = new List<NormConsumptionRate>
                {
                    new NormConsumptionRate{TypeMaterial=TypeMaterial.SCHETKI_POLIROVOCHNYE, Norm=0.1,AutoSelection=true,PriceMaterial=1635.4},
                    new NormConsumptionRate{TypeMaterial=TypeMaterial.PASTA_POLIROVOCHNAYA, Norm=0.1,AutoSelection=false,PriceMaterial=915.68, NameMaterial="Препараты для гигиены полости рта Detartrine"},
                      new NormConsumptionRate{TypeMaterial=TypeMaterial.GOLOVKA_POLIROVOCHNAYA, Norm=0.25,AutoSelection=true,PriceMaterial=6431.1}
                }},
                new PriceList{ ServiceCode="1.22.",ServiceName="Временная пломба",Price= 2650,
                NormConsumptionRates = new List<NormConsumptionRate>
                {
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.TEMP_PLOMB_MATERIAL, Norm=0.5,AutoSelection=false,PriceMaterial=114.79,NameMaterial="Дентин-паста"},
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.TEMP_PLOMB_MATERIAL, Norm=0.5,AutoSelection=false,PriceMaterial=1666.6,NameMaterial="Септопак"},
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.TEMP_PLOMB_MATERIAL, Norm=0.5,AutoSelection=false,PriceMaterial=43680,NameMaterial="Apex Cal"}
                }
                },

                new PriceList{ ServiceCode="1.24.",ServiceName="Удаление пломбы",Price= 3180,
                 NormConsumptionRates = new List<NormConsumptionRate>
                {
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.BOR_ALMAZN, Norm=0.25,AutoSelection=true,PriceMaterial=6753.5},
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.RASTVOR_ANTISEPTIK, Norm=1,AutoSelection=true,PriceMaterial=9.72}
                }
                },
                new PriceList{ ServiceCode="1.28.",ServiceName="Орошение полости рта антисептиком",Price= 1590,
                NormConsumptionRates = new List<NormConsumptionRate>
                {
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.RASTVOR_ANTISEPTIKSPECIFIC, Norm=50,AutoSelection=false,PriceMaterial=9.72,NameMaterial="перекись водорода"},
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.RASTVOR_ANTISEPTIKSPECIFIC, Norm=50,AutoSelection=false,PriceMaterial=12,NameMaterial="Паркан"},
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.RASTVOR_ANTISEPTIKSPECIFIC, Norm=50,AutoSelection=false,PriceMaterial=15.86,NameMaterial="Хлоргексидин"}

                }},
                new PriceList{ ServiceCode="1.47.",ServiceName="Проводниковая анестезия",Price= 7940,
                 NormConsumptionRates = new List<NormConsumptionRate>
                {
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.RASTVOR_ANTISEPTIK, Norm=1,AutoSelection=true,PriceMaterial=9.72},
                      new NormConsumptionRate{TypeMaterial=TypeMaterial.ANESTETIK, Norm=1 ,AutoSelection=false,PriceMaterial=1976,NameMaterial="Ультракаин"},
                      new NormConsumptionRate{TypeMaterial=TypeMaterial.ANESTETIK, Norm=1 ,AutoSelection=false,PriceMaterial=976.3,NameMaterial="Убистезин"},
                      new NormConsumptionRate{TypeMaterial=TypeMaterial.ANESTETIK, Norm=1 ,AutoSelection=false,PriceMaterial=1177.8,NameMaterial="Скандонест"}
                }},
                new PriceList{ ServiceCode="2.03.02.",ServiceName="Препарирование кариозной полости при разрушении до 1/3 коронки зуба",Price= 6350,
                 NormConsumptionRates = new List<NormConsumptionRate>
                {
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.BOR_ALMAZN, Norm=0.25,AutoSelection=true,PriceMaterial=6753.5},
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.RASTVOR_ANTISEPTIK, Norm=1,AutoSelection=true,PriceMaterial=9.72}
                }},
                new PriceList{ ServiceCode="2.03.03.",ServiceName="Препарирование кариозной полости  при разрушении до 1/2 коронки зуба",Price= 9530,
                 NormConsumptionRates = new List<NormConsumptionRate>
                {
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.BOR_ALMAZN, Norm=0.3333,AutoSelection=true,PriceMaterial=6753.5},
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.RASTVOR_ANTISEPTIK, Norm=1,AutoSelection=true,PriceMaterial=9.72}
                }},
                new PriceList{ ServiceCode="2.05.",ServiceName="Изготовление изолирующей прокладки  из цемента",Price= 4240,
                 NormConsumptionRates = new List<NormConsumptionRate>
                {
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.CEMENT_STEKLOIONOMERNYI, Norm=0.3,AutoSelection=false,PriceMaterial=10920,NameMaterial="Ketac Molar Easymix"}
                 }
                },

                new PriceList{ ServiceCode="2.07.",ServiceName="Изготовление изолирующей прокладки адгезивной системой",Price= 2650},
                new PriceList{ ServiceCode="2.09.02.",ServiceName="Препарирование кариозной полости и полости многокорневого зуба",Price=7940,
                NormConsumptionRates = new List<NormConsumptionRate>
                {
                         new NormConsumptionRate{TypeMaterial=TypeMaterial.BOR_ALMAZN, Norm=0.5,AutoSelection=true,PriceMaterial=6753.5},
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.RASTVOR_ANTISEPTIK, Norm=1,AutoSelection=true,PriceMaterial=9.72} }
                },
                new PriceList{ ServiceCode="2.09.04.",ServiceName="Инструментальная  обработка  одного  хорошо  проходимого канала",Price= 5300,
                  NormConsumptionRates = new List<NormConsumptionRate>
                {
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.TOOL_KANAL, Norm=0.8,AutoSelection=false, PriceMaterial=7300, NameMaterial="файлы"},
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.RASTVOR_ANTISEPTIK, Norm=1,AutoSelection=true,PriceMaterial=9.72} }
                },
                new PriceList{ ServiceCode="2.09.05.",ServiceName="Инструментальная  обработка  одного плохо проходимого канала",Price= 11650,
                 NormConsumptionRates = new List<NormConsumptionRate>
                {
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.TOOL_KANAL, Norm=1.2,AutoSelection=false, PriceMaterial=7300, NameMaterial="файлы"},
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.RASTVOR_ANTISEPTIK, Norm=1,AutoSelection=true,PriceMaterial=9.72} }
                },
                new PriceList{ ServiceCode="2.09.10.",ServiceName="Экстирпация пульпы из одного канала",Price= 2120,
                 NormConsumptionRates = new List<NormConsumptionRate>
                {
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.PULPOEXTRACTOR, Norm=1,AutoSelection=true,PriceMaterial=325},
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.RASTVOR_ANTISEPTIK, Norm=1,AutoSelection=true,PriceMaterial=9.72} }
                },
                new PriceList{ ServiceCode="2.09.11.",ServiceName="Распломбирование  и инструментальная обработка одного канала зуба, ранее запломбированного пастой",Price= 10590,
                 NormConsumptionRates = new List<NormConsumptionRate>
                {
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.TOOL_KANAL, Norm=1,AutoSelection=false, PriceMaterial=7300, NameMaterial="файлы"},
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.RASTVOR_ANTISEPTIK, Norm=2,AutoSelection=true,PriceMaterial=9.72} }
                },
                new PriceList{ ServiceCode="2.09.12.",ServiceName="Распломбирование и инструментальная обработка одного канала зуба, ранее запломбированного цементом, резорцинформалином",Price= 15890,
                 NormConsumptionRates = new List<NormConsumptionRate>
                {
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.TOOL_KANAL, Norm=1.4,AutoSelection=false, PriceMaterial=7300, NameMaterial="файлы"},
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.RASTVOR_ANTISEPTIK, Norm=2,AutoSelection=true,PriceMaterial=9.72} }
                },
                new PriceList{ ServiceCode="2.09.15.",ServiceName="Антисептическая  обработка одного канала",Price= 2650,
                 NormConsumptionRates = new List<NormConsumptionRate>
                {
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.RASTVOR_ANTISEPTIK, Norm=1,AutoSelection=true,PriceMaterial=9.72},
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.SHTIFT_PAPER, Norm=4,AutoSelection=true,PriceMaterial=45.5} }
                },
                new PriceList{ ServiceCode="2.09.16.",ServiceName="Медикаментозная обработка одного канала с помощью специальных средств для прохождения и расширения корневого канала (люмбрикантов)",Price= 4240,
                 NormConsumptionRates = new List<NormConsumptionRate>
                {
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.LUMBRIKANT, Norm=0.3,AutoSelection=false,PriceMaterial=4253.17,NameMaterial="Эндогель"},
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.SHTIFT_PAPER, Norm=4,AutoSelection=true,PriceMaterial=45.5} }
                },
                new PriceList{ ServiceCode="2.09.17.",ServiceName="Лечебная внутриканальная повязка одного канала",Price= 3180,
                 NormConsumptionRates = new List<NormConsumptionRate>
                {
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.TEMP_PLOMB_MATERIAL, Norm=0.3,AutoSelection=false,PriceMaterial=114.79,NameMaterial="Дентин-паста"},
                        new NormConsumptionRate{TypeMaterial=TypeMaterial.TEMP_PLOMB_MATERIAL, Norm=0.3,AutoSelection=false,PriceMaterial=1666.6,NameMaterial="Септопак"},
                         new NormConsumptionRate{TypeMaterial=TypeMaterial.TEMP_PLOMB_MATERIAL, Norm=0.3,AutoSelection=false,PriceMaterial=43680,NameMaterial="Apex Cal"},

                       new NormConsumptionRate{TypeMaterial=TypeMaterial.RASTVOR_ANTISEPTIK, Norm=0.5,AutoSelection=true,PriceMaterial=9.72},
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.KANALONAPOLNITEL, Norm=0.1,AutoSelection=true,PriceMaterial=652.6},
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.SHTIFT_PAPER, Norm=5,AutoSelection=true,PriceMaterial=45.5} }
                },
                new PriceList{ ServiceCode="2.09.21.",ServiceName="Пломбирование одного канала гуттаперчевыми штифтами на силлере методом конденсации",Price= 9530,
                 NormConsumptionRates = new List<NormConsumptionRate>
                {
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.MATERIAL_PLOMB_INSIDEKANAL, Norm=0.15,AutoSelection=false,PriceMaterial=9243,NameMaterial="Эндометазон"},
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.MATERIAL_PLOMB_INSIDEKANAL, Norm=0.15,AutoSelection=false,PriceMaterial=7121.4,NameMaterial="Акросил"},
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.MATERIAL_PLOMB_INSIDEKANAL, Norm=0.15,AutoSelection=false,PriceMaterial=5453.5,NameMaterial="Адсил"},
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.SPIRITUS, Norm=0.5,AutoSelection=true,PriceMaterial=31.79},
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.KANALONAPOLNITEL, Norm=0.1,AutoSelection=true,PriceMaterial=652.6},
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.SHTIFT_PAPER, Norm=5,AutoSelection=true,PriceMaterial=45.5},
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.SHTIFT_GUTAPERCHIV, Norm=5,AutoSelection=true,PriceMaterial=70.2},
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.SPREDER, Norm=0.16,AutoSelection=true,PriceMaterial=8076.9}
                 }
                },
                new PriceList{ ServiceCode="2.09.23.",ServiceName="Измерение длины канала при помощи аппарата Апекслокатор",Price= 4240,
                NormConsumptionRates = new List<NormConsumptionRate>
                {
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.RASTVOR_ANTISEPTIK, Norm=1,AutoSelection=true,PriceMaterial=9.72}
                }
                },
                new PriceList{ ServiceCode="2.11.03.",ServiceName="Реставрация коронковой части одного зуба  фотополимерным композиционным материалом при лечении кариозной полости I, II, III, IV, V классов по Блэку с локализацией полостей независимо от поверхности кариозной полости при разрушении до 1/2 коронки зуба",Price= 13240,
                NormConsumptionRates = new List<NormConsumptionRate>
                {
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.MATERIAL_PLOMB, Norm=0.4,AutoSelection=false,PriceMaterial=28021.5,NameMaterial="Filtek"},
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.MATERIAL_PLOMB, Norm=0.4,AutoSelection=false,PriceMaterial=28977,NameMaterial="REVOLUTION KIT А2,В3,С3"},
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.MATERIAL_PLOMB, Norm=0.4,AutoSelection=false,PriceMaterial=10920,NameMaterial="Ketac Molar Easymix"}
                }
                },
                new PriceList{ ServiceCode="2.11.04.",ServiceName="Реставрация коронковой части одного зуба  фотополимерным композиционным материалом при лечении кариозной полости I, II, III, IV, V классов по Блэку с локализацией полостей независимо от поверхности кариозной полости при разрушении более 1/2 коронки зуба	",Price= 15890,
                 NormConsumptionRates = new List<NormConsumptionRate>
                {
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.MATERIAL_PLOMB, Norm=0.5,AutoSelection=false,PriceMaterial=28021.5,NameMaterial="Filtek"},
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.MATERIAL_PLOMB, Norm=0.5,AutoSelection=false,PriceMaterial=28977,NameMaterial="REVOLUTION KIT А2,В3,С3"},
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.MATERIAL_PLOMB, Norm=0.5,AutoSelection=false,PriceMaterial=10920,NameMaterial="Ketac Molar Easymix"}
                }
                },
                new PriceList{ ServiceCode="2.23.",ServiceName="Наложение матрицы",Price= 1060,
                 NormConsumptionRates = new List<NormConsumptionRate>
                {
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.MATRIZA_METALL, Norm=1,AutoSelection=true,PriceMaterial=65}
                }
                },
                new PriceList{ ServiceCode="2.24.",ServiceName="Установка матрицедержателя",Price= 1060},
                new PriceList{ ServiceCode="2.25.",ServiceName="Установка межзубных клиньев",Price= 530,
                 NormConsumptionRates = new List<NormConsumptionRate>
                {
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.MEZHZUBNIE_KLINIYA, Norm=2,AutoSelection=true,PriceMaterial=450.84}
                }
                },
                new PriceList{ ServiceCode="2.27.02.",ServiceName="Шлифовка, полировка пломбы из композиционного материала фотоотверждаемого",Price= 7940,
                NormConsumptionRates = new List<NormConsumptionRate>
                {
                    new NormConsumptionRate{TypeMaterial=TypeMaterial.SCHETKI_POLIROVOCHNYE, Norm=0.1,AutoSelection=true, PriceMaterial=1635.4},
                    new NormConsumptionRate{TypeMaterial=TypeMaterial.PASTA_POLIROVOCHNAYA, Norm=0.2,AutoSelection=false,PriceMaterial=915.68, NameMaterial="Препараты для гигиены полости рта Detartrine"},
                      new NormConsumptionRate{TypeMaterial=TypeMaterial.GOLOVKA_POLIROVOCHNAYA, Norm=0.1,AutoSelection=true,PriceMaterial=6431.1},
                       new NormConsumptionRate{TypeMaterial=TypeMaterial.BOR_ALMAZN, Norm=0.2,AutoSelection=true,PriceMaterial=6753.5}
                }
                },
                new PriceList{ ServiceCode="2.30.",ServiceName="Герметизация пломбы",Price= 2120}
            });
            context.SaveChanges();
            //нормы расхода
            context.DentalMaterials.AddRange(new DentalMaterial[]
            {
                new DentalMaterial{ TypeMaterial=TypeMaterial.SPIRITUS, MaterialName="спирт 72%",Unit=Unit.мл,MaterialPrice=24.45, MaterialQuantity=1000,PurchaseDate=new DateTime(2018,01,01)},
                new DentalMaterial{ TypeMaterial=TypeMaterial.BOR_ALMAZN, MaterialName = "бор алмазный", Unit=Unit.шт,MaterialPrice=5195,MaterialQuantity=50,PurchaseDate=new DateTime(2018,01,01) },
                new DentalMaterial{ TypeMaterial=TypeMaterial.MATRIZA_METALL, MaterialName = "матрица металлическая", Unit=Unit.шт,MaterialPrice=50,MaterialQuantity=50,PurchaseDate=new DateTime(2018,01,01) },
                new DentalMaterial{ TypeMaterial=TypeMaterial.LUMBRIKANT, MaterialName = "Эндогель", Unit=Unit.мл,MaterialPrice=3271.67,MaterialQuantity=20,PurchaseDate=new DateTime(2018,01,01) },
                new DentalMaterial{ TypeMaterial=TypeMaterial.GOLOVKA_POLIROVOCHNAYA, MaterialName = "головка полировочная", Unit=Unit.шт,MaterialPrice=4947,MaterialQuantity=30,PurchaseDate=new DateTime(2018,01,01) },
                new DentalMaterial{ TypeMaterial=TypeMaterial.MATERIAL_PLOMB, MaterialName = "Filtek", Unit=Unit.г,MaterialPrice=21555,MaterialQuantity=10 ,PurchaseDate=new DateTime(2018,01,01)},
                new DentalMaterial{ TypeMaterial=TypeMaterial.MATERIAL_PLOMB, MaterialName = "REVOLUTION KIT А2,В3,С3", Unit=Unit.г,MaterialPrice=22290,MaterialQuantity=10,PurchaseDate=new DateTime(2018,01,01) },
                new DentalMaterial{ TypeMaterial=TypeMaterial.MATERIAL_PLOMB, MaterialName = "Ketac Molar Easymix", Unit=Unit.г,MaterialPrice=8400,MaterialQuantity=10,PurchaseDate=new DateTime(2018,01,01) },
                new DentalMaterial{ TypeMaterial=TypeMaterial.TOOL_KANAL, MaterialName = "файлы", Unit=Unit.шт,MaterialPrice=5616,MaterialQuantity=100,PurchaseDate=new DateTime(2018,01,01) },
                new DentalMaterial{ TypeMaterial=TypeMaterial.TEMP_PLOMB_MATERIAL, MaterialName = "Дентин-паста", Unit=Unit.г,MaterialPrice=88.3,MaterialQuantity=10 ,PurchaseDate=new DateTime(2018,01,01)},
                new DentalMaterial{ TypeMaterial=TypeMaterial.TEMP_PLOMB_MATERIAL, MaterialName = "Септопак", Unit=Unit.г,MaterialPrice=1282,MaterialQuantity=10,PurchaseDate=new DateTime(2018,01,01) },
                new DentalMaterial{ TypeMaterial=TypeMaterial.TEMP_PLOMB_MATERIAL, MaterialName = "Apex Cal", Unit=Unit.г,MaterialPrice=33600,MaterialQuantity=10 ,PurchaseDate=new DateTime(2018,01,01)},
                new DentalMaterial{ TypeMaterial=TypeMaterial.MATERIAL_PLOMB_INSIDEKANAL, MaterialName = "Эндометазон", Unit=Unit.г,MaterialPrice=7110,MaterialQuantity=14,PurchaseDate=new DateTime(2018,01,01) },
                new DentalMaterial{ TypeMaterial=TypeMaterial.MATERIAL_PLOMB_INSIDEKANAL, MaterialName = "Акросил", Unit=Unit.г,MaterialPrice=5478,MaterialQuantity=14,PurchaseDate=new DateTime(2018,01,01) },
                new DentalMaterial{ TypeMaterial=TypeMaterial.MATERIAL_PLOMB_INSIDEKANAL, MaterialName = "Адсил", Unit=Unit.г,MaterialPrice=4195,MaterialQuantity=14,PurchaseDate=new DateTime(2018,01,01) },
                new DentalMaterial{ TypeMaterial=TypeMaterial.MEZHZUBNIE_KLINIYA, MaterialName = "межзубные клинья", Unit=Unit.шт,MaterialPrice=346.8,MaterialQuantity=100,PurchaseDate=new DateTime(2018,01,01) },
                new DentalMaterial{ TypeMaterial=TypeMaterial.PREPARAT_FLUOKAL, MaterialName = "Флюокал", Unit=Unit.мл,MaterialPrice=1541.13,MaterialQuantity=20,PurchaseDate=new DateTime(2018,01,01) },
                new DentalMaterial{ TypeMaterial=TypeMaterial.RASTVOR_ANTISEPTIK, MaterialName = "перекись водорода", Unit=Unit.мл,MaterialPrice=7.48,MaterialQuantity=400,PurchaseDate=new DateTime(2018,01,01) },
                 new DentalMaterial{ TypeMaterial=TypeMaterial.RASTVOR_ANTISEPTIKSPECIFIC, MaterialName = "перекись водорода", Unit=Unit.мл,MaterialPrice=7.48,MaterialQuantity=400,PurchaseDate=new DateTime(2018,01,01) },
                new DentalMaterial{ TypeMaterial=TypeMaterial.RASTVOR_ANTISEPTIKSPECIFIC, MaterialName = "Паркан", Unit=Unit.мл,MaterialPrice=10,MaterialQuantity=100 ,PurchaseDate=new DateTime(2018,01,01)},
                new DentalMaterial{ TypeMaterial=TypeMaterial.RASTVOR_ANTISEPTIKSPECIFIC, MaterialName = "Хлоргексидин", Unit=Unit.мл,MaterialPrice=12.2,MaterialQuantity=200 ,PurchaseDate=new DateTime(2018,01,01)},
                new DentalMaterial{ TypeMaterial=TypeMaterial.RETRAKCION_NIT, MaterialName = "ретракционная нить", Unit=Unit.мм,MaterialPrice=14.4,MaterialQuantity=500,PurchaseDate=new DateTime(2018,01,01) },
                new DentalMaterial{ TypeMaterial=TypeMaterial.CEMENT_STEKLOIONOMERNYI, MaterialName = "Ketac Molar Easymix", Unit=Unit.г,MaterialPrice=8400,MaterialQuantity=10 ,PurchaseDate=new DateTime(2018,01,01)},
                new DentalMaterial{ TypeMaterial=TypeMaterial.PASTA_POLIROVOCHNAYA, MaterialName = "Препараты для гигиены полости рта Detartrine", Unit=Unit.г,MaterialPrice=704.37,MaterialQuantity=150,PurchaseDate=new DateTime(2018,01,01) },
                new DentalMaterial{ TypeMaterial=TypeMaterial.SHTRIPSA, MaterialName = "штрипса", Unit=Unit.шт,MaterialPrice=448,MaterialQuantity=100 ,PurchaseDate=new DateTime(2018,01,01)},
                new DentalMaterial{ TypeMaterial=TypeMaterial.DISC_POLIROVOCHNYI, MaterialName = "диск полировочный", Unit=Unit.шт,MaterialPrice=951,MaterialQuantity=100 ,PurchaseDate=new DateTime(2018,01,01)},
                new DentalMaterial{ TypeMaterial=TypeMaterial.VALIKI, MaterialName = "валики", Unit=Unit.шт,MaterialPrice=11.58,MaterialQuantity=100,PurchaseDate=new DateTime(2018,01,01) },
                new DentalMaterial{ TypeMaterial=TypeMaterial.ANESTETIK, MaterialName = "Ультракаин", Unit=Unit.шт,MaterialPrice=1520,MaterialQuantity=50 ,PurchaseDate=new DateTime(2018,01,01)},
                new DentalMaterial{ TypeMaterial=TypeMaterial.ANESTETIK, MaterialName = "Убистезин", Unit=Unit.шт,MaterialPrice=751,MaterialQuantity=50,PurchaseDate=new DateTime(2018,01,01) },
                new DentalMaterial{ TypeMaterial=TypeMaterial.ANESTETIK, MaterialName = "Скандонест", Unit=Unit.шт,MaterialPrice=906,MaterialQuantity=50,PurchaseDate=new DateTime(2018,01,01) },
                new DentalMaterial{ TypeMaterial=TypeMaterial.SHTIFT_PAPER, MaterialName = "штифты бумажные", Unit=Unit.шт,MaterialPrice=35,MaterialQuantity=50 ,PurchaseDate=new DateTime(2018,01,01)},
                new DentalMaterial{ TypeMaterial=TypeMaterial.SCHETKI_POLIROVOCHNYE, MaterialName = "щетки полировочные", Unit=Unit.шт,MaterialPrice=1258,MaterialQuantity=30,PurchaseDate=new DateTime(2018,01,01) },
                new DentalMaterial{ TypeMaterial=TypeMaterial.PULPOEXTRACTOR, MaterialName = "пульпоэкстракторы", Unit=Unit.шт,MaterialPrice=250,MaterialQuantity=30 ,PurchaseDate=new DateTime(2018,01,01)},
                new DentalMaterial{ TypeMaterial=TypeMaterial.KANALONAPOLNITEL, MaterialName = "каналонаполнитель", Unit=Unit.шт,MaterialPrice=502,MaterialQuantity=20 ,PurchaseDate=new DateTime(2018,01,01)},
                new DentalMaterial{ TypeMaterial=TypeMaterial.SHTIFT_GUTAPERCHIV, MaterialName = "гуттаперчевый штифт", Unit=Unit.шт,MaterialPrice=54,MaterialQuantity=100 ,PurchaseDate=new DateTime(2018,01,01)},
                new DentalMaterial{ TypeMaterial=TypeMaterial.SPREDER, MaterialName = "спредер", Unit=Unit.шт,MaterialPrice=6213,MaterialQuantity=20,PurchaseDate=new DateTime(2018,01,01) }

            });
            context.SaveChanges();

            context.Visits.AddRange(new Visit[]
            {
                //1
                new Visit {VisitDate=new DateTime(2018,08,01),VisitNumber="1", Diagnose="Профилактика",VisitCost=100690,MaterialCost=16466,TotalCost=117156,
              DoctorId=1,PatientId=1,Treatments=new List<Treatment>
              {
                  new Treatment{ PriceListId=28,Quantity=1} ,

                  new Treatment{ PriceListId=30,Quantity=1} ,

                  new Treatment{ PriceListId=32,Quantity=1} ,

                  new Treatment{ PriceListId=1,Quantity=20, MaterialConsumptions =new List<MaterialConsumption>
                    {
                      new MaterialConsumption{DentalMaterialId=17,ConsumptionQuant=2,SellingPrice=1541.13 }
                  } } ,

                  new Treatment{ PriceListId=3,Quantity=9,MaterialConsumptions =new List<MaterialConsumption>
                    {
                        new MaterialConsumption{DentalMaterialId=18,ConsumptionQuant=9,SellingPrice=9.72 }
                  } } ,

                  new Treatment{ PriceListId=4,Quantity=9,MaterialConsumptions =new List<MaterialConsumption>
                    {
                        new MaterialConsumption{DentalMaterialId=32,ConsumptionQuant=0.9,SellingPrice=1258 },
                        new MaterialConsumption{DentalMaterialId=24,ConsumptionQuant=0.9,SellingPrice=704.37 },
                        new MaterialConsumption{DentalMaterialId=5,ConsumptionQuant=2.25,SellingPrice=4947 } } } ,

                  new Treatment{ PriceListId=7,Quantity=1,MaterialConsumptions =new List<MaterialConsumption>
                    {
                        new MaterialConsumption{DentalMaterialId=18,ConsumptionQuant=50,SellingPrice=8 }

                    } }}
              } , 
                //2
                new Visit{VisitDate=new DateTime(2018,08,02),VisitNumber="2", Diagnose="Эндодонтия пульпит 46 зуба",VisitCost=75200,MaterialCost=30258,TotalCost=105458,
                DoctorId=1,PatientId=3,Treatments=new List<Treatment>
              {
                    new Treatment{PriceListId=29,Quantity=1},
                    new Treatment{ PriceListId=30,Quantity=1},
                    new Treatment{ PriceListId=8,Quantity=1, MaterialConsumptions = new List<MaterialConsumption>
                     {
                         new MaterialConsumption{DentalMaterialId=30, ConsumptionQuant=1,SellingPrice=1177.8},
                         new MaterialConsumption{DentalMaterialId=18, ConsumptionQuant=1,SellingPrice=9.72}
                     } },
                    new Treatment{ PriceListId=18,Quantity=3, MaterialConsumptions = new List<MaterialConsumption>
                    {
                        new MaterialConsumption{DentalMaterialId=18, ConsumptionQuant=3,SellingPrice=9.72},
                        new MaterialConsumption{DentalMaterialId=31, ConsumptionQuant=12,SellingPrice=45.5}
                    } },
                    new Treatment{ PriceListId=21,Quantity=3, MaterialConsumptions = new List<MaterialConsumption>
                    {
                        new MaterialConsumption{DentalMaterialId=13, ConsumptionQuant=0.45,SellingPrice=9243},
                        new MaterialConsumption{DentalMaterialId=1, ConsumptionQuant=1.5,SellingPrice=31.79},
                        new MaterialConsumption{DentalMaterialId=34, ConsumptionQuant=0.45,SellingPrice=9243},
                        new MaterialConsumption{DentalMaterialId=31, ConsumptionQuant=15,SellingPrice=45.5},
                        new MaterialConsumption{DentalMaterialId=35, ConsumptionQuant=15,SellingPrice=70.2},
                        new MaterialConsumption{DentalMaterialId=36, ConsumptionQuant=0.48,SellingPrice=8076.9}
                    } },
                     new Treatment{ PriceListId=23,Quantity=1, MaterialConsumptions = new List<MaterialConsumption>
                    {
                        new MaterialConsumption{DentalMaterialId=6, ConsumptionQuant=0.4,SellingPrice=28021.5}
                     } },

                      new Treatment{ PriceListId=25,Quantity=1, MaterialConsumptions = new List<MaterialConsumption>
                    {
                        new MaterialConsumption{DentalMaterialId=3, ConsumptionQuant=1,SellingPrice=65}
                     } },

                       new Treatment{ PriceListId=26,Quantity=1, MaterialConsumptions = new List<MaterialConsumption>
                    {
                        new MaterialConsumption{DentalMaterialId=16, ConsumptionQuant=2,SellingPrice=450.84}
                     } },
                       new Treatment{ PriceListId=27,Quantity=1, MaterialConsumptions = new List<MaterialConsumption>
                    {
                        new MaterialConsumption{DentalMaterialId=32, ConsumptionQuant=0.1,SellingPrice=1635.4},
                        new MaterialConsumption{DentalMaterialId=24, ConsumptionQuant=0.2,SellingPrice=915.68},
                        new MaterialConsumption{DentalMaterialId=5, ConsumptionQuant=0.1,SellingPrice=6431.1},
                        new MaterialConsumption{DentalMaterialId=2, ConsumptionQuant=0.2,SellingPrice=6753.5}
                     } },
                } },
                //3
                 new Visit{VisitDate=new DateTime(2018,08,03),VisitNumber="3", Diagnose="Эндодонтия апикальный периодонтит 46 зуба",VisitCost=57730,MaterialCost=18921,TotalCost=76651,
                DoctorId=2,PatientId=2,Treatments=new List<Treatment>
              {
                    new Treatment{PriceListId=29,Quantity=1},
                    new Treatment{ PriceListId=30,Quantity=1},
                    new Treatment{ PriceListId=18,Quantity=3, MaterialConsumptions = new List<MaterialConsumption>
                    {
                        new MaterialConsumption{DentalMaterialId=18, ConsumptionQuant=3,SellingPrice=9.72},
                        new MaterialConsumption{DentalMaterialId=31, ConsumptionQuant=12,SellingPrice=45.5}
                    } },
                     new Treatment{ PriceListId=21,Quantity=3, MaterialConsumptions = new List<MaterialConsumption>
                    {
                        new MaterialConsumption{DentalMaterialId=13, ConsumptionQuant=0.45,SellingPrice=9243},
                        new MaterialConsumption{DentalMaterialId=1, ConsumptionQuant=1.5,SellingPrice=31.79},
                        new MaterialConsumption{DentalMaterialId=34, ConsumptionQuant=0.45,SellingPrice=9243},
                        new MaterialConsumption{DentalMaterialId=31, ConsumptionQuant=15,SellingPrice=45.5},
                        new MaterialConsumption{DentalMaterialId=35, ConsumptionQuant=15,SellingPrice=70.2},
                        new MaterialConsumption{DentalMaterialId=36, ConsumptionQuant=0.48,SellingPrice=8076.9}
                    } },
                      new Treatment{ PriceListId=23,Quantity=1, MaterialConsumptions = new List<MaterialConsumption>
                    {
                         new MaterialConsumption{DentalMaterialId=8, ConsumptionQuant=0.4,SellingPrice=10920}
                    } },

                } },
                 //4
                 new Visit{VisitDate=new DateTime(2018,08,04),VisitNumber="4", Diagnose="Кариес 18 зуба",VisitCost=45010,MaterialCost=19013,TotalCost=64023,
                DoctorId=2,PatientId=2,Treatments=new List<Treatment>
              {
                    new Treatment{PriceListId=28,Quantity=1},
                    new Treatment{ PriceListId=7,Quantity=1,MaterialConsumptions=new List<MaterialConsumption>
                    {
                         new MaterialConsumption{DentalMaterialId=19, ConsumptionQuant=50,SellingPrice=9.72}                      
                    } },
                    new Treatment{ PriceListId=9,Quantity=1, MaterialConsumptions = new List<MaterialConsumption>
                    {
                        new MaterialConsumption{DentalMaterialId=2, ConsumptionQuant=0.25,SellingPrice=6753.5},
                        new MaterialConsumption{DentalMaterialId=18, ConsumptionQuant=1,SellingPrice=9.72}                       
                    } },
                    new Treatment{PriceListId=33,Quantity=1},
                     new Treatment{ PriceListId=24,Quantity=1, MaterialConsumptions = new List<MaterialConsumption>
                    {
                        new MaterialConsumption{DentalMaterialId=7, ConsumptionQuant=0.5,SellingPrice=28977}                       
                    } },
                     new Treatment{ PriceListId=27,Quantity=1, MaterialConsumptions = new List<MaterialConsumption>
                    {
                        new MaterialConsumption{DentalMaterialId=32, ConsumptionQuant=0.1,SellingPrice=1635.4},
                        new MaterialConsumption{DentalMaterialId=24, ConsumptionQuant=0.2,SellingPrice=915.68},
                        new MaterialConsumption{DentalMaterialId=5, ConsumptionQuant=0.1,SellingPrice=6431.1},
                        new MaterialConsumption{DentalMaterialId=2, ConsumptionQuant=0.2,SellingPrice=6753.5}
                     } }
                } },
                  //5
                 new Visit{VisitDate=new DateTime(2018,08,05),VisitNumber="5", Diagnose="Эндодонтия периодонтит 46 зуба",VisitCost=98520,MaterialCost=75286,TotalCost=173806,
                DoctorId=1,PatientId=4,Treatments=new List<Treatment>
              {
                    new Treatment{PriceListId=28,Quantity=1},
                      new Treatment{PriceListId=30,Quantity=1},
                    new Treatment{ PriceListId=5,Quantity=1,MaterialConsumptions=new List<MaterialConsumption>
                    {
                         new MaterialConsumption{DentalMaterialId=11, ConsumptionQuant=0.5,SellingPrice=1666.6}                        
                    } },
                    new Treatment{ PriceListId=8,Quantity=2, MaterialConsumptions = new List<MaterialConsumption>
                    {
                        new MaterialConsumption{DentalMaterialId=18, ConsumptionQuant=2,SellingPrice=9.72},
                        new MaterialConsumption{DentalMaterialId=28, ConsumptionQuant=2,SellingPrice=1976}                        
                    } },

                     new Treatment{ PriceListId=12,Quantity=1, MaterialConsumptions = new List<MaterialConsumption>
                    {
                        new MaterialConsumption{DentalMaterialId=2, ConsumptionQuant=0.5,SellingPrice=6753.5},
                         new MaterialConsumption{DentalMaterialId=18, ConsumptionQuant=1,SellingPrice=9.72}                        
                    } },
                     new Treatment{ PriceListId=13,Quantity=1, MaterialConsumptions = new List<MaterialConsumption>
                    {
                        new MaterialConsumption{DentalMaterialId=9, ConsumptionQuant=0.8,SellingPrice=7300},
                        new MaterialConsumption{DentalMaterialId=18, ConsumptionQuant=1,SellingPrice=9.72}                            
                     } },
                     new Treatment{ PriceListId=14,Quantity=2, MaterialConsumptions = new List<MaterialConsumption>
                    {
                        new MaterialConsumption{DentalMaterialId=9, ConsumptionQuant=2.4,SellingPrice=7300},
                        new MaterialConsumption{DentalMaterialId=18, ConsumptionQuant=2,SellingPrice=9.72}
     
                     } },
                     new Treatment{ PriceListId=18,Quantity=3, MaterialConsumptions = new List<MaterialConsumption>
                    {
                        new MaterialConsumption{DentalMaterialId=18, ConsumptionQuant=3,SellingPrice=9.72},
                        new MaterialConsumption{DentalMaterialId=31, ConsumptionQuant=12,SellingPrice=45.5}                       
                     } },
                     new Treatment{ PriceListId=19,Quantity=2, MaterialConsumptions = new List<MaterialConsumption>
                    {
                        new MaterialConsumption{DentalMaterialId=4, ConsumptionQuant=0.6,SellingPrice=4253.17},
                        new MaterialConsumption{DentalMaterialId=31, ConsumptionQuant=8,SellingPrice=45.5}
                        
                     } },
                     new Treatment{ PriceListId=20,Quantity=3, MaterialConsumptions = new List<MaterialConsumption>
                    {
                        new MaterialConsumption{DentalMaterialId=12, ConsumptionQuant=0.9,SellingPrice=43680},
                        new MaterialConsumption{DentalMaterialId=18, ConsumptionQuant=1.5,SellingPrice=9.72},
                        new MaterialConsumption{DentalMaterialId=34, ConsumptionQuant=0.3,SellingPrice=652.6},
                        new MaterialConsumption{DentalMaterialId=31, ConsumptionQuant=15,SellingPrice=45.5}                  
                     } },
                       new Treatment{ PriceListId=22,Quantity=1, MaterialConsumptions = new List<MaterialConsumption>
                    {  
                        new MaterialConsumption{DentalMaterialId=18, ConsumptionQuant=1,SellingPrice=9.72},
                     } },
                        } },
                       //6
                        new Visit{VisitDate=new DateTime(2018,08,05),VisitNumber="6", Diagnose="Кариес",VisitCost=52960,MaterialCost=14232,TotalCost=67192,
                DoctorId=2,PatientId=4,Treatments=new List<Treatment>
              {
                    new Treatment{PriceListId=29,Quantity=1},                      
                    new Treatment{ PriceListId=8,Quantity=1,MaterialConsumptions=new List<MaterialConsumption>
                    {
                        new MaterialConsumption{DentalMaterialId=18, ConsumptionQuant=1,SellingPrice=9.72},
                        new MaterialConsumption{DentalMaterialId=28, ConsumptionQuant=1,SellingPrice=1976}
                    } },
                     new Treatment{ PriceListId=10,Quantity=1,MaterialConsumptions=new List<MaterialConsumption>
                    {
                           new MaterialConsumption{DentalMaterialId=2, ConsumptionQuant=0.3333,SellingPrice=6753.5},
                        new MaterialConsumption{DentalMaterialId=18, ConsumptionQuant=1,SellingPrice=9.72}                        
                    } },
                     new Treatment{ PriceListId=11,Quantity=1,MaterialConsumptions=new List<MaterialConsumption>
                    {
                           new MaterialConsumption{DentalMaterialId=8, ConsumptionQuant=0.3,SellingPrice=10920},
                     } },
                     new Treatment{ PriceListId=33,Quantity=1 },
                       new Treatment{ PriceListId=23,Quantity=1, MaterialConsumptions = new List<MaterialConsumption>
                    {
                         new MaterialConsumption{DentalMaterialId=8, ConsumptionQuant=0.4,SellingPrice=10920}
                    } },
                       new Treatment{ PriceListId=27,Quantity=1, MaterialConsumptions = new List<MaterialConsumption>
                    {
                        new MaterialConsumption{DentalMaterialId=32, ConsumptionQuant=0.1,SellingPrice=1635.4},
                        new MaterialConsumption{DentalMaterialId=24, ConsumptionQuant=0.2,SellingPrice=915.68},
                        new MaterialConsumption{DentalMaterialId=5, ConsumptionQuant=0.1,SellingPrice=6431.1},
                        new MaterialConsumption{DentalMaterialId=2, ConsumptionQuant=0.2,SellingPrice=6753.5}
                     } },
                       new Treatment{ PriceListId=35,Quantity=1 }
                } },
                        //7
                        new Visit{VisitDate=new DateTime(2018,08,05),VisitNumber="7", Diagnose="КАРИЕС 45, 48 зубов",VisitCost=92160,MaterialCost=29818,TotalCost=121978,
                DoctorId=1,PatientId=2,Treatments=new List<Treatment>
              {
                    new Treatment{PriceListId=29,Quantity=1},
                    new Treatment{ PriceListId=6,Quantity=2,MaterialConsumptions=new List<MaterialConsumption>
                    {
                        new MaterialConsumption{DentalMaterialId=2, ConsumptionQuant=0.5,SellingPrice=6753.5},
                        new MaterialConsumption{DentalMaterialId=18, ConsumptionQuant=2,SellingPrice=9.72}                       
                    } },
                     new Treatment{ PriceListId=10,Quantity=2,MaterialConsumptions=new List<MaterialConsumption>
                    {
                           new MaterialConsumption{DentalMaterialId=2, ConsumptionQuant=0.6666,SellingPrice=6753.5},
                        new MaterialConsumption{DentalMaterialId=18, ConsumptionQuant=2,SellingPrice=9.72}
                    } },
                     new Treatment{ PriceListId=11,Quantity=2,MaterialConsumptions=new List<MaterialConsumption>
                    {
                           new MaterialConsumption{DentalMaterialId=8, ConsumptionQuant=0.6,SellingPrice=10920},
                     } },
                     new Treatment{ PriceListId=33,Quantity=2 },
                     new Treatment{ PriceListId=23,Quantity=2, MaterialConsumptions = new List<MaterialConsumption>
                    {
                         new MaterialConsumption{DentalMaterialId=8, ConsumptionQuant=0.8,SellingPrice=10920}
                    } },
                        new Treatment{ PriceListId=25,Quantity=2, MaterialConsumptions = new List<MaterialConsumption>
                    {
                         new MaterialConsumption{DentalMaterialId=3, ConsumptionQuant=2,SellingPrice=65}                         
                    } },
                     new Treatment{ PriceListId=34,Quantity=2 },
                     new Treatment{ PriceListId=26,Quantity=2, MaterialConsumptions = new List<MaterialConsumption>
                    {
                        new MaterialConsumption{DentalMaterialId=16, ConsumptionQuant=4,SellingPrice=450.84}
                     } },
                       new Treatment{ PriceListId=27,Quantity=2, MaterialConsumptions = new List<MaterialConsumption>
                    {
                        new MaterialConsumption{DentalMaterialId=32, ConsumptionQuant=0.2,SellingPrice=1635.4},
                        new MaterialConsumption{DentalMaterialId=24, ConsumptionQuant=0.4,SellingPrice=915.68},
                        new MaterialConsumption{DentalMaterialId=5, ConsumptionQuant=0.2,SellingPrice=6431.1},
                        new MaterialConsumption{DentalMaterialId=2, ConsumptionQuant=0.4,SellingPrice=6753.5}
                     } }
                } }
            });
            context.Authorizations.AddRange(new Authorization[]
                {
                    new Authorization{User="Solomevich", Password="12345",Role=Role.Dentist},
                    new Authorization{User="Darevskiy", Password="12345",Role=Role.Dentist},
                    new Authorization{User="Tron", Password="123456789",Role=Role.Admin},
                    new Authorization{User="Ivanov", Password="123",Role=Role.Registrator}
                });
        }

    }
}

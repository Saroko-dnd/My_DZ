using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.Windows;
using System.Windows.Input;


namespace MapDBContext
{
    public class BanksDBContext : DbContext
    {
        public DbSet<Bank> Banks { get; set; }
        public DbSet<BankBranch> BankBranches { get; set; }
        public DbSet<BreakTime> BreakTimes { get; set; }
        public DbSet<Cashier> Cashiers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ExchangeRates> ExchangeRates { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<WorkingHours> WorkingHours { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankBranch>()
              .HasOptional(a => a.RelatedCashier)
              .WithRequired(s => s.RelatedBranch);
        }

        public BanksDBContext()
        {
        }

        public BanksDBContext(string ConnectionString)
            : base(ConnectionString)
        {
            Database.SetInitializer<BanksDBContext>(new BanksDBInitializer());
        }
    }

    public class BanksDBInitializer : CreateDatabaseIfNotExists<BanksDBContext>
    {
        protected override void Seed(BanksDBContext TestContext)
        {
            TestContext.Services.Add(new Service() { Servise = "Кредитование" });
            TestContext.Services.Add(new Service() { Servise = "Денежные переводы" });
            TestContext.Services.Add(new Service() { Servise = "Оплата коммунальных услуг" });
            TestContext.Services.Add(new Service() { Servise = "Страхование" });
            TestContext.Services.Add(new Service() { Servise = "Вклады" });
            TestContext.Banks.Add(new Bank() { BankName = "UltraBank" });
            TestContext.Banks.Add(new Bank() { BankName = "GalacticBank" });
            TestContext.Banks.Add(new Bank() { BankName = "NativeBank" });
            TestContext.SaveChanges();
            BankBranch Branch_1 = new BankBranch()
            {
                BranchName = "Отделение UltraBank №1",
                MapLocation = DbGeography.FromText("POINT(27.5611889362335 53.9117074828877)"),
                RelatedBank = TestContext.Banks.Where(res => res.BankName == "UltraBank").First(),
                WorkingHours = new WorkingHours() { StartHour = 9, StartMinutes = 30, EndHour = 21, EndMinutes = 30 },
                RelatedCashier = new Cashier()
                {
                    FirstName = "Иван",
                    LastName = "Васильевич",
                    Patronymic = "Горчинский",
                    Phone = "6675544"
                },
                Address = "г.Минск, ул.Алоизы Пашкевич, дом 3",
                Phone = "5467788",
                RelatedRates = new ExchangeRates()
                {
                    EUROBuy = 56987.0,
                    EUROSell = 45987.0,
                    RuBuy = 300.0,
                    RuSell = 500.0,
                    USDBuy = 20800.0,
                    USDSell = 23900.0
                },
                OpeningDate = new DateTime(1998, 4, 23)
            };
            Branch_1.RelatedServices.Add(TestContext.Services.Where(res => res.Servise == "Кредитование").First());
            Branch_1.RelatedServices.Add(TestContext.Services.Where(res => res.Servise == "Денежные переводы").First());
            Branch_1.BreakTimes.Add(new BreakTime() { StartHour = 11, StartMinutes = 20, EndHour = 11, EndMinutes = 40 });
            Branch_1.BreakTimes.Add(new BreakTime() { StartHour = 16, StartMinutes = 10, EndHour = 16, EndMinutes = 30 });
            Branch_1.RelatedComments.Add(new Comment() { CommentItself = "Отличный банк!", RelatedBranch = Branch_1 });
            Branch_1.RelatedComments.Add(new Comment() { CommentItself = "Слишком медленно обслуживают.", RelatedBranch = Branch_1 });
            TestContext.BankBranches.Add(Branch_1);

            BankBranch Branch_2 = new BankBranch()
            {
                BranchName = "Отделение UltraBank №2",
                MapLocation = DbGeography.FromText("POINT(27.5638872385025 53.8771501542879)"),
                RelatedBank = TestContext.Banks.Where(res => res.BankName == "UltraBank").First(),
                WorkingHours = new WorkingHours() { StartHour = 9, StartMinutes = 0, EndHour = 21, EndMinutes = 0 },
                RelatedCashier = new Cashier()
                {
                    FirstName = "Денис",
                    LastName = "Махнёв",
                    Patronymic = "Михаилович",
                    Phone = "1231231"
                },
                Address = "г.Минск, ул. Надеждинская, дом 56",
                Phone = "5511221",
                RelatedRates = new ExchangeRates()
                {
                    EUROBuy = 24400,
                    EUROSell = 24100,
                    RuBuy = 286.0,
                    RuSell = 281.5,
                    USDBuy = 21900,
                    USDSell = 21650
                },
                OpeningDate = new DateTime(2000, 10, 23)
            };
            Branch_2.RelatedServices.Add(TestContext.Services.Where(res => res.Servise == "Страхование").First());
            Branch_2.RelatedServices.Add(TestContext.Services.Where(res => res.Servise == "Денежные переводы").First());
            Branch_2.BreakTimes.Add(new BreakTime() { StartHour = 12, StartMinutes = 10, EndHour = 12, EndMinutes = 30 });
            Branch_2.BreakTimes.Add(new BreakTime() { StartHour = 17, StartMinutes = 0, EndHour = 17, EndMinutes = 20 });
            Branch_2.RelatedComments.Add(new Comment() { CommentItself = "СЛИШКОМ БОЛЬШИЕ ОЧЕРЕДИ!", RelatedBranch = Branch_2 });
            Branch_2.RelatedComments.Add(new Comment() { CommentItself = "Здесь было НЛО...", RelatedBranch = Branch_2 });
            TestContext.BankBranches.Add(Branch_2);

            BankBranch Branch_3 = new BankBranch()
            {
                BranchName = "Отделение UltraBank №3",
                MapLocation = DbGeography.FromText("POINT(27.5564360618591 53.8766346760167)"),
                RelatedBank = TestContext.Banks.Where(res => res.BankName == "UltraBank").First(),
                WorkingHours = new WorkingHours() { StartHour = 7, StartMinutes = 0, EndHour = 21, EndMinutes = 20 },
                RelatedCashier = new Cashier()
                {
                    FirstName = "Елена",
                    LastName = "Петровна",
                    Patronymic = "Мохайчук",
                    Phone = "4422443"
                },
                Address = "г.Минск, ул. Левкова, дом 52",
                Phone = "7654321",
                RelatedRates = new ExchangeRates()
                {
                    EUROBuy = 24500,
                    EUROSell = 24250,
                    RuBuy = 288.0,
                    RuSell = 279.0,
                    USDBuy = 21950,
                    USDSell = 21800
                },
                OpeningDate = new DateTime(2009, 1, 1)
            };
            Branch_3.RelatedServices.Add(TestContext.Services.Where(res => res.Servise == "Оплата коммунальных услуг").First());
            Branch_3.RelatedServices.Add(TestContext.Services.Where(res => res.Servise == "Вклады").First());
            Branch_3.BreakTimes.Add(new BreakTime() { StartHour = 12, StartMinutes = 20, EndHour = 12, EndMinutes = 40 });
            Branch_3.BreakTimes.Add(new BreakTime() { StartHour = 18, StartMinutes = 0, EndHour = 18, EndMinutes = 20 });
            Branch_3.RelatedComments.Add(new Comment() { CommentItself = "Слишком маленький процент!", RelatedBranch = Branch_3 });
            Branch_3.RelatedComments.Add(new Comment() { CommentItself = "Отличное, качественное обслуживание.", RelatedBranch = Branch_3 });
            TestContext.BankBranches.Add(Branch_3);

            BankBranch Branch_4 = new BankBranch()
            {
                BranchName = "Отделение NativeBank №1",
                MapLocation = DbGeography.FromText("POINT(27.5511145591736 53.8744841459664)"),
                RelatedBank = TestContext.Banks.Where(res => res.BankName == "NativeBank").First(),
                WorkingHours = new WorkingHours() { StartHour = 8, StartMinutes = 0, EndHour = 21, EndMinutes = 0 },
                RelatedCashier = new Cashier()
                {
                    FirstName = "Коваль",
                    LastName = "Нина",
                    Patronymic = "Семеновна",
                    Phone = "1111111"
                },
                Address = "г.Минск, Сенницкая, дом 2",
                Phone = "8989894",
                RelatedRates = new ExchangeRates()
                {
                    EUROBuy = 24500,
                    EUROSell = 23950,
                    RuBuy = 285.0,
                    RuSell = 263.0,
                    USDBuy = 21990,
                    USDSell = 21650
                },
                OpeningDate = new DateTime(2010, 10, 15)
            };
            Branch_4.RelatedServices.Add(TestContext.Services.Where(res => res.Servise == "Оплата коммунальных услуг").First());
            Branch_4.RelatedServices.Add(TestContext.Services.Where(res => res.Servise == "Вклады").First());
            Branch_4.BreakTimes.Add(new BreakTime() { StartHour = 13, StartMinutes = 0, EndHour = 13, EndMinutes = 20 });
            Branch_4.BreakTimes.Add(new BreakTime() { StartHour = 18, StartMinutes = 0, EndHour = 18, EndMinutes = 30 });
            Branch_4.RelatedComments.Add(new Comment() { CommentItself = "Хороший банк.", RelatedBranch = Branch_4 });
            Branch_4.RelatedComments.Add(new Comment() { CommentItself = "Мой любимый банк уже 5 лет.", RelatedBranch = Branch_4 });
            TestContext.BankBranches.Add(Branch_4);

            BankBranch Branch_5 = new BankBranch()
            {
                BranchName = "Отделение NativeBank №2",
                MapLocation = DbGeography.FromText("POINT(27.533004283905 53.8779692140537)"),
                RelatedBank = TestContext.Banks.Where(res => res.BankName == "NativeBank").First(),
                WorkingHours = new WorkingHours() { StartHour = 10, StartMinutes = 0, EndHour = 22, EndMinutes = 0 },
                RelatedCashier = new Cashier()
                {
                    FirstName = "Покровский",
                    LastName = "Александр",
                    Patronymic = "Леонидович",
                    Phone = "1213211"
                },
                Address = "г.Минск,ул. Брилевская, дом 8, корпус 1",
                Phone = "1559663",
                RelatedRates = new ExchangeRates()
                {
                    EUROBuy = 24470,
                    EUROSell = 24150,
                    RuBuy = 285.0,
                    RuSell = 279.0,
                    USDBuy = 22000,
                    USDSell = 21650
                },
                OpeningDate = new DateTime(2003, 12, 2)
            };
            Branch_5.RelatedServices.Add(TestContext.Services.Where(res => res.Servise == "Кредитование").First());
            Branch_5.RelatedServices.Add(TestContext.Services.Where(res => res.Servise == "Вклады").First());
            Branch_5.BreakTimes.Add(new BreakTime() { StartHour = 11, StartMinutes = 50, EndHour = 12, EndMinutes = 10 });
            Branch_5.BreakTimes.Add(new BreakTime() { StartHour = 18, StartMinutes = 30, EndHour = 18, EndMinutes = 40 });
            Branch_5.RelatedComments.Add(new Comment() { CommentItself = "Лучший банк на свете.", RelatedBranch = Branch_5 });
            Branch_5.RelatedComments.Add(new Comment() { CommentItself = "Худший банк на свете.", RelatedBranch = Branch_5 });
            TestContext.BankBranches.Add(Branch_5);

            BankBranch Branch_6 = new BankBranch()
            {
                BranchName = "Отделение NativeBank №3",
                MapLocation = DbGeography.FromText("POINT(27.4806904792786 53.9146997155977)"),
                RelatedBank = TestContext.Banks.Where(res => res.BankName == "NativeBank").First(),
                WorkingHours = new WorkingHours() { StartHour = 12, StartMinutes = 0, EndHour = 24, EndMinutes = 0 },
                RelatedCashier = new Cashier()
                {
                    FirstName = "Тевосова",
                    LastName = "Вероника",
                    Patronymic = "Владиленовна",
                    Phone = "1233219"
                },
                Address = "г.Минск,ул. Матусевича, дом 19",
                Phone = "1379852",
                RelatedRates = new ExchangeRates()
                {
                    EUROBuy = 24450,
                    EUROSell = 24160,
                    RuBuy = 285.5,
                    RuSell = 279.0,
                    USDBuy = 21920,
                    USDSell = 21660
                },
                OpeningDate = new DateTime(2000, 5, 20)
            };
            Branch_6.RelatedServices.Add(TestContext.Services.Where(res => res.Servise == "Кредитование").First());
            Branch_6.RelatedServices.Add(TestContext.Services.Where(res => res.Servise == "Страхование").First());
            Branch_6.BreakTimes.Add(new BreakTime() { StartHour = 15, StartMinutes = 50, EndHour = 16, EndMinutes = 10 });
            Branch_6.BreakTimes.Add(new BreakTime() { StartHour = 20, StartMinutes = 30, EndHour = 20, EndMinutes = 50 });
            Branch_6.RelatedComments.Add(new Comment() { CommentItself = "Банк не хуже и не лучше других.", RelatedBranch = Branch_6 });
            Branch_6.RelatedComments.Add(new Comment() { CommentItself = "Нормальный банк.", RelatedBranch = Branch_6 });
            TestContext.BankBranches.Add(Branch_6);

            BankBranch Branch_7 = new BankBranch()
            {
                BranchName = "Отделение GalacticBank №1",
                MapLocation = DbGeography.FromText("POINT(27.611340880394 53.9392929865815)"),
                RelatedBank = TestContext.Banks.Where(res => res.BankName == "GalacticBank").First(),
                WorkingHours = new WorkingHours() { StartHour = 6, StartMinutes = 0, EndHour = 24, EndMinutes = 0 },
                RelatedCashier = new Cashier()
                {
                    FirstName = "Винокуров",
                    LastName = "Дмитрий",
                    Patronymic = "Ростиславович",
                    Phone = "1122334"
                },
                Address = "г.Минск,ул. Чайковского, дом 84",
                Phone = "9632587",
                RelatedRates = new ExchangeRates()
                {
                    EUROBuy = 24450,
                    EUROSell = 24200,
                    RuBuy = 285.5,
                    RuSell = 278.5,
                    USDBuy = 21950,
                    USDSell = 21750
                },
                OpeningDate = new DateTime(2012, 6, 28)
            };
            Branch_7.RelatedServices.Add(TestContext.Services.Where(res => res.Servise == "Денежные переводы").First());
            Branch_7.RelatedServices.Add(TestContext.Services.Where(res => res.Servise == "Страхование").First());
            Branch_7.BreakTimes.Add(new BreakTime() { StartHour = 11, StartMinutes = 30, EndHour = 12, EndMinutes = 0 });
            Branch_7.BreakTimes.Add(new BreakTime() { StartHour = 18, StartMinutes = 30, EndHour = 19, EndMinutes = 0 });
            Branch_7.RelatedComments.Add(new Comment() { CommentItself = "Нормальный банк, все устраивает.", RelatedBranch = Branch_7 });
            Branch_7.RelatedComments.Add(new Comment() { CommentItself = "UltraBank лучше!", RelatedBranch = Branch_7 });
            TestContext.BankBranches.Add(Branch_7);

            BankBranch Branch_8 = new BankBranch()
            {
                BranchName = "Отделение GalacticBank №2",
                MapLocation = DbGeography.FromText("POINT(27.631698846817 53.9286785318838)"),
                RelatedBank = TestContext.Banks.Where(res => res.BankName == "GalacticBank").First(),
                WorkingHours = new WorkingHours() { StartHour = 6, StartMinutes = 30, EndHour = 23, EndMinutes = 30 },
                RelatedCashier = new Cashier()
                {
                    FirstName = "Ямсков",
                    LastName = "Изяслав",
                    Patronymic = "Филиппович",
                    Phone = "3339996"
                },
                Address = "г.Минск,проспект Независимости, дом 104",
                Phone = "1535957",
                RelatedRates = new ExchangeRates()
                {
                    EUROBuy = 24450,
                    EUROSell = 24200,
                    RuBuy = 285.5,
                    RuSell = 278.5,
                    USDBuy = 21950,
                    USDSell = 21750
                },
                OpeningDate = new DateTime(2012, 6, 28)
            };
            Branch_8.RelatedServices.Add(TestContext.Services.Where(res => res.Servise == "Денежные переводы").First());
            Branch_8.RelatedServices.Add(TestContext.Services.Where(res => res.Servise == "Оплата коммунальных услуг").First());
            Branch_8.BreakTimes.Add(new BreakTime() { StartHour = 9, StartMinutes = 30, EndHour = 9, EndMinutes = 50 });
            Branch_8.BreakTimes.Add(new BreakTime() { StartHour = 20, StartMinutes = 20, EndHour = 20, EndMinutes = 40 });
            Branch_8.RelatedComments.Add(new Comment() { CommentItself = "Жалоб нет.", RelatedBranch = Branch_8 });
            Branch_8.RelatedComments.Add(new Comment() { CommentItself = "NativeBank лучше!", RelatedBranch = Branch_8 });
            TestContext.BankBranches.Add(Branch_8);

            BankBranch Branch_9 = new BankBranch()
            {
                BranchName = "Отделение GalacticBank №3",
                MapLocation = DbGeography.FromText("POINT(27.5730550289154 53.9178181080021)"),
                RelatedBank = TestContext.Banks.Where(res => res.BankName == "GalacticBank").First(),
                WorkingHours = new WorkingHours() { StartHour = 7, StartMinutes = 30, EndHour = 23, EndMinutes = 30 },
                RelatedCashier = new Cashier()
                {
                    FirstName = "Корнеев",
                    LastName = "Александр",
                    Patronymic = "Арсениевич",
                    Phone = "4321234"
                },
                Address = "г.Минск,ул. Куйбышева, дом 57",
                Phone = "1199774",
                RelatedRates = new ExchangeRates()
                {
                    EUROBuy = 24600,
                    EUROSell = 24100,
                    RuBuy = 288.0,
                    RuSell = 274.0,
                    USDBuy = 22000,
                    USDSell = 21600
                },
                OpeningDate = new DateTime(2015, 3, 22)
            };
            Branch_9.RelatedServices.Add(TestContext.Services.Where(res => res.Servise == "Денежные переводы").First());
            Branch_9.RelatedServices.Add(TestContext.Services.Where(res => res.Servise == "Кредитование").First());
            Branch_9.BreakTimes.Add(new BreakTime() { StartHour = 10, StartMinutes = 10, EndHour = 10, EndMinutes = 30 });
            Branch_9.BreakTimes.Add(new BreakTime() { StartHour = 15, StartMinutes = 20, EndHour = 15, EndMinutes = 40 });
            Branch_9.RelatedComments.Add(new Comment() { CommentItself = "В последнее время банк испортился.", RelatedBranch = Branch_9 });
            Branch_9.RelatedComments.Add(new Comment() { CommentItself = "Самый лучший банк из всех!", RelatedBranch = Branch_9 });
            TestContext.BankBranches.Add(Branch_9);
            TestContext.SaveChanges();
            base.Seed(TestContext);
        }
    }

}

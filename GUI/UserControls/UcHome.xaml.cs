using BLL.Services;
using FontAwesome6;
using GUI.View;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace GUI.UserControls
{
    public partial class UcHome : UserControl
    {
        private readonly ExpenseService _expenseService = new ExpenseService();
        private readonly IncomeService _incomeService  = new IncomeService();
        readonly string _userId = BLL.AppContext.Instance.UserId;
        private int _currentImageIndex = 0; // Vị trí ảnh hiện tại
        private readonly Image[] _images;   // Mảng chứa các hình ảnh
        private readonly DispatcherTimer _bannerTimer; // Bộ đếm thời gian
        public UcHome()
        {
            InitializeComponent();
            ShowInfoCard();
            _images = new[] { Image1, Image2};

            // Khởi tạo timer cho việc chuyển banner
            _bannerTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(3) // Chuyển sau mỗi 3 giây
            };
            _bannerTimer.Tick += BannerTimer_Tick;
            _bannerTimer.Start(); // Bắt đầu tự động chuyển
        }

        private void BtnOpenFormExpense_OnClick(object sender, RoutedEventArgs e)
        {
            WFormExpense wf = new WFormExpense();
            wf.ShowDialog();
            ShowInfoCard();
        }
        
        private void BtnOpenFormIncome_OnClick(object sender, RoutedEventArgs e)
        {
            WFormIncome wf = new WFormIncome();
            wf.ShowDialog();
            ShowInfoCard();
        }
        
        void ShowInfoCard()
        {

            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;

            LoadCardIncome(currentMonth, currentYear);
            LoadCardExpense(currentMonth, currentYear);
            LoadCardWallet(currentMonth, currentYear);

        }

        void LoadCardIncome(int currentMonth, int currentYear)
        {
            var brushConverter = new BrushConverter();

            decimal totalIncome = _incomeService.GetTotalIncomeByUserId(_userId);

            int lastMonth = currentMonth - 1;
            int year = lastMonth > 0 ? currentYear : currentYear - 1;

            decimal currentMonthAmountIncome = _incomeService.GetTotalAmountByMonthly(_userId, currentMonth, currentYear);
            decimal lastMonthAmountIncome = _incomeService.GetTotalAmountByMonthly(_userId, lastMonth, year);

            decimal incomeLastMonthly = currentMonthAmountIncome - lastMonthAmountIncome;
            decimal percentIncome = 0;

            if (currentMonthAmountIncome != 0)
            {
                percentIncome = (currentMonthAmountIncome - lastMonthAmountIncome) / currentMonthAmountIncome * 100;
            }
            if (percentIncome >= 0)
            {
                FaIconIncome.Icon = EFontAwesomeIcon.Solid_ArrowUp;
                FaIconIncome.PrimaryColor = (Brush)brushConverter.ConvertFromString("#04a08b");
                TxtPercentIncome.Foreground = (Brush)brushConverter.ConvertFromString("#04a08b");
                BorderBgIncome.Background = (Brush)brushConverter.ConvertFromString("#d1f5f0");
                TxtIncomeLastMonth.Text = $"+ {incomeLastMonthly:N0}";
            }
            else
            {
                FaIconIncome.Icon = EFontAwesomeIcon.Solid_ArrowDown;
                FaIconIncome.PrimaryColor = (Brush)brushConverter.ConvertFromString("#ff562f");
                TxtPercentIncome.Foreground = (Brush)brushConverter.ConvertFromString("#ff562f");
                BorderBgIncome.Background = (Brush)brushConverter.ConvertFromString("#fff2e2");
                TxtIncomeLastMonth.Text = $"- {Math.Abs(incomeLastMonthly):N0}";
            }
                
            TxtPercentIncome.Text = $"{percentIncome:N2}%";
            TxtIncome.Text = $"+ {totalIncome:N0}";
        }

        void LoadCardExpense(int currentMonth, int currentYear)
        {
            var brushConverter = new BrushConverter();
            decimal totalExpense = _expenseService.GetTotalExpensesByUserId(_userId);
            
            int lastMonth = currentMonth - 1;
            int year = lastMonth > 0 ? currentYear : currentYear - 1;

            decimal currentMonthAmountExpense = _expenseService.GetTotalAmountByMonthly(_userId, currentMonth, currentYear);
            decimal lastMonthAmountExpense = _expenseService.GetTotalAmountByMonthly(_userId, lastMonth, year);

            decimal expenseLastMonthly = currentMonthAmountExpense - lastMonthAmountExpense;
            decimal percentExpense = 0;
            if(currentMonthAmountExpense != 0)
            {
                percentExpense = (currentMonthAmountExpense - lastMonthAmountExpense) / currentMonthAmountExpense * 100;
            }
            if (percentExpense >= 0)
            {
                FaIconExpense.Icon = EFontAwesomeIcon.Solid_ArrowUp;
                FaIconExpense.PrimaryColor = (Brush)brushConverter.ConvertFromString("#04a08b");
                TxtPercentExpense.Foreground = (Brush)brushConverter.ConvertFromString("#04a08b");
                BorderBgExpense.Background = (Brush)brushConverter.ConvertFromString("#d1f5f0");
                TxtExpenseLastMonth.Text = $"+ {expenseLastMonthly:N0}";
            }
            else
            {
                FaIconExpense.Icon = EFontAwesomeIcon.Solid_ArrowDown;
                FaIconExpense.PrimaryColor = (Brush)brushConverter.ConvertFromString("#ff562f");
                TxtPercentExpense.Foreground = (Brush)brushConverter.ConvertFromString("#ff562f");
                BorderBgExpense.Background = (Brush)brushConverter.ConvertFromString("#fff2e2");
                TxtExpenseLastMonth.Text = $"- {Math.Abs(expenseLastMonthly):N0}";
            }
                
            TxtPercentExpense.Text = $"{percentExpense:N2}%";
            TxtExpense.Text = $"- {totalExpense:N0}";
        }

        void LoadCardWallet(int currentMonth, int currentYear)
        {
            var brushConverter = new BrushConverter();

            decimal totalExpense = _expenseService.GetTotalExpensesByUserId(_userId);
            decimal totalIncome = _incomeService.GetTotalIncomeByUserId(_userId);
            decimal totalWallet = totalIncome - totalExpense;
            
            int lastMonth = currentMonth - 1;
            int year = lastMonth > 0 ? currentYear : currentYear - 1;

            decimal currentMonthAmountWallet = _incomeService.GetTotalAmountByMonthly(_userId, currentMonth, currentYear) 
                                               - _expenseService.GetTotalAmountByMonthly(_userId, currentMonth, currentYear);
            decimal lastMonthAmountWallet = _incomeService.GetTotalAmountByMonthly(_userId, lastMonth, year) 
                                            - _expenseService.GetTotalAmountByMonthly(_userId, lastMonth, year);

            decimal incomeLastMonthly = currentMonthAmountWallet - lastMonthAmountWallet;
            decimal percentWallet = 0;
            if (currentMonthAmountWallet != 0)
            {
                percentWallet = (currentMonthAmountWallet - lastMonthAmountWallet) / currentMonthAmountWallet * 100;
            }
            if (percentWallet >= 0)
            {
                FaIconWallet.Icon = EFontAwesomeIcon.Solid_ArrowUp;
                FaIconWallet.PrimaryColor = (Brush)brushConverter.ConvertFromString("#04a08b");
                TxtPercentWallet.Foreground = (Brush)brushConverter.ConvertFromString("#04a08b");
                BorderBgWallet.Background = (Brush)brushConverter.ConvertFromString("#d1f5f0");
                TxtWalletLastMonth.Text = $"+ {incomeLastMonthly:N0}";
            }
            else
            {
                FaIconWallet.Icon = EFontAwesomeIcon.Solid_ArrowDown;
                FaIconWallet.PrimaryColor = (Brush)brushConverter.ConvertFromString("#ff562f");
                TxtPercentWallet.Foreground = (Brush)brushConverter.ConvertFromString("#ff562f");
                BorderBgWallet.Background = (Brush)brushConverter.ConvertFromString("#fff2e2");
                TxtWalletLastMonth.Text = $"- {Math.Abs(incomeLastMonthly):N0}";
            }
                
            TxtPercentWallet.Text = $"{percentWallet:N2}%";
            TxtWallet.Text = $"{totalWallet:N0}";
        }

        private void BannerTimer_Tick(object sender, EventArgs e)
        {
            ShowNextBanner();
        }

        // Hiển thị hình ảnh tiếp theo
        private void ShowNextBanner()
        {
            // Ẩn hình ảnh hiện tại
            _images[_currentImageIndex].Visibility = Visibility.Collapsed;

            // Chuyển sang hình ảnh tiếp theo
            _currentImageIndex = (_currentImageIndex + 1) % _images.Length;

            // Hiển thị hình ảnh mới
            _images[_currentImageIndex].Visibility = Visibility.Visible;
        }
    }
}
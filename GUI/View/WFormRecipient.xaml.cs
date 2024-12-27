using BLL.DTO.Recipient;
using BLL.Services;
using DAL.Models;
using DAL.Repositories;
using System;
using System.Windows;
using System.Windows.Documents;
using AppContext = BLL.AppContext;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for WFormRecipient.xaml
    /// </summary>
    public partial class WFormRecipient : Window
    {
        private readonly RecipientService _recipientService = new RecipientService();
        private readonly string _recipientId;
        
        public WFormRecipient()
        {
            InitializeComponent();
        }
        
        public WFormRecipient(string recipientId)
        {
            InitializeComponent();
            this._recipientId = recipientId;
            LoadData();
        }
        
        private void BtnSaveRecipient_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(TxtRecipientId.Text))
                {
                    
                    var updateRecipient = new Recipient()
                    {
                        RecipientId = TxtRecipientId.Text,
                        RecipientName = TxtRecipientName.Text,
                        Description = new TextRange(RtbDescription.Document.ContentStart, RtbDescription.Document.ContentEnd).Text.Trim()
                    };
                    _recipientService.UpdateCategory(updateRecipient);
                    DialogCustoms dialogCustomsUpdate = new DialogCustoms("Cập nhật người nhận thành công.", "Thông báo", DialogCustoms.OK);
                    dialogCustomsUpdate.ShowDialog();
                }
                else
                {
                    var addRecipientDto = new AddRecipientDto
                    {
                        UserId = AppContext.Instance.UserId,
                        RecipientName = TxtRecipientName.Text,
                        Description = new TextRange(RtbDescription.Document.ContentStart, RtbDescription.Document.ContentEnd).Text.Trim()
                    };
                
                    _recipientService.AddRecipient(addRecipientDto);
                    DialogCustoms dialogCustoms = new DialogCustoms("Thêm người nhận thành công.", "Thông báo", DialogCustoms.OK);
                    dialogCustoms.ShowDialog();
                }
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Lỗi: " + exception.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            DialogCustoms diaglog = new DialogCustoms("Bạn có chắc chắn muốn hủy không?", "Xác nhận", DialogCustoms.YesNo);
            diaglog.ShowDialog();
            if (diaglog.DialogResult == true)
            {
                ResetFormRecipient();
                this.Close();
            }
        }
        
        void LoadData()
        {
            RecipientDto recipient = _recipientService.GetRecipientByRecipientId(this._recipientId);
            TxtRecipientId.Text = recipient.RecipientId;
            TxtRecipientName.Text = recipient.RecipientName;
            RtbDescription.Document.Blocks.Clear();
            RtbDescription.Document.Blocks.Add(new Paragraph(new Run(recipient.Description)));
        }

        void ResetFormRecipient()
        {
            TxtRecipientId.Text = "";
            TxtRecipientName.Text = "";
            RtbDescription.Document.Blocks.Clear();
        }
    }
}

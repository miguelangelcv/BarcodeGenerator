using BarcodeGenerator.Services;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace BarcodeGenerator.ViewModels
{
    public class HomeViewModel : MvxViewModel
    {
        private readonly IBarcodeService _barcodeService;
        private readonly IClipboardService _clipboardService;

        const string WRONGICON = "󰀨";
        const string INFOICON = "󰋼";

        private bool _isNewCode = true;
        public bool IsNewCode
        {
            get => _isNewCode;
            set 
            {
                _isNewCode = value;
                Code = "";
                RaisePropertyChanged();
            }
        }

        public bool IsExistingCode
        {
            get => !IsNewCode;
            set
            {
                _isNewCode = !value;
                Code = "";
                RaisePropertyChanged();
            }
        }

        private string _code = "";
        public string Code
        {
            get => _code;
            set
            {
                _code = value;
                RaisePropertyChanged();
            }
        }

        private string _barcode;

        public string Barcode
        {
            get => _barcode;
            set 
            { 
                _barcode = value;
                RaisePropertyChanged();
            }
        }

        private string _statusBarIcon;
        public string StatusBarIcon
        {
            get => _statusBarIcon;
            set 
            { 
                _statusBarIcon = value;
                RaisePropertyChanged();
            }
        }

        private string _statusBarMessage;
        public string StatusBarMessage
        {
            get => _statusBarMessage;
            set 
            { 
                _statusBarMessage = value;
                RaisePropertyChanged();
            }
        }

        public IMvxCommand GenerateCodeCommand => new MvxCommand(GenerateCode);
        private void GenerateCode()
        {
            Barcode = "";
            StatusBarIcon = "";
            StatusBarMessage = "";
            if (IsNewCode)
            {
                if (Code != "")
                    Barcode = _barcodeService.GenerateNewBarcode(int.Parse(Code));
            }
            else
            {
                if (Code.Length == 13)
                {
                    int lastDigit = _barcodeService.CalcCheckDigit(Code.Remove(Code.Length - 1));
                    if (lastDigit.ToString() == Code[Code.Length - 1].ToString())
                        Barcode = _barcodeService.GenerateBarcode(Code);
                    else
                    {
                        StatusBarMessage = "Check inválido: Revise el código introducido.";
                        StatusBarIcon = WRONGICON;
                    }
                }
                else
                {
                    StatusBarMessage = "Longitud inválida.";
                    StatusBarIcon = WRONGICON;
                }
            }
        }

        public IMvxCommand CleanCodeCommand => new MvxCommand(CleanCode);
        private void CleanCode()
        {
            Barcode = "";
            Code = "";
            StatusBarIcon = "";
            StatusBarMessage = "";
        }

        public IMvxCommand CopyToClipboardCommand => new MvxCommand(CopyToClipboard);
        private void CopyToClipboard()
        {
            if (Barcode.Length > 0)
            {
                _clipboardService.SetText(Barcode);
                StatusBarMessage = "Código de barras copiado al portapapeles.";
                StatusBarIcon = INFOICON;
            }
        }

        public HomeViewModel()
        {
            _barcodeService = new BarcodeService();
            _clipboardService = new ClipboardService();
        }
    }
}
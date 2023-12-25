using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace NekoMacro.Utils
{
    [ValueConversion(typeof(int), typeof(string))]
    public class IntToStringConverter : IValueConverter
    {
        /// <summary>
        /// Проставление значения по умолчанию (0) если строка пустая или null
        /// </summary>
        public bool SetDefaultValueInsteadNullOrEmpty { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString())) return (SetDefaultValueInsteadNullOrEmpty) ? (int?)default(int) : null;
            return value;
        }


    }
    public class IntegerTextBox : NumberTextBox
    {
        protected override System.Windows.Data.IValueConverter NumberConverter { get; }

        public override bool CanBeNull
        {
            get => _canBeNull;
            set
            {
                _canBeNull = value;
                if (NumberConverter != null && NumberConverter is IntToStringConverter)
                    (NumberConverter as IntToStringConverter).SetDefaultValueInsteadNullOrEmpty = !CanBeNull;
            }
        }
        private bool _canBeNull;

        public IntegerTextBox()
            : base()
        {
            NumberConverter = new IntToStringConverter() { SetDefaultValueInsteadNullOrEmpty = !CanBeNull };
        }


        protected override bool CheckConvertingToNumber(string testText, bool canBeNegative)
        {
            return int.TryParse(testText.Trim(), out var result) && (canBeNegative || result >= 0);
        }
    }
    public abstract class NumberTextBox : TextBox
    {
        #region Properties
        //разрешение на ввод знака минус впереди
        public bool CanBeNegative { get; set; }

        /// <summary>
        /// разрешение на установку пустого значения (по умолчанию true) 
        /// </summary>
        public abstract bool CanBeNull { get; set; }
        protected abstract System.Windows.Data.IValueConverter NumberConverter { get; }
        #endregion

        #region Constructors

        protected NumberTextBox()
        {
            CanBeNull = true;
            this.PreviewTextInput += NumberTextBox_PreviewTextInput;
            this.PreviewKeyDown += NumberTextBox_PreviewKeyDown;
            DataObject.AddPastingHandler(this, OnPaste);
            this.Initialized += NumberTextBox_Initialized;
            this.MaxLength = 15;
        }

        void NumberTextBox_Initialized(object sender, EventArgs e)
        {
            //определяем требуется ли переопределять Binding 
            if (NumberConverter == null) return;

            var binding = this.GetBindingExpression(TextBox.TextProperty);
            if (binding == null) return;

            BindingOverridingBasedOnSpecifiedBinding(binding.ParentBinding);

            //определение нового Binding на основе имеющегося
            var newBinding = BindingOverridingBasedOnSpecifiedBinding(binding.ParentBinding);

            this.SetBinding(TextBox.TextProperty, newBinding);
        }

        System.Windows.Data.Binding BindingOverridingBasedOnSpecifiedBinding(System.Windows.Data.Binding binding)
        {
            //определение нового Binding на основе имеющегося
            var newBinding = new System.Windows.Data.Binding();
            newBinding.Path = binding.Path;
            newBinding.RelativeSource = binding.RelativeSource;
            newBinding.TargetNullValue = binding.TargetNullValue;
            newBinding.UpdateSourceTrigger = binding.UpdateSourceTrigger;
            newBinding.ValidatesOnDataErrors = binding.ValidatesOnDataErrors;
            newBinding.Mode = binding.Mode;
            newBinding.IsAsync = binding.IsAsync;
            newBinding.FallbackValue = binding.FallbackValue;
            newBinding.Converter = binding.Converter;
            //bind.ElementName = binding.ParentBinding.ElementName;
            // bind.Source = binding.ParentBinding.Source;

            foreach (var item in binding.ValidationRules)
                newBinding.ValidationRules.Add(item);

            //добавление изменеий в имеющийся Binding
            //if (!CanBeNull) newBinding.ValidationRules.Add(new NotNullValidationRule());

            //устанавливаем конвертер, если только он не был задан в xaml
            if (newBinding.Converter == null && NumberConverter != null)
                newBinding.Converter = NumberConverter;

            return newBinding;
        }

        protected abstract bool CheckConvertingToNumber(string testText, bool canBeNegative);
        #endregion

        #region TextInput
        protected void NumberTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string resultText;
            if (!IsDataValid(sender as NumberTextBox, e.Text.
                                                        Replace(".", Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator).
                                                        Replace(",", Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator).Trim('+'), out resultText))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Обработка клавиши Пробела 
        /// </summary>
        protected static void NumberTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            var isText = e.SourceDataObject.GetDataPresent(System.Windows.DataFormats.Text, true);
            if (!isText) return;

            var text = e.SourceDataObject.GetData(DataFormats.Text) as string;
            string resultText;
            if (!IsDataValid(sender as NumberTextBox, text, out resultText))
            {
                e.CancelCommand();
                e.Handled = true;
            }
        }

        #endregion

        protected virtual bool IsDataValid(NumberTextBox textBox, string addText, out string resultText)
        {
            if (String.IsNullOrEmpty(addText))
            {
                resultText = textBox.Text;
                return false;
            }

            resultText = GetNewText(textBox, addText);
            //проверка на начало ввода отрицательного числа 
            if (textBox.CanBeNegative && String.Compare(resultText.Trim(), "-") == 0)
                return true;
            return CheckConvertingToNumber(resultText, textBox.CanBeNegative);
        }

        /// <summary>
        /// Формирование значения после  
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="addText"></param>
        /// <returns></returns>
        protected string GetNewText(NumberTextBox textBox, string addText)
        {
            try
            {
                var sourceText = textBox.Text;
                var selStartIndex = textBox.SelectionStart;
                if (sourceText.Length < selStartIndex) selStartIndex = sourceText.Length;

                var selLength = textBox.SelectionLength;
                if (sourceText.Length < selStartIndex + selLength)
                    selLength = sourceText.Length - selStartIndex;

                var newText = sourceText;
                if (selStartIndex != -1)
                    newText = sourceText.Remove(selStartIndex, selLength);

                var caretIndex = textBox.CaretIndex;
                if (newText.Length < caretIndex)
                    caretIndex = newText.Length;

                newText = newText.Insert(caretIndex, addText.Trim());

                return newText;
            }
            catch
            {
                return null;
            }
        }

    }
}

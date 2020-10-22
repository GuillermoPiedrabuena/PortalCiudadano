using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Windows.Media;
using System.IO;


namespace Portal_Ciudadano
{
    public partial class MainWindow : Window
    {


        void ListViewSelectionChange(object sender, SelectionChangedEventArgs args)
        {


            //WE GET THE SELECTED ITEM LISTVIEW
            Contracts selected = (Contracts)lvUsers.SelectedItem;
            string content = System.IO.File.ReadAllText($@"C:\Users\gnpie\OneDrive\Escritorio\proyectos\Portal Ciudadano\{ListViewHeaderSet.ToString()}\{selected.Name}.txt");

            //ON THE RELATED .TXT WE SEEK THE VARIABLES CONTAINED ON IT
            List<int> listOfLegalDocumentVariablesIndices = new List<int>();
            bool variableFinded = true;
            int lastsearching = 0;
            while (variableFinded == true)
            {

                int startIndex = content.IndexOf("${", lastsearching);
                if (startIndex == -1) { variableFinded = false; }
                if (startIndex != -1)
                {
                    listOfLegalDocumentVariablesIndices.Add(startIndex + 2);
                }
                int endIndex = content.IndexOf("}", lastsearching);
                if (endIndex != -1)
                {
                    listOfLegalDocumentVariablesIndices.Add(endIndex);
                }
                lastsearching = endIndex + 1;

            }

            int iterator = 0;
            List<string> listOfLegalDocumentVariablesNames = new List<string>(); //HERE ARE THE VARIABLES FOR THE DOCUMENT BUILDING


            while (iterator <= listOfLegalDocumentVariablesIndices.Count - 2)
            {

                int length = listOfLegalDocumentVariablesIndices[iterator + 1] - listOfLegalDocumentVariablesIndices[iterator];
                string piece = content.Substring(listOfLegalDocumentVariablesIndices[iterator], length);
                listOfLegalDocumentVariablesNames.Add(piece);
                iterator = iterator + 2;
            }
            //HERE GOES THE XALM MANIPULATION
            if (listOfLegalDocumentVariablesNames.Count > 0) {

                int i = 0;

                StackLayoutMap.Children.Clear();
                do
                {
                    IntegerConverter integerConverter = new IntegerConverter();
                    string textboxName = integerConverter.Convert(i +1).Trim();

                    var label = new Label
                    {
                        Content = listOfLegalDocumentVariablesNames[i]
                    };

                    var textbox = new TextBox();

                    if (listOfLegalDocumentVariablesNames[i] == "ANTECEDENTES") 
                    {
                         textbox = new TextBox
                        {
                            Text = "",
                            Name = textboxName,
                            Height = 180,
                            TextWrapping = TextWrapping.Wrap
                         };
                    } 
                    else 
                    {
                         textbox = new TextBox
                        {
                            Text = "",
                            Name = textboxName
                        };
                    }
                    

                    StackLayoutMap.Children.Add(label);
                    StackLayoutMap.Children.Add(textbox);
                    textboxesList.Add(textboxName);

                    i = i + 1;
                }
                while (i < listOfLegalDocumentVariablesNames.Count);
            }
        }



        public ObservableCollection<User> users = new ObservableCollection<User>();

        private List<Client> allClients;
        public List<Client> AllClients {

            get { return this.allClients; }
            set
            {
                if (this.allClients != value)
                {
                    this.allClients = value;
                }
            }
        }

        //FUNCTIONAL CLASSES    
        class IntegerConverter {
            public string Convert(long number)
            { // NO PORCESA BIEN EL RUT DE MAGALY DENICE BOHRINGER BUSTOS, ni 3000000

                string numberToString = number.ToString();

                string result = "";
                int millionsCounter = 0;
                while (numberToString.Length > 0)
                {
                    string getUnits = "";
                    string hundreds = "";
                    string tens = "";
                    string units = "";


                    if (numberToString.Length >= 3) {
                        getUnits = numberToString.Substring(numberToString.Length - 3);
                        units = getUnits.Substring(getUnits.Length - 1);
                        tens = getUnits.Substring(getUnits.Length - 2, getUnits.Length - 2);
                        hundreds = getUnits.Substring(0, 1);
                    }
                    if (numberToString.Length == 2)
                    {

                        getUnits = numberToString.Substring(numberToString.Length - 2);
                        units = getUnits.Substring(getUnits.Length - 1);
                        tens = getUnits.Substring(0, 1);

                    }
                    if (numberToString.Length == 1)
                    {

                        getUnits = numberToString.Substring(numberToString.Length - 1);
                        units = getUnits;
                    }

                    switch (millionsCounter) //Aca el problema?
                    {
                        case 1:
                            result = " mil " + result;
                            break;
                        case 2:
                            result = " millones " + result;
                            break;
                        case 3:
                            result = " mil millones " + result;
                            break;
                        case 4:
                            result = " billones " + result;
                            break;
                        case 5:
                            result = " mil billones " + result;
                            break;
                        case 6:
                            result = " trillones " + result;
                            break;
                        case 7:
                            result = " mil trillones " + result;
                            break;
                        case 8:
                            result = " cuatrillones " + result;
                            break;
                        case 9:
                            result = " mil cuatrillones " + result;
                            break;
                    }
                    switch (tens + units)
                    {
                        case "11":
                            result = " once" + result;
                            break;
                        case "12":
                            result = " doce" + result;
                            break;
                        case "13":
                            result = " trece" + result;
                            break;
                        case "14":
                            result = " catorce" + result;
                            break;
                        case "15":
                            result = " quince" + result;
                            break;
                        case "10":
                            result = " diez" + result;
                            break;
                        case "20":
                            result = " veinte" + result;
                            break;
                        case "30":
                            result = " treinta" + result;
                            break;
                        case "40":
                            result = " cuarenta" + result;
                            break;
                        case "50":
                            result = " cincuenta" + result;
                            break;
                        case "60":
                            result = " sesenta" + result;
                            break;
                        case "70":
                            result = " setenta" + result;
                            break;
                        case "80":
                            result = " ochenta" + result;
                            break;
                        case "90":
                            result = " noventa " + result;
                            break;

                        default:
                            switch (units)
                            {
                                case "0":
                                    result = "" + result;
                                    break;
                                case "1":
                                    result = "uno " + result;
                                    break;
                                case "2":
                                    result = "dos " + result;
                                    break;
                                case "3":
                                    result = "tres " + result;
                                    break;
                                case "4":
                                    result = "cuatro " + result;
                                    break;
                                case "5":
                                    result = "cinco " + result;
                                    break;
                                case "6":
                                    result = "seis " + result;
                                    break;
                                case "7":
                                    result = "siete " + result;
                                    break;
                                case "8":
                                    result = "ocho " + result;
                                    break;
                                case "9":
                                    result = "nueve " + result;
                                    break;
                            }
                            switch (tens)
                            {
                                case "0":
                                    result = "" + result;
                                    break;
                                case "1":
                                    result = "dieci" + result;
                                    break;
                                case "2":
                                    result = "veinti" + result;
                                    break;
                                case "3":
                                    result = "treinta y " + result;
                                    break;
                                case "4":
                                    result = "cuarenta y " + result;
                                    break;
                                case "5":
                                    result = "cincuenta y " + result;
                                    break;
                                case "6":
                                    result = "sesenta y " + result;
                                    break;
                                case "7":
                                    result = "setenta y " + result;
                                    break;
                                case "8":
                                    result = "ochenta y " + result;
                                    break;
                                case "9":
                                    result = "noventa y " + result;
                                    break;
                            }
                            switch (hundreds)
                            {
                                case "0":
                                    result = "" + result;
                                    break;
                                case "1":
                                    result = "ciento " + result;
                                    break;
                                case "2":
                                    result = "doscientos " + result;
                                    break;
                                case "3":
                                    result = "trescientos " + result;
                                    break;
                                case "4":
                                    result = "cuatroscientos " + result;
                                    break;
                                case "5":
                                    result = "quinientos " + result;
                                    break;
                                case "6":
                                    result = "seiscientos " + result;
                                    break;
                                case "7":
                                    result = "setecientos " + result;
                                    break;
                                case "8":
                                    result = "ochocientos " + result;
                                    break;
                                case "9":
                                    result = "novecientos " + result;
                                    break;
                            }

                            break;


                    }


                    string remainder = "";

                    if (numberToString.Length >= 3) { remainder = numberToString.Substring(0, numberToString.Length - 3); }
                    if (numberToString.Length == 2) { remainder = numberToString.Substring(0, numberToString.Length - 2); }
                    if (numberToString.Length == 1) { remainder = numberToString.Substring(0, numberToString.Length - 1); }
                    numberToString = remainder;



                    millionsCounter = millionsCounter + 1;
                }

                return result;

            }

        }

        //PUBLIC CLASSES AND VARIABLES

        public List<Contracts> contratList = new List<Contracts>();        
        public List<Plaints> plaintList = new List<Plaints>();
        public class Contracts
        {
            public string Name { get; set; }
        }

        public class Plaints
        {
            public string Name { get; set; }
        }

        public string ListViewHeaderSet { get; set; }

        public class SelectedContract
        {
            public string ContractSelected { get; set; }

        }

        public List<string> textboxesList = new List<string>();
  



        public class User : INotifyPropertyChanged
        {
            private string props;
            public string Props
            {
                get { return this.props; }
                set
                {
                    if (this.props != value)
                    {
                        this.props = value;
                        this.NotifyPropertyChanged("Name");
                    }
                }
            }


            public event PropertyChangedEventHandler PropertyChanged;

            public void NotifyPropertyChanged(string propName)
            {
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        
        public MainWindow()
        {
            this.DataContext = this;
            InitializeComponent();

            Reciber();
            lbUsers.ItemsSource = users;

            ListViewHeaderSet = "Contratos";

            //List<Contracts> contratList = new List<Contracts>();
            //List<Plaints> plaintList = new List<Plaints>();
            
            //WE LOAD THE DOCUMENTS MODELS

            string[] ContractFilesArr = Directory.GetFiles($@"C:\Users\gnpie\OneDrive\Escritorio\proyectos\Portal Ciudadano\Contratos");

            foreach (string entireFilePath in ContractFilesArr)
            {
                int index = entireFilePath.IndexOf("Ciudadano");
                string substr = entireFilePath.Substring(index+10);
                int index2 = substr.IndexOf(@"\");
                string substring2 = substr.Substring(index2 +1);
                string substring3 = substring2.Replace(".txt","");
                string onlyFileName = substring3;
                contratList.Add(new Contracts() { Name = onlyFileName });
            }

            string[] PlaintFilesArr = Directory.GetFiles($@"C:\Users\gnpie\OneDrive\Escritorio\proyectos\Portal Ciudadano\Demandas");
            foreach (string entireFilePath in PlaintFilesArr)
            {
                int index = entireFilePath.IndexOf("Ciudadano");
                string substr = entireFilePath.Substring(index + 10);
                int index2 = substr.IndexOf(@"\");
                string substring2 = substr.Substring(index2 + 1);
                string substring3 = substring2.Replace(".txt", "");
                string onlyFileName = substring3;
                Console.WriteLine(onlyFileName);
                plaintList.Add(new Plaints() { Name = onlyFileName });
            }
            lvUsers.ItemsSource = contratList;
        }

        private void ChangeList_Click(object sender, RoutedEventArgs e) {

            if (VieListHeader.Header.ToString() == "Contratos")
            {
                VieListHeader.Header = "Demandas";
                ListViewHeaderSet = "Demandas";
                lvUsers.ItemsSource = plaintList;
            }
            else {
                VieListHeader.Header = "Contratos";
                ListViewHeaderSet = "Contratos";
                lvUsers.ItemsSource = contratList;
            }
            
        }

        private void WriteUp_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(ListViewHeaderSet);
            IntegerConverter integerConverter = new IntegerConverter();

            //WE GET THE SELECTED ITEM LISTVIEW
            Contracts selected = (Contracts)lvUsers.SelectedItem;
            string content = System.IO.File.ReadAllText($@"C:\Users\gnpie\OneDrive\Escritorio\proyectos\Portal Ciudadano\{ListViewHeaderSet.ToString()}\{selected.Name}.txt");

            //ON THE RELATED .TXT WE SEEK THE VARIABLES CONTAINDE ON IT
            List<int> listOfLegalDocumentVariablesIndices = new List<int>();
            bool variableFinded = true;
            int lastsearching = 0;
            while (variableFinded == true)
            {
                
                int startIndex = content.IndexOf("${", lastsearching);
                if (startIndex == -1) { variableFinded = false; }
                if (startIndex != -1)
                {
                    listOfLegalDocumentVariablesIndices.Add(startIndex +2);
                }
                int endIndex = content.IndexOf("}", lastsearching);
                if (endIndex != -1)
                {
                    listOfLegalDocumentVariablesIndices.Add(endIndex);
                }
                lastsearching = endIndex + 1;
          
            }

            int iterator = 0;
            List<string> listOfLegalDocumentVariablesNames = new List<string>();
            while (iterator <= listOfLegalDocumentVariablesIndices.Count -2) {

                int length = listOfLegalDocumentVariablesIndices[iterator +1] - listOfLegalDocumentVariablesIndices[iterator];
                string piece = content.Substring(listOfLegalDocumentVariablesIndices[iterator], length);
                listOfLegalDocumentVariablesNames.Add(piece);
                iterator = iterator + 2;
            }

            // INITIALIZATION OF WORD DOCUMENT
            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
            word.Documents.Add();
            word.Visible = true;
            word.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone;
            Microsoft.Office.Interop.Word.Document doc = word.ActiveDocument;

            //WE FIND THE SELECTED CASE DATA

            Client selectedClient = new Client();
            foreach (Client item in AllClients)
            {

                if (item.clients_name == lbUsers.Text) {
                  
                    selectedClient = item;
                }

            }

            //WE STORE THE INPUTS (TEXTBOXES) CONTENT, FOR THE DOCUMENT CONSTRUCTOR


            int maxLoops= VisualTreeHelper.GetChildrenCount(StackLayoutMap);
            Dictionary<string, string> labelsAndTextBoxesDict = new Dictionary<string, string>();

            for (int i = 0; i < maxLoops; i = i+2)
            {
                string xamlLabelEle = VisualTreeHelper.GetChild(StackLayoutMap, i).ToString();
                //ver lector de xml xdoc
                string xamlTextBoxEle = "";
                if (i == maxLoops - 1) { xamlTextBoxEle = VisualTreeHelper.GetChild(StackLayoutMap, i).ToString(); }
                else { xamlTextBoxEle = VisualTreeHelper.GetChild(StackLayoutMap, i + 1).ToString(); }
                int startOnTextBox = xamlTextBoxEle.IndexOf("TextBox: ") + 9;
                int startOnLabel = xamlLabelEle.IndexOf("Label: ") + 7;

                string textPropertiOfXamlElementTextBox = xamlTextBoxEle.Substring(startOnTextBox); //HERE WE GET THE CONTENT OF TEXTBOX, WITH A TRICK
                string contentPropertiOfXamlElementLabel = xamlLabelEle.Substring(startOnLabel);

                labelsAndTextBoxesDict.Add(contentPropertiOfXamlElementLabel, textPropertiOfXamlElementTextBox);
            }

            // SECTIONS VARIABLES

            string rut = selectedClient.clients_rut;
            string[] rutArr = rut.Split('-');
            string verifyingDigit = integerConverter.Convert(Convert.ToInt64(rutArr[1]));
            string theRestOFTheRut = rutArr[0].Replace(".", "");
            long theRestOFTheRutInteger = Convert.ToInt64(theRestOFTheRut);
            string theRestOFTheRutOnWords = integerConverter.Convert(theRestOFTheRutInteger);
            string stringifiedRut = theRestOFTheRutOnWords + " guion " +verifyingDigit;
           
            string introduction = $"EN SANTIAGO, REPÚBLICA DE CHILE, a {integerConverter.Convert(DateTime.Today.Day)} de {DateTime.Now.ToString("MMMM")} del año {integerConverter.Convert(DateTime.Today.Year)}, ante mí, R. ALFREDO MARTÍN ILLANES, abogado, Notario Púbico Titular de la Décimo Quinta Notaria de Santiago, con oficio en calle Mardoqueo Fernández doscientos uno, oficinas ciento uno y ciento dos, comuna de Providencia, comparece: ";
            string comparecencia = $"{selectedClient.clients_name}, {selectedClient.clients_nationality}, {selectedClient.clients_civilStatus}, {selectedClient.clients_job}, cédula nacional de identidad número {stringifiedRut}";
            string prefacio = @" el compareciente mayor de edad, quien me acredita su identidad con la cédula personal antes citada y expone: ";
            //SECTION 1.a
            Microsoft.Office.Interop.Word.Paragraph paragraph1A = doc.Content.Paragraphs.Add(System.Reflection.Missing.Value);
            paragraph1A.Range.Text = (selected.Name).ToUpper();
            paragraph1A.Range.Font.Size = 14;
            paragraph1A.Range.Font.Bold = 1;
            paragraph1A.Range.Font.Name = "Helvetica";
            paragraph1A.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
            paragraph1A.Range.InsertParagraphAfter();
            //SECTION 1.b
            Microsoft.Office.Interop.Word.Paragraph paragraph1B = doc.Content.Paragraphs.Add(System.Reflection.Missing.Value);
            paragraph1B.Range.Text = selectedClient.clients_name.ToUpper();
            paragraph1B.Range.Font.Size = 14;
            paragraph1B.Range.Font.Bold = 1;
            paragraph1B.Range.Font.Name = "Helvetica";
            paragraph1B.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
            paragraph1B.Range.InsertParagraphAfter();
            //SECTION 1.c
            Microsoft.Office.Interop.Word.Paragraph paragraph1C = doc.Content.Paragraphs.Add(System.Reflection.Missing.Value);
            paragraph1C.Range.Text = "A";
            paragraph1C.Range.Font.Size = 14;
            paragraph1C.Range.Font.Bold = 1;
            paragraph1C.Range.Font.Name = "Helvetica";
            paragraph1C.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
            paragraph1C.Range.InsertParagraphAfter();
            //SECTION 1.d
            Microsoft.Office.Interop.Word.Paragraph paragraph1D = doc.Content.Paragraphs.Add(System.Reflection.Missing.Value);
            paragraph1D.Range.Text = "MEMIN";
            paragraph1D.Range.Font.Size = 14;
            paragraph1D.Range.Font.Bold = 1;
            paragraph1D.Range.Font.Name = "Helvetica";
            paragraph1D.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
            paragraph1D.Range.InsertParagraphAfter();

            //SECTION 2
            Microsoft.Office.Interop.Word.Paragraph paragraph2 = doc.Content.Paragraphs.Add(System.Reflection.Missing.Value);
            paragraph2.Range.Text = introduction + comparecencia + prefacio;
            paragraph2.Range.Font.Size = 12;
            paragraph2.Range.Font.Bold = 0;
            paragraph2.Range.Font.Name = "Helvetica";
            paragraph2.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify;
            //paragraph.Range.ParagraphFormat.LineSpacing = (float) Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpace1pt5;
            paragraph2.Range.InsertParagraphAfter();
            //SECTION 3
            Microsoft.Office.Interop.Word.Paragraph paragraph3 = doc.Content.Paragraphs.Add(System.Reflection.Missing.Value);

            //============================================================================================================================            

           
            foreach(KeyValuePair<string, string> entry in labelsAndTextBoxesDict)
            {

                if (content.Contains(entry.Key)) {
                    int a;
                    if (Int32.TryParse(entry.Value, out a)) {
                        IntegerConverter intConverter = new IntegerConverter();
                        content = content.Replace("${" + entry.Key + "}", intConverter.Convert(Int64.Parse(entry.Value)));
                    }
                    else 
                    {
                        content = content.Replace("${" + entry.Key + "}", entry.Value); 
                    } 
                }
            }

            /*string modifiedContent = content.Replace("${ANTECEDENTES}", "");
            string modifiedContent2 = modifiedContent.Replace("${FOJAS}", "");
            string modifiedContent3 = modifiedContent2.Replace("${NUMERO}", "");
            string modifiedContent4 = modifiedContent3.Replace("${AÑO}", "");
            string modifiedContent5 = modifiedContent4.Replace("${CIRCUNSCRIPCION}", "");
            string modifiedContent6 = modifiedContent5.Replace("${PRECIO}", "");*/
            paragraph3.Range.Text = content;
            paragraph3.Range.Font.Size = 12;
            paragraph3.Range.Font.Bold = 0;
            paragraph3.Range.Font.Name = "Helvetica";
            paragraph3.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustify;
            //paragraph.Range.ParagraphFormat.LineSpacing = (float)Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceDouble;
            paragraph3.Range.InsertParagraphAfter();

            //SIGN SECTION
            Microsoft.Office.Interop.Word.Paragraph sign1 = doc.Content.Paragraphs.Add(System.Reflection.Missing.Value);
            paragraph1B.Range.Text = selectedClient.clients_name.ToUpper();
            paragraph1B.Range.Font.Size = 14;
            paragraph1B.Range.Font.Bold = 1;
            paragraph1B.Range.Font.Name = "Helvetica";
            paragraph1B.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
            paragraph1B.Range.InsertParagraphAfter();
            Microsoft.Office.Interop.Word.Paragraph rut1 = doc.Content.Paragraphs.Add(System.Reflection.Missing.Value);
            paragraph1B.Range.Text = selectedClient.clients_rut;
            paragraph1B.Range.Font.Size = 14;
            paragraph1B.Range.Font.Bold = 1;
            paragraph1B.Range.Font.Name = "Helvetica";
            paragraph1B.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
            paragraph1B.Range.InsertParagraphAfter();


        }

        public async Task<List<Client>> Reciber() {//ACA SE PONEN LOS JSON QUE LLEGAN
            List<Client> caseData = await Requester.GetRequest("http://guillermopiedrabuena.pythonanywhere.com/clientes/17.402.744-7");
            foreach(Client item in caseData){

                users.Add(new User() { Props = item.clients_name });
            }

            AllClients = caseData;
            return caseData;
        }
    }
}
namespace HW9
{
    interface ICalc
    {
        int Less(int valueToCompare);
        int Greater(int valueToCompare);
    }

    interface IOutput2
    {
        void ShowEven();
        void ShowOdd();
    }

    interface ICalc2
    {
        int CountDistinct();
        int EqualToValue(int valueToCompare);
    }

    interface IRemoteControl
    {
        void TurnOn();
        void TurnOff();
        void SetChannel(int channel);
    }

    interface IValidator
    {
        bool Validate();
    }

    class Array : ICalc, IOutput2, ICalc2
    {
        private int[] data;

        public int Length
        {
            get { return data.Length; }
        }

        public int this[int index]
        {
            get
            {
                if (index < 0 || index >= data.Length)
                {
                    throw new IndexOutOfRangeException("Индекс вне диапазона массива");
                }
                return data[index];
            }
            set
            {
                if (index < 0 || index >= data.Length)
                {
                    throw new IndexOutOfRangeException("Индекс вне диапазона массива");
                }
                data[index] = value;
            }
        }

        public Array(int[] values)
        {
            if (values == null)
            {
                throw new ArgumentException("Массив не может быть пустым");
            }
            data = new int[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                data[i] = values[i];
            }
        }

        public void Show()
        {
            Console.WriteLine("Массив: " + string.Join(" ", data));
        }

        public int Less(int valueToCompare)
        {
            int count = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] < valueToCompare)
                {
                    count++;
                }
            }
            return count;
        }

        public int Greater(int valueToCompare)
        {
            int count = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] > valueToCompare)
                {
                    count++;
                }
            }
            return count;
        }

        public void ShowEven()
        {
            Console.Write("Чётные: ");
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] % 2 == 0)
                {
                    Console.Write(data[i] + " ");
                }
            }
            Console.WriteLine();
        }

        public void ShowOdd()
        {
            Console.Write("Нечётные: ");
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] % 2 != 0)
                {
                    Console.Write(data[i] + " ");
                }
            }
            Console.WriteLine();
        }

        public int CountDistinct()
        {
            int count = 0;
            for (int i = 0; i < data.Length; i++)
            {
                bool isNew = true;
                for (int j = 0; j < i; j++)
                {
                    if (data[j] == data[i])
                    {
                        isNew = false;
                        break;
                    }
                }
                if (isNew)
                {
                    count++;
                }
            }
            return count;
        }

        public int EqualToValue(int valueToCompare)
        {
            int count = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == valueToCompare)
                {
                    count++;
                }
            }
            return count;
        }
    }

    class TV : IRemoteControl
    {
        private string model;
        private bool isOn = false;
        private int channel = 1;
        private int maxChannel;

        public string Model
        {
            get { return model; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Модель не может быть пустой");
                }
                model = value;
            }
        }

        public TV(string model, int maxChannel)
        {
            this.model = model;
            Model = model;
            if (maxChannel <= 0)
            {
                throw new ArgumentException("Количество каналов должно быть больше нуля");
            }
            this.maxChannel = maxChannel;
        }

        public void TurnOn()
        {
            isOn = true;
            Console.WriteLine("Телевизор " + model + " включён, канал " + channel);
        }

        public void TurnOff()
        {
            isOn = false;
            Console.WriteLine("Телевизор " + model + " выключен");
        }

        public void SetChannel(int channel)
        {
            if (!isOn)
            {
                Console.WriteLine("Телевизор выключен, сначала включите его");
                return;
            }
            if (channel < 1 || channel > maxChannel)
            {
                Console.WriteLine("Нет такого канала: " + channel + " (доступно 1-" + maxChannel + ")");
                return;
            }
            this.channel = channel;
            Console.WriteLine("Телевизор: переключено на канал " + channel);
        }
    }

    class Radio : IRemoteControl
    {
        private string model;
        private bool isOn = false;
        private int[] stations;
        private int channel = 0;

        public string Model
        {
            get { return model; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Модель не может быть пустой");
                }
                model = value;
            }
        }

        public Radio(string model, int[] stations)
        {
            this.model = model;
            Model = model;
            if (stations == null || stations.Length == 0)
            {
                throw new ArgumentException("Список станций не может быть пустым");
            }
            this.stations = stations;
        }

        public void TurnOn()
        {
            isOn = true;
            Console.WriteLine("Радио " + model + " включено, станция " + (channel + 1) + " (" + stations[channel] / 10.0 + " FM)");
        }

        public void TurnOff()
        {
            isOn = false;
            Console.WriteLine("Радио " + model + " выключено");
        }

        public void SetChannel(int channel)
        {
            if (!isOn)
            {
                Console.WriteLine("Радио выключено, сначала включите его");
                return;
            }
            if (channel < 1 || channel > stations.Length)
            {
                Console.WriteLine("Нет такой станции: " + channel + " (доступно 1-" + stations.Length + ")");
                return;
            }
            this.channel = channel - 1;
            Console.WriteLine("Радио: станция " + channel + " (" + stations[this.channel] / 10.0 + " FM)");
        }
    }

    class PasswordValidator : IValidator
    {
        private string password;

        public PasswordValidator(string password)
        {
            this.password = password;
        }

        public bool Validate()
        {
            if (string.IsNullOrEmpty(password) || password.Length < 8)
            {
                Console.WriteLine("Пароль \"" + password + "\": меньше 8 символов");
                return false;
            }

            bool hasDigit = false;
            bool hasUpper = false;
            bool hasLower = false;
            foreach (char c in password)
            {
                if (char.IsDigit(c)) hasDigit = true;
                if (char.IsUpper(c)) hasUpper = true;
                if (char.IsLower(c)) hasLower = true;
            }

            if (!hasDigit || !hasUpper || !hasLower)
            {
                Console.WriteLine("Пароль \"" + password + "\": нужна цифра, большая и маленькая буква");
                return false;
            }

            Console.WriteLine("Пароль \"" + password + "\": корректный");
            return true;
        }
    }

    class EmailValidator : IValidator
    {
        private string email;

        public EmailValidator(string email)
        {
            this.email = email;
        }

        public bool Validate()
        {
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(email) || email.Contains(' '))
            {
                isValid = false;
            }
            else
            {
                int at = email.IndexOf('@');
                int dot = email.LastIndexOf('.');
                if (at <= 0 || at != email.LastIndexOf('@') || dot < at + 2 || dot == email.Length - 1)
                {
                    isValid = false;
                }
            }

            if (isValid)
            {
                Console.WriteLine("Почта \"" + email + "\": корректная");
            }
            else
            {
                Console.WriteLine("Почта \"" + email + "\": некорректная");
            }
            return isValid;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Array array = new Array(new int[] { 5, -3, 12, 8, 0, 7, 15, -10, 4, 9 });
            array.Show();

            // N1
            Console.WriteLine();
            ICalc calc = array;
            Console.WriteLine("Меньше 5: " + calc.Less(5));
            Console.WriteLine("Больше 5: " + calc.Greater(5));
            Console.WriteLine("Меньше 0: " + calc.Less(0));
            Console.WriteLine("Больше 100: " + calc.Greater(100));

            // N2
            Console.WriteLine();
            IOutput2 output = array;
            output.ShowEven();
            output.ShowOdd();

            // N3
            Console.WriteLine();
            Array array2 = new Array(new int[] { 3, 7, 3, 1, 7, 7, 2, 9, 1 });
            array2.Show();
            ICalc2 calc2 = array2;
            Console.WriteLine("Уникальных значений: " + calc2.CountDistinct());
            Console.WriteLine("Равных 7: " + calc2.EqualToValue(7));
            Console.WriteLine("Равных 5: " + calc2.EqualToValue(5));

            // N4
            Console.WriteLine();
            IRemoteControl[] devices = new IRemoteControl[]
            {
                new TV("Samsung", 100),
                new Radio("Sony", new int[] { 1015, 1043, 1067 })
            };

            foreach (IRemoteControl device in devices)
            {
                device.SetChannel(2);
                device.TurnOn();
                device.SetChannel(2);
                device.SetChannel(200);
                device.TurnOff();
                Console.WriteLine();
            }

            // N5
            IValidator[] validators = new IValidator[]
            {
                new PasswordValidator("12345"),
                new PasswordValidator("password123"),
                new PasswordValidator("Qwerty123"),
                new EmailValidator("user@gmail.com"),
                new EmailValidator("user.gmail.com"),
                new EmailValidator("user@gmail"),
                new EmailValidator("@gmail.com")
            };

            int validCount = 0;
            foreach (IValidator validator in validators)
            {
                if (validator.Validate())
                {
                    validCount++;
                }
            }
            Console.WriteLine("Прошли проверку: " + validCount + " из " + validators.Length);
        }
    }
}

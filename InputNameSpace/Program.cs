using System;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;

namespace InputNameSpace
{

    public class Input
    {
        private readonly string input;
        private readonly int length;
        private int position;
        private int lineNumber;
        //Properties
        public int Length
        {
            get
            {
                return this.length;
            }
        }
        public int Position
        {
            get
            {
                return this.position;
            }
        }
        public int NextPosition
        {
            get
            {
                return this.position + 1;
            }
        }
        public int LineNumber
        {
            get
            {
                return this.lineNumber;
            }
        }
        public char Character
        {
            get
            {
                if (this.position > -1) return this.input[this.position];
                else return '\0';
            }
        }
        public Input(string input)
        {
            this.input = input;
            this.length = input.Length;
            this.position = -1;
            this.lineNumber = 1;
        }
        public bool hasMore(int numOfSteps = 1)
        {
            if (numOfSteps <= 0) throw new Exception("Invalid number of steps");
            return (this.position + numOfSteps) < this.length;
        }
        public bool hasLess(int numOfSteps = 1)
        {
            if (numOfSteps <= 0) throw new Exception("Invalid number of steps");
            return (this.position - numOfSteps) > -1;
        }
        //callback -> delegate
        public Input step(int numOfSteps = 1)
        {
            if (this.hasMore(numOfSteps))
                this.position += numOfSteps;
            else
            {
                throw new Exception("There is no more step");
            }
            return this;
        }
        public Input back(int numOfSteps = 1)
        {
            if (this.hasLess(numOfSteps))
                this.position -= numOfSteps;
            else
            {
                throw new Exception("There is no more step");
            }
            return this;
        }
        public Input reset() {
            this.position = -1;
            this.lineNumber = 1;
            return this;
        }


        public char jump(int index = 1) {

            int i = 0;
            reset();
            if (hasMore())
            {
                i=this.Position + index + 1;
                
            }
            return this.step(i+1).Character; 
        
        }



        public char peek(int numOfSteps = 1)
        {

            if (this.hasMore(numOfSteps))
            {
                
                return this.input[Position + numOfSteps];
           }
            else
            {
                Console.WriteLine("invalid");
                return '\0';
            }
        }
        //public bool hasMore(int numOfSteps=1) { return true; }

        public  string sha1Hash(Input inputx)
        {
            StringBuilder st = new StringBuilder();
            byte[] textBytes = Encoding.ASCII.GetBytes(inputx.input);
            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] computHash = sha1.ComputeHash(textBytes);
                for (int i = 0; i < computHash.Length; i++)
                {
                    st.Append(computHash[i].ToString("x2"));
                }
            }
            return st.ToString();
        }


        public  bool isValidSha1(Input inputx, string hash)
        {
            string temphash = sha1Hash(inputx);
            bool flag;
            if (string.Compare(temphash, hash) == 0)
                flag = true;
            else
            {
                flag = false;
            }

            return flag;
        }
        public string subString(int index)
        {
             int numOfSteps = index - Position;
             string subText =null;
            int Position2 = Position;
         
           for (int i =0; i<numOfSteps; i++)
            {
               
                subText += input[Position2];
                Position2++;
            }

            // }
           // Console.WriteLine("the position value {0}", Position);
            return subText;
        }
    }



    class Program
    {


        static void Main(string[] args)
        {

            Input text = new Input("welcometoc#");
            Console.WriteLine(text.step().step().Character);
            //Console.WriteLine(text.peek(6));
            Console.WriteLine(text.Position);
         //   Console.WriteLine(text.sha1Hash(text));
          //  Console.WriteLine(text.isValidSha1(text, text.sha1Hash(text)));
           // text.reset();
          //  Console.WriteLine(text.Position);
          //  Console.WriteLine(text.peek());
            //Console.WriteLine(text.back());
            // Console.WriteLine(text.jump(8));


           Console.WriteLine(text.subString(5));
            Console.WriteLine(text.Position);





        }
    }
}
